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
        if (Input.GetKey(playerControls.GetAttackKey))
        {
            anim.SetBool("Throw",true);
        }else if (Input.GetKeyUp(playerControls.GetAttackKey))
        {
            anim.SetBool("Throw", false);
        }
    }
    
    public PlayerControls SetPlayerControls { set { playerControls = value; } }
}
