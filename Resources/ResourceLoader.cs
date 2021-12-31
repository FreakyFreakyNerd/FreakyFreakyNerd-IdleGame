using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FreakyFreakyNerd.Resources
{
    public class ResourceLoader : MonoBehaviour
    {
        [SerializeField]
        private Sprite Missing;

        public static ResourceLoader Instance;
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Logger.Log("More Than One Resource Loader");
            }
        }
        public static Sprite GetSprite(string key)
        {
            return Instance.Missing;
        }
    }
}
