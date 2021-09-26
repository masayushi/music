using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // AudioSourse 調用音樂時會用的類別
    [Header("音樂")]
    public AudioSource theMusic;

    [Header("開始遊戲")]
    public bool startPlaying;

    // *額外補充 - Scrollbar 主要用在上下捲動螢幕時會用到 ex:在某任務劇情的提示上因為文本太大量，所以會需要用到這個做捲動整理
    // BeatScroller [不確定是甚麼] 這裡會用到的原因是因為節拍會讓音符落在螢幕上
    public BeatScroller theBS;

    public static GameManager instance;

    [Header("當前分數")]
    public int currentScore;

    [Header("獲得分數")]
    public int scorePerNote = 100;

    public int scorePerGootNote = 125;

    public int scorePerPerfectNote = 150;

    // 文字文本：隨著獲得分數的增加，文本會自動增加設定的值

    [Header("當前乘數")]
    public int currentMultiplier;

    [Header("累加乘數")]
    public int multiplierTracker;

    [Header("乘數的底線")]
    public int[] multiplierThresholds;

    // 分數的文字
    public Text scoreText;

    // 乘數的文字
    public Text multiText;

    [Header("箭頭總打擊數")]
    public float totalNotes;

    [Header("normal的打擊數")]
    public float normalHits;

    [Header("good的打擊數")]
    public float goodHits;

    [Header("perfect的打擊數")]
    public float perfectHits;

    [Header("miss的數量")]
    public float missedHits;

    public GameObject resultsScreen;

    public Text percentHitText, normalsHitText, goodsHitText, perfectsHitText, missesHitText, rankText, finalScoreText;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";

        currentMultiplier = 1;

        // FindObjectsOfType<> 可以透過這個程式碼來抓Unity裡的目標類型物件

        // 影片裡用到這則程式碼主要是要用來抓箭頭的總數量總共有多少
        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        #region 啟動遊戲 & 遊戲開始時音樂撥放

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
        else
        {
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsHitText.text = "" + normalHits;
                goodsHitText.text = goodHits.ToString();
                percentHitText.text = perfectHits.ToString();
                missesHitText.text = "" + missedHits;

                float totalHits = normalHits + goodHits + perfectHits;
                float percentHit = (totalHits / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 85)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }
                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }
        #endregion
    }

    #region 箭頭下移時，是否有在正確的時間、正確的點上觸發按壓
    // 觸發
    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        // 這邊主要是為了防止 乘數 明明沒有小於4 卻還是會乘與 4、8、12的Bug

        // 如果 當前乘數 - 1 是小於 乘數陣列的最小值
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {

            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        // currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        // 要用來計算有幾個normal，由於是從0開始遞增所以會用++，下面的++也都是這個意思
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGootNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
    }


    // 沒有觸發
    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;


        missedHits++;
    }

    #endregion
}
