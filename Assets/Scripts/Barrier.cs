using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    // 当たり判定内に他オブジェクトが侵入した際呼び出されるメソッド
    void OnTriggerEnter2D(Collider2D collider)
    {
        // ボスが発射した弾でなければ処理を終了
        if (collider.gameObject.name != "EnemyBullet")
        { // 接触オブジェクト名がEnemyBulletで無ければ
            return; // メソッド終了
        }

        // 弾オブジェクトを消滅させる
        Destroy(collider.gameObject);
    }
}