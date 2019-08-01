using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScore : MonoBehaviour
{
    private GameObject resetAlart;
    private Button resetButton;

    void Start()
    {
        resetAlart = GameObject.Find("ResetAlart");
        resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        resetAlart.SetActive(false);
    }

    public void OpenResetAlart()
    {
        resetAlart.SetActive(true);
        resetButton.gameObject.SetActive(false);
        HighScore.highScoreText.enabled = false;
    }

    public void ResetAgree()
    {
        HighScore.SaveScore(99f);
        HighScore.HighScoreOut();
        resetAlart.SetActive(false);
        resetButton.gameObject.SetActive(true);
    }

    public void ResetCancel()
    {
        resetAlart.SetActive(false);
        resetButton.gameObject.SetActive(true);
        if (HighScore.highScore != 99f)
            HighScore.highScoreText.enabled = true;
    }
}
