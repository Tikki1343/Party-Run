using UnityEngine;

public class RaceManager : MonoBehaviour
{
    void Start()
    {
        ScoreManager.Score = 0;
        ScoreManager.Finished = false;

        GameObject scoreObj = new GameObject("ScoreManager");
        scoreObj.AddComponent<ScoreManager>();

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0f, 1f, -22f);
            if (player.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = player.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }

        float[] checkpointZ = { -18f, -14f, -10f, -6f, -2f, 2f, 6f, 10f, 14f, 18f };
        for (int i = 0; i < checkpointZ.Length; i++)
        {
            CreateCheckpoint("Checkpoint" + (i + 1), checkpointZ[i], new Color(1f, 0.6f, 0f), 10, false);
        }

        CreateCheckpoint("Goal", 22f, new Color(0.6f, 0.3f, 1f), 100, true);
    }

    void CreateCheckpoint(string checkpointName, float z, Color color, int points, bool isGoal)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = checkpointName;
        cube.transform.position = new Vector3(0f, 1f, z);
        cube.transform.localScale = new Vector3(4f, 2f, 0.5f);
        cube.GetComponent<Collider>().isTrigger = true;
        cube.GetComponent<Renderer>().material.color = color;

        Checkpoint cp = cube.AddComponent<Checkpoint>();
        cp.points = points;
        cp.isGoal = isGoal;
    }
}
