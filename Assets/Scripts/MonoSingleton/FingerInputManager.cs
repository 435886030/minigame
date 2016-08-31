using UnityEngine;
using Assets.Scripts.Common;
namespace Assets.Scripts.MonoSingleton
{
    public class FingerInputManager : MonoSingleton<FingerInputManager>
    {
        public FingerGestures editorGestures;
        public FingerGestures desktopGestures;
        public FingerGestures iosGestures;
        public FingerGestures androidGestures;

		/// <summary>
		/// Mono单例在Awake后会调用Init,注意不要Override Awake.
		/// 否则可能漏了调用base.Awake()产生BUG.
		/// </summary>
        protected override void Init()
        {
            //初始化触摸组件
            InitFingerGesture();

            //按下事件： OnFingerDown就是按下事件监听的方法，这个名子可以由你来自定义。方法只能在本类中监听。下面所有的事件都一样！！！
            FingerGestures.OnFingerDown += OnFingerDown;
            //抬起事件
            FingerGestures.OnFingerUp += OnFingerUp;
            //开始拖动事件
            FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
            //拖动中事件...
            FingerGestures.OnFingerDragMove += OnFingerDragMove;
            //拖动结束事件
            FingerGestures.OnFingerDragEnd += OnFingerDragEnd; 
            //上、下、左、右、四个方向的手势滑动
            FingerGestures.OnFingerSwipe += OnFingerSwipe;
            //连击事件 连续点击事件
            FingerGestures.OnFingerTap += OnFingerTap;
            //按下事件后调用一下三个方法
            FingerGestures.OnFingerStationaryBegin += OnFingerStationaryBegin;
            FingerGestures.OnFingerStationary += OnFingerStationary;
            FingerGestures.OnFingerStationaryEnd += OnFingerStationaryEnd;
            //长按事件
            FingerGestures.OnFingerLongPress += OnFingerLongPress;
        }

        protected override void Uninit()
        {
            //关闭时调用，这里销毁手势操作的事件
            //和上面一样
            FingerGestures.OnFingerDown -= OnFingerDown;
            FingerGestures.OnFingerUp -= OnFingerUp;
            FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
            FingerGestures.OnFingerDragMove -= OnFingerDragMove;
            FingerGestures.OnFingerDragEnd -= OnFingerDragEnd; 
            FingerGestures.OnFingerSwipe -= OnFingerSwipe;
            FingerGestures.OnFingerTap -= OnFingerTap;
            FingerGestures.OnFingerStationaryBegin -= OnFingerStationaryBegin;
            FingerGestures.OnFingerStationary -= OnFingerStationary;
            FingerGestures.OnFingerStationaryEnd -= OnFingerStationaryEnd;
            FingerGestures.OnFingerLongPress -= OnFingerLongPress;
        }

        //按下时调用
        void OnFingerDown( int fingerIndex, Vector2 fingerPos )
        {
            EventParam eventParam =  EventDispatcher.GetInstance().GetEventParam();
            eventParam.m_objParam = fingerPos;
            eventParam.m_nParam = fingerIndex;
            EventDispatcher.GetInstance().DispatchEvent((uint)LogicEvent.EVENT_FINGER_DOWN,eventParam);
        }
        
        //抬起时调用
        void OnFingerUp( int fingerIndex, Vector2 fingerPos, float timeHeldDown )
        {
            //Debug.Log(" OnFingerUp ="  +fingerPos); 
        }
        
        //开始滑动
        void OnFingerDragBegin( int fingerIndex, Vector2 fingerPos, Vector2 startPos )
        {
            //Debug.Log("OnFingerDragBegin fingerIndex =" + fingerIndex  + " fingerPos ="+fingerPos +"startPos =" +startPos); 
        }
        //滑动结束
        void OnFingerDragEnd( int fingerIndex, Vector2 fingerPos )
        {
            //Debug.Log("OnFingerDragEnd fingerIndex =" + fingerIndex  + " fingerPos ="+fingerPos); 
        }
        //滑动中
        void OnFingerDragMove( int fingerIndex, Vector2 fingerPos, Vector2 delta )
        {
            //transform.position = GetWorldPos( fingerPos );
            //Debug.Log(" OnFingerDragMove ="  +fingerPos); 
            
        }
        //上下左右四方方向滑动手势操作
        void OnFingerSwipe( int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity )
        {
            //结果是 Up Down Left Right 四个方向
            //Debug.Log("OnFingerSwipe " + direction + " with finger " + fingerIndex);
        }
        
        //连续按下事件， tapCount就是当前连续按下几次
        void OnFingerTap( int fingerIndex, Vector2 fingerPos, int tapCount )
        {
            //Debug.Log("OnFingerTap " + tapCount + " times with finger " + fingerIndex);
        }
        
        //按下事件开始后调用，包括 开始 结束 持续中状态只到下次事件开始！
        void OnFingerStationaryBegin( int fingerIndex, Vector2 fingerPos )
        {
            //Debug.Log("OnFingerStationaryBegin " + fingerPos + " times with finger " + fingerIndex);
        }
        
        
        void OnFingerStationary( int fingerIndex, Vector2 fingerPos, float elapsedTime )
        {
            //Debug.Log("OnFingerStationary " + fingerPos + " times with finger " + fingerIndex); 
        }
        
        void OnFingerStationaryEnd( int fingerIndex, Vector2 fingerPos, float elapsedTime )
        {
            //Debug.Log("OnFingerStationaryEnd " + fingerPos + " times with finger " + fingerIndex);
        }
        
        
        //长按事件
        void OnFingerLongPress( int fingerIndex, Vector2 fingerPos )
        {
            //Debug.Log("OnFingerLongPress " + fingerPos );
        }
        
        //把Unity屏幕坐标换算成3D坐标
        Vector3 GetWorldPos( Vector2 screenPos )
        {
            Camera mainCamera = Camera.main;
            return mainCamera.ScreenToWorldPoint( new Vector3( screenPos.x, screenPos.y, Mathf.Abs( transform.position.z - mainCamera.transform.position.z ) ) );
        }

        // 初始化触摸组件
        private void InitFingerGesture()
        {
            if( !FingerGestures.Instance )
            {
                FingerGestures prefab;

                if( Application.isEditor )
                {
                    prefab = editorGestures;
                }
                else
                {
    #if UNITY_IPHONE
                    prefab = iosGestures;
    #elif UNITY_ANDROID
                    prefab = androidGestures;
    #else
                    prefab = desktopGestures;
    #endif
                }

                FingerGestures instance = Instantiate( prefab ) as FingerGestures;
                instance.name = prefab.name;
                
                DontDestroyOnLoad( instance.gameObject );
            }
        }

    }
}