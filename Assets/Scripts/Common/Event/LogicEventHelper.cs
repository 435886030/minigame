using System.Collections.Generic;


/// <summary>
/// 使用方法：
/// step 1: 为某个类添加一个 LogicEventHelper 对象: private LogicEventHelper _logicEventHelper = new LogicEventHelper();
/// step 2: 对于mono类，在其 OnEnable 函数中调用 LogicEventHelper 对象的添加监听的方法；对于普通类，在其RegisterLogicEvent函数中添加监听
/// step 3: 对于mono类，在 OnDisable 函数中调用 LogicEventHelper 对象的ClearAll方法；对于普通类，在UnInit()中调用
/// </summary>


public class LogicEventHelper
{
    public struct EventStruct
    {
        public uint eventID;
        public EventHandler handler;

        public EventStruct(uint _eventID, EventHandler _handler)
        {
            eventID = _eventID;
            handler = _handler;
        }
        
    }
    private List<EventStruct> _registerdEventHandler = null;
    public void AddEventListener(uint eventID,EventHandler handler)
    {
        if(_registerdEventHandler == null)
        {
            _registerdEventHandler = new List<EventStruct>();    
        }

        _registerdEventHandler.Add(new EventStruct(eventID,handler));
        EventDispatcher.Instance.AttachListener(eventID,handler);
    }
    
    public void RemoveEventListener(uint eventID,EventHandler handler)
    {
        if(_registerdEventHandler != null)
        {
            _registerdEventHandler.Remove(new EventStruct(eventID,handler));
        }
        EventDispatcher.Instance.DetachListener(eventID,handler);
    }

    public void ClearAll()
    {
        if(_registerdEventHandler != null)
        {
            EventStruct eventStruct ;
            for(int i = 0; i < _registerdEventHandler.Count ;i++)
            {
                eventStruct = _registerdEventHandler[i];
                EventDispatcher.Instance.DetachListener(eventStruct.eventID, eventStruct.handler);
            }

            _registerdEventHandler.Clear();
            _registerdEventHandler = null;
        }
    }
}