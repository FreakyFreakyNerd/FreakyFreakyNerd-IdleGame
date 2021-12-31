using FreakyFreakyNerd.IdleLibrary.Achievement;
using FreakyFreakyNerd.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Prestige
{
    public class GamePrestige : IHookable, ILocalizable
    {
        string ID;
        string DisplayName;
        private Reset PrestigeReset;
        private RewardHandler Rewards;

        public GamePrestige(string id, Reset reset)
        {
            ID = id;
            PrestigeReset = reset;
        }

        public string GetDisplayName()
        {
            return DisplayName;
        }

        public void OnLanguageLoad()
        {
            DisplayName = LanguageManager.GetLocalizedText(String.Format("prestige.{0}.name", ID));
        }

        public override void SetupHooks()
        {
            LanguageManager.OnLanguageLoad += OnLanguageLoad;
        }
    }
}
