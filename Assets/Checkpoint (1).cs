using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int points = 10;
    public bool isGoal = false;
    private bool passed = false;

    void Update()
    {
        if (passed) return;
        GameObject player = GameObject.Find("Player");
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 2.5f)
        {
            Pass();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (passed) return;
        if (other.gameObject.name != "Player") return;
        Pass();
    }

    void Pass()
    {
        passed = true;
        ScoreManager.AddScore(points);
        if (isGoal)
        {
            ScoreManager.Finish();
        }

        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            r.material.color = Color.gray;
        }
    }
}
