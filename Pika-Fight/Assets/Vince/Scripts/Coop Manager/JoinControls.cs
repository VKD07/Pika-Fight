using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="JoiningControls", menuName ="JoinControls")]
public class JoinControls : ScriptableObject
{
    [Header("Keys To Join the game")]
    public KeyCode Player_WASD = KeyCode.J;
    public KeyCode Player_Arrow = KeyCode.C;
    public KeyCode Player_Joystick1 = KeyCode.Joystick1Button0;
    public KeyCode Player_Joystick2 = KeyCode.Joystick2Button0;
    public KeyCode Player_Joystick3 = KeyCode.Joystick3Button0;
    public KeyCode Player_Joystick4 = KeyCode.Joystick4Button0;
}