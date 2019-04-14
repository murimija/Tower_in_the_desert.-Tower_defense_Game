using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    private const float diamOfHex = 6.4f;

    [Header("Parameters of hexagonal grid")] [SerializeField]
    private int numOfRings;

    [SerializeField] private GameObject hexPref;
    [SerializeField] private GameObject hexNoEffectPref;

    [Header("Enemy settings")] [SerializeField]
    private GameObject enemyPrefYellow;

    [SerializeField] private GameObject enemyPrefRed;
    [SerializeField] private GameObject enemyPrefBlue;
    [SerializeField] private GameObject enemySpawnEffect;
    [SerializeField] private float enemySpawnWait;
    [SerializeField] private float accelerationOfEnemySpawnWait;

    [Header("Level Settings")] 
    [SerializeField] private int numOfEnemyOnLevel = 20;
    [SerializeField] private Text numOfEnemyText;

    private readonly Vector3 north = new Vector3(0, 0, 1);
    private readonly Vector3 northEast = new Vector3(Mathf.Cos(Mathf.PI / 6) * 1, 0, Mathf.Sin(Mathf.PI / 6) * 1);
    private readonly Vector3 southEast = new Vector3(Mathf.Cos(Mathf.PI / 6) * 1, 0, -Mathf.Sin(Mathf.PI / 6) * 1);

    private int counterOfSpawnPosition;
    private Vector3[] spawnPositionArray;

    private Vector3 enemySpawnPosition;

    private void CreateGrid()
    {
        for (var i = 1; i <= numOfRings; i++)
        {
            buildRayOfHex(north, southEast, i);
            buildRayOfHex(northEast, -north, i);
            buildRayOfHex(southEast, -northEast, i);
            buildRayOfHex(-north, -southEast, i);
            buildRayOfHex(-northEast, north, i);
            buildRayOfHex(-southEast, northEast, i);
        }
    }

    private GameObject choseHex()
    {
        return Random.value < 0.7 ? hexPref : hexNoEffectPref;
    }

    private void buildRayOfHex(Vector3 startDirection, Vector3 direction, int numOfCurrentRing)
    {
        if (numOfCurrentRing != numOfRings)
        {
            var spawnPosition = startDirection * diamOfHex * numOfCurrentRing;
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            for (int i = 1; i <= numOfCurrentRing; i++)
            {
                Instantiate(choseHex(), spawnPosition, Quaternion.identity);
                spawnPosition += direction * diamOfHex;
            }
        }
        else
        {
            var spawnPosition = startDirection * diamOfHex * numOfCurrentRing;
            spawnPositionArray[counterOfSpawnPosition] = spawnPosition;

            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            for (int i = 1; i <= numOfCurrentRing; i++)
            {
                Instantiate(hexNoEffectPref, spawnPosition, Quaternion.identity);
                spawnPosition += direction * diamOfHex;
                spawnPositionArray[counterOfSpawnPosition] = spawnPosition;
                counterOfSpawnPosition++;
            }
        }
    }

    private void Start()
    {
        spawnPositionArray = new Vector3[numOfRings * 6];
        numOfEnemyText.text = numOfEnemyOnLevel.ToString();
        CreateGrid();
        StartCoroutine(SpawnOfEnemies());
    }

    private GameObject choiceOfEnemyType()
    {
        var sumOfProbably = enemyPrefYellow.GetComponent<EnemyController>().probabilityOfOccurrence +
                             enemyPrefRed.GetComponent<EnemyController>().probabilityOfOccurrence +
                             enemyPrefBlue.GetComponent<EnemyController>().probabilityOfOccurrence;
        var chose = Random.Range(0, sumOfProbably);
        if (chose < enemyPrefYellow.GetComponent<EnemyController>().probabilityOfOccurrence)
        {
            return enemyPrefYellow;
        }

        return chose > enemyPrefYellow.GetComponent<EnemyController>().probabilityOfOccurrence +
               enemyPrefRed.GetComponent<EnemyController>().probabilityOfOccurrence ? enemyPrefBlue : enemyPrefRed;
    }

    private IEnumerator SpawnOfEnemies()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            enemySpawnPosition = spawnPositionArray[Random.Range(0, spawnPositionArray.Length)];
            enemySpawnPosition.y = 2;
            var newEnemy = Instantiate(choiceOfEnemyType(), enemySpawnPosition, Quaternion.identity);
            newEnemy.GetComponent<EnemyController>().spawnPosition = enemySpawnPosition;
            var spawnedEffect = Instantiate(enemySpawnEffect, enemySpawnPosition + new Vector3(0, 1, 1),
                Quaternion.identity);
            Destroy(spawnedEffect, 2f);
            yield return new WaitForSeconds(enemySpawnWait);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    public void UpdateNumOfEnemy()
    {
        numOfEnemyOnLevel--;
        numOfEnemyText.text = numOfEnemyOnLevel.ToString();
        UpdateEnemySpawnWait();
        if (numOfEnemyOnLevel <= 0)
        {
            gameObject.GetComponent<SceneChanger>().GoToScene("Win_screen");
        }
    }

    private void UpdateEnemySpawnWait()
    {
        if (enemySpawnWait >= 1.5)
        {
            enemySpawnWait -= accelerationOfEnemySpawnWait;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        gameObject.GetComponent<SceneChanger>().GoToScene("GameOver_screen");
    }
}