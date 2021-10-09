using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Bounds bounds;

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
        transform.position += new Vector3(speed, 0f);
    }

    private void CheckBounds()
    {
        if (transform.position.x > bounds.extents.x)
        {
            bullet_manager_.ReturnBullet(this.gameObject);
        }
    }
}
