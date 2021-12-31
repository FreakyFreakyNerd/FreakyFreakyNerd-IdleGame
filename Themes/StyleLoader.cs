using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakyFreakyNerd.Themes
{
    public abstract class StyleLoader : MonoBehaviour
    {
        [SerializeField]
        string key;
        // Start is called before the first frame update
        void Start()
        {
            if (key == null || key == "")
            {
                key = "ui." + gameObject.name.ToLower().Replace(" ", "_") + ".style";
            }
            StyleManager.OnStyleLoad += OnStyleLoad;
        }

        internal abstract void OnStyleLoad();
    }
}
