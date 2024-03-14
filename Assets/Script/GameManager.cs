using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text txtTotalEnemiesKilled;
    public int totalKills;
    public GameObject enemyContainer;

    float timer;
    public Text txtTimer;

    void Start()
    {
        instance = this;
        totalKills = enemyContainer.GetComponentsInChildren<enemyController>().Length;
        txtTotalEnemiesKilled.text = "Total Enemies: " + totalKills.ToString();
        timer = 0.0f;
        txtTimer.text = "Time: " + timer.ToString("n2");
    }
    public void AddEnemyKilled()
    {
        totalKills--;
        txtTotalEnemiesKilled.text = "Total Enemies: " + totalKills.ToString();

        if(totalKills<=0)
        {
            FinGame(true);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        txtTimer.text = "Time: " + timer.ToString("n2");

        if(Input.GetKeyDown(KeyCode.Return))
        {
            StaticValues.winner=-1;
            SceneManager.LoadScene(0);
        }

        
    }

    public void FinGame(bool isWin)
    {
        if(isWin == true)
        {
            Debug.Log("HAS GANADO");
            StaticValues.winner=1;
            if(PlayerPrefs.HasKey("record")==true)
            {
                float t = PlayerPrefs.GetFloat("record");
                if(timer < t)
                {
                    PlayerPrefs.SetFloat("record", timer);
                }
            }
            else
            {
                PlayerPrefs.SetFloat("record", timer);
            }
            PlayerPrefs.Save();
        }
        else 
        {
            Debug.Log("HAS PERDIDO");
            StaticValues.winner=0;
        }

        SceneManager.LoadScene(0);
    }

}
