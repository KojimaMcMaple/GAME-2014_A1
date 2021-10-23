using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  The Source file name: FoodFactory.cs
///  Author's name: Trung Le (Kyle Hunter)
///  Student Number: 101264698
///  Program description: Manages the type of object to spawn
///  Date last Modified: See GitHub
///  Revision History: See GitHub
/// </summary>
[System.Serializable]
public class FoodFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> food_list_ = new List<GameObject>();

    /// <summary>
    /// Instantiates an object and returns a reference to it
    /// </summary>
    /// <returns></returns>
    public GameObject CreateObj()
    {
        GameObject temp = null;
        temp = Instantiate(food_list_[Random.Range(0, food_list_.Count)], this.transform); 
        temp.SetActive(false);
        return temp;
    }
}
