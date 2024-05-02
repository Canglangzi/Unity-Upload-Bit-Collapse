using System;
using UnityEngine;

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
      public bool triggerTriggerAnimationsByButtonPress;

        [Header("Objects to Open")]
        [Tooltip("需要打开的游戏对象数组")]
        
        public GameObject[] gameObjectsToOpen;
        public bool  triggerOpenGameObjectsByButtonPress;

        [Header("Interaction Panel")]
        [Tooltip("是否显示交互提示面板")]
        public bool showInteractionPanelInRange = true;
        public GameObject interactionPanelInRange;

        [Header("Scene Transition")]
        [Tooltip("需要加载的场景名称")]
        public string[] sceneNames;
          public bool triggerTriggersceneByButtonPress;

        [Header("Trigger Tag")]
        public string TriggerTag;

        [Header("Subtitle Settings")]
        [Tooltip("是否启用字幕")]
        public bool enableSubtitles = true; // 是否启用字幕
        [Tooltip("是否按键触发字幕")]
        public bool triggerByButtonPress = true; // 是否按键触发字幕
        public string[] subtitleLines;
        public GameObject subtitlePanel; // 字幕面板

        [Header("Dialogue Settings")]
        [Tooltip("是否启用对话")]
        public bool enableDialogue = false;
        public DialogueSystem dialogueSystem;
        public DialogueLayer[] dialogueLayers;
        public float textSpeed = 0.05f;

        [Header("Dialogue Trigger Settings")]
        [Tooltip("是否按下交互键触发对话，否则直接触发")]
        public bool triggerDialogueByButtonPress = true;

        private InputManager _inputManager;
        private SubtitleSystem subtitleSystem; // 引用字幕系统的实例

        private bool isInRange = false;

       public  bool enableAnimations;
       public  bool enableScene;
        public  bool enableGameObjects;
        private void Start()
        {
            _inputManager = FindObjectOfType<InputManager>(); // 搜索场景中的 InputManager 组件
            subtitleSystem = FindObjectOfType<SubtitleSystem>(); // 获取字幕系统的实例
          dialogueSystem = FindObjectOfType<DialogueSystem>(); // 获取字幕系统的实例
              subtitlePanel = GameObject.Find("SubtitlePanel");

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TriggerTag))
            {
                isInRange = true;
                if (showInteractionPanelInRange && interactionPanelInRange != null)
                    interactionPanelInRange.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TriggerTag))
            {
                if (showInteractionPanelInRange && interactionPanelInRange != null)
                    interactionPanelInRange.SetActive(false);
                isInRange = false;
            }
        }

        private void Update()
        {
            if (isInRange && interactable && Input.GetKeyDown(_inputManager.interactKey))
            {
             
               if (triggerTriggerAnimationsByButtonPress)
                    TriggerAnimations();

                if (triggerTriggersceneByButtonPress)
                    LoadNextScene();

                if (triggerOpenGameObjectsByButtonPress)
                    OpenGameObjects();

                if (enableSubtitles && triggerByButtonPress)
                    ShowSubtitle();

                if (enableDialogue && triggerDialogueByButtonPress)
                    StartDialogue();
            }
           if (isInRange && !hasTriggered)
    {
        if (enableSubtitles && !triggerByButtonPress)
        {
            ShowSubtitle();
            hasTriggered = true;
          
        }

        if (enableDialogue && !triggerDialogueByButtonPress)
        {
            StartDialogue();
            hasTriggered = true;
          
        }
         if (enableAnimations && !triggerTriggerAnimationsByButtonPress)
        {
           TriggerAnimations();
            hasTriggered = true;
           
        }
      if ( enableScene && !triggerTriggersceneByButtonPress)
        {
            LoadNextScene();
            hasTriggered = true;
           
        }
         if ( enableGameObjects && !triggerOpenGameObjectsByButtonPress)
        {
            OpenGameObjects();
            hasTriggered = true;
            
        }
    }
        }
      
private bool hasTriggered = false;
        private void StartDialogue()
        {
            dialogueSystem.StartDialogue(dialogueLayers, textSpeed);
        }

        private void ShowSubtitle()
        {
            subtitlePanel.SetActive(true);
            subtitleSystem.ShowSubtitle(subtitleLines);
        }

        private void TriggerAnimations()
        {
            for (int i = 0; i < objectsToTrigger.Length; i++)
            {
                GameObject obj = objectsToTrigger[i];
                string animationName = animationNames[i];

                // 检查物体是否存在以及是否有动画组件
                if (obj != null && obj.TryGetComponent<Animator>(out Animator animator))
                {
                    animator.Play(animationName); // 播放动画
                }
            }
        }

        private void LoadNextScene()
        {
            if (sceneNames.Length > 0)
            {
                SceneLoader.Instance.LoadScene(sceneNames[0]);
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
