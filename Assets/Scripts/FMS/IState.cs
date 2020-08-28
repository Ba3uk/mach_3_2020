using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS
{
    public interface IState
    {
        void Enter(Logic.Grid grid);

        void Execute();

        void Exit();
    }
}
