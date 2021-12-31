using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Prestige
{
    public interface IResetable
    {
        public void OnReset(Reset reset);
    }
}
