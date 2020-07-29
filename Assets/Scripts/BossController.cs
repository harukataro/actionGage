using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject bulletPrefab;
    public int health;
    public int maxHealth;
    private float time;
    public GameObject playerObj;
    public GameObject VanishParticlePrefab;
    public int stage;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameManager.SetBossHealthUI(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
            return;

        time += Time.deltaTime;
        // if (time > 0.4f)
        // {
        //     time -= 0.4f;

        //     Shot(8.0f, GetAngleToPlayer(), 5.0f);
        //     Shot(3.0f, GetAngleToPlayer() - 20.0f, 5.0f);
        //     Shot(3.0f, GetAngleToPlayer() + 20.0f, 5.0f);
        // }
        if (time > 0.4f)
        {
            time -= 0.4f;
            if (stage == 1)
            {
                Shot(5.0f, GetAngleToPlayer(), 5.0f);
            }
            else if (stage == 2)
            {
                Shot(5.0f, GetAngleToPlayer() - 20.0f, 5.0f);
                Shot(5.0f, GetAngleToPlayer(), 5.0f);
                Shot(5.0f, GetAngleToPlayer() + 20.0f, 5.0f);
                Shot_Homing(5.0f, GetAngleToPlayer(), 5.0f);
            }
            else if (stage == 3)
            {
                Shot(8.0f, GetAngleToPlayer(), 5.0f);
                Shot(5.0f, GetAngleToPlayer() - 20.0f, 5.0f);
                Shot(5.0f, GetAngleToPlayer(), 5.0f);
                Shot(5.0f, GetAngleToPlayer() + 20.0f, 5.0f);
                Shot_Bounding(5.0f, GetAngleToPlayer(), 5.0f);
            }
            else if (stage == 4)
            {
                Shot_Bounding(5.0f, GetAngleToPlayer() - 20.0f, 5.0f);
                Shot_Bounding(5.0f, GetAngleToPlayer() + 20.0f, 5.0f);
            }
            else if (stage == 5)
            {
                Shot(8.0f, GetAngleToPlayer(), 5.0f);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "playerBullet")
            return;

        Destroy(collider.gameObject);

        health--;
        gameManager.SetBossHealthUI(health, maxHealth);
        if (health <= 0)
        {
            // 被弾パーティクルを発生させる
            GameObject obj = Instantiate(VanishParticlePrefab);
            // 被弾パーティクルの座標を弾の座標に変更
            obj.transform.position = collider.gameObject.transform.position;
            // 被弾パーティクルの消滅を3秒後に予約
            Destroy(obj, 3.0f);

            Destroy(gameObject);
        }
    }

    float GetAngleToPlayer()
    {
        float playerAng;
        Vector2 target;
        target = playerObj.transform.position - transform.position;
        playerAng = Mathf.Atan2(target.y, target.x);
        playerAng *= Mathf.Rad2Deg;
        return playerAng;
    }

    void Shot(float speed, float angle, float limitTime)
    {
        GameObject obj;
        obj = Instantiate(bulletPrefab);
        obj.transform.position = transform.position;
        obj.name = "EnemyBullet";

        obj.GetComponent<Bullet>().speed = 4.0f;
        obj.GetComponent<Bullet>().angle = angle;
        obj.GetComponent<Bullet>().limitTime = 5.0f;
        obj.GetComponent<SpriteRenderer>().color = Color.magenta;

    }
    void Shot_Homing(float speed, float angle, float limitTime)
    {
        // GameObject型ローカル変数を宣言
        GameObject obj;
        // 弾プレハブのインスタンスを生成し、変数objに格納
        obj = Instantiate(bulletPrefab);
        // 弾インスタンスの座標にボスの座標をセット
        obj.transform.position = transform.position;
        // インスタンスのオブジェクト名を変更(自弾と区別するため)
        obj.name = "EnemyBullet";
        // 弾のパラメータをセット
        obj.GetComponent<Bullet>().speed = speed;          // 速度
        obj.GetComponent<Bullet>().angle = angle;          // 角度
        obj.GetComponent<Bullet>().limitTime = limitTime;  // 生存時間
        obj.GetComponent<Bullet>().mode_Homing = true;          // 誘導弾モードをON
        obj.GetComponent<Bullet>().homingTarget = playerObj;    // 誘導対象を指定
        obj.GetComponent<SpriteRenderer>().color = Color.blue; // 青
    }

    void Shot_Bounding(float speed, float angle, float limitTime)
    {
        // GameObject型ローカル変数を宣言
        GameObject obj;
        // 弾プレハブのインスタンスを生成し、変数objに格納
        obj = Instantiate(bulletPrefab);
        // 弾インスタンスの座標にボスの座標をセット
        obj.transform.position = transform.position;
        // インスタンスのオブジェクト名を変更(自弾と区別するため)
        obj.name = "EnemyBullet";
        // 弾のパラメータをセット
        obj.GetComponent<Bullet>().speed = speed;          // 速度
        obj.GetComponent<Bullet>().angle = angle;          // 角度
        obj.GetComponent<Bullet>().limitTime = limitTime;  // 生存時間
        obj.GetComponent<Bullet>().mode_Bounding = true;     // 反射弾モードをON
        obj.GetComponent<SpriteRenderer>().color = Color.green; // 緑
    }
}

