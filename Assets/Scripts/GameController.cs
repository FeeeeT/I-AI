using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text centerText;         // 中央テキスト
    public Image centerBack;        // 中央テキスト背景
    private GameObject kuroobi;     // 黒帯
    private GameObject konoha;      // 木の葉
    public Text aizuText;           // 合図テキスト

    private float countTime;        // カウントダウン
    public float limitTime;         // 制限時間
    private float textCloseTime;    // 中央テキスト削除まで
    private bool start;             // 開始フラグ
    private bool aizu;              // 合図フラグ
    private bool seCheck;           // 合図の音フラグ
    private bool win;               // 勝利フラグ
    private Timer timeScript;       // タイマースクリプト

    private AudioSource bgm;        // 背景音
    private AudioSource[] soundEffect;// 効果音
    private AudioSource failed_se;  // 失敗音
    private AudioSource aizu_se;    // 合図音
    private AudioSource slash_se;   // 斬りつける音
    private AudioSource noutou_se;  // 納刀音

    public GameObject[] Enemys;     // 敵リスト
    private Animator shinobiDeath;  // 忍者の死亡アニメ

    private GameObject backGround;  // 背景

    private Animator mainCameraAnim;      // メインカメラアニメーション

    private ParticleSystem konohaAnim;



    void Start()
    {
        // カメラ取得
        mainCameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();

        // 難易度
        switch (StartButton.getDifficult())
        {
            case 0: // 初級
                limitTime = 0.5f;
                break;
            case 1: // 中級
                limitTime = 0.3f;
                break;
            case 2: // 上級
                limitTime = 0.19f;
                break;
            case 3: // ハイスコアモード
                limitTime = HighScore.highScore;
                break;
        }

        countTime = Random.Range(6.0f, 9.0f);   // 乱数
        textCloseTime = 1;
        start = false;
        aizu = true;
        win = false;
        centerText.enabled = true;
        centerText.text = "構え";
        centerBack.enabled = true;
        kuroobi = GameObject.Find("Kuroobi");
        kuroobi.SetActive(false);
        konoha = GameObject.Find("Konoha");
        konohaAnim = konoha.GetComponent<ParticleSystem>();
        aizuText.enabled = false;

        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        soundEffect = GameObject.Find("SE").GetComponents<AudioSource>();
        failed_se = soundEffect[0];
        aizu_se = soundEffect[2];
        aizu_se.time = 0.1f;
        slash_se = soundEffect[3];
        noutou_se = soundEffect[4];

        timeScript = gameObject.GetComponent<Timer>();

        shinobiDeath = Enemys[0].GetComponent(typeof(Animator)) as Animator;

        backGround = GameObject.Find("mori_hiru");

    }

    void Update()
    {
        // デバッグコマンド
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Title");
        }
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("Main");
        }

        // テキストを消すまでカウントダウン
        if (textCloseTime > 0)
        {
            textCloseTime -= Time.deltaTime;
            if (textCloseTime <= 0)
            {
                // テキストを消して、スタート
                centerText.enabled = false;
                centerBack.enabled = false;
                start = true;
            }
        }

        // startがtrueの場合
        if (start)
        {
            countTime -= Time.deltaTime;    // カウントダウンする
            if (countTime > 0)
            {
                // お手つき
                if (Input.anyKeyDown)
                {
                    StartCoroutine("Otetsuki");
                }
            }

            if (countTime <= 0 && win == false) // カウントダウンが終わった場合
            {
                if (aizu)
                {
                    Aizu();
                    timeScript.timeFlag = true;
                }
                // ボタンで止める
                if (Input.anyKeyDown)
                {
                    StartCoroutine("Success");
                }

                if (timeScript.Scoretime >= limitTime && win == false)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }

    IEnumerator Otetsuki()
    {
        start = false;
        failed_se.PlayOneShot(failed_se.clip);
        centerText.text = "失格";
        centerBack.enabled = true;
        centerText.enabled = true;
        Record.SaveFailedRecord();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Main");
    }

    private void Aizu()
    {
        if (!seCheck)
        {
            bgm.Stop();
            aizu_se.Play();
            aizuText.enabled = true;
            seCheck = true;
            // 木の葉停止
            konohaAnim.Pause();
        }
    }

    IEnumerator Success()
    {
        // カメラ演出
        mainCameraAnim.SetTrigger("cameraAction");

        win = true;
        aizuText.enabled = false;
        aizu = false;
        timeScript.timeFlag = false;
        kuroobi.SetActive(true);
        shinobiDeath.Play("ShinobiDeath");
        slash_se.Play();
        backGround.SetActive(false);
        timeScript.TimeOut(timeScript.Scoretime);
        konoha.SetActive(false);

        // 斬った回数のレコード記録
        Record.SaveKillRecord();
        
        // 難易度別レコード記録
        switch (StartButton.getDifficult())
        {
            case 0:
                Record.SaveSyokyuRecord();
                break;
            case 1:
                Record.SaveTyukyuRecord();
                break;
            case 2:
                Record.SaveJyokyuRecord();
                break;
            case 3:
                Record.SaveHighRecord();
                break;
        }

        // 0.1秒未満だった場合、レコード記録
        if(timeScript.Scoretime < 0.1f)
        {
            Record.SaveAwesomeRecord();
        }
        yield return new WaitForSeconds(0.8f);
        noutou_se.Play();
        if(HighScore.highScore > timeScript.Scoretime)
        {
            centerText.text = "新記録";
            centerText.enabled = true;
            centerBack.enabled = true;
            HighScore.SaveScore(timeScript.Scoretime);
        }
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Title");
    }
}
