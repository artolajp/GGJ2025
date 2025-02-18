using UnityEngine;

public struct GameData
{
    private static int score_01 = 4;
    private static int score_02 = 4;
    private static int targetScore = 6;

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

    public static int TargetScore
    {
        get { return targetScore; }
        set { targetScore = value; }
    }

    public static void ResetGameData()
    {
        score_01 = 4;
        score_02 = 4;
        targetScore = 6;
    }
}