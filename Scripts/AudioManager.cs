using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip fly;

    [SerializeField] private AudioClip Score;

    [SerializeField] private AudioClip hit;

    [SerializeField] private AudioClip die;

    private AudioSource audioSource;


    public void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Fly()
    {
        audioSource.PlayOneShot(fly);

    }
    public void score()
    {
        audioSource.PlayOneShot(Score);

    }
    public void Hit()
    {
        audioSource.PlayOneShot(hit);

    }
    public void Die()
    {
        audioSource.PlayOneShot(die);

    }


}
