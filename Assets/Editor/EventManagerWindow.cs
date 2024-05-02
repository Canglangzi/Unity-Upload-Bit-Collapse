using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class EventManagerWindow : EditorWindow
{
    private List<string> eventNames = new List<string>();
    private string newEventName = "";

    [MenuItem("Window/Event Manager")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(EventManagerWindow));
    }

    void OnGUI()
    {
       
    }
}
