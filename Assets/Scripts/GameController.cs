using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
    
    [SerializeField] private float diamOfHex;
    [SerializeField] private int numOfRings;
    public GameObject hexPref;
    private Vector3 north = new Vector3(0, 0, 1);
    private Vector3 northEast = new Vector3(Mathf.Cos(Mathf.PI / 6) * 1, 0, Mathf.Sin(Mathf.PI / 6) * 1);
    private Vector3 southEast = new Vector3(Mathf.Cos(Mathf.PI / 6) * 1, 0, -Mathf.Sin(Mathf.PI / 6) * 1);
    private int counterOfSpawnPosition = 0;
    private Vector3[] spawnPositionArray;
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private float enemySpavnWait;
    private Vector3 enemySpawnPosition;
    [SerializeField] private int numOfEnemyOnLevel = 20;
    [SerializeField] private Text numOfEnemyText;

    void CreateGrid()
    {
        for (int i = 1; i <= numOfRings; i++)
        {
            buildRayOfHex(north, southEast, i);
            buildRayOfHex(northEast, -north, i);
            buildRayOfHex(southEast, -northEast, i);
            buildRayOfHex(-north, -southEast, i);
            buildRayOfHex(-northEast, north, i);
            buildRayOfHex(-southEast, northEast, i);
        }
    }

    void buildRayOfHex(Vector3 startDirection, Vector3 direction, int numOfCurrentRing)
    {
        if (numOfCurrentRing != numOfRings)
        {
            Vector3 spawnPosition = startDirection * diamOfHex * numOfCurrentRing;
            for (int i = 1; i <= numOfCurrentRing; i++)
            {
                Instantiate(hexPref, spawnPosition, Quaternion.identity);
                spawnPosition += direction * diamOfHex;
            }
        }
        else
        {
            Vector3 spawnPosition = startDirection * diamOfHex * numOfCurrentRing;
            spawnPositionArray[counterOfSpawnPosition] = spawnPosition;

            for (int i = 1; i <= numOfCurrentRing; i++)
            {
                Instantiate(hexPref, spawnPosition, Quaternion.identity);
                spawnPosition += direction * diamOfHex;
                spawnPositionArray[counterOfSpawnPosition] = spawnPosition;
                counterOfSpawnPosition++;
            }
        }
    }

    private void Start()
    {
        spawnPositionArray = new Vector3[numOfRings * 6];
        CreateGrid();
        StartCoroutine(SpawnOfEnimies());
    }

    IEnumerator SpawnOfEnimies()
    {
        while (true)
        {
            enemySpawnPosition = spawnPositionArray[Random.Range(0, spawnPositionArray.Length)];
            enemySpawnPosition.y = 2;
            var newEnemy = Instantiate(enemyPref, enemySpawnPosition, Quaternion.identity);
            newEnemy.GetComponent<EnemyController>().spawnPosition = enemySpawnPosition;
            yield return new WaitForSeconds(enemySpavnWait);
        }
    }

    public void UpdateNumOfEnemy()
    {
        numOfEnemyOnLevel--;
        numOfEnemyText.text = numOfEnemyOnLevel.ToString();
    }

    void GameOver()
    {
        Time.timeScale = 0;
    }
}