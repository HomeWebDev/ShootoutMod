using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    private bool gameOver = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!pauseCanvas.activeInHierarchy)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void BackToMainMenu()
    {
        gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("HeroSelectionScene");
        AudioListener.pause = false;
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        Destroy(player1);
        GameObject progressController = GameObject.FindGameObjectWithTag("ProgressController");
        Destroy(progressController);

        GameObject introMusicPlayer = Instantiate(Resources.Load("Prefabs/Music/IntroSoundPlayer") as GameObject);
        DontDestroyOnLoad(introMusicPlayer);
    }

    public void ExitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
