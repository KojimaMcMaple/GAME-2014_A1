using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  The Source file name: FoodManager.cs
///  Author's name: Trung Le (Kyle Hunter)
///  Student Number: 101264698
///  Program description: Manages the queue
///  Date last Modified: See GitHub
///  Revision History: See GitHub
/// </summary>
[System.Serializable]
public class FoodManager : MonoBehaviour
{
    private Queue<GameObject> pool_;
    [SerializeField] private int pool_num_;

    private FoodFactory factory_;

    private void Awake()
    {
        pool_ = new Queue<GameObject>();
        factory_ = GetComponent<FoodFactory>();
        BuildPool(); //pre-build a certain num of objects to improve performance
    }

    /// <summary>
    /// Builds a pool of objects in pool_num_ amount
    /// </summary>
    private void BuildPool()
    {
        for (int i = 0; i < pool_num_; i++)
        {
            AddObj();
        }
    }

    /// <summary>
    /// Uses the factory to spawn one object, add it to the queue, and increase the pool size 
    /// </summary>
    private void AddObj()
    {
        //var temp = Instantiate(bullet_obj, this.transform);
        var temp = factory_.CreateObj();
        pool_.Enqueue(temp);
        pool_num_++;
    }

    /// <summary>
    /// Removes an object from the pool and returns a reference to it
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject GetObj(Vector2 position)
    {
        GameObject temp = null;
        if (pool_.Count < 1) //add one obj if pool empty
        {
            AddObj();
        }
        temp = pool_.Dequeue();
        temp.transform.position = position;
        temp.GetComponent<FoodController>().SetSpawnPos(position);
        temp.SetActive(true);
        return temp;
    }

    /// <summary>
    /// Returns an object back into the pool
    /// </summary>
    /// <param name="returned_obj"></param>
    public void ReturnObj(GameObject returned_obj)
    {
        returned_obj.SetActive(false);
        pool_.Enqueue(returned_obj);
    }
}
