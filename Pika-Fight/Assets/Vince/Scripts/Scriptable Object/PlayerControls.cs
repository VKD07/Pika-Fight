using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Controls", menuName = ("Player/PlayerControls"))]
public class PlayerControls : ScriptableObject
{
    [Header("Player Game Controls")]
    [SerializeField] PlayerControl playerMovementAxes;
    [SerializeField] KeyCode attackKey = KeyCode.E;
    [SerializeField] KeyCode dashKey = KeyCode.LeftControl;

    [Header("Player Join Game Controls")]
    [SerializeField] KeyCode playerReadyKey = KeyCode.J;
    [SerializeField] KeyCode playerUnreadyKey = KeyCode.Escape;
    [SerializeField] KeyCode playerStartKey = KeyCode.J;
    [SerializeField] public bool controlIstaken;
    public PlayerControl GetMovementAxes => playerMovementAxes;
    public KeyCode GetAttackKey { get => attackKey; set => attackKey = value; }
    public KeyCode GetDashKey { get => dashKey; set => dashKey = value; }

    public KeyCode PlayerReadyKey => playerReadyKey;
    public KeyCode PlayerUnreadyKey => playerUnreadyKey;
    public KeyCode PlayerStartKey => playerStartKey;

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
        Joystick3,
        Joystick4,
    }
    private void OnDisable()
    {
        controlIstaken = false;
    }
}
