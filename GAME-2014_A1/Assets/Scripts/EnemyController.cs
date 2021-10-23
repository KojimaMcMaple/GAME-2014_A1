using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  The Source file name: EnemyController.cs
///  Author's name: Trung Le (Kyle Hunter)
///  Student Number: 101264698
///  Program description: Global game manager script
///  Date last Modified: See GitHub
///  Revision History: See GitHub
/// </summary>
public class EnemyController : MonoBehaviour, IDamageable<int>
{
    [Header("Stats")]
    [SerializeField] private int hp_ = 100;
    [SerializeField] private int score_ = 50;

    [Header("Movement")]
    private Vector3 startingPoint;
    [SerializeField] private float vertical_range_ = 0.47f;
    
    [Header("Bullets")]
    private Transform bullet_spawn_pos_;
    //public GameObject bulletPrefab;
    [SerializeField] private float speed_ = 0.75f;
    [SerializeField] private float firerate_ = 0.47f;
    private float shoot_countdown_ = 0.0f;

    private Transform fov_;
    private bool is_facing_left_ = true;

    private BulletManager bullet_manager_;
    private FoodManager food_manager_;
    private GameManager game_manager_;

    void Awake()
    {
        startingPoint = transform.position;
        is_facing_left_ = transform.localScale.x < 0 ? true : false;
        bullet_spawn_pos_ = transform.Find("BulletSpawnPosition");
        bullet_manager_ = GameObject.FindObjectOfType<BulletManager>();
        food_manager_ = GameObject.FindObjectOfType<FoodManager>();
        shoot_countdown_ = firerate_;

        game_manager_ = FindObjectOfType<GameManager>();

        Init(); //IDamageable method
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.PingPong(Time.time * speed_, vertical_range_) + startingPoint.y);

        shoot_countdown_ -= Time.deltaTime;
        if (shoot_countdown_ <= 0)
        {
            if (transform.localScale.x > 0)
            {
                bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.ObjType.ENEMY, GlobalEnums.BulletDir.RIGHT);
            }
            else
            {
                bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.ObjType.ENEMY, GlobalEnums.BulletDir.LEFT);
            }
            shoot_countdown_ = firerate_;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        IDamageable<int> other_interface = collision.gameObject.GetComponent<IDamageable<int>>();
        if (other_interface != null)
        {
            if (other_interface.obj_type == GlobalEnums.ObjType.PLAYER)
            {

            }
        }
    }

    /// <summary>
    /// IDamageable methods
    /// </summary>
    public void Init() //Link hp to class hp
    {
        health = hp_;
        obj_type = GlobalEnums.ObjType.ENEMY;
    }
    public int health { get; set; } //Health points
    public GlobalEnums.ObjType obj_type { get; set; } //Type of gameobject
    public void ApplyDamage(int damage_value) //Deals damage to object
    {
        health -= damage_value;
        if (health <= 0)
        {
            food_manager_.GetObj(this.transform.position, (GlobalEnums.FoodType)Random.Range(0, (int)GlobalEnums.FoodType.TYPE_COUNT));
            game_manager_.IncrementScore(score_);
            this.gameObject.SetActive(false);
        }
    }
    public void HealDamage(int heal_value) { } //Adds health to object

    /// <summary>
    /// Visual debug
    /// </summary>
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
