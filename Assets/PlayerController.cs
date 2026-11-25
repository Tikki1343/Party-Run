using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0f, z);
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
