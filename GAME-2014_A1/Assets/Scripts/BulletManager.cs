using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public Queue<GameObject> player_bullet_pool;
    public Queue<GameObject> enemy_bullet_pool;
    public int player_bullet_num;
    public int enemy_bullet_num;
    //public GameObject bullet_obj;

    private BulletFactory factory_;

    private void Awake()
    {
        player_bullet_pool = new Queue<GameObject>();
        enemy_bullet_pool = new Queue<GameObject>();
        factory_ = GetComponent<BulletFactory>();
        BuildBulletPool(); //pre-build a certain num of bullets to improve performance
    }

    // Builds a pool of bullets in bullet_num amount
    private void BuildBulletPool()
    {
        for (int i = 0; i < player_bullet_num; i++)
        {
            AddBullet(GlobalEnums.BulletType.PLAYER);
        }
        for (int i = 0; i < enemy_bullet_num; i++)
        {
            AddBullet(GlobalEnums.BulletType.ENEMY);
        }
    }

    private void AddBullet(GlobalEnums.BulletType type = GlobalEnums.BulletType.PLAYER)
    {
        //var temp = Instantiate(bullet_obj, this.transform);
        var temp = factory_.CreateBullet(type);

        switch (type)
        {
            case GlobalEnums.BulletType.PLAYER:
                //temp.SetActive(false);
                player_bullet_pool.Enqueue(temp);
                player_bullet_num++;
                break;
            case GlobalEnums.BulletType.ENEMY:
                //temp.SetActive(false);
                enemy_bullet_pool.Enqueue(temp);
                enemy_bullet_num++;
                break;
            default:
                break;
        }
    }

    // Removes a bullet from the pool and return a ref to it
    public GameObject GetBullet(Vector2 position, 
                                GlobalEnums.BulletType type = GlobalEnums.BulletType.PLAYER,
                                GlobalEnums.BulletDir dir = GlobalEnums.BulletDir.RIGHT)
    {
        GameObject temp = null;
        switch (type)
        {
            case GlobalEnums.BulletType.PLAYER:
                if (player_bullet_pool.Count < 1) //add one bullet if pool empty
                {
                    AddBullet(GlobalEnums.BulletType.PLAYER);
                }
                temp = player_bullet_pool.Dequeue();
                break;
            case GlobalEnums.BulletType.ENEMY:
                if (enemy_bullet_pool.Count < 1) //add one bullet if pool empty
                {
                    AddBullet(GlobalEnums.BulletType.ENEMY);
                }
                temp = enemy_bullet_pool.Dequeue();
                break;
            default:
                break;
        }
        temp.transform.position = position;
        temp.GetComponent<BulletController>().SetSpawnPos(position);
        temp.GetComponent<BulletController>().SetDir(dir);
        temp.SetActive(true);
        return temp;
    }

    // Returns a bullet back into the pool
    public void ReturnBullet(GameObject returned_bullet, GlobalEnums.BulletType type = GlobalEnums.BulletType.PLAYER)
    {
        returned_bullet.SetActive(false);

        switch (type)
        {
            case GlobalEnums.BulletType.PLAYER:
                player_bullet_pool.Enqueue(returned_bullet);
                break;
            case GlobalEnums.BulletType.ENEMY:
                enemy_bullet_pool.Enqueue(returned_bullet);
                break;
            default:
                break;
        }
    }
}
