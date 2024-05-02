using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerClz : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.Instance.TriggerEvent("OnPlayerDeath");
        }
    }
}