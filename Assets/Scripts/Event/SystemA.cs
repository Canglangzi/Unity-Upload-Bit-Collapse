using UnityEngine;
public struct EventData
{
    public int Health;
    public string Name;
    // Add more fields as needed

    public EventData(int health, string name)
    {
        Health = health;
        Name = name;
    }
}

public class SystemA : MonoBehaviour
{
    private const int NumEventsPerSecond = 80; 

    private void Update()
    {
      
        for (int i = 0; i < NumEventsPerSecond; i++)
        {
           // EventData data = new EventData(100, "PlayerName");
          //  EventRelay<EventData>.Instance.InvokeEventAsync(CLZ_EventType.OnHealthChanged, data);

            EventRelay<string>.Instance.InvokeEventAsync(CLZ_EventType.OnFire, "game");
        }
    }
}