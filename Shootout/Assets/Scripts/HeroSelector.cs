using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroSelector : MonoBehaviour {

    public List<GameObject> CharacterList;
    private GameObject currentCharacter;
    private int currentInt = 0;
    private List<GameObject> playerList;
    private float nextShift;
    private float shiftRate = 0.25f;

    // Use this for initialization
    void Start() {

        playerList = new List<GameObject>();

        for (int i = 0; i < CharacterList.Count; i++)
        {
            GameObject player = Instantiate(CharacterList[i], new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
            player.SetActive(false);
            player.GetComponent<HeroController>().enabled = false;
            playerList.Add(player);
        }

        playerList[currentInt].SetActive(true);

        SetCharacterText();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.RightArrow) && Time.time > nextShift)
        {
            nextShift = Time.time + shiftRate;
            ShiftPlayerPlus();
            SetNewPlayerActive();
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Time.time > nextShift)
        {
            nextShift = Time.time + shiftRate;
            ShiftPlayerMinus();
            SetNewPlayerActive();
        }

        if (Input.GetKey(KeyCode.Return) && Time.time > nextShift)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("IntroSoundPlayer"));

        GameObject player1 = playerList[currentInt];
        player1.GetComponent<HeroController>().enabled = true;
        DontDestroyOnLoad(player1);

        SceneManager.LoadScene("LoadLevelScene");

        //StartCoroutine(LoadAsyncronously());
    }

    IEnumerator LoadAsyncronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadLevelScene");

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }

    private void SetCharacterText()
    {
        Text text = GameObject.FindGameObjectWithTag("CharacterText").GetComponent<Text>();
        text.text = playerList[currentInt].gameObject.name.Substring(0, playerList[currentInt].gameObject.name.Length - 7);
    }

    private void SetNewPlayerActive()
    {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            playerList[i].SetActive(false);
        }

        playerList[currentInt].SetActive(true);
        SetCharacterText();
    }

    private void ShiftPlayerPlus()
    {
        if (currentInt < CharacterList.Count - 1)
        {
            currentInt++;
        }
        else
        {
            currentInt = 0;
        }
    }

    private void ShiftPlayerMinus()
    {
        if (currentInt > 0)
        {
            currentInt--;
        }
        else
        {
            currentInt = CharacterList.Count - 1;
        }
    }
}
