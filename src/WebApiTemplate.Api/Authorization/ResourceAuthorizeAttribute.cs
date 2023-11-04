using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.SharedKernel.Constants;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class ResourceAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly PermissionResource _resource;
        private readonly PermissionType _requiredPermissions;        

        // Constructor: Initializes a new instance of the ResourceAuthorizeAttribute class.
        // Parameters:
        //   resource: The resource for which permissions are required.
        //   requiredPermissions: The required permissions for the resource.
        public ResourceAuthorizeAttribute(PermissionResource resource, params PermissionType[] requiredPermissions)
        {
            _resource = resource;
            // Aggregate the required permissions using bitwise OR.
            _requiredPermissions = requiredPermissions.Aggregate(PermissionType.None, (acc, p) => acc | p);
        }

        // Called when an action is being authorized.
        // Parameters:
        //   context: The authorization filter context.
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the current user from the HTTP context.
            var user = context.HttpContext.User;

            // Check if the user is authenticated.
            if (!user.Identity.IsAuthenticated)
            {
                // If not authenticated, return an Unauthorized result.
                context.Result = new UnauthorizedResult();
                return;
            }

            // Retrieve the permissions claim from the user's claims.
            var permissionsClaim = user.Claims.FirstOrDefault(c => c.Type == UserClaimTypes.Permissions);
            if (permissionsClaim == null)
            {
                // If the permissions claim is not found, return a Forbidden result.
                context.Result = new ForbidResult();
                return;
            }

            // Deserialize the permissions claim value to a dictionary.
            var permissions = JsonConvert.DeserializeObject<Dictionary<PermissionResource, PermissionType>>(permissionsClaim.Value);

            // Try to get the user's permissions for the specified resource.
            if (!permissions.TryGetValue(_resource, out var userPermissions) || (userPermissions & _requiredPermissions) != _requiredPermissions)
            {
                // If the required permissions are not set, return a Forbidden result.
                context.Result = new ForbidResult();
                return;
            }

            // If this point is reached, the user has the required permissions, and the action is authorized.
        }
    }
}
