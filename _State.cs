using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _State<T>
{
    public abstract void Enter(T target);
    public abstract void Execute(T target);
    public abstract void Exit(T target);
}
