using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private GameManager game_manager_;

    private void Awake()
    {
        game_manager_ = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// When object collides with player, ends level
    /// </summary>
    public void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable<int> other_interface = other.gameObject.GetComponent<IDamageable<int>>();
        if (other_interface != null)
        {
            if (other_interface.obj_type == GlobalEnums.ObjType.PLAYER)
            {
                game_manager_.DoShowOverlayPanel();
            }
        }
    }
}
