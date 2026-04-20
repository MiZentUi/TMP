using System.Collections.Generic;

namespace LR13_14.UI.ViewModels.Pages.Routes;

public abstract class RouteViewModelBase : PageViewModelBase
{
    public IDictionary<string, object>? Parameters { get; set; }
}