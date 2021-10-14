using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float move_speed_ = 5.0f;
    private float max_move_speed_ = 7.0f;
    private float jump_force_ = 10.0f; //from https://youtu.be/vdOFUFMiPDU (How To Jump in Unity - Unity Jumping Tutorial | Make Your Characters Jump in Unity)
    private float fall_multiplier_ = 0.0f; //from https://youtu.be/7KiK0Aqtmzc (Better Jumping in Unity With Four Lines of Code)
    private float low_jump_multiplier_ = 1.0f;

    private Rigidbody2D rb_;
    private CapsuleCollider2D player_collider_;
    private Vector2 move_dir_;
    private float scale_x_ = 1f;

    private Animator animator_;

    private Transform bullet_spawn_pos_;
    private BulletManager bullet_manager_;

    void Awake()
    {
        rb_ = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
        player_collider_ = GetComponent<CapsuleCollider2D>();

        bullet_spawn_pos_ = transform.Find("BulletSpawnPosition"); 
        bullet_manager_ = GameObject.FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool is_grounded = IsGrounded();

        // CONTROLS
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb_.velocity = new Vector2(Mathf.Clamp(move_speed_, 0.0f, max_move_speed_), rb_.velocity.y);
            scale_x_ = +1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb_.velocity = new Vector2(Mathf.Clamp(move_speed_, -max_move_speed_, 0.0f), rb_.velocity.y);
            scale_x_ = -1f;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (is_grounded)
            {
                rb_.velocity = new Vector2(rb_.velocity.x, jump_force_);
            }
        }

        // JUMP MODIFIERS FOR BETTER FEEL
        if (rb_.velocity.y < 0)
        {
            rb_.velocity += Vector2.up * Physics.gravity.y * fall_multiplier_ * Time.deltaTime; //using Time.deltaTime due to acceleration
        }
        else if (rb_.velocity.y > 0)
        {
            rb_.velocity += Vector2.up * Physics.gravity.y * low_jump_multiplier_ * Time.deltaTime; //using Time.deltaTime due to acceleration
        }


        // ANIMATOR
        animator_.SetFloat("VelocityX", Mathf.Abs(rb_.velocity.x));
        animator_.SetFloat("VelocityY", rb_.velocity.y);
        if (is_grounded)
        {
            animator_.SetBool("IsGrounded", true);
            animator_.SetBool("IsJumping", false);
            if (rb_.velocity.x != 0)
            {
                animator_.SetBool("IsRunning", true);
            }
            else
            {
                animator_.SetBool("IsRunning", false);
            }
        }
        else
        {
            animator_.SetBool("IsGrounded", false);
            if (rb_.velocity.y > 0)
            {
                animator_.SetBool("IsJumping", true);
            }
        }

        transform.localScale = new Vector3(scale_x_, transform.localScale.y, transform.localScale.z);
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(new Vector2(player_collider_.transform.position.x, player_collider_.bounds.min.y), Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }

    public void OnFireBullet()
    {
        if (transform.localScale.x > 0)
        {
            bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.BulletType.PLAYER, GlobalEnums.BulletDir.RIGHT);
        }
        else
        {
            bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.BulletType.PLAYER, GlobalEnums.BulletDir.LEFT);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.Find("BulletSpawnPosition").position, 0.1f);
    }
}
