using UnityEngine;
using TMPro;
using System;

public class showText : MonoBehaviour
{
    private loadScoresManager loadManager;

    int trees, totalUpgrades, wealth;
    double co2Desctruction;
    float endTime;
    string changeCondition;

    public TMP_Text messageText;
    public TMP_Text highscoreText;

    public TMP_Text timeUsed;

    public TMP_Text treesText;
    public TMP_Text wealthText;
    public TMP_Text environmentText;
    public TMP_Text upgradesText;



    void Awake()
    {
        loadManager = FindObjectOfType<loadScoresManager>();

        if (loadManager == null)
        {
            Debug.LogError("Could not find loadScoresManager script in the scene!");
            return;
        }

        initializeValues();

        insertText();
    }

    void insertText()
    {
        if (changeCondition == "outOfTime")
        {
            messageText.text = "Too bad! You ran out of time, better luck next time :'(";
        }
        else if(changeCondition == "wonWealth")
        {
            messageText.text = "Congratulations! You earned 1 000 000 $ without killing the planet, remember to check your highscore!";
        } 
        else if (changeCondition == "lostCO2")
        {
            messageText.text = "Too bad! You killed the planet via CO2 emission, better luck next time :'(";
        }

        string textTime = calculateTimeToString();

        if (hasHighScore())
        {
            if (endTime < getHighScoreFloat() && changeCondition == "wonWealth")
            {
                saveHighScore(textTime, endTime);
                highscoreText.text = "All time highscore: " + textTime;
            }
            else
            {
                highscoreText.text = "All time highscore: " + getHighScoreText();
            }
        }
        else if (changeCondition == "wonWealth") 
        {
            saveHighScore(textTime, endTime);
            highscoreText.text = "All time highscore: " + textTime;
        }
        else
        {
            highscoreText.text = "There is currently no saved or achieved highscore :'(";
        }

        timeUsed.text = "(You used " + textTime + ")";

        treesText.text = trees + " trees";

        wealthText.text = wealth + " $";

        environmentText.text = co2Desctruction + "% of the environment";

        upgradesText.text = totalUpgrades + " upgrades";

    }

    string calculateTimeToString()
    {
        int minutes = Mathf.FloorToInt(endTime / 60);
        int seconds = Mathf.FloorToInt(endTime - minutes * 60f);

        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    bool hasHighScore()
    {
        return PlayerPrefs.HasKey("HighScoreFloat");
    }

    void saveHighScore(string formatString, float time)
    {
        PlayerPrefs.SetFloat("HighScoreFloat", time);
        PlayerPrefs.SetString("HighScoreText", formatString);

        PlayerPrefs.Save();
    }

    string getHighScoreText()  
    {
        return PlayerPrefs.GetString("HighScoreText"); 
    }
    float getHighScoreFloat() 
    {
        return PlayerPrefs.GetFloat("HighScoreFloat"); 
    }

    void initializeValues()
    {
        changeCondition = loadManager.returnCondition();
        wealth = loadManager.returnWealth();
        trees = loadManager.returnTrees();
        totalUpgrades = loadManager.returnUpgrades();
        co2Desctruction = loadManager.returnEmission();
        endTime = loadManager.returnTime();

        loadManager.destroyManager();
    }
}
