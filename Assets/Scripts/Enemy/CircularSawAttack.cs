using UnityEngine;
using System.Collections;

public class CircularSawAttack : MonoBehaviour {

	public float hurtValue = 10f; //单位时间伤害值
    public float hurtInterval = 0.2f; //两次伤害时间间隔，秒

    private GameObject _player;
    private PlayerHealth _playerHealth;
    private bool _playerInRange;
    private float _timer;

    private LogicEventHelper _logicEventHelper = new LogicEventHelper();

    #region UNITY_FUNCTION
    void Awake()
    {
        //TODO 后期改为从对象池中通过id获取 _player
        _player = GameObject.FindWithTag("player");
        if (_player)
        {
            _playerHealth = _player.GetComponent<PlayerHealth>();
        }
        _timer = 0f;
        
    }

    void OnEnable()
    {
        _logicEventHelper.AddEventListener((uint)LogicEvent.PLAYER_ON_DIE, Dead);
    }

    void OnDestroy()
    {
        _logicEventHelper.RemoveEventListener((uint)LogicEvent.PLAYER_ON_DIE, Dead);
    }

    void Update ()
	{
	    _timer += Time.deltaTime;
	    if (_timer >= hurtInterval && _playerInRange)
	    {
	        Attack();
	    }
	}



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _player)
        {
            _playerInRange = true;
        }
        else
        {
            //TODO 被子弹打中，会不会播放火花动画
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _player)
        {
            _playerInRange = false;
        }
    }

    #endregion



    #region MY_FUNCTION



    void Attack()
    {
        _timer = 0f;

        _playerHealth.TakeDamage(hurtValue);
        Debug.Log("circular saw attack once.");

    }

    void Dead(EventParam param)
    {
        _playerInRange = false;

    }

    #endregion
}
