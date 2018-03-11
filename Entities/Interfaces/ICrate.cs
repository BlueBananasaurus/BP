using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_GL
{
    public interface ICrate
    {
        RectangleF Boundary { get; set; }
        void Update();
        void Open();
        void Draw();
        void DrawNormal();
    }
}
