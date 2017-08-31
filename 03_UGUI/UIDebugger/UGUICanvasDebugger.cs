using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ver 0.0.0.1版只提供了最最基础的显示10条log的功能
/// </summary>
public class UGUICanvasDebugger : MonoBehaviour {

    [HideInInspector]
    public List<string> cached_log = new List<string>();
    public int log_preserve = 10;
    public UnityEngine.UI.Text txt_content;
    
    void OnEnable() 
    {
        Application.logMessageReceived += Application_logMessageReceived;
    }

    void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error)
            cached_log.Add("ERROR::" + condition);
        else
            cached_log.Add(condition);

        if (cached_log.Count > log_preserve ) 
        {
            cached_log.RemoveAt(0);
        }

        string result = "";
        foreach (var str in cached_log) 
        {
            result += str + "\n";
        }

        txt_content.text = result;
    }

    void OnDisable() 
    {
        Application.logMessageReceived -= Application_logMessageReceived;
    }
	
}
