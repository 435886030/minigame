using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public float maxHP = 100f;

    public float HP
    {
        get { return _currentHP; }//外界不可直接修改血量，必须调用TakeDamage
    }

    public float flashSpeed = 5f;
    public Image damageImage;//用于闪红
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);



    [HideInInspector] public Transform cachedTransform;

    public float _currentHP;
    private PlayerMovement _playerMovement;
    private bool _damaged;

    private LogicEventHelper _logicEventHelper = new LogicEventHelper();

    #region UNITY_FUNCTION
    void Awake()
    {
        cachedTransform = gameObject.transform;
        _playerMovement = GetComponent<PlayerMovement>();
        _damaged = false;
        _currentHP = maxHP;
    }


    void Update () {
        if (_damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        _damaged = false;
    }

    void OnEnable()
    {
        _logicEventHelper.AddEventListener((uint)LogicEvent.PLAYER_ON_DIE, Dead);
    }

    void OnDisable()
    {
        _logicEventHelper.RemoveEventListener((uint)LogicEvent.PLAYER_ON_DIE, Dead);
    }

    #endregion

    #region MY_FUNCTION



    /// <summary>
    /// 
    /// </summary>
    /// <param name="hurtValue"></param>
    /// <returns></returns>
    public int TakeDamage(float hurtValue)
    {
        
        if (_currentHP > 0f)
        {
            _currentHP -= hurtValue;
            _damaged = true;
            
        }
        else
        {
            EventParam ep = EventDispatcher.GetInstance().GetEventParam();
            EventDispatcher.GetInstance().DispatchEvent((uint) LogicEvent.PLAYER_ON_DIE, ep);

        }
        return 0;
    }

    private void Dead(EventParam param)
    {
        //_playerMovement.enabled = false;
        _currentHP = maxHP;
    }
    #endregion
}