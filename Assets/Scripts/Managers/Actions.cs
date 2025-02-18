using UnityEngine;
using System;

public class Actions
{
    public static Action<PlayerController> playerDeath;
    public static Action<PlayerController> playerScored;
    public static Action<PlayerBuilderController> playerBuilded;
    public static Action portalBuilded;
    public static Action<bool> spelunkyTime;
    public static Action rulerIsDone;
    public static Action<bool> canShoot;
    public static Action<Lasert> lasertPortalTouched;
    public static Action<Lasert> lasertPortalUntouched;
    public static Action<string> makeTransition;
}
