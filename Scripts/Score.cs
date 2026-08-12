using UnityEngine;

public class Score : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject .CompareTag("bird"))
        {
            GameManager.instance.AddScore();
        }
    }
}
