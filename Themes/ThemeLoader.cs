using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThemeLoader : MonoBehaviour
{
    [SerializeField]
    string key;
    // Start is called before the first frame update
    void Start()
    {
        if (key == null || key == "")
        {
            key = "ui." + gameObject.name.ToLower().Replace(" ", "_") + ".name";
        }
        ThemeManager.OnThemeLoad += OnThemeLoad;
    }

    internal abstract void OnThemeLoad();
}
