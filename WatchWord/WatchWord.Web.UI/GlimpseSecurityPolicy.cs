using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;

namespace WatchWord.Web.UI
{
    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            var httpContext = policyContext.GetHttpContext();
            //TODO: max is for test. todo: asp identity roles.
            return httpContext.User.Identity.Name == "max" ? RuntimePolicy.On : RuntimePolicy.Off;
        }

        public RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource; }
        }
    }
}