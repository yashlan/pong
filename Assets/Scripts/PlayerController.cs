using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerType { Player1, Player2 }

    [Header("Type Input")]
    [SerializeField]
    private PlayerType playerType;

    private const float yBoundary = 9f;

    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;
    private int score = 0;
    private ContactPoint2D lastContactPoint;

    public int Score => score;
    public ContactPoint2D LastContactPoint => lastContactPoint;

    public void IncrementScore() => score++;
    public void ResetScore() => score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        setMinMaxPosition();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    void Movement()
    {
        Vector2 velocity = rb.velocity;

        if(playerType == PlayerType.Player1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                velocity.y = speed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                velocity.y = -speed;
            }
            else
            {
                velocity.y = 0f;
            }
        }

        if(playerType == PlayerType.Player2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                velocity.y = speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                velocity.y = -speed;
            }
            else
            {
                velocity.y = 0f;
            }
        }

        rb.velocity = velocity;
    }

    void setMinMaxPosition()
    {
        Vector3 position = transform.position;

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        transform.position = position;
    }
}
