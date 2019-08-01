using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private AudioSource cursor;
    public static int difficult;
    public static int getDifficult() {
        return difficult;
    }

    void Start()
    {
        cursor = GameObject.Find("SE").GetComponent<AudioSource>();
    }

    public void Syokyu()
    {
        cursor.Play();
        difficult = 0;
        StartCoroutine("GameLoad");
    }    

    public void Tyukyu()
    {
        cursor.Play();
        difficult = 1;
        StartCoroutine("GameLoad");
    }

    public void Jyokyu()
    {
        cursor.Play();
        difficult = 2;
        StartCoroutine("GameLoad");
    }

    public void HighMode()
    {
        cursor.Play();
        difficult = 3;
        StartCoroutine("GameLoad");
    }

    IEnumerator GameLoad()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Main");
    }
}
