using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timeText;           // 時間テキスト出力

    private float scoreTime;        // 時間計測
    public float Scoretime {
        get { return this.scoreTime; }
        private set { this.scoreTime = value; }
    }
    public bool timeFlag;             // 開始フラグ

    void Start()
    {
        scoreTime = 0;
        timeFlag = false;
        timeText.enabled = false;
    }

    void Update()
    {
        if (timeFlag)
        {
            scoreTime += Time.deltaTime;            // scoreTimeに時間加算
        }
    }

    public void TimeOut(float scoreTime)
    {
        int sec = (int)scoreTime % 60;              // 秒換算
        int msec = (int)(scoreTime * 1000 % 1000);  // ミリ秒換算
        string secText, msecText;                   // テキスト出力用

        secText = sec.ToString();

        if (msec < 10)
            msecText = "00" + msec.ToString();
        else if (msec < 100)
            msecText = "0" + msec.ToString();
        else
            msecText = msec.ToString();

        timeText.text = secText + "." + msecText;
        timeText.enabled = true;
    }
}
