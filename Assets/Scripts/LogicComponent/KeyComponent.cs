using UnityEngine;
using System.Collections;
using Assets.Scripts.Common;

public class KeyComponent : MonoBehaviour
{
    //关联的门
    public DoorComponent doorComponent = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (doorComponent != null)
        {
            doorComponent.OpenDoor();
        }
        else
        {
            DebugHelper.Assert(false);
        }
    }

}
