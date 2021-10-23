using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>
{
    void Init(); //Link vars to class vars
    T health { get; set; } //Health points
    GlobalEnums.ObjType obj_type { get; set; } //Type of gameobject
    void ApplyDamage(T damage_value); //Deals damage to objects
}
