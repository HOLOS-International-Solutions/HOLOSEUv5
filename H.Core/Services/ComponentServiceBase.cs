using H.Core.Models;

namespace H.Core.Services;

public abstract class ComponentServiceBase : IComponentService
{
    public string GetUniqueComponentName(Farm farm, ComponentBase component)
    {
        var i = 2;

        // Don't add number to component name at first (i.e. just use "Cow-Calf" and not "Cow-Calf #1").
        var proposedName = component.ComponentNameDisplayString;

        // While the names are the same, try and make a unique name for this component.
        while (farm.Components.Where(x => string.IsNullOrWhiteSpace(x.Name) == false).Any(y => y.Name.Equals(proposedName)))
        {
            proposedName = component.ComponentNameDisplayString + " #" + (i++);
        }

        return proposedName;
    }
}