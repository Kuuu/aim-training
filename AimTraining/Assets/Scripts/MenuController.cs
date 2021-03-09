using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Text bestScore;
    public Text lastScore;

    // Start is called before the first frame update
    void Start()
    {
        bestScore.text = "" + PlayerPrefs.GetInt("bestScore");
        lastScore.text = "" + PlayerPrefs.GetInt("lastScore");
    }

    // Update is called once per frame
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
