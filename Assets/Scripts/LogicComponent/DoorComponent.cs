using UnityEngine;
using System.Collections;

public class DoorComponent : MonoBehaviour
{

    public Transform left = null;
    public Transform right = null;

    public float length = 0.83f;

    public float angle = 90.0f;

    private bool bOpening = false;

    private Vector2 leftOrigPos = Vector2.zero;
    private Vector3 leftEuler = Vector3.zero;
    private Vector2 rightOrigPos = Vector2.zero;
    private Vector3 rightEuler = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (bOpening)
	    {
	        Vector2 leftCur = left.transform.localPosition;
            Vector2 leftTargget = Vector2.zero;
	        leftTargget.x = Mathf.Cos(Mathf.Deg2Rad*angle)*leftOrigPos.x;
	        leftTargget.y = -Mathf.Sin(Mathf.Deg2Rad*angle)*Mathf.Abs(leftOrigPos.x);
	        left.transform.localPosition = Vector2.Lerp(leftCur, leftTargget, 0.1f);

	        Vector3 leftEulerCur = left.transform.localEulerAngles;
	        Vector3 targetEuler = leftEulerCur;
	        targetEuler.z = angle;
	        left.transform.localEulerAngles = Vector3.Lerp(leftEulerCur, targetEuler, 0.1f);



	    }
	}

    public void OpenDoor()
    {
        if (left != null && right != null)
        {
            bOpening = true;
            leftOrigPos = this.left.localPosition;
            rightOrigPos = this.right.localPosition;
            leftEuler = this.left.localEulerAngles;
            rightEuler = this.right.localEulerAngles;
        }
    }
}
