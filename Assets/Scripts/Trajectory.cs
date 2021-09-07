using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField]
    private BallController ball;
    [SerializeField]
    private CircleCollider2D ballCollider;
    [SerializeField]
    private Rigidbody2D ballRigidbody;
    [SerializeField]
    private GameObject ballAtCollision;
    [SerializeField]
    private bool drawBallAtCollision = false;

    void Awake()
    {
        enabled = false;
    }

    void Update()
    {
        Vector2 offsetHitPoint = new Vector2();
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius, ballRigidbody.velocity.normalized);

        foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if (circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallController>() == null)
            {
                Vector2 hitPoint = circleCastHit2D.point;
                Vector2 hitNormal = circleCastHit2D.normal;
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if (outDot > -1.0f && outDot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offsetHitPoint,
                            offsetHitPoint + outVector * 10.0f);

                        drawBallAtCollision = true;
                    }
                }
                break;
            }
        }

        if (drawBallAtCollision)
        {
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        }
        else
        {
            ballAtCollision.SetActive(false);
        }
    }
}
