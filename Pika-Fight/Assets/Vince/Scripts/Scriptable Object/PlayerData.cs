using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerData_", menuName = "Player/New_PlayerData")]
public class PlayerData : ScriptableObject
{
    public FloatReference playerVeloctiy;
    public FloatReference movementSpeed;
    public FloatReference playerHealth;
    public PlayerAnimationData playerAnimData;
}
