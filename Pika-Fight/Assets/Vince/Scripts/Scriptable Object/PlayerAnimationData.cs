using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerAnimationData", menuName = "Player/PlayerAnimationData")]
public class PlayerAnimationData : ScriptableObject
{
    [SerializeField] bool isThrowing;
    [SerializeField] bool ballOnHand;
    public bool IsThrowing
    {
        get { return isThrowing; }
        set { isThrowing = value; }
    }

    public bool BallOnHand
    {
        get { return ballOnHand; }
        set { ballOnHand = value; }
    }
}
