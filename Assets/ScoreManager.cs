using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score = 0;
    public static bool Finished = false;

    public static void AddScore(int points)
    {
        Score += points;
        Debug.Log("スコア: " + Score);
    }

    public static void Finish()
    {
        Finished = true;
        Debug.Log("ゴール！ 最終スコア: " + Score);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 30), "スコア: " + Score);
        if (Finished)
        {
            GUI.Label(new Rect(10, 40, 400, 30), "ゴール！ 最終スコア: " + Score);
        }
    }
}
