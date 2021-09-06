using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private float firstSpeed = 0;
    private bool GetFirstSpeed = false;

    [SerializeField]
    private float xInitialForce;
    [SerializeField]
    private float yInitialForce;

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
        StartCoroutine(PushBall(1.75f));
    }

    void ResetBall() => rb.velocity = transform.position = Vector2.zero;

    IEnumerator PushBall(float time)
    {
        yield return new WaitForSeconds(time);
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);

        if (randomDirection < 1.0f)
        {
            rb.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else
        {
            rb.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
        yield return new WaitForSeconds(0.25f);
        firstSpeed = rb.velocity.magnitude;
        GetFirstSpeed = true;
        yield break;
    }

    void ForceConstantSpeed()
    {
        if (rb.velocity.magnitude != firstSpeed && GetFirstSpeed)
        {
            rb.velocity = rb.velocity.normalized * firstSpeed;
        }
    }

    void Update()
    {
        ForceConstantSpeed();
    }
}
