using UnityEngine;

public struct GameData
{
    private static int setScore_01 = 1;
    private static int setScore_02 = 1;
    private static int setTargetScore = 16;

    private static int score_01 = setScore_01;
    private static int score_02 = setScore_02;
    private static int targetScore = setTargetScore;

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
        score_01 = setScore_01;
        score_02 = setScore_02;
        targetScore = setTargetScore;
    }
}