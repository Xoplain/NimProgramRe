using NimProgramRe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Interface
{
    public interface IPlayer
    {
        void ChooseMove(Board currentBoard);
        void Win();
        void Lose();
    }
}
