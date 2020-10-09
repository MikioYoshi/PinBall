using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    private HingeJoint myHingeJoint;

    private float defaultAngle = 20;

    private float flickAngle = -20;

    int fingerId = -1;

    // Start is called before the first frame update
    void Start()
    {
        this.myHingeJoint = GetComponent<HingeJoint>();
        SetAngle(this.defaultAngle);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }

        //発展課題
        TouchControll();


    }

    void TouchControll()
    { 
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began &&
                (Input.touches[i].position.x <= Screen.width / 2) && tag == "LeftFripperTag" && fingerId == -1)
            {
                fingerId = Input.touches[i].fingerId;

                SetAngle(this.flickAngle);
            }
            if (Input.touches[i].phase == TouchPhase.Began &&
                (Input.touches[i].position.x >= Screen.width / 2) && tag == "RightFripperTag" && fingerId == -1)
            {
                fingerId = Input.touches[i].fingerId;
                SetAngle(this.flickAngle);
            }
            if (Input.touches[i].phase == TouchPhase.Ended &&
                (Input.touches[i].position.x <= Screen.width / 2) && tag == "LeftFripperTag" && fingerId == Input.touches[i].fingerId)
            {
                fingerId = -1;
                SetAngle(this.defaultAngle);
            }
            if (Input.touches[i].phase == TouchPhase.Ended &&
                (Input.touches[i].position.x >= Screen.width / 2) && tag == "RightFripperTag" && fingerId == Input.touches[i].fingerId)
            {
                fingerId = -1;
                SetAngle(this.defaultAngle);
            }


            Debug.Log("touches[" + i + "]");
            Debug.Log("  position.x : " + Input.touches[i].position.x);
            Debug.Log("  phase : " + Input.touches[i].phase);
        }
    }

    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}
