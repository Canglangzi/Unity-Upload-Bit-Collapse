using UnityEngine;

public class SystemA : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          
            EventRelay<int>.Instance.InvokeEventAsync(EventType.OnFire, 10);
        }
    }
}