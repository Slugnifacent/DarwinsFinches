using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarwinsFinches
{
    public abstract class StatusEffect
    {
        public string Name  { get; set; }
        public int Duration { get; set; }

        public StatusEffect(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }
        public virtual void ApplyEffect(GameObject unit)
        {
            
        }
    }
}
