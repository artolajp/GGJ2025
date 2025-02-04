using UnityEngine;
using System;

public class Actions
{
    public static Action<PlayerController> PlayerDeath;
    public static Action<PlayerController> PlayerScored;
    public static Action<PlayerBuilderController> PlayerBuilded;
}
