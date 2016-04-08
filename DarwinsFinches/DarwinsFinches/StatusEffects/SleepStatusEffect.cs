using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarwinsFinches.StatusEffects;

namespace DarwinsFinches.StatusEffects
{
    public class SleepStatusEffect : StatusEffect
    {
        public SleepStatusEffect(string name, int duration)
            : base(name, duration)
        {
        }
    }
}
