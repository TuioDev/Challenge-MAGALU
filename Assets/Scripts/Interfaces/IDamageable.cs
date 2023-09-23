using System;

public interface IDamageable
{
    float MinimumDamageToPlayAnimation { get; set; }
    void TakeDamageOrHeal(float damage);
    int GetEnemyHealthPercentage();
}
