using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float move_speed_ = 5.0f;
    private float max_move_speed_ = 7.0f;
    [SerializeField] private float jump_force_ = 10.0f; //from https://youtu.be/vdOFUFMiPDU (How To Jump in Unity - Unity Jumping Tutorial | Make Your Characters Jump in Unity)
    [SerializeField] private float fall_multiplier_ = 1.5f; //from https://youtu.be/7KiK0Aqtmzc (Better Jumping in Unity With Four Lines of Code)
    [SerializeField] private float low_jump_multiplier_ = 1.0f;

    private Rigidbody2D rb_;
    private CapsuleCollider2D player_collider_;
    //private Vector2 move_dir_;
    private float scale_x_ = 1f;

    private Animator animator_;

    [SerializeField] private float time_delay_ = 0.25f;
    private bool can_shoot_ = true;
    private Transform bullet_spawn_pos_;
    private BulletManager bullet_manager_;

    private PlayerInputControls input_;

    void Awake()
    {
        input_ = new PlayerInputControls();

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
        Vector2 movement_input = input_.PlayerMain.Move.ReadValue<Vector2>();
        rb_.velocity = new Vector2(movement_input.x * move_speed_, rb_.velocity.y);
        if (movement_input.x > 0) //has to split movement_input.x > 0 and movement_input.x < 0 so player faces the right direction. Because scale_x_ = movement_input.x > 0 ? 1 :-1 will fail.
        {
            scale_x_ = 1;
        }
        else if (movement_input.x < 0)
        {
            scale_x_ = -1;
        }
        
       
        if (input_.PlayerMain.Jump.triggered && is_grounded)
        {
            rb_.velocity = new Vector2(rb_.velocity.x, jump_force_);
        }
        if (input_.PlayerMain.Shoot.triggered)
        {
            if (can_shoot_)
            {
                DoFireBullet();
                StartCoroutine("ShootDelay");
            }
        }

        // JUMP MODIFIERS FOR BETTER FEEL
        if (rb_.velocity.y < 0)
        {
            rb_.velocity += Vector2.up * Physics.gravity.y * fall_multiplier_ * Time.deltaTime; //using Time.deltaTime due to acceleration
        }
        else if (rb_.velocity.y > 0 && !input_.PlayerMain.Jump.triggered) //when not holding button on the next frame, jump is shorter
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

        transform.localScale = new Vector3(scale_x_, transform.localScale.y, transform.localScale.z); //sets which way the player faces
    }

    private void OnEnable()
    {
        input_.Enable();
    }

    private void OnDisable()
    {
        input_.Disable();
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(time_delay_);
        can_shoot_ = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(new Vector2(player_collider_.transform.position.x, player_collider_.bounds.min.y), Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }

    public void DoFireBullet()
    {
        if (transform.localScale.x > 0)
        {
            bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.BulletType.PLAYER, GlobalEnums.BulletDir.RIGHT);
        }
        else
        {
            bullet_manager_.GetBullet(bullet_spawn_pos_.position, GlobalEnums.BulletType.PLAYER, GlobalEnums.BulletDir.LEFT);
        }
        can_shoot_ = false;
    }

    public void SetCanShoot()
    {
        can_shoot_ = true;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.Find("BulletSpawnPosition").position, 0.1f);
    }
}
