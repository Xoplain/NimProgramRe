using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    [Serializable()]
    class StateValue
    {
        public double AccumulatedValue { get; set; }
        public int Occurences { get; set; }

        public StateValue()
        {
            AccumulatedValue = 0;
            Occurences = 0;
        }

        public void UpdateValue(double val) {
            Occurences++;
            AccumulatedValue += val;
        }

        public double GetAverage()
        {
            return (Occurences == 0) ? 0 : AccumulatedValue / Occurences;
        }
    }
}
