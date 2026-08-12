using UnityEngine;

public class Ground_Scrolling : MonoBehaviour
{
   [SerializeField] private float Scroll_Speed = 2f;

    private Material  material;

    private Vector2 offset;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (GameManager.instance.CurrentGameState == GameState.GameOver)
        {
            return;
        }
           
       
      
        offset.x += Scroll_Speed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }

}
