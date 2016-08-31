using UnityEngine;
using System.Collections;

public class SelfRotateMovement : MonoBehaviour
{


    public float rotateSpeed = 10f;
    [HideInInspector] public Transform cachedTransform;


    void Awake()
    {
        cachedTransform = gameObject.transform;

    }


    void Update ()
    {
        cachedTransform.Rotate(Vector3.forward*Time.deltaTime*rotateSpeed);
    }
}
