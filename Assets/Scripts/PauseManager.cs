using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;
    private bool isPaused = false;

    private void Start()
    {
        // 在游戏开始时查找名为 "PauseUI" 的对象，并将其赋值给 pauseUI 变量
        pauseUI = GameObject.Find("PauseUI");
        if (pauseUI == null)
        {
            Debug.LogError("PauseUI object not found in the scene.");
        }
        
        ResumeGame(); // 游戏开始时确保不处于暂停状态
        pauseUI.SetActive(false); //)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // 如果游戏已暂停，则恢复游戏
            }
            else
            {
                PauseGame(); // 如果游戏未暂停，则暂停游戏
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // 将时间缩放设置为0，暂停游戏
        isPaused = true;
        pauseUI.SetActive(true); // 显示暂停界面
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // 恢复时间缩放为正常值，恢复游戏
        isPaused = false;
        pauseUI.SetActive(false); // 隐藏暂停界面
    }
}
