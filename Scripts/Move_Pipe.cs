using UnityEngine;

public class Move_Pipe : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private float leftedge;

    private void Start()
    {
        leftedge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        if(GameManager.instance.CurrentGameState == GameState.Playing)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if(transform.position.x < leftedge || GameManager .instance.CurrentGameState == GameState.GetReady)
        {
            Destroy(gameObject);
        }
       
    }

}
