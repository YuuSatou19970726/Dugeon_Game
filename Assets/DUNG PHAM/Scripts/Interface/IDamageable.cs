using UnityEngine;

public interface IDamageable
{
    void GetDamage(float damage, Transform knocker);

    float GetHealth();

}