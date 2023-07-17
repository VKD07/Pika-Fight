using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerAnimationData", menuName = "Player/PlayerAnimationData")]
public class PlayerAnimationData : ScriptableObject
{
    [SerializeField] bool isThrowing;

    public bool IsThrowing
    {
        get { return isThrowing; }
        set { isThrowing = value; }
    }
}
