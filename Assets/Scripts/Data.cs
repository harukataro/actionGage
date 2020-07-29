using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{

    // メンバ変数宣言
    public float BestTime_01; // ステージ1最速クリアタイム
    public float BestTime_02; // ステージ1最速クリアタイム
    public float BestTime_03; // ステージ1最速クリアタイム
    static public Data instance;

    // 起動時に１回だけ呼び出されるメソッド
    void Start()
    {
        // オブジェクトに「シーン切り替え時に破棄されない」設定を付与
        DontDestroyOnLoad(gameObject);

        // 初期化処理
        BestTime_01 = 99.99f;
        BestTime_02 = 99.99f;
        BestTime_03 = 99.99f;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}