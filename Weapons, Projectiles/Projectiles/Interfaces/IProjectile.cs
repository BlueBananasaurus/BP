using System.Collections.Generic;

namespace Monogame_GL
{
    public interface IProjectile
    {
        void Draw();

        void DrawLight();

        void DrawNormal();

        void Update(Map map);
    }
}