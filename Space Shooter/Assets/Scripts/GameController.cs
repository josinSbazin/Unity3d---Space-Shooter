using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text gameOverText;
    public GameObject restartButtom;

    private bool gameOver;
    private bool restart;

    private int score;

    private void Start()
    {
        gameOver = false;
        restart = false;
        //        restartText.text = "";
        gameOverText.text = "";
        restartButtom.SetActive(false);
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

//    private void Update()
//    {
//        if (restart)
//        {
//            if (Input.GetKeyDown(KeyCode.R))
//            {
//                Application.LoadLevel(Application.loadedLevel);
//            }
//        }
//    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true) 
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                //                restartText.text = "Press 'R' for Restart";
                restartButtom.SetActive(true);
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
	
}
