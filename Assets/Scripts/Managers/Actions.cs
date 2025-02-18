using UnityEngine;
using System;

public class Actions
{
    public static Action<PlayerController> PlayerDeath;
    public static Action<PlayerController> PlayerScored;
    public static Action<PlayerBuilderController> PlayerBuilded;
    public static Action PortalBuilded;
    public static Action<bool> spelunkyTime;
    public static Action rulerIsDone;
    public static Action<bool> canShoot;
    public static Action<Lasert> lasertPortalTouched;
    public static Action<Lasert> lasertPortalUntouched;
    public static Action<string> makeTransition;
}
