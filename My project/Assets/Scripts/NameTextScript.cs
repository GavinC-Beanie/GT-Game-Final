using UnityEngine;
using UnityEngine.SceneManagement;

public class NameTextScript : MonoBehaviour
{
    public void StartGame()
    {
        // Load your game scene - change "GameScene" to your actual scene name
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        Debug.Log("Game Quit");
    }
}