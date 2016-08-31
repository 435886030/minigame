using Assets.Scripts.Common;
namespace Assets.Scripts.Module
{
    //游戏启动后持续性数据存储的地方，不受场景切换影响
    public class PlayerInfoModule : Singleton<PlayerInfoModule> , IModuleBase
    {
        //构造完单例后初始化数据
        public override void Init()
        {
        }

        //定义逻辑事件监听
        public void RegisterLogicEvent()
        {

        }

        public void Update(float lastFrameTookSecond)
        {

        }

        //暂时没用，目前游戏中的单例（非Mono），是不会调用DestroyInstance的
        public override void UnInit()
        {
        }
        
    }
}