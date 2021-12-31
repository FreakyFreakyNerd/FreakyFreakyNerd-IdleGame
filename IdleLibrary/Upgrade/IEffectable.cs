using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Upgrade
{
    public interface IEffectable
    {
        public void ApplyEffect(Effect effect);
        public void RemoveEffect(Effect effect);
    }
}
