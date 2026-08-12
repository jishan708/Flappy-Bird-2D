using UnityEngine;

public class Pipe_Spawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.4f;
    [SerializeField] private GameObject _pipe;

    private float timer = 0f;

    private void Start()
    {
        SpawnPipe();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            SpawnPipe();
            timer = 0;
        }

      
    }

    private void SpawnPipe()
    {
        Vector3 Spawnpos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange),0);
        if(GameManager.instance.CurrentGameState == GameState.Playing)
        {
            GameObject pipe = Instantiate(_pipe, Spawnpos, Quaternion.identity);
        }
        
      

        
    }
   
}
