using UnityEngine;
using UnityEngine.SceneManagement;

public class scoresUiManager : MonoBehaviour
{
    public void exitToMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
