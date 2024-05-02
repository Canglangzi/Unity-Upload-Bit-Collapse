using UnityEngine;
using System.Collections.Generic; // 引入 List 类型所在的命名空间
using TMPro;


 namespace  CLz
{
    public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel; // 对话面板
    public DialogueSystem dialogueSystem; // 对话系统脚本

    public DialogueLayer[] dialogueLayers;

    private InputManager inputManager;
    private bool isInRange = false; // 是否在触发范围内
    public float textSpeed = 0.05f; // 文本显示速度，每个字符显示的时间间隔

    private void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(inputManager.interactKey))
        {
            StartDialogue(); // 触发对话
          PlayerManager.Instance. DisablePlayerMovement();


        }
    }

    private void StartDialogue()
    {
        dialogueSystem.StartDialogue(dialogueLayers, textSpeed);
    }
}

}
