using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameObject selectButtons;
    // Start is called before the first frame update
    void Start()
    {
        Record.SaveKilledRecord();
        selectButtons = GameObject.Find("SelectButton");
        selectButtons.SetActive(false);
        StartCoroutine("SelectMenu");
    }

    private IEnumerator SelectMenu()
    {
        yield return new WaitForSeconds(1.0f);
        selectButtons.SetActive(true);
    }

    public void Revenge()
    {
        SceneManager.LoadScene("Main");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Title");
    }
}
