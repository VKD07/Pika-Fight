using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Controls", menuName = ("Player/PlayerControls"))]
public class PlayerControls : ScriptableObject
{
    [Header("Player Controls")]
    [SerializeField] PlayerControl playerMovementAxes;
    [SerializeField] KeyCode attackKey = KeyCode.E;
    [SerializeField] KeyCode dashKey = KeyCode.LeftControl;
    [SerializeField] public bool controlIstaken;

    public PlayerControl GetMovementAxes => playerMovementAxes;
    public KeyCode GetAttackKey => attackKey;
    public KeyCode GetDashKey => dashKey;

    public bool ControlIsTaken
    {
        get { return controlIstaken; }
        set { controlIstaken = value; }
    }

    public enum PlayerControl
    {
        WASD,
        Arrow,
        Joystick1,
        Joystick2,
    }
    private void OnDisable()
    {
        controlIstaken = false;
    }
}
