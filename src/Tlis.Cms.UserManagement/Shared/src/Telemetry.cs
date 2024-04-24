using System.Diagnostics;

namespace Tlis.Cms.UserManagement.Shared;

public static class Telemetry
{
    public static readonly string ServiceName = "cms-user-management-service";

    public static readonly ActivitySource ActivitySource = new(ServiceName);
}