using System.Collections.Generic;

namespace LeTai.Selections
{
public abstract class Selector
{
    SelectablesManager selectablesManager;

    public abstract int GetSelected(ref List<Selectable> result);
}
}