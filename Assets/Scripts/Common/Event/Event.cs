using System.Collections.Generic;
using Assets.Scripts.Common;
/// <summary>
/// 定义委托函数
/// </summary>
/// <param name="param">
/// The param.
/// </param>
public delegate void EventHandler(EventParam param = null);

/// <summary>
/// The event dispatcher.
/// </summary>
public class EventDispatcher : Singleton<EventDispatcher>
{
    /// <summary>
    /// The m_dict event handler map.
    /// </summary>
    private Dictionary<uint, EventHandler> m_dictEventHandlerMap = new Dictionary<uint, EventHandler>();

    /// <summary>
    /// The m_event param pool.
    /// </summary>
    private List<EventParam> m_eventParamPool = new List<EventParam>();

    /// <summary>
    /// The destroy.
    /// </summary>
    void Destroy()
    {
        this.Clear();
    }

    /// <summary>
    /// The attach listener.
    /// </summary>
    /// <param name="eventKey">
    /// The event key.
    /// </param>
    /// <param name="handler">
    /// The handler.
    /// </param>
    public void AttachListener(uint eventKey, EventHandler handler)
    {
        if (m_dictEventHandlerMap.ContainsKey(eventKey))
        {
            m_dictEventHandlerMap[eventKey] -= handler;
            m_dictEventHandlerMap[eventKey] += handler;
        }
        else
        {
            m_dictEventHandlerMap[eventKey] = delegate { };
            m_dictEventHandlerMap[eventKey] += handler;
        }
    }

    /// <summary>
    /// The detach listener.
    /// </summary>
    /// <param name="eventKey">
    /// The event key.
    /// </param>
    /// <param name="handler">
    /// The handler.
    /// </param>
    public void DetachListener(uint eventKey, EventHandler handler)
    {
        if (m_dictEventHandlerMap.ContainsKey(eventKey))
        {
            m_dictEventHandlerMap[eventKey] -= handler;
        }
    }

    /// <summary>
    /// The trigger event.
    /// </summary>
    /// <param name="eventKey">
    /// The event key.
    /// </param>
    /// <param name="param">
    /// The param.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected bool TriggerEvent(uint eventKey, EventParam param)
    {
        if (m_dictEventHandlerMap.ContainsKey(eventKey))
        {
            EventHandler handler = m_dictEventHandlerMap[eventKey];
            handler(param);
            return true;
        }

        return false;
    }

    /// <summary>
    /// The dispatch event.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="param">
    /// The param.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool DispatchEvent(uint key, EventParam param)
    {
        bool bResult = TriggerEvent(key, param);
        param.m_bInUse = false;
        return bResult;
    }

    /// <summary>
    /// The dispatch event.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="inValue">
    /// The in value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool DispatchEvent(uint key, object inValue)
    {
        EventParam param = this.GetEventParam();
        param.m_objParam = inValue;
        return this.DispatchEvent(key, param);
    }

    /// <summary>
    /// The dispatch event.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="inValue">
    /// The in value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool DispatchEvent(uint key, int inValue)
    {
        EventParam param = this.GetEventParam();
        param.m_nParam = inValue;
        return this.DispatchEvent(key, param);
    }

    /// <summary>
    /// The dispatch event.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="inValue">
    /// The in value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool DispatchEvent(uint key, float inValue)
    {
        EventParam param = this.GetEventParam();
        param.m_fParam = inValue;
        return this.DispatchEvent(key, param);
    }

    /// <summary>
    /// The dispatch event.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="inValue">
    /// The in value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool DispatchEvent(uint key, string inValue)
    {
        EventParam param = this.GetEventParam();
        param.m_strParam = inValue;
        return this.DispatchEvent(key, param);
    }

    /// <summary>
    /// The clear.
    /// </summary>
    public void Clear()
    {
        m_dictEventHandlerMap.Clear();
    }

    /// <summary>
    /// The get event param.
    /// </summary>
    /// <returns>
    /// The <see cref="EventParam"/>.
    /// </returns>
    public EventParam GetEventParam()
    {
        return EventParam.GetEventParam(m_eventParamPool);
    }
}
