using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private Vector3 lastCheckpointPos = new Vector3(0f, 1f, -22f);
    private GameObject[] holes = new GameObject[3];
    private GameObject[] walls = new GameObject[2];
    private float[] wallBaseZ = new float[2];
    private string message = "";
    private float messageTimer = 0f;
    private float respawnTimer = 0f;

    void Start()
    {
        for (int i = 0; i < holes.Length; i++)
        {
            holes[i] = GameObject.Find("Hole" + (i + 1));
            if (holes[i] != null)
            {
                Renderer r = holes[i].GetComponent<Renderer>();
                if (r != null) r.material.color = Color.black;
            }
        }

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = GameObject.Find("MovingWall" + (i + 1));
            if (walls[i] != null)
            {
                wallBaseZ[i] = walls[i].transform.position.z;
                Renderer r = walls[i].GetComponent<Renderer>();
                if (r != null) r.material.color = Color.red;
            }
        }
    }

    void Update()
    {
        respawnTimer -= Time.deltaTime;
        messageTimer -= Time.deltaTime;

        GameObject player = GameObject.Find("Player");
        if (player == null) return;

        for (int i = 1; i <= 10; i++)
        {
            GameObject cp = GameObject.Find("Checkpoint" + i);
            if (cp == null) continue;
            if (Vector3.Distance(cp.transform.position, player.transform.position) < 2.5f)
            {
                lastCheckpointPos = new Vector3(cp.transform.position.x, 1f, cp.transform.position.z);
            }
        }

        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i] == null) continue;
            float x = Mathf.Sin(Time.time * 1.5f + i * 2f) * 3f;
            walls[i].transform.position = new Vector3(x, 1f, wallBaseZ[i]);
        }

        if (respawnTimer > 0f) return;

        foreach (GameObject hole in holes)
        {
            if (hole == null) continue;
            Vector3 p = player.transform.position;
            Vector3 h = hole.transform.position;
            float d = Mathf.Sqrt((p.x - h.x) * (p.x - h.x) + (p.z - h.z) * (p.z - h.z));
            if (d < 1.2f)
            {
                Respawn(player, "穴に落ちた！ チェックポイントに戻るよ");
                return;
            }
        }

        foreach (GameObject wall in walls)
        {
            if (wall == null) continue;
            if (Vector3.Distance(wall.transform.position, player.transform.position) < 1.8f)
            {
                Respawn(player, "壁にぶつかった！ チェックポイントに戻るよ");
                return;
            }
        }
    }

    void Respawn(GameObject player, string msg)
    {
        player.transform.position = lastCheckpointPos;
        message = msg;
        messageTimer = 2f;
        respawnTimer = 1f;
    }

    void OnGUI()
    {
        if (messageTimer > 0f)
        {
            GUI.Label(new Rect(10, 70, 500, 30), message);
        }
    }
}
