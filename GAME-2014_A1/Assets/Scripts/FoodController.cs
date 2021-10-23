using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  The Source file name: FoodController.cs
///  Author's name: Trung Le (Kyle Hunter)
///  Student Number: 101264698
///  Program description: Responsible for the individual object spawn from the pool & factory
///  Date last Modified: See GitHub
///  Revision History: See GitHub
/// </summary>
public class FoodController : MonoBehaviour
{
    [SerializeField] private int heal_value_ = 20;
    private Vector3 spawn_pos_;
    private FoodManager manager_;

    private void Awake()
    {
        manager_ = GameObject.FindObjectOfType<FoodManager>();
    }

    /// <summary>
    /// Mutator for private variable
    /// </summary>
    /// <param name="value"></param>
    public void SetSpawnPos(Vector3 value)
    {
        spawn_pos_ = value;
    }

    /// <summary>
    /// When bullet collides with something, move bullet back to pool
    /// </summary>
    public void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable<int> other_interface = other.gameObject.GetComponent<IDamageable<int>>();
        if (other_interface != null)
        {
            if (other_interface.obj_type == GlobalEnums.ObjType.PLAYER)
            {
                other_interface.HealDamage(heal_value_);
            }
        }
        manager_.ReturnObj(this.gameObject);
    }
}
