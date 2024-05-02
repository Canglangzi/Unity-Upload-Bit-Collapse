using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLayer
{
    // 对话文本数组
    public string[] dialogueTexts;
    // 选项数据数组
    public OptionData[] options;
}

[System.Serializable]
public class OptionData
{
    // 选项文本
    public string optionText;
    // 下一个对话层索引
    public int nextLayerIndex;
}

public class DialogueSystem : MonoBehaviour
{
    // 对话面板
    public GameObject dialoguePanel;
    // 对话文本组件
    public TMP_Text dialogueText;
    // 选项面板
    public GameObject optionsPanel;
    // 选项按钮预制体
    public Button optionButtonPrefab;
public Transform optionsContent;
    // 当前对话层索引
    private int currentLayerIndex = 0;
    // 对话层数组
    private DialogueLayer[] dialogueLayers;
    // 文本显示速度
    private float textSpeed = 0.05f; // 默认文本显示速度

    // 开始对话方法
    public void StartDialogue(DialogueLayer[] layers, float speed = 0.05f)
    {
        dialogueLayers = layers;
        currentLayerIndex = 0;
        textSpeed = speed;
        StartCoroutine(DisplayTextCoroutine());
        PlayerManager.Instance. DisablePlayerMovement();
    }

    // 文本显示协程
    private IEnumerator DisplayTextCoroutine()
    {
        dialoguePanel.SetActive(true);
        optionsPanel.SetActive(false);

        dialogueText.text = ""; // 清空先前的文本

        foreach (string line in dialogueLayers[currentLayerIndex].dialogueTexts)
        {
            foreach (char letter in line)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }

               dialogueText.text = ""; // 清空先前的文本
            yield return new WaitForSeconds(0.1f); // 在行之间添加短暂暂停
        }

        if (dialogueLayers[currentLayerIndex].options != null && dialogueLayers[currentLayerIndex].options.Length > 0)
        {
            DisplayOptions();
            
        }
        else
        {
            NextDialogue();
        }
    }

    // 显示选项方法
    private void DisplayOptions()
    {
        optionsPanel.SetActive(true);

        foreach (OptionData option in dialogueLayers[currentLayerIndex].options)
        {
            Button optionButton = Instantiate(optionButtonPrefab, optionsContent);
            optionButton.GetComponentInChildren<TMP_Text>().text = option.optionText;
            optionButton.onClick.AddListener(() => OnOptionSelected(option.nextLayerIndex));
        }
    }

    // 选项选择回调方法
    private void OnOptionSelected(int nextLayerIndex)
    {
        // 禁用选项面板
         optionsPanel.SetActive(false);
        currentLayerIndex = nextLayerIndex;
        DestroyOptionButtons();
        StartCoroutine(DisplayTextCoroutine());
    }

    // 下一个对话方法
    private void NextDialogue()
    {
        dialoguePanel.SetActive(false);
    

        currentLayerIndex++;

        if (currentLayerIndex < dialogueLayers.Length)
        {
            StartCoroutine(DisplayTextCoroutine());
        }
        else
        {
            EndDialogue();
        }
    }

    // 结束对话方法
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        optionsPanel.SetActive(false);
     PlayerManager.Instance.EnablePlayerMovement();
        currentLayerIndex = 0; // 重置当前对话层索引
    }

    // 销毁选项按钮方法
    private void DestroyOptionButtons()
    {
        foreach (Button button in optionsPanel.GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
    }
}
