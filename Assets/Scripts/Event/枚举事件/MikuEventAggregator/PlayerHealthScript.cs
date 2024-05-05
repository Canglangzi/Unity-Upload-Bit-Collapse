using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    private int currentHealth = 100;

    private void Update()
    {
        // 模拟玩家受到伤害
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // 发布健康值变化事件
        MikuEventAggregator.Instance.Publish<int>(MikuEventType.OnHealthChanged, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // 发布玩家死亡事件
        MikuEventAggregator.Instance.Publish<object>(MikuEventType.OnPlayerDeath, null);

    }
}