using System.Collections.Generic;

namespace LeTai.Selections
{
public interface ISelector
{
    int GetSelected(IEnumerable<ISelectable> selectables, ICollection<ISelectable> result);
}
}
