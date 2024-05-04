using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public GameObject gameOverPanel;

    private void Start()
    {
        // 订阅健康值变化事件
        MikuEventAggregator.Instance.Subscribe<int>(MikuEventType.OnHealthChanged, OnHealthChanged);
        // 订阅玩家死亡事件
        MikuEventAggregator.Instance.Subscribe<object>(MikuEventType.OnPlayerDeath, OnPlayerDeath);
    }

    private void OnHealthChanged(int newHealth)
    {
        healthText.text = "Health: " + newHealth;
    }

    private void OnPlayerDeath(object eventData)
    {
        gameOverPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        // 取消订阅事件，防止内存泄漏
        MikuEventAggregator.Instance.Unsubscribe<int>(MikuEventType.OnHealthChanged, OnHealthChanged);
        MikuEventAggregator.Instance.Unsubscribe<object>(MikuEventType.OnPlayerDeath, OnPlayerDeath);
    }
}