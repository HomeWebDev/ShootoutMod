using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class used to handle result scene
/// </summary>
public class ResultController : MonoBehaviour {

	/// <summary>
    /// Update score texts on instantialization
    /// </summary>
	void Start () {
        UpdateScoreTexts();
    }

    /// <summary>
    /// Update score texts
    /// </summary>
    private void UpdateScoreTexts()
    {
        //Create dictionary to sort players results
        Dictionary<int, int> dict = new Dictionary<int, int>();

        //Update texts if 2 players
        if (GameControllerOld.nrOfPlayers == 2)
        {
            dict.Add(1, GameControllerOld.Player1Kills);
            dict.Add(2, GameControllerOld.Player2Kills);

            var sortedDict = from entry in dict orderby entry.Value descending select entry;

            GameObject firstPlayerTextGameObject = GameObject.Find("FirstPlayerText");
            UnityEngine.UI.Text firstPlayerText = firstPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            firstPlayerText.text = "Rank 1: Player " + sortedDict.ElementAt(0).Key + " with " + sortedDict.ElementAt(0).Value + " kills";

            GameObject secondPlayerTextGameObject = GameObject.Find("SecondPlayerText");
            UnityEngine.UI.Text secondPlayerText = secondPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            secondPlayerText.text = "Rank 2: Player " + sortedDict.ElementAt(1).Key + " with " + sortedDict.ElementAt(1).Value + " kills";

            GameObject thirdPlayerTextGameObject = GameObject.Find("ThirdPlayerText");
            UnityEngine.UI.Text thirdPlayerText = thirdPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            thirdPlayerText.text = "";

            GameObject fourthPlayerTextGameObject = GameObject.Find("FourthPlayerText");
            UnityEngine.UI.Text fourthPlayerText = fourthPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            fourthPlayerText.text = "";
        }

        //Update texts if 3 players
        if (GameControllerOld.nrOfPlayers == 3)
        {
            dict.Add(1, GameControllerOld.Player1Kills);
            dict.Add(2, GameControllerOld.Player2Kills);
            dict.Add(3, GameControllerOld.Player3Kills);

            var sortedDict = from entry in dict orderby entry.Value descending select entry;

            GameObject firstPlayerTextGameObject = GameObject.Find("FirstPlayerText");
            UnityEngine.UI.Text firstPlayerText = firstPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            firstPlayerText.text = "Rank 1: Player " + sortedDict.ElementAt(0).Key + " with " + sortedDict.ElementAt(0).Value + " kills";

            GameObject secondPlayerTextGameObject = GameObject.Find("SecondPlayerText");
            UnityEngine.UI.Text secondPlayerText = secondPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            secondPlayerText.text = "Rank 2: Player " + sortedDict.ElementAt(1).Key + " with " + sortedDict.ElementAt(1).Value + " kills";

            GameObject thirdPlayerTextGameObject = GameObject.Find("ThirdPlayerText");
            UnityEngine.UI.Text thirdPlayerText = thirdPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            thirdPlayerText.text = "Rank 3: Player " + sortedDict.ElementAt(2).Key + " with " + sortedDict.ElementAt(2).Value + " kills";

            GameObject fourthPlayerTextGameObject = GameObject.Find("FourthPlayerText");
            UnityEngine.UI.Text fourthPlayerText = fourthPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            fourthPlayerText.text = "";
        }

        //Update texts if 4 players
        if (GameControllerOld.nrOfPlayers == 4)
        {
            dict.Add(1, GameControllerOld.Player1Kills);
            dict.Add(2, GameControllerOld.Player2Kills);
            dict.Add(3, GameControllerOld.Player3Kills);
            dict.Add(4, GameControllerOld.Player4Kills);

            var sortedDict = from entry in dict orderby entry.Value descending select entry;

            GameObject firstPlayerTextGameObject = GameObject.Find("FirstPlayerText");
            UnityEngine.UI.Text firstPlayerText = firstPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            firstPlayerText.text = "Rank 1: Player " + sortedDict.ElementAt(0).Key + " with " + sortedDict.ElementAt(0).Value + " kills";

            GameObject secondPlayerTextGameObject = GameObject.Find("SecondPlayerText");
            UnityEngine.UI.Text secondPlayerText = secondPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            secondPlayerText.text = "Rank 2: Player " + sortedDict.ElementAt(1).Key + " with " + sortedDict.ElementAt(1).Value + " kills";

            GameObject thirdPlayerTextGameObject = GameObject.Find("ThirdPlayerText");
            UnityEngine.UI.Text thirdPlayerText = thirdPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            thirdPlayerText.text = "Rank 3: Player " + sortedDict.ElementAt(2).Key + " with " + sortedDict.ElementAt(2).Value + " kills";

            GameObject fourthPlayerTextGameObject = GameObject.Find("FourthPlayerText");
            UnityEngine.UI.Text fourthPlayerText = fourthPlayerTextGameObject.GetComponent<UnityEngine.UI.Text>();
            fourthPlayerText.text = "Rank 4: Player " + sortedDict.ElementAt(3).Key + " with " + sortedDict.ElementAt(3).Value + " kills";
        }
    }
}
