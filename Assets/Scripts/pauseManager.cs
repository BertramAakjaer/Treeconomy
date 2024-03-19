using UnityEngine.SceneManagement;
using UnityEngine;

public class pauseManager : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;
    public GameObject startMenuUi;

    bool hasBeenTriggered = false;

    public static pauseManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) 
            { 
                Resume();
            }
            else
            {
                Pause();
            }
        }

        Scene currScene = SceneManager.GetActiveScene();

        if (currScene.name == "GameScene" && !hasBeenTriggered)
        {
            hasBeenTriggered = true;
            StartMenuShow();
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        startMenuUi.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void StartMenuShow()
    {
        startMenuUi.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void exitToMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
