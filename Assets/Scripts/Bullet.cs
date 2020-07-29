using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    private float time = 0.0f;
    public float angle;
    public float limitTime;
    public bool mode_Homing;        // 誘導弾モード
    public GameObject homingTarget; // 誘導対象オブジェクト
    public bool mode_Bounding;  // 反射弾モード
    public bool bounded;		// 反射済みフラグ

    void Update()
    {
        float rad;
        Vector2 vec;
        float move_x, move_y;

        rad = angle * Mathf.Deg2Rad;

        move_x = Mathf.Cos(rad) * speed * Time.deltaTime;
        move_y = Mathf.Sin(rad) * speed * Time.deltaTime;
        vec = new Vector2(move_x, move_y);
        transform.Translate(vec);

        if (mode_Homing)
        {
            if (homingTarget != null)
            {
                Vector2 target = homingTarget.transform.position;
                rad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
            }
        }
        else if (mode_Bounding)
        {
            if (!bounded)
            {
                Vector2 screenPos;  // 画面のサイズ
                bool check = false; // 画面端に接触した判定

                screenPos = Camera.main.WorldToScreenPoint(transform.position);

                // 画面端の接触を検出
                if (screenPos.x <= 0.0f ||
                    screenPos.x >= Screen.width)
                { // 左右端に接触
                    check = true;
                    angle = 180.0f - angle; // 反射した角度を計算してセット
                }
                if (screenPos.y <= 0.0f ||
                    screenPos.y >= Screen.height)
                { // 上下端に接触
                    check = true;
                    angle = 360.0f - angle; // 反射した角度を計算してセット
                }

                // 反射時判定
                if (check)
                {
                    bounded = true; // 反射済みをセット
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
        }

        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject);
        }
    }
}
