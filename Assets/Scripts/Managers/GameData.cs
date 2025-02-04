using UnityEngine;

public struct GameData
{
    private static int score_01 = 2;
    private static int score_02 = 2;

    public static int Score_01
    {
        get { return score_01; }
        set { score_01 = value; }
    }

    public static int Score_02
    {
        get { return score_02; }
        set { score_02 = value; }
    }
}