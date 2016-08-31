public enum LogicEvent
{
    EVENT_BEGIN = 0,
    //触屏相关事件
    EVENT_FINGER_DOWN=1,

    #region 场景加载
    EVENT_SCENE_LOADED = 100,
    EVENT_MAX = 101,
#endregion

#region 主角事件
    PLAYER_TO_DIE = 1000,//主角请求死亡
    PLAYER_ON_DIE = 1001,//主角响应死亡

#endregion

}
