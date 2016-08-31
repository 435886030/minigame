using System;
using UnityEngine;
using System.Collections;
// ReSharper disable All


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f;//以编辑器里的为准
    public float rotateSpeed = 0.1f;
    //public float epsilon = 0.01f;

    public GameObject spawnPoint;
    [HideInInspector] public Transform cachedTransform ;
    //private Rigidbody2D playerRigidBody2D;
    private Vector3 moveMent3D;
    private float rotationAngle;
    private Vector2 mousePosition;

    private LogicEventHelper _logicEventHelper = new LogicEventHelper();

    #region  UNITY_FUNCTION
    void Awake ()
	{
	    cachedTransform = gameObject.transform;
	    //playerRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        moveMent3D.Set(0f,0f,0f);
        rotationAngle = 0f;
        Reborn(null);

    }

    void OnEnable()
    {
        //TODO 从logicEventHelper中监听
        //按下事件
        FingerGestures.OnFingerDown += OnFingerDown;
        //抬起事件
        FingerGestures.OnFingerUp += OnFingerUp;
        //长按事件
        FingerGestures.OnFingerLongPress += OnFingerLongPress;


        _logicEventHelper.AddEventListener((uint)LogicEvent.PLAYER_ON_DIE, Reborn);
    }

    void OnDisable()
    {
        //按下事件
        FingerGestures.OnFingerDown -= OnFingerDown;
        //抬起事件
        FingerGestures.OnFingerUp -= OnFingerUp;
        //长按事件
        FingerGestures.OnFingerLongPress -= OnFingerLongPress;

        _logicEventHelper.AddEventListener((uint)LogicEvent.PLAYER_ON_DIE, Reborn);
    }


    void FixedUpdate()
    {
       MoveAndTurn();
    }

    #endregion



    #region MY_FUNCTION

    void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        //fingerIndex 是手指的ID 第一按下的手指就是 0 第二个按下的手指就是1
        //fingerPos 手指按下屏幕中的2D坐标，需要转化为世界坐标
        //Debug.Log(" OnFingerDown =" + fingerPos);
        mousePosition = GetWorldPos(fingerPos);
        rotationAngle = GetAngle(mousePosition, (Vector2) cachedTransform.position);
        //Debug.Log(" mousePosition =" + mousePosition);
    }

    void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        //Debug.Log(" OnFingerUp =" + fingerPos);
    }

    void OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
    {

        Debug.Log("OnFingerLongPress " + fingerPos);
        mousePosition = fingerPos;
    }


    void MoveAndTurn()
    {
        moveMent3D = (Vector3)mousePosition - cachedTransform.position;

        //if (moveMent3D.sqrMagnitude <= epsilon)
        //{
        //    moveMent3D = Vector3.zero;
        //}
        //else
        //{
        //    moveMent3D = moveMent3D.normalized * moveSpeed * Time.deltaTime;
        //    cachedTransform.position += moveMent3D;

            cachedTransform.rotation = Quaternion.Euler(new Vector3(0,0, rotationAngle));
        ////Vector3 newdir = Vector3.RotateTowards(cachedTransform.forward, -moveMent3D, rotateSpeed * Time.deltaTime, 0.0f);
        ////cachedTransform.rotation = Quaternion.LookRotation(newdir);
        //}
        cachedTransform.position = Vector3.MoveTowards(cachedTransform.position, (Vector3) mousePosition, moveSpeed*Time.deltaTime);
        
        //TODO: 慢慢地转向，然后变速朝目标点前进
    }


    void Reborn(EventParam ep)
    {
        if (spawnPoint)
        {
            cachedTransform.position = spawnPoint.transform.position;
            mousePosition = (Vector2) spawnPoint.transform.position;
        }
    }


    //把Unity屏幕坐标换算成3D坐标
    Vector3 GetWorldPos(Vector2 screenPos)
    {
        Camera mainCamera = Camera.main;
        return mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(transform.position.z - mainCamera.transform.position.z)));
    }

    float GetAngle(Vector2 dstPos, Vector2 srcPos)
    {
        return Mathf.Atan2(dstPos.y - srcPos.y, dstPos.x-srcPos.x) * Mathf.Rad2Deg;

    }
    #endregion
}
