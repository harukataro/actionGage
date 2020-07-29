using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    public Text text_BestTime_1; // ステージ1最速クリアタイム表示UI
    public Text text_BestTime_2; // ステージ1最速クリアタイム表示UI
    public Text text_BestTime_3; // ステージ1最速クリアタイム表示UI
    void start()
    {
        Data data = GameObject.Find("DataManager").GetComponent<Data>(); // データスクリプトを取得
        text_BestTime_1.text = "BestTime : \n" + data.BestTime_01.ToString("F2");
        text_BestTime_2.text = "BestTime : \n" + data.BestTime_02.ToString("F2");
        text_BestTime_3.text = "BestTime : \n" + data.BestTime_03.ToString("F2");
    }

    public void TransSceneSwitch_1()
    {
        TransitionScene(1);
    }
    public void TransSceneSwitch_2()
    {
        TransitionScene(2);
    }
    public void TransSceneSwitch_3()
    {
        TransitionScene(3);
    }
    public void TransSceneSwitch_4()
    {
        TransitionScene(4);
    }

    private void TransitionScene(int stage)
    {
        string sceneName = "Stage" + stage.ToString();

        GetComponent<AudioSource>().Play();
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene(sceneName);
    }
}
