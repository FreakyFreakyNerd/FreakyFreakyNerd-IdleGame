using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Prestige
{
    public class ResetImmunity
    {
        private List<Reset> ResetImmunities = new List<Reset>();

        public bool Immune(Reset reset)
        {
            return ResetImmunities.Contains(reset);
        }

        public void AddImmunity(Reset reset)
        {
            if (!Immune(reset))
                ResetImmunities.Add(reset);
        }

        public void AddImmunitys(Reset[] resets)
        {
            for(int i = 0; i < resets.Length; i++)
            {
                AddImmunity(resets[i]);
            }
        }
        public void RemoveImmunity(Reset reset)
        {
            if (Immune(reset))
                ResetImmunities.Remove(reset);
        }

        public void RemoveImmunitys(Reset[] resets)
        {
            for (int i = 0; i < resets.Length; i++)
            {
                RemoveImmunity(resets[i]);
            }
        }
    }
}
