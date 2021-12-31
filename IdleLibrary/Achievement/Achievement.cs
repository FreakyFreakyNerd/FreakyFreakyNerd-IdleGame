using FreakyFreakyNerd.Language;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FreakyFreakyNerd.IdleLibrary.Achievement;

namespace FreakyFreakyNerd.IdleLibrary.Achievement
{
    public class GameAchievement : IHookable, ILocalizable, IDelayedTickable
    {
        private string ID;
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        private RequirementContainer Requirements = new RequirementContainer();

        public bool Obtained { get; private set; } = false;

        public GameAchievement(string id)
        {
            ID = id;
            LanguageManager.OnLanguageLoad += OnLanguageLoad;
        }
        public GameAchievement(string id, params IRequirement[] args)
        {
            ID = id;
            LanguageManager.OnLanguageLoad += OnLanguageLoad;
        }

        public GameAchievement AddRequirement(IRequirement requirement)
        {
            if (!Requirements.Contains(requirement))
            {
                Requirements.Add(requirement);
            }
            return this;
        }

        public GameAchievement AddRequirements(params IRequirement[] args)
        {
            this.AddRequirementArray(args);
            return this;
        }

        public GameAchievement AddRequirementArray(IRequirement[] requirements)
        {
            for(int i = 0; i < requirements.Length; i++)
            {
                AddRequirement(requirements[i]);
            }
            return this;
        }

        private bool HasRequirements()
        {
            foreach(IRequirement r in Requirements)
            {
                if (!r.HasRequirement())
                    return false;
            }
            return true;
        }
        public void CheckForObtain()
        {
            if (this.Obtained)
                return;
            if (this.HasRequirements())
                this.Obtained = true;
        }

        public void OnLanguageLoad()
        {
            DisplayName = LanguageManager.GetLocalizedText(string.Format("achievmeent.{0}.name", ID));
            Description = LanguageManager.GetLocalizedText(string.Format("achievmeent.{0}.description", ID));
        }

        public void OnDelayedTick()
        {
            this.CheckForObtain();
        }

        public override void SetupHooks()
        {
            LanguageManager.OnLanguageLoad += OnLanguageLoad;
            GameManager.DelayedTick += OnDelayedTick;
        }

        public string GetDisplayName()
        {
            return DisplayName;
        }
    }
}