using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    private void Update()
    {
        timer += Time.deltaTime;
        txtTimer.text = "Time: " + timer.ToString("n2");
    }

}
