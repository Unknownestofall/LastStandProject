using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] GameObject[] enemyToSpawn;//random
    [SerializeField] Transform[] enemySpawnPos;
    [SerializeField] Transform container;

    [SerializeField] float timeBetweenEnemySpawn,timeBetweenWaves;

    [SerializeField] int numbOfEnemiesOnWave,curEnemiesSpawned;
    [SerializeField] int curWave, enemiesKilled;

    uiManager ui;
    GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").GetComponent<uiManager>();
        _gm = GameObject.Find("GM").GetComponent<GameManager>();
        StartCoroutine(StartSpawningEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        StartNextWave();
        if(curWave == 21) {
            _gm.isVictory();
        }
    }
    IEnumerator StartSpawningEnemies() {
        while (!isWaveOver()) { 
            yield return new WaitForSeconds(timeBetweenWaves);
            int randEnemy = Random.Range(0, enemyToSpawn.Length);
            int randPosSpawn = Random.Range(0, enemySpawnPos.Length);
            GameObject enemy = Instantiate(enemyToSpawn[randEnemy], enemySpawnPos[randPosSpawn].position, Quaternion.identity);
            enemy.transform.parent = container;
            curEnemiesSpawned++;
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
    }
    bool isWaveOver() { 
        if(curEnemiesSpawned < numbOfEnemiesOnWave) {
            Debug.Log("enemies comin");
            return false;
        }
        Debug.Log("enemies stoped");
        return true;
    }
    bool canStartWave() { 
        if(enemiesKilled >= numbOfEnemiesOnWave) { 
            return true;
        }return false;
    }
    void StartNextWave() {
        if (canStartWave()) {
            enemiesKilled = 0;
            curEnemiesSpawned = 0;
            curWave++;
            updateEnemiesAfterWave();
            StartCoroutine(StartSpawningEnemies());
        }
    }
    void updateEnemiesAfterWave() {    
        switch (curWave) {
            case 0:
                numbOfEnemiesOnWave = 15;
                ui.updateWave(curWave);
                break;
            case 21:
                numbOfEnemiesOnWave = 0;
                break;
            default:
                numbOfEnemiesOnWave += 10;
                ui.updateWave(curWave);
                break;
        }
    }
    public void EnemyKilled() => enemiesKilled++;
}
