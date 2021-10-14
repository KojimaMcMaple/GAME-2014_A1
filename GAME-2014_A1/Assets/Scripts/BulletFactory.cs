using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{
    [Header("Bullet Types")]
    public GameObject enemy_bullet;
    public GameObject player_bullet;

    public GameObject CreateBullet(GlobalEnums.BulletType type = GlobalEnums.BulletType.PLAYER)
    {
        GameObject temp = null;
        switch (type)
        {
            case GlobalEnums.BulletType.PLAYER:
                temp = Instantiate(player_bullet, this.transform);
                break;
            case GlobalEnums.BulletType.ENEMY:
                temp = Instantiate(enemy_bullet, this.transform);
                break;
            default:
                break;
        }
        temp.SetActive(false);
        return temp;
    }
}
