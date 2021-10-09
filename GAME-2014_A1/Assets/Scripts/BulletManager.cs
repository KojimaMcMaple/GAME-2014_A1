using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public Queue<GameObject> bullet_pool;
    public int bullet_num;
    public GameObject bullet_obj;

    private void Awake()
    {
        bullet_pool = new Queue<GameObject>();

        BuildBulletPool();
    }

    // Builds a pool of bullets in bullet_num amount
    private void BuildBulletPool()
    {
        for (int i = 0; i < bullet_num; i++)
        {
            var temp = Instantiate(bullet_obj, this.transform);
            temp.SetActive(false);
            bullet_pool.Enqueue(temp);
        }
    }

    // Removes a bullet from the pool and return a ref to it
    public GameObject GetBullet(Vector2 position)
    {
        var temp = bullet_pool.Dequeue();
        temp.transform.position = position;
        temp.SetActive(true);
        return temp;
    }

    // Returns a bullet back into the pool
    public void ReturnBullet(GameObject returned_bullet)
    {
        returned_bullet.SetActive(false);
        bullet_pool.Enqueue(returned_bullet);
    }
}
