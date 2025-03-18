using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PersonAim : MonoBehaviour
{
    private Animator anim;
    private float switchAngleTurn = 45f;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void PlayPersonAnim(Vector3 m_Input, bool isRunning)
    {
        float animationSpeed = isRunning ? 1 : 0.5f;
        if (m_Input.sqrMagnitude > 0)
        {
            anim.SetFloat("velocityX", m_Input.x * animationSpeed, 0.1f, Time.deltaTime);
            anim.SetFloat("velocityY", m_Input.z * animationSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("velocityX", 0, 0.1f, Time.deltaTime);
            anim.SetFloat("velocityY", 0, 0.1f, Time.deltaTime);
        }
    }
    public void Jumping(bool isJump)
    { 
        anim.SetBool("isJumping", isJump);
    }
    public void TurnAnimation(Vector3 inputCam, bool isLimitAngle)
    {
        if (isLimitAngle && Mathf.Abs(inputCam.x) > 0.1f)
        {
            float currentDeltaMouse = anim.GetFloat("delta");
            float smoothDeltaMouse = Mathf.Lerp(currentDeltaMouse, inputCam.x * switchAngleTurn, 0.1f);
            anim.SetFloat("delta", smoothDeltaMouse, 0.1f, Time.smoothDeltaTime);
            Debug.Log("turn " + isLimitAngle);
        }
        else
        {
            anim.SetFloat("delta", 0, 0.1f, Time.smoothDeltaTime);
            Debug.Log("turn " + isLimitAngle);
        }
    }
}
