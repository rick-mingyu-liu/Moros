using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BossState.cs
public abstract class BossState
{
    public abstract void OnEnter();
    public abstract void Update();
    public abstract void OnExit();
}
