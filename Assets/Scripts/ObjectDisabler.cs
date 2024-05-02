using UnityEngine;

namespace CLz
{
    public class ObjectDisabler : MonoBehaviour
    {
        public GameObject[] objectsToDisable; // 需要被禁用的游戏对象数组

        private InputManager inputManager;

        void Start()
        {
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        }

        void Update()
        {
            // 如果按下了互动按键，并且物品处于范围内，则禁用指定的游戏对象数组
            if (Input.GetKeyDown(inputManager.interactKey))
            {
                foreach (GameObject obj in objectsToDisable)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
