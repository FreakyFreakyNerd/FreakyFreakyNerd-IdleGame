using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public static Logger Instance;
    public delegate void StringDelegate(string info);
    public static StringDelegate LogHandler;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LogHandler += Debug.Log;
        }
        else
        {
            Debug.Log("Logger already Instanced, Will Destroy: " + name);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Log(object info)
    {
        if (LogHandler != null)
            LogHandler(info.ToString());
    }
}
