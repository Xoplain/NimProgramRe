using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    class StateValue
    {
        public double AccumulatedValue { get; set; }
        public int Occurences { get; set; }

        public void UpdateValue(double val) {
            Occurences++;
            AccumulatedValue += val;
        }

        public double GetAverage()
        {
            return AccumulatedValue / Occurences;
        }
    }
}
