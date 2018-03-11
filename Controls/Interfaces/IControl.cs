using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_GL
{
    public interface IControl
    {
        bool Update();
        void Draw();
    }
}
