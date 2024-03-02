using Hangfire.Dashboard;

namespace snglrtycrvtureofspce.Core.Filters;

public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}