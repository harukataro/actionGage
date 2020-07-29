using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;
    public GameObject barrierPrefab;    // バリアのプレハブ
    private GameObject barrierInstance;	// バリアのインスタンス(実体)
    private int health;
    private float energy;       // エネルギー
    private float maxEnergy;	// 最大エネルギー
    private float invisibleTime;

    public AudioSource se_Shot;     // (効果音)ショット
    public AudioSource se_Barrier;  // (効果音)バリア展開
    public AudioSource se_Damaged;  // (効果音)被弾

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        maxEnergy = 5;
        gameManager.SetPlayerHealthUI(health);
    }

    // Update is called once per frame
    void Update()
    {
        // move
        Vector2 cursorPos = Input.mousePosition;
        cursorPos = Camera.main.ScreenToWorldPoint(cursorPos);
        transform.position = cursorPos;

        //fire a bullet
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj;

            obj = Instantiate(bulletPrefab);
            obj.transform.position = transform.position;

            obj.name = "playerBullet";

            obj.GetComponent<Bullet>().speed = 6.0f;
            obj.GetComponent<Bullet>().angle = 0.0f;
            obj.GetComponent<Bullet>().limitTime = 5.0f;
            obj.GetComponent<SpriteRenderer>().color = Color.cyan;
            se_Shot.Play();
        }

        if (Input.GetMouseButtonDown(1) && barrierInstance == null)
        {
            barrierInstance = Instantiate(barrierPrefab, transform);
            barrierInstance.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            se_Barrier.Play();
        }

        if (Input.GetMouseButtonUp(1) && barrierInstance != null)
        {
            Destroy(barrierInstance);
            barrierInstance = null;
        }

        // -----エネルギー処理-----
        if (barrierInstance == null)
        {
            energy += Time.deltaTime; // 時間経過で回復
        }
        else
        {
            energy -= 2.0f * Time.deltaTime;
            if (energy < 0.0f)
            {
                Destroy(barrierInstance);
                barrierInstance = null;
            }
        }
        energy = Mathf.Clamp(energy, 0.0f, maxEnergy);
        gameManager.SetEnergyUI(energy, maxEnergy);

        // -----無敵時間処理-----
        Color col = GetComponent<SpriteRenderer>().color;
        if (invisibleTime > 0.0f)
        {
            invisibleTime -= Time.deltaTime;
            col.a = 0.5f;
        }
        else
        {
            col.a = 1.0f;
        }
        GetComponent<SpriteRenderer>().color = col;
        if (invisibleTime > 0.0f)
        {
            invisibleTime -= Time.deltaTime;
        }
    }

    //collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "EnemyBullet")
            return;
        if (barrierInstance != null)
            return;
        if (invisibleTime > 2.0f)
            return;

        Destroy(collider.gameObject);
        health--;
        gameManager.SetPlayerHealthUI(health);
        se_Damaged.Play();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        invisibleTime = 1.5f;
    }


}
