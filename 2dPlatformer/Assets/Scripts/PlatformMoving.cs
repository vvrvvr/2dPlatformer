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
    }

    private void FixedUpdate()
    {
        if ((sj.limitState == JointLimitState2D.LowerLimit || sj.limitState == JointLimitState2D.UpperLimit) && (!isDirectionChanged))
        {
            isDirectionChanged = true;
            motor.motorSpeed *= -1;
            sj.motor = motor;
            StartCoroutine(WaitToChangeBool(0.05f));
        }
    }

    private IEnumerator WaitToChangeBool(float time)
    {
        yield return new WaitForSeconds(time);
        isDirectionChanged = !isDirectionChanged;
    }

}
