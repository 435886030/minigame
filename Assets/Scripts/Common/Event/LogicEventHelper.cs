using System.Collections.Generic;


/// <summary>
/// ʹ�÷�����
/// step 1: Ϊĳ�������һ�� LogicEventHelper ����: private LogicEventHelper _logicEventHelper = new LogicEventHelper();
/// step 2: ����mono�࣬���� OnEnable �����е��� LogicEventHelper �������Ӽ����ķ�����������ͨ�࣬����RegisterLogicEvent��������Ӽ���
/// step 3: ����mono�࣬�� OnDisable �����е��� LogicEventHelper �����ClearAll������������ͨ�࣬��UnInit()�е���
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