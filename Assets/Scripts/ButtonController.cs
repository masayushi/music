using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // 圖片渲染器
    private SpriteRenderer theSR;

    // 預設圖片樣式
    public Sprite defaultImage;

    // 按壓時的圖片樣式
    public Sprite pressedImage;

    // 按壓的按鍵
    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        #region 讓the SR抓 圖片渲染器的設定
        theSR = GetComponent<SpriteRenderer>();
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        #region 設定 判定區按壓與不按壓時的圖片變換
        // 如果 按下指定按鍵時
        if (Input.GetKeyDown(keyToPress))
        {
            // 圖片渲染器的狀態 會等於 按壓時的圖片
            theSR.sprite = pressedImage;
        }

        // 按壓過後要確認方才的按鍵，所以要再加下面這行程式

        // 如果 按壓的指定按鍵按鍵彈回時
        if (Input.GetKeyUp(keyToPress))
        {
            // 圖片渲染器的狀態 會等於 預設的圖片
            theSR.sprite = defaultImage;
        }
        #endregion

    }
}
