using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// タイトル管理スクリプト
public class TitleManager : MonoBehaviour
{
    // メンバ変数宣言
    public GameObject playerPicture;    // プレイヤー画像UI
    public GameObject bossPicture;      // ボス画像UI
    private Vector2 playerPicDefaultPos;    // プレイヤー画像UIの初期座標
    private Vector2 bossPicDefaultPos;      // ボス画像UIの初期座標
    private float time; // 経過時間

    // 起動時に１回だけ呼び出されるメソッド
    void Start()
    {
        // 変数初期化処理
        playerPicDefaultPos = playerPicture.transform.position; // プレイヤー画像UIの初期座標取得
        bossPicDefaultPos = bossPicture.transform.position;     // ボス画像UIの初期座標取得
        time = 0.0f;
    }

    // 毎フレーム呼び出されるメソッド
    void Update()
    {
        // 経過時間をカウント
        time += Time.deltaTime;

        // プレイヤー画像UIを時間経過で上下に揺らす
        Vector2 playerPicFixPos = new Vector2(0.0f, Mathf.Sin(time * 1.5f) * 50.0f);    // 移動量計算
        playerPicture.transform.position = playerPicDefaultPos + playerPicFixPos;       // 初期座標に移動量を足した値を座標にセット

        // ボス画像UIを時間経過で上下に揺らす
        Vector2 bossPicFixPos = new Vector2(0.0f, Mathf.Sin(time) * 30.0f);   // 移動量計算
        bossPicture.transform.position = bossPicDefaultPos + bossPicFixPos;     // 初期座標に移動量を足した値を座標にセット
    }

    public void TransitionScene()
    {
        // SampleSceneをロードする
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("StageSelectScene");
        GetComponent<AudioSource>().Play();
    }
}
