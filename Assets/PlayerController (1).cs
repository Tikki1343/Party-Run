using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float dashSpeed = 18f;
    public float dashDuration = 0.35f;
    public float dashCooldown = 8f;

    private float dashTimer = 8f;
    private float dashTimeLeft = 0f;
    private Vector3 dashDirection = Vector3.forward;
    private Vector3 lastMoveDirection = Vector3.forward;

    void Update()
    {
        dashTimer += Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0f, z);

        if (move.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = move.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashTimer >= dashCooldown && dashTimeLeft <= 0f)
        {
            dashDirection = lastMoveDirection;
            dashTimeLeft = dashDuration;
            dashTimer = 0f;
        }

        if (dashTimeLeft > 0f)
        {
            dashTimeLeft -= Time.deltaTime;
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }
    }
}
