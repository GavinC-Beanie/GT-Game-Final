using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject obj;
    public void PlayGame()
    {
        // awdSceneManager.LoadScene(1);
        obj.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}