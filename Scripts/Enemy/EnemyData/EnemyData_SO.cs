using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName ="Enemy Stats/Data")]
public class EnemyData_SO : ScriptableObject 
{
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;


}
