using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScoresManager : MonoBehaviour
{
    int trees;
    int wealth;

    int totalUpgrades;
    double co2Desctruction;

    float currTime;

    string changeCondition;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void loadScoreboard(string tempCondition)
    {
        changeCondition = tempCondition;


        trees = FindObjectOfType<timerChecker>().returnTrees();

        wealth = FindObjectOfType<wealthManager>().returnWealth();
        co2Desctruction = Math.Clamp(FindObjectOfType<wealthManager>().returnCO2() / Math.Pow(10, 6) * 100, 0, 100);

        totalUpgrades = FindObjectOfType<wealthManager>().returnUpgrades();

        currTime = FindObjectOfType<timerChecker>().returnTime();

        SceneManager.LoadScene("ScoreView");
        
        
    }

    public int returnTrees()
    {
        return trees;
    }

    public int returnWealth()
    {
        return wealth;
    }

    public int returnUpgrades()
    {
        return totalUpgrades;
    }

    public string returnCondition()
    {
        return changeCondition;
    }

    public double returnEmission()
    {
        return co2Desctruction;
    }

    public void destroyManager()
    {
        Destroy(gameObject);
    }

    public float returnTime()
    {
        return currTime;
    }
}
