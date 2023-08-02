using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigBridge : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    
    public PlayerConfig PlayerConfig { get => playerConfig; set => playerConfig = value; }

    public void SetDeathStatus(bool enable)
    {
        playerConfig.PlayerIsDead = enable;
    }
}
