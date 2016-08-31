using System.Collections.Generic;

/// <summary>
/// The event param.
/// </summary>
public class EventParam
{
    /// <summary>
    /// The m_b in use.
    /// </summary>
    public bool m_bInUse = false;

    /// <summary>
    /// The m_n param.
    /// </summary>
    public int m_nParam;

    /// <summary>
    /// The m_f param.
    /// </summary>
    public float m_fParam;

    /// <summary>
    /// The m_str param.
    /// </summary>
    public string m_strParam;

    /// <summary>
    /// The m_n param 2.
    /// </summary>
    public int m_nParam2;

    public ulong m_lParam;

    /// <summary>
    /// The m_obj param.
    /// </summary>
    public object m_objParam;

    /// <summary>
    /// The clear.
    /// </summary>
    public void Clear()
    {
        m_bInUse = false;

        m_nParam = 0;
        m_nParam2 = 0;
        m_fParam = 0.0f;
        m_lParam = 0;
        m_strParam = null;
        m_objParam = null;
    }

    /// <summary>
    /// The get event param.
    /// </summary>
    /// <returns>
    /// The <see cref="EventParam"/>.
    /// </returns>
    public static EventParam GetEventParam(List<EventParam> eventPool)
    {
        if(eventPool == null)
        {
            Assets.Scripts.Common.DebugHelper.Assert(false);
            return null;
        }
        
        for (int i = 0; i < eventPool.Count; i++)
        {
            var obj = eventPool[i];
            if (!obj.m_bInUse)
            {
                obj.m_bInUse = true;
                return obj;
            }
        }

        EventParam evtParam = new EventParam();
        evtParam.m_bInUse = true;

        eventPool.Add(evtParam);

        return evtParam;
    }

    //只允许从GetEventParam函数获取EventParam
    private EventParam(){}
}
