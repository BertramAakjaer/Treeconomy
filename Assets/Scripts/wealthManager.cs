using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class wealthManager : MonoBehaviour
{
    public Button[] buttons;
    public upgrades[] thresholds;

    public GameObject treesScript;

    private int wealth = 0;
    private int trees;
    private int CO2Emission = 0;

    private float currTime = 0f;

    public TMP_Text wealthPrSecondText;

    public TMP_Text Co2PrSecondText;



    void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            thresholds[i].header.text = thresholds[i].name + " (" + thresholds[i].price + " trees)";
            thresholds[i].description.text = "Generates " + thresholds[i].wealthPrSecond + " $ each second, but produces " + thresholds[i].co2Impact + " kg CO2 each second as well.";
            thresholds[i].countMany.text = "You have " + thresholds[i].upgradeCount;
        }
        wealthPrSecondText.text = "0 $ pr second";
        Co2PrSecondText.text = "0 kg of carbon emission";
    }

    void Update()
    {
        trees = treesScript.GetComponent<timerChecker>().returnTrees();
        checkButtons();

        if ((Time.time - currTime) >= 1)
        {
            int tempWealthPrSecond = 0;
            int tempCo2PrSecond = 0;

            currTime = Time.time;
            for (int i = 0; i < thresholds.Length; i++)
            {
                wealth += thresholds[i].wealthPrSecond * thresholds[i].upgradeCount;
                tempWealthPrSecond += thresholds[i].wealthPrSecond * thresholds[i].upgradeCount;

                CO2Emission += thresholds[i].co2Impact * thresholds[i].upgradeCount;
                tempCo2PrSecond += thresholds[i].co2Impact * thresholds[i].upgradeCount;
            }
            wealthPrSecondText.text = tempWealthPrSecond + " $ pr second";
            Co2PrSecondText.text = tempCo2PrSecond + " kg of carbon emission";
        }

        if (wealth >= Math.Pow(10, 6))
        {
            FindObjectOfType<loadScoresManager>().loadScoreboard("wonWealth");
        }

        if (CO2Emission >= Math.Pow(10, 6))
        {
            FindObjectOfType<loadScoresManager>().loadScoreboard("lostCO2");
        }
    }

    void checkButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (trees >= thresholds[i].price)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void buttonPressed(int a)
    {
        trees = treesScript.GetComponent<timerChecker>().returnTrees();
        if (trees >= thresholds[a].price)
        {
            thresholds[a].upgradeCount += 1;
            thresholds[a].countMany.text = "You have " + thresholds[a].upgradeCount;
            treesScript.GetComponent<timerChecker>().minimizeTrees(thresholds[a].price);
        }
    }

    public int returnWealth()
    {
        return wealth;
    }

    public int returnCO2()
    {
        return CO2Emission;
    }

    public int returnUpgrades()
    {
        int temp = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            temp += thresholds[i].upgradeCount;
        }
        return temp;
    }

    public void incrementCoal (int a)
    {
        thresholds[0].upgradeCount += a;
        thresholds[0].countMany.text = "You have " + thresholds[0].upgradeCount;
    }
}
