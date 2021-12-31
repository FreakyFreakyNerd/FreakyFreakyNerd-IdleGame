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
        private RequirementContainer PrestigeRequirements;

        public GamePrestige(string id, Reset reset)
        {
            ID = id;
            PrestigeReset = reset;
        }

        public GamePrestige AddRequirement(IRequirement requirement)
        {
            PrestigeRequirements.AddRequirement(requirement);
            return this;
        }
        public GamePrestige AddRequirements(params IRequirement[] requirements)
        {
            PrestigeRequirements.AddRequirements(requirements);
            return this;
        }
        public GamePrestige AddRequirementArray(IRequirement[] requirements)
        {
            PrestigeRequirements.AddRequirements(requirements);
            return this;
        }

        public GamePrestige AddReward(IReward reward)
        {
            Rewards.AddReward(reward);
            return this;
        }
        public GamePrestige AddRewards(params IReward[] rewards)
        {
            Rewards.AddRewards(rewards);
            return this;
        }
        public GamePrestige AddRewardArray(IReward[] rewards)
        {
            Rewards.AddRewards(rewards);
            return this;
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

        public bool CanPrestige()
        {
            return PrestigeRequirements.HasRequirements();
        }

        public void Prestige()
        {
            if (!CanPrestige())
                return;
            Rewards.ApplyRewards();
            DoReset();
        }

        private void DoReset()
        {
            PrestigeReset.DoReset();
        }
    }
}
