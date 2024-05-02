using UnityEngine;

public class TriggerAnimationController : MonoBehaviour
{
    public GameObject targetObject;

    public string targetAnimation;
    public string currentString; // ���ڴ洢��ǰ����״̬����
    private Animator targetAnimator;
    public Animator �ж�;
    public string TriggerTag;

    void Start()
    {
        targetAnimator = targetObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("��������룡");
        // ����Ƿ�ΪĿ�����
        if (other.CompareTag(TriggerTag) && targetAnimator != null)
        {
            Debug.Log("���봥������");

            AnimatorStateInfo currentState = �ж�.GetCurrentAnimatorStateInfo(0);
            Debug.Log("��ǰ����״̬��" + currentState.fullPathHash);

            // ��鵱ǰ����״̬�Ƿ���ָ���Ķ���״̬�������
            if (currentState.IsName(currentString))
            {
                // ����Ŀ�궯��
                targetAnimator.Play(targetAnimation);
                Debug.Log("���Ŷ�����" + targetAnimation);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // �ж��Ƿ�ΪĿ������˳��˴�������Χ��
        if (other.gameObject == targetObject)
        {
            Debug.Log("�뿪��������");
        }
    }
}
