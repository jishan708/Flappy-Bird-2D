using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird_Movement : MonoBehaviour
{
    [Header("Floating")]
    [SerializeField] private float floatAmplitude = 0.1f;
    [SerializeField] private float floatSpeed = 4f;

    [Header("Movement")]
    [SerializeField] private float jumpforce = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Rigidbody2D rb;
    private Vector3 StartPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Start()
    {
        StartPosition = Vector3.zero;
        rb.simulated = false; 
    }



    private void Update()
    {
        if (GameManager.instance.CurrentGameState == GameState.Home || GameManager.instance.CurrentGameState == GameState.GetReady)
        {
            floatidle();
        }  
        if(GameManager.instance .CurrentGameState == GameState.Playing)
        {
            RotateBird();
        }
    }

    private void floatidle()
    {
        float newY =Convert.ToSingle( StartPosition.y + Math.Sin(Time.time * floatSpeed) * floatAmplitude);
        transform.position = new Vector3(StartPosition.x, newY, StartPosition.z);
    }

    private void RotateBird()
    {
        float angle = Math.Clamp(rb.linearVelocityY * 5f, -90, 30);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle) , rotationSpeed * Time.deltaTime);
    }

    public void OnTap(InputAction .CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (GameManager.instance.CurrentGameState == GameState.Home || GameManager.instance.CurrentGameState == GameState.GameOver) return;

        if(GameManager .instance.CurrentGameState == GameState.GetReady)
        {
            GameManager.instance.GamePlay();
            rb.simulated = true;
        }
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0f);
        rb.AddForce(Vector2.up* jumpforce , ForceMode2D.Impulse);

        AudioManager.instance.Fly();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.CurrentGameState == GameState.GameOver) return;
        if(collision.gameObject .CompareTag ("pipe") || collision .gameObject .CompareTag("ground"))
        {
            // Audio
            AudioManager.instance.Hit();
            StartCoroutine(DieSoundDelay());
            GameManager.instance.GameOver();
        }

        IEnumerator DieSoundDelay()
        {
            yield return new WaitForSeconds(1.5f);
            AudioManager.instance.Die();
        }
       
    }

    public void ResetPlayer()
    {
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        StartPosition = new Vector3(-0.5f, 0f, 0f);
        transform.position = StartPosition;
        transform.rotation = Quaternion.identity;

    }


}
