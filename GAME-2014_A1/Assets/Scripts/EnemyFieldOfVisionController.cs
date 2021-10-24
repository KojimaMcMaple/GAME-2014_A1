using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfVisionController : MonoBehaviour
{
    private EnemyController parent_controller_;

    void Awake()
    {
        parent_controller_ = transform.parent.transform.GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable<int> other_interface = other.gameObject.GetComponent<IDamageable<int>>();
        if (other_interface != null)
        {
            if (other_interface.obj_type == GlobalEnums.ObjType.PLAYER)
            {
                parent_controller_.SetState(GlobalEnums.EnemyState.ATTACK);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IDamageable<int> other_interface = other.gameObject.GetComponent<IDamageable<int>>();
        if (other_interface != null)
        {
            if (other_interface.obj_type == GlobalEnums.ObjType.PLAYER)
            {
                parent_controller_.SetState(GlobalEnums.EnemyState.IDLE);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        IDamageable<int> other_interface = other.gameObject.GetComponent<IDamageable<int>>();
        if (other_interface != null)
        {
            if (other_interface.obj_type == GlobalEnums.ObjType.PLAYER)
            {
                parent_controller_.SetIsFacingLeft(other.transform.position.x > transform.position.x ? false : true);
            }
        }
    }
}
