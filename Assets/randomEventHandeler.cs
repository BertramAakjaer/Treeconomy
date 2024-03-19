using TMPro;
using UnityEngine;

public class randomEventHandeler : MonoBehaviour
{
    public GameObject resumePanel;

    public TMP_Text eventText;

    public static bool GameIsPaused = false;

    public void resumeGame ()
    {
        resumePanel.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void startEvent()
    {
        int a = Random.Range(3, 9);
        eventText.text = "EV1L CO. has generously donated " + a + " coal factories";

        FindObjectOfType<wealthManager>().incrementCoal(a);

        resumePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


}
