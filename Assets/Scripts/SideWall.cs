using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Ball")
        {
            player.IncrementScore();

            if (player.Score < gameManager.MaxScore)
            {
                collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
