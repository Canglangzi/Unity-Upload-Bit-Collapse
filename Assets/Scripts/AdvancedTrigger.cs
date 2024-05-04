using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CLz
{
    public class AdvancedTrigger : MonoBehaviour
    {
        [Header("Interactivity Settings")]
        [Tooltip("指示是否可与此对象交互")]
        public bool interactable = true;

        [Header("Trigger Objects and Animations")]
        [Tooltip("需要触发的物体数组")]
        public GameObject[] objectsToTrigger;
        [Tooltip("与物体数组对应的动画名称数组")]
        public string[] animationNames;
        [Tooltip("按下按钮触发动画")]
        public KeyCode animationTriggerKey;

        [Header("Objects to Open")]
        [Tooltip("需要打开的游戏对象数组")]
        public GameObject[] gameObjectsToOpen;
        [Tooltip("按下按钮打开游戏对象")]
        public KeyCode openObjectsTriggerKey;

        [Header("Interaction Panel")]
        [Tooltip("是否显示交互提示面板")]
        public bool showInteractionPanelInRange = true;
        public GameObject interactionPanelInRange;

        [Header("Scene Transition")]
        [Tooltip("需要加载的场景名称")]
        public string[] sceneNames;
        [Tooltip("按下按钮触发场景转换")]
        public KeyCode sceneTransitionKey;

        [Header("Subtitle Settings")]
        [Tooltip("是否启用字幕")]
        public bool enableSubtitles = true;
        [Tooltip("是否按键触发字幕")]
        public bool triggerSubtitlesByButtonPress = true;
        public string[] subtitleLines;
        public GameObject subtitlePanel;

        [Header("Dialogue Settings")]
        [Tooltip("是否启用对话")]
        public bool enableDialogue = false;
        public DialogueSystem dialogueSystem;
        public DialogueLayer[] dialogueLayers;
        public float textSpeed = 0.05f;

        [Header("Dialogue Trigger Settings")]
        [Tooltip("是否按下交互键触发对话，否则直接触发")]
        public bool triggerDialogueByButtonPress = true;

        private bool isInRange = false;
        private bool hasTriggered = false;

        private void Start()
        {
            dialogueSystem = FindObjectOfType<DialogueSystem>();
            subtitlePanel = GameObject.Find("SubtitlePanel");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInRange = true;
                if (showInteractionPanelInRange && interactionPanelInRange != null)
                    interactionPanelInRange.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (showInteractionPanelInRange && interactionPanelInRange != null)
                    interactionPanelInRange.SetActive(false);
                isInRange = false;
            }
        }

        private void Update()
        {
            if (isInRange && interactable)
            {
                if (triggerDialogueByButtonPress && Input.GetKeyDown(KeyCode.E))
                    StartDialogue();

                if (!triggerDialogueByButtonPress && enableDialogue && !hasTriggered)
                {
                    StartDialogue();
                    hasTriggered = true;
                }

                if (triggerSubtitlesByButtonPress && Input.GetKeyDown(KeyCode.F))
                    ShowSubtitle();

                if (!triggerSubtitlesByButtonPress && enableSubtitles && !hasTriggered)
                {
                    ShowSubtitle();
                    hasTriggered = true;
                }

                if (Input.GetKeyDown(animationTriggerKey))
                    TriggerAnimations();

                if (Input.GetKeyDown(openObjectsTriggerKey))
                    OpenGameObjects();

                if (Input.GetKeyDown(sceneTransitionKey))
                    LoadNextScene();
            }
        }

        private void StartDialogue()
        {
            dialogueSystem.StartDialogue(dialogueLayers, textSpeed);
        }

        private void ShowSubtitle()
        {
            subtitlePanel.SetActive(true);
            // 根据需求显示字幕
        }

        private void TriggerAnimations()
        {
            for (int i = 0; i < objectsToTrigger.Length; i++)
            {
                GameObject obj = objectsToTrigger[i];
                string animationName = animationNames[i];

                if (obj != null && obj.TryGetComponent<Animator>(out Animator animator))
                {
                    animator.Play(animationName);
                }
            }
        }

        private void LoadNextScene()
        {
            if (sceneNames.Length > 0)
            {
                SceneManager.LoadScene(sceneNames[0]);
            }
        }

        private void OpenGameObjects()
        {
            foreach (GameObject obj in gameObjectsToOpen)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
