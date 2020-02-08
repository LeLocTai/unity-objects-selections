using System.Collections.Generic;

namespace LeTai.Selections
{
public abstract class Selector
{
    SelectablesManager selectablesManager;

    public abstract int GetSelected(IEnumerable<ISelectable> selectables, ICollection<ISelectable> result);
}
}
