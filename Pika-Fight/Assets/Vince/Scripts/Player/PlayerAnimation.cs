using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] FloatReference velocity;
    [SerializeField] PlayerAnimationData playerAnimData;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        RunAnim();
        BallThrowAnim();
    }
    public void RunAnim()
    {
        anim.SetFloat("Velocity", velocity.Value);
    }
    public void BallThrowAnim()
    {
        anim.SetBool("Throw", playerAnimData.IsThrowing);
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
}
