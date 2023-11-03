namespace server.Services.UserServices.Authorization.Basic.Attributes
{
    // Decorator [AllowAnonymous] - allows anonymous access to specified routes
    // that are decorated with the [Authorize] attribute
    
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute { }
}
