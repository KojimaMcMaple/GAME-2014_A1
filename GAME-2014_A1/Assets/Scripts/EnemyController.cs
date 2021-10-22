using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Movement")]

    [Header("Bullets")]
    private Transform bullet_spawn_pos_;
    //public GameObject bulletPrefab;
    [SerializeField] private float speed_ = 0.75f;
    [SerializeField] private float firerate_ = 1.0f;
    private float shoot_countdown_ = 0.0f;

    private Vector3 startingPoint;
    [SerializeField] private float vertical_range_;
    private BulletManager bullet_manager_;

    void Awake()
    {
        startingPoint = transform.position;
        bullet_spawn_pos_ = transform.Find("BulletSpawnPosition");
        bullet_manager_ = GameObject.FindObjectOfType<BulletManager>();
        shoot_countdown_ = firerate_;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.PingPong(Time.time * speed_, vertical_range_) + startingPoint.y);

        shoot_countdown_ -= Time.deltaTime;
        if (shoot_countdown_ <= 0)
        {
            if (transform.localScale.x > 0)
            {
                bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.BulletType.ENEMY, GlobalEnums.BulletDir.RIGHT);
            }
            else
            {
                bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.BulletType.ENEMY, GlobalEnums.BulletDir.LEFT);
            }
            shoot_countdown_ = firerate_;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.Find("BulletSpawnPosition").position, 0.05f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + vertical_range_, transform.position.z), new Vector3(0.2f, 0.05f, 1));
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - vertical_range_, transform.position.z), new Vector3(0.2f, 0.05f, 1));
    }
}
