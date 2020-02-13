using System.Collections.Generic;

namespace LeTai.Selections
{
public interface Selector
{
    int GetSelected(IEnumerable<ISelectable> selectables, ICollection<ISelectable> result);
}
}
