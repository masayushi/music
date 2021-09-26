using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [Header("箭頭落下的速度")]
    public float beatTempo;

    [Header("開始遊戲")]
    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        #region 設定箭頭落下的速度
        // 箭頭落下的速度 = 箭頭落下的速度除以60幀
        // 速度之所以設定120的原因就是因為一幀會以60的速度下去跑，這樣的結果就是變成落下的速度會約3秒
        beatTempo = beatTempo / 60f;
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        #region 遊戲開始後，按隨意按鍵可以開始遊戲 & 使箭頭落下
        // 如果沒有開始遊戲
        if (!hasStarted)
        {   // 如果按下了任何按鍵的時候
            /* if (Input.anyKeyDown)
             {
                 // 開始遊戲 會被啟動
                 hasStarted = true;
             }
            */
        }
        // 另外 (在遊戲啟動過後)
        else
        {   // 這份C#檔案的受體(箭頭)位置 會隨著時間遞減(只有Y軸的部分下落)
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
        #endregion
    
    }

}
