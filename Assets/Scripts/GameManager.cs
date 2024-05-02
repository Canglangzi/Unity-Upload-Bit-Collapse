using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGameOver = false;

    private void Awake()
    {
        // 单例模式，确保只有一个GameManager实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保证在场景切换时GameManager不被销毁
        }
        else
        {
            Destroy(gameObject); // 如果已存在GameManager实例，则销毁新的实例
        }
    }

    private void Update()
    {
        // 监听游戏结束条件
        if (!isGameOver && IsGameOverConditionMet())
        {
            GameOver();
        }
    }

    // 游戏结束条件检查
    private bool IsGameOverConditionMet()
    {
        // 这里可以是任何游戏结束的条件，例如玩家生命值耗尽等
        return false;
    }

    // 游戏结束
    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        // 在这里可以执行游戏结束的逻辑，例如显示游戏结束UI等
    }

    // 重新开始游戏
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameOver = false;
    }

    // 退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }
}
