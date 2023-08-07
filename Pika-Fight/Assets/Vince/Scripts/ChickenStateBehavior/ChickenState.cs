using Unity.VisualScripting;
using UnityEngine;

public abstract class ChickenState
{
    public abstract void OnEnterState(ChickenStateManager state);
    public abstract void OnUpdateState(ChickenStateManager state);

    public abstract void OnCollisionEnter(ChickenStateManager state);
}
