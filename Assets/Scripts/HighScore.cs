using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static Text highScoreText;
    public static float highScore;
    private string key = "HIGH SCORE";

    void Start()
    {
        // 保存しておいたハイスコアをキーで呼び出し取得し保存されていなければ0になる
        highScore = PlayerPrefs.GetFloat(key, 99f);
        Debug.Log("highScore:" + highScore);
        highScoreText = gameObject.GetComponent<Text>();

        HighScoreOut();
    }

    // ハイスコア表示
    public static void HighScoreOut()
    {
        if (highScore != 99f)
        {
            int sec = (int)highScore % 60;              // 秒換算
            int msec = (int)(highScore * 1000 % 1000);  // ミリ秒換算
            string secText, msecText;                   // テキスト出力用

            secText = sec.ToString();

            if (msec < 10)
                msecText = "00" + msec.ToString();
            else if (msec < 100)
                msecText = "0" + msec.ToString();
            else
                msecText = msec.ToString();

            highScoreText.text = "最高記録\n" + secText + "." + msecText + "秒";
            highScoreText.enabled = true;
        }

        // 記録がない場合、非表示
        if (highScore == 99f)
        {
            highScoreText.enabled = false;
        }
    }

    public static void SaveScore(float scoreTime)
    {
        Debug.Log("ハイスコア更新");
        highScore = scoreTime;
        PlayerPrefs.SetFloat("HIGH SCORE", scoreTime);
        PlayerPrefs.Save();
        Debug.Log("highScore:" + highScore);
    }
}
