using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject enemyPrefab;
    [Range(0f,20f)]
    public float generatorTimer;
    Vector3 tempPos;
    

    // Start is called before the first frame update
    void Start()
    {
        tempPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        tempPos.y = Random.Range(-3.9f, 3.9f);
        
    }
    void CreateEnemy()
    {
        transform.position = tempPos;
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
       
    }
    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }
    public void CancelGenerator()
    {
        CancelInvoke("CreateEnemy");
    }
}
