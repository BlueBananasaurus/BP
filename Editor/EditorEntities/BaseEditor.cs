using System.Collections.Generic;

namespace Monogame_GL
{
    public class BaseEditor
    {
        protected List<ItemHolder> _items;

        protected BaseEditor()
        {
            _items = new List<ItemHolder>();
        }
    }
}