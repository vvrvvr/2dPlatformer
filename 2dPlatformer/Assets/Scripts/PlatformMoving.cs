using System.Collections;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    SliderJoint2D sj;
    JointMotor2D motor;
    private bool isDirectionChanged;

    private void Start()
    {
        sj = GetComponent<SliderJoint2D>();
        motor = sj.motor;
        //motor.motorSpeed = 10f;
    }
    private void Update()
    {
        //Debug.Log(sj.limitState);
        //Debug.Log(motor.motorSpeed);
        
    }

    private void FixedUpdate()
    {
        if ((sj.limitState == JointLimitState2D.LowerLimit || sj.limitState == JointLimitState2D.UpperLimit) && (!isDirectionChanged))
        {
            isDirectionChanged = true;
            //sj.useMotor = false;
            motor.motorSpeed *= -1;
            sj.motor = motor;
           // sj.useMotor = true;
            StartCoroutine(WaitToChangeBool(0.05f));
        }
    }

    private IEnumerator WaitToChangeBool(float time)
    {
        yield return new WaitForSeconds(time);
       // yield return new WaitForEndOfFrame();
        isDirectionChanged = !isDirectionChanged;
    }

}
