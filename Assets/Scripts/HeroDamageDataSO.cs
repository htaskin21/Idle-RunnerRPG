using UnityEngine;

[CreateAssetMenu(menuName = "Create HeroDamageDataSO", fileName = "HeroDamageDataSO")]
public class HeroDamageDataSO :ScriptableObject
{
    public double heroAttack = 10;
    public int attackCooldown;
    
    public double tapAttack = 1;
   
    public float criticalAttack = 5;
    public float criticalAttackChance = 0;

    public double lightningAttackPoint;


    public double earthDamageMultiplier = 1;
    public double plantDamageMultiplier = 1;
    public double waterDamageMultiplier = 1;

}