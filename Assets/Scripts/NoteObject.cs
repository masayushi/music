using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    // 影片中主要是想在下落時的偵測區域做偵測，讓方塊偵測箭頭
    [Header("能夠偵測的區域")]
    public bool canBePressed;

    // 這邊的目的是要讓玩家按指定的方向後，讓箭頭消除
    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        #region 當 判定區 偵測到玩家按壓時 箭頭會消失
        // 如果按下指定按鍵
        if (Input.GetKeyDown(keyToPress))
        {
            // 且如果 能夠偵測的區域做動
            if (canBePressed)
            {
                // 遊戲物件的顯示狀態會為否
                gameObject.SetActive(false);

                // GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("Hit");

                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good Hit");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }
        }
        #endregion


    }

    // 這邊影片作者是透過Box Collider來偵測的作法

    #region 下面的方塊(判定區) 是否有偵測到下移的箭頭
    // OnTriggerEnter2D A物件進入.碰觸到B物件的觸發點
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果 這個other物件的tag 偵測到是 Activator(箭頭的Tag)的話
        if (other.tag == "Activator")
        {
            // 箭頭就可以被按壓
            canBePressed = true;
        }
    }

    // OnTriggerExit2D A物件離開B物件的觸發點
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;

                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
    #endregion
}
