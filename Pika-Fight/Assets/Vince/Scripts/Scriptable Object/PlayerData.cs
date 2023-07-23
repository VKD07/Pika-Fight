using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="CharacterData_", menuName = "Player/New_CharacterData")]
public class PlayerData : ScriptableObject
{
    public FloatReference playerVeloctiy;
    public FloatReference movementSpeed;
    public FloatReference playerHealth;
    public PlayerAnimationData playerAnimData;
}
