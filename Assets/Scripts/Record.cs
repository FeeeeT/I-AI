using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{

    public static int killRecord;   // 斬った回数
    private string killRecordKey = "KILL RECORD";

    public static int killedRecord; // 斬られた回数
    private string killedRecordKey = "KILLED RECORD";

    public static int failedRecord; // お手つき回数
    private string failedRecordKey = "FAILED RECORD";

    public static int syokyuRecord; // 初級クリア回数
    private string syokyuRecordKey = "SYOKYU RECORD";

    public static int tyukyuRecord; // 中級クリア回数
    private string tyukyuRecordKey = "TYUKYU RECORD";

    public static int jyokyuRecord; // 上級クリア回数
    private string jyokyuRecordKey = "JYOKYU RECORD";

    public static int highRecord;   // 記録級クリア回数
    private string highRecordKey = "HIGH MODE RECORD";

    public static int awesomeRecord;// 0.1秒未満を出した回数
    private string awesomeRecordKey = "AWESOME RECORD";

    private GameObject titleText;   // タイトルテキスト
    private GameObject difficults;  // 難易度ボタン
    private GameObject highScore;   // ハイスコアテキスト
    private GameObject resetButton; // リセットボタン

    private Text recordText;        // 記録表示用テキスト
    private GameObject recordButton;    // 記録表示用ボタン
    private GameObject backButton;      // 記録閉じる用ボタン

    void Start()
    {
        killRecord = PlayerPrefs.GetInt(killRecordKey, 0);
        killedRecord = PlayerPrefs.GetInt(killedRecordKey, 0);
        failedRecord = PlayerPrefs.GetInt(failedRecordKey, 0);
        syokyuRecord = PlayerPrefs.GetInt(syokyuRecordKey, 0);
        tyukyuRecord = PlayerPrefs.GetInt(tyukyuRecordKey, 0);
        jyokyuRecord = PlayerPrefs.GetInt(jyokyuRecordKey, 0);
        highRecord = PlayerPrefs.GetInt(highRecordKey, 0);
        awesomeRecord = PlayerPrefs.GetInt(awesomeRecordKey, 0);

        titleText = GameObject.Find("TitleText").GetComponent<Text>().gameObject;
        difficults = GameObject.Find("Difficults");
        highScore = GameObject.Find("HighScore").GetComponent<Text>().gameObject;
        resetButton = GameObject.Find("Reset");

        recordText = GameObject.Find("RecordText").GetComponent<Text>();
        recordButton = GameObject.Find("RecordButton").GetComponent<Button>().gameObject;
        backButton = GameObject.Find("BackButton").GetComponent<Button>().gameObject;
        backButton.SetActive(false);
        recordText.enabled = false;
    }

    public void RecordOut()
    {
        titleText.SetActive(false);
        difficults.SetActive(false);
        highScore.SetActive(false);
        resetButton.SetActive(false);
        recordButton.SetActive(false);

        recordText.text =
            "　　　　　 斬った回数：" + killRecord + "\n\n" +
            "　　　　 斬られた回数：" + killedRecord + "\n\n" +
            "　　　　 お手付き回数：" + failedRecord + "\n\n" +
            "　　　 　初級勝利回数：" + syokyuRecord + "\n\n" +
            " 　　　　中級勝利回数：" + tyukyuRecord + "\n\n" +
            " 　　　　上級勝利回数：" + jyokyuRecord + "\n\n" +
            " 　　　記録級勝利回数：" + highRecord + "\n\n" +
            "0.1秒未満で斬った回数：" + awesomeRecord;

        recordText.enabled = true;
        backButton.SetActive(true);
    }

    public void RecordClose()
    {
        recordText.enabled = false;
        backButton.SetActive(false);

        recordButton.SetActive(true);
        titleText.SetActive(true);
        difficults.SetActive(true);
        highScore.SetActive(true);
        resetButton.SetActive(true);
    }

    public static void SaveKillRecord()
    {
        Debug.Log("斬った回数を更新");
        PlayerPrefs.SetInt("KILL RECORD", ++killRecord);
        PlayerPrefs.Save();
    }

    public static void SaveKilledRecord()
    {
        Debug.Log("斬られた回数を更新");
        PlayerPrefs.SetInt("KILLED RECORD", ++killedRecord);
        PlayerPrefs.Save();
    }

    public static void SaveFailedRecord()
    {
        Debug.Log("お手つき回数を更新");
        PlayerPrefs.SetInt("FAILED RECORD", ++failedRecord);
        PlayerPrefs.Save();
    }

    public static void SaveSyokyuRecord()
    {
        Debug.Log("初級クリア回数を更新");
        PlayerPrefs.SetInt("SYOKYU RECORD", ++syokyuRecord);
        PlayerPrefs.Save();
    }

    public static void SaveTyukyuRecord()
    {
        Debug.Log("中級クリア回数を更新");
        PlayerPrefs.SetInt("TYUKYU RECORD", ++tyukyuRecord);
        PlayerPrefs.Save();
    }

    public static void SaveJyokyuRecord()
    {
        Debug.Log("上級クリア回数を更新");
        PlayerPrefs.SetInt("JYOKYU RECORD", ++jyokyuRecord);
        PlayerPrefs.Save();
    }

    public static void SaveHighRecord()
    {
        Debug.Log("記録級クリア回数を更新");
        PlayerPrefs.SetInt("HIGH MODE RECORD", ++highRecord);
        PlayerPrefs.Save();
    }

    public static void SaveAwesomeRecord()
    {
        Debug.Log("0.1秒未満の回数を更新");
        PlayerPrefs.SetInt("AWESOME RECORD", ++awesomeRecord);
        PlayerPrefs.Save();
    }
}
