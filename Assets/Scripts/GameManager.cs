using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // メンバ変数宣言
    public GameObject imageObj_Health_1;
    public GameObject imageObj_Health_2;
    public GameObject imageObj_Health_3;
    public Image image_BossHealth;
    public Image image_EnergyGage;
    public Text text_Result;
    public GameObject playerObj;        // プレイヤーオブジェクト
    public GameObject bossObj;			// ボスオブジェクト

    public BossController bossController;
    private bool afterFinish;           // 戦闘が終了しているかのフラグ
    private float afterFinishTime;      // 戦闘終了後の経過時間(秒)
    public Text text_battleTime;        // 戦闘時間表示UI
    private float battleTime;           // 戦闘時間(秒)


    // プレイヤーの残り体力をUIに適用(PlayerControllerから呼び出される)
    // 引数health : 残り体力
    public void SetPlayerHealthUI(int health)
    {
        // 残り体力によって非表示にすべき体力アイコンを消去する
        if (health == 2)
        { // 体力2になった場合
            Destroy(imageObj_Health_3); // 3つめのアイコンを消去
        }
        else if (health == 1)
        { // 体力1になった場合
            Destroy(imageObj_Health_2); // 2つめのアイコンを消去
        }
        else if (health == 0)
        { // 体力0になった場合
            Destroy(imageObj_Health_1); // 1つめのアイコンを消去
        }
    }

    // ボスの残り体力をUIに適用(BossControllerから呼び出される)
    public void SetBossHealthUI(int health, int maxHealth)
    {
        // ボスの残り体力の比率を計算
        float ratio; // ボス残り体力の割合(0～1)
        ratio = (float)health / maxHealth; // float型へのキャストが必要

        // 画像のfillAmountに比率をセット
        image_BossHealth.fillAmount = ratio;
    }
    public void SetEnergyUI(float energy, float maxEnergy)
    {
        // 残りエネルギーの比率を計算
        float ratio; // ボス残り体力の割合(0～1)
        ratio = energy / maxEnergy;

        // 画像のfillAmountに比率をセット
        image_EnergyGage.fillAmount = ratio;
    }

    void Update()
    {
        if (!afterFinish)
        {
            battleTime += Time.deltaTime;
            text_battleTime.text = "Time : " + battleTime.ToString("F2");

            if (playerObj == null)
            {
                text_Result.text = "Player Lose...";
                afterFinish = true;
                afterFinishTime = 0.0f;
            }
            else if (bossObj == null)
            {
                text_Result.text = "Player Win!!";
                afterFinish = true;
                afterFinishTime = 0.0f;
                //CheckBestTime();
            }
        }
        else //afterFinish
        {
            afterFinishTime += Time.deltaTime;
            if (afterFinishTime > 2.0f)
            {
                string stageName;
                if (bossController.stage < 4 && bossObj == null)
                {
                    stageName = "Stage" + (bossController.stage + 1).ToString();
                    GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene(stageName);
                }
                else
                {
                    GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("StageSelectScene");
                }
            }

        }
    }
    void CheckBestTime()
    {
        // データオブジェクトを取得
        GameObject dataObj = GameObject.Find("DataManager");
        if (dataObj == null)
            return;
        // データスクリプトを取得
        Data data = dataObj.GetComponent<Data>();

        // 現在のステージ番号を取得
        // (アクティブシーンの名前の6文字目を取得し、それを数値に変換してセット)
        int num = SceneManager.GetActiveScene().name[5] - '0';

        // 戦闘時間がステージの最速タイムより短ければ最速タイムを更新する処理
        if (num == 1)
        { // ステージ1の場合
            if (battleTime < data.BestTime_01)
                data.BestTime_01 = battleTime;
        }
        else if (num == 2)
        { // ステージ2の場合
            if (battleTime < data.BestTime_02)
                data.BestTime_02 = battleTime;
        }
        else if (num == 3)
        { // ステージ3の場合
            if (battleTime < data.BestTime_03)
                data.BestTime_03 = battleTime;
        }
    }
}
