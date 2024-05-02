using UnityEngine;

namespace CLz
{
    public class ObjectDisabler : MonoBehaviour
    {
        public GameObject[] objectsToDisable; // ��Ҫ�����õ���Ϸ��������

        private InputManager inputManager;

        void Start()
        {
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        }

        void Update()
        {
            // ��������˻���������������Ʒ���ڷ�Χ�ڣ������ָ������Ϸ��������
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
