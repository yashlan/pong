using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Max Score")]
    [SerializeField]
    private int maxScore;
    private bool isDebugWindowShown = false;

    [Header("Trajectory")]
    [SerializeField]
    private Trajectory trajectory;

    [Header("Player 1")]
    [SerializeField]
    private PlayerController player1; 
    [SerializeField]
    private Rigidbody2D player1_rb; 

    [Header("Player 2")]
    [SerializeField]
    private PlayerController player2;
    [SerializeField]
    private Rigidbody2D player2_rb;

    [Header("Ball")]
    [SerializeField]
    private BallController ball;
    [SerializeField]
    private Rigidbody2D ballRigidbody;
    [SerializeField]
    private CircleCollider2D ballCollider;

    public int MaxScore => maxScore;


    // Untuk menampilkan GUI
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }

        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;
        }


        GUI.Label(new Rect(Screen.width / 2 - 150 - 45, 20, 100, 100), "Player 1");
        GUI.Label(new Rect(Screen.width / 2 - 150 - 25, 40, 100, 100), player1.Score.ToString());
        GUI.Label(new Rect(Screen.width / 2 + 150 + 45, 20, 100, 100), "Player 2");
        GUI.Label(new Rect(Screen.width / 2 + 150 + 65, 40, 100, 100), player2.Score.ToString());

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();

            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if (player1.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }
}
