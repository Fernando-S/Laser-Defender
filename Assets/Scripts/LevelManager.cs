using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float playerExplosionInSeconds;
    [SerializeField] AudioClip startSound = null;


    // Loads Menu Scene
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }


    // Loads Game Scene
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }


    // Loads Game Over Scene
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(playerExplosionInSeconds);
        SceneManager.LoadScene("Game Over");
    }


    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
