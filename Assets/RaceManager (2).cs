using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public Vector3[] checkpointPositions = new Vector3[]
    {
        new Vector3(0f, 1f, -18f),
        new Vector3(0f, 1f, -14f),
        new Vector3(3f, 1f, -10f),
        new Vector3(7f, 1f, -6f),
        new Vector3(10f, 1f, -2f),
        new Vector3(10f, 1f, 2f),
        new Vector3(7f, 1f, 6f),
        new Vector3(3f, 1f, 10f),
        new Vector3(0f, 1f, 14f),
        new Vector3(0f, 1f, 18f),
    };
    public Vector3 goalPosition = new Vector3(0f, 1f, 22f);

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

        for (int i = 0; i < checkpointPositions.Length; i++)
        {
            string checkpointName = "Checkpoint" + (i + 1);
            GameObject existing = GameObject.Find(checkpointName);
            if (existing != null)
            {
                SetupCheckpoint(existing, 10, false);
            }
            else
            {
                CreateCheckpoint(checkpointName, checkpointPositions[i], new Color(1f, 0.6f, 0f), 10, false);
            }
        }

        GameObject existingGoal = GameObject.Find("Goal");
        if (existingGoal != null)
        {
            SetupCheckpoint(existingGoal, 100, true);
        }
        else
        {
            CreateCheckpoint("Goal", goalPosition, new Color(0.6f, 0.3f, 1f), 100, true);
        }
    }

    void SetupCheckpoint(GameObject obj, int points, bool isGoal)
    {
        obj.GetComponent<Collider>().isTrigger = true;
        Checkpoint cp = obj.GetComponent<Checkpoint>();
        if (cp == null) cp = obj.AddComponent<Checkpoint>();
        cp.points = points;
        cp.isGoal = isGoal;
    }

    void CreateCheckpoint(string checkpointName, Vector3 pos, Color color, int points, bool isGoal)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = checkpointName;
        cube.transform.position = pos;
        cube.transform.localScale = new Vector3(4f, 2f, 0.5f);
        cube.GetComponent<Collider>().isTrigger = true;
        cube.GetComponent<Renderer>().material.color = color;

        Checkpoint cp = cube.AddComponent<Checkpoint>();
        cp.points = points;
        cp.isGoal = isGoal;
    }
}
