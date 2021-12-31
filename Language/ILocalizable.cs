using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.Language
{
    public interface ILocalizable
    {
        public void OnLanguageLoad();
        public string GetDisplayName();
    }
}