using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  The Source file name: PlayerController.cs
///  Author's name: Trung Le (Kyle Hunter)
///  Student Number: 101264698
///  Program description: Global game manager script
///  Date last Modified: See GitHub
///  Revision History: See GitHub
/// </summary>

public class ParallaxBkgController : MonoBehaviour
{
    //https://youtu.be/wBol2xzxCOU
    [SerializeField] private Vector2 parallax_effect_multiplier_ = new Vector2(0.5f, 0);
    [SerializeField] private bool is_infinite_x_ = true;
    [SerializeField] private bool is_infinite_y_ = false;

    private Transform camera_transform_;
    private Vector3 prev_camera_pos_;
    private float texture_unit_size_x_ = 1f;
    private float texture_unit_size_y_ = 1f;
    
    void Start()
    {
        camera_transform_ = Camera.main.transform;
        prev_camera_pos_ = camera_transform_.position;
        if (GetComponent<SpriteRenderer>() != null)
        {
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            texture_unit_size_x_ = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
            Debug.Log("texture.width = " + texture.width);
            Debug.Log("sprite.pixelsPerUnit = " + sprite.pixelsPerUnit);
            Debug.Log("texture_unit_size_x_ = " + texture_unit_size_x_);
            texture_unit_size_y_ = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;
        }
    }

    void LateUpdate()
    {
        Vector3 delta_movement = (camera_transform_.position - prev_camera_pos_);

        transform.position += new Vector3(delta_movement.x * parallax_effect_multiplier_.x, delta_movement.y * parallax_effect_multiplier_.y, 0);
        prev_camera_pos_ = camera_transform_.position;

        if (is_infinite_x_)
        {
            if (Mathf.Abs(camera_transform_.position.x - transform.position.x) >= texture_unit_size_x_)
            {
                float offset_x = (camera_transform_.position.x - transform.position.x) % texture_unit_size_x_;
                transform.position = new Vector3(camera_transform_.position.x + offset_x, transform.position.y);
            }
        }
        if (is_infinite_y_)
        {
            if (Mathf.Abs(camera_transform_.position.y - transform.position.y) >= texture_unit_size_y_)
            {
                float offset_y = (camera_transform_.position.y - transform.position.y) % texture_unit_size_y_;
                transform.position = new Vector3(transform.position.x, camera_transform_.position.y + offset_y );
            }
        }
    }
}