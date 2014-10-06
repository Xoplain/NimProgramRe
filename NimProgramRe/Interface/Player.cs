using NimProgramRe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Interface
{
    public abstract class Player
    {
        public abstract void ChooseMove(Board currentBoard);
    }
}
