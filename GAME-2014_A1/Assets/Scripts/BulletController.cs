using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GlobalEnums.BulletType type;

    [SerializeField] private float speed_;
    [SerializeField] private float travel_distance_ = 10f;
    private Vector3 spawn_pos_;
    private GlobalEnums.BulletDir dir_ = GlobalEnums.BulletDir.DEFAULT;
    private BulletManager bullet_manager_;

    private void Awake()
    {
        bullet_manager_ = GameObject.FindObjectOfType<BulletManager>();
    }

    private void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        switch (dir_)
        {
            case GlobalEnums.BulletDir.LEFT:
                transform.position -= new Vector3(speed_, 0f);
                break;
            case GlobalEnums.BulletDir.RIGHT:
                transform.position += new Vector3(speed_, 0f);
                break;
            default:
                break;
        }
        
    }

    private void CheckBounds()
    {
        if (transform.position.x > spawn_pos_.x + travel_distance_ || transform.position.x < spawn_pos_.x - travel_distance_)
        {
            bullet_manager_.ReturnBullet(this.gameObject, type);
        }
    }

    public void SetSpawnPos(Vector3 value)
    {
        spawn_pos_ = value;
    }

    public void SetDir(GlobalEnums.BulletDir value)
    {
        dir_ = value;
    }
}
