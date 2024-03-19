using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class timerChecker : MonoBehaviour
{
    public Slider timerSlider;
    public TMP_Text timerText;
    public float gameTime;
    private bool stopTimer = false;

    private int trees = 0;
    public TMP_Text score;
    public GameObject treeSpawner;

    public GameObject wealthBoi;

    private int currWealth = 0;
    private int currCO2 = 0;

    public Slider wealthSlider;
    public TMP_Text wealthText;

    public int maxWealth = 1000000;


    public Image image; // Assign the UI Image component in the Inspector
    public TMP_Text text;   // Assign the UI Text component in the Inspector

    public float startTime;


    public int maxCO2 = 1000000;

    private string perfectText = "Perfect Condition";
    private string goodText = "Good Condition";
    private string normalText = "Normal Condition";
    private string badText = "Bad Condition";
    private string criticalText = "Critical Condition";

    float randomEventTime;

    void Start()
    {
        startTime = Time.time;
        randomEventTime = Random.Range(25.0F, 65.0F) + (Time.time - startTime);

        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;

        wealthSlider.maxValue = maxWealth;
        timerSlider.value = currWealth;
    }

    void Update()
    {

        UpdateSlider();
        score.text = trees.ToString() + " trees";

        UpdateWealthSlider();

        conditonChecker();

        if (randomEventTime <= (Time.time - startTime))
        {
            FindObjectOfType<randomEventHandeler>().startEvent();
            randomEventTime = Random.Range(25.0F, 65.0F) + (Time.time - startTime);
        }

    }

    void UpdateSlider()
    {
        float time = gameTime - (Time.time - startTime);

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);

        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (time <= 0)
        {
            stopTimer = true;
            FindObjectOfType<loadScoresManager>().loadScoreboard("outOfTime");
        }

        if (stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = time;

            // Calculate color interpolation between green and red based on timer progress
            float normalizedTime = Mathf.Clamp01(time / gameTime);
            Color color = Color.Lerp(Color.green, Color.red, 1f - normalizedTime); // Invert normalized time for color interpolation
            timerSlider.fillRect.GetComponent<Image>().color = color;
        }
    }

    void UpdateWealthSlider()
    {
        wealthSlider.value = currWealth;
        wealthText.text = currWealth.ToString() + " $";
    }

    public void buttonPressed()
    {
        trees++;
        treeSpawner.GetComponent<treeSpawner>().SpawnPrefab();
    }

    public int returnTrees()
    {
        currWealth = wealthBoi.GetComponent<wealthManager>().returnWealth();
        currCO2 = wealthBoi.GetComponent<wealthManager>().returnCO2();
        return trees;
    }

    public float returnTime()
    {
        return(Time.time - startTime);
    }

    public void minimizeTrees(int a)
    {
        trees = trees - a;
    }

    public void conditonChecker()
    {
        if (currCO2 <= maxCO2 * 0.2)
        {
            text.text = perfectText;
            image.color = new Color(0.5235849f, 0.9128973f, 1f);
        }
        else if (currCO2 <= maxCO2 * 0.4)
        {
            text.text = goodText;
            image.color = new Color(0.5254902f, 1f, 0.5660538f);
        }
        else if (currCO2 <= maxCO2 * 0.6)
        {
            text.text = normalText;
            image.color = new Color(1f, 1f, 1f);
        }
        else if (currCO2 <= maxCO2 * 0.8)
        {
            text.text = badText;
            image.color = new Color(1f, 0.9186509f, 0f);
        }
        else
        {
            text.text = criticalText;
            image.color = new Color(0.7830189f, 0f, 0.07547203f);
        }
    }
}
