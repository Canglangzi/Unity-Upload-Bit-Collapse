using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleSystem : MonoBehaviour
{
    public TMP_Text subtitleText; // 字幕文本显示文本框
    public GameObject subtitlePanel; // 字幕面板
    public float textSpeed = 0.05f; // 文本显示速度，每个字符显示的时间间隔
    public float subtitleDelay = 1.0f; // 字条之间的间隔时间

    private string[] currentSubtitleLines; // 当前字幕文本数组
    private int currentLineIndex = 0; // 当前字幕文本索引
    private bool isSubtitleActive = false; // 字幕是否激活
    

    private void Start()
    {
        subtitlePanel.SetActive(false); // 初始化时隐藏字幕面板
    }

    public void ShowSubtitle(string[] lines)
    {
        currentSubtitleLines = lines;
        isSubtitleActive = true;
        subtitlePanel.SetActive(true); // 激活字幕面板
       // PlayerManager.Instance. EnablePlayerMovement();
       PlayerManager.Instance. DisablePlayerMovement();

        StartCoroutine(DisplaySubtitleCoroutine());
    }

    private IEnumerator DisplaySubtitleCoroutine()
    {
        subtitleText.text = ""; // 清空文本
        foreach (char c in currentSubtitleLines[currentLineIndex])
        {
            subtitleText.text += c;
            yield return new WaitForSeconds(textSpeed); // 等待一段时间再显示下一个字符
        }

        // 字幕显示完毕后等待间隔时间
        yield return new WaitForSeconds(subtitleDelay);

        // 播放下一个字条
        ShowNextSubtitle();
    }

    public void ShowNextSubtitle()
    {
        currentLineIndex++;

        if (currentLineIndex < currentSubtitleLines.Length)
        {
            StartCoroutine(DisplaySubtitleCoroutine()); // 使用协程显示下一条字幕
        }
        else
        {
            EndSubtitle();
        }
    }

    private void EndSubtitle()
    {
        isSubtitleActive = false;
        subtitlePanel.SetActive(false); // 关闭字幕面板
         PlayerManager.Instance. EnablePlayerMovement();
    }
}
