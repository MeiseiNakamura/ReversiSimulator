using DxFramework.Reversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework.Reversi
{
    public interface ILookaheadBase
    {
        Int2 lookahead(Game reversi);
    }
}
