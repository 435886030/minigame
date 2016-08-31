using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Common;
using Assets.Scripts.Module;
using System.Collections.Generic;

namespace Assets.Scripts.MonoSingleton
{
    public class GameApp : MonoSingleton<GameApp>
    {
        //由GameApp来管理所有Module
        private List<IModuleBase> _modules = new List<IModuleBase>();

        //注册事件辅助实例
        private LogicEventHelper _logicEventHelper = new LogicEventHelper();
        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                if (_modules[i] != null)
                {
                    _modules[i].Update(Time.deltaTime);
                }
            }
        }

        /// <summary>
        /// Mono单例在Awake后会调用Init,注意不要Override Awake.
        /// 否则可能漏了调用base.Awake()产生BUG.
        /// </summary>
        protected override void Init()
        {
            //AddModule Begin:
            //在这里把所有的单例模块添加进来，添加的顺序就是各模块Update的顺序
            AddModule(PlayerInfoModule.Instance);

            // AddModule End.

            //添加完后给一个注册事件监听的机会
            NtyAllModuleRegisteEvent();
        }

        /// <summary>
        /// Mono单例在OnDestroy后会调用UnInit
        /// </summary>
        protected override void Uninit()
        {
            this._modules.Clear();
            this._logicEventHelper.ClearAll();
        }

        private void AddModule(IModuleBase module)
        {
            if (_modules.Contains(module))
            {
                DebugHelper.Assert(false);
            }
            else
            {
                _modules.Add(module);
            }
        }

        private void NtyAllModuleRegisteEvent()
        {
            if (_modules == null)
            {
                DebugHelper.Assert(false);
                return;
            }

            //给各个Module一个注册监听的机会
            for (int i = 0; i < _modules.Count; i++)
            {
                if (_modules[i] != null)
                {
                    _modules[i].RegisterLogicEvent();
                }
            }
        }

        //关卡加载成功
        public void OnLevelWasLoaded(int level)
        {
            Scene scene = SceneManager.GetActiveScene();
            DebugHelper.Log("Scene "+ scene.name + " Was Loaded Success.");

            EventParam eventParam = EventDispatcher.GetInstance().GetEventParam();
            eventParam.m_objParam = scene;
            EventDispatcher.GetInstance().DispatchEvent((uint)LogicEvent.EVENT_SCENE_LOADED, eventParam);
        }
    }
}