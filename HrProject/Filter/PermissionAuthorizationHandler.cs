using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace HrProject.Filter
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirment>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
        {
            if (context.User == null)
                return;


            var canAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");
            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }

        }
    }
}
