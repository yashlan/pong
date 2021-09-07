using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float xInitialForce = 80f;
    private const float yInitialForce = 15f;

    private Rigidbody2D rb;
    private Vector2 trajectoryOrigin;
    public Vector2 TrajectoryOrigin => trajectoryOrigin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        RestartGame();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke(nameof(PushBall), 2f);
    }

    void ResetBall() => rb.velocity = transform.position = Vector2.zero;

    void PushBall()
    {
        float randomDirection = Random.Range(0, 2);

        if (randomDirection < 1.0f)
        {
            rb.AddForce(new Vector2(-xInitialForce, yInitialForce));
        }
        else
        {
            rb.AddForce(new Vector2(xInitialForce, yInitialForce));
        }
    }
}
