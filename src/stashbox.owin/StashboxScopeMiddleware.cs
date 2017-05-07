using System.Threading.Tasks;
using Microsoft.Owin;
using Stashbox.Infrastructure;

namespace Stashbox.Owin
{
    internal class StashboxScopeMiddleware : OwinMiddleware
    {
        private readonly IStashboxContainer container;

        public StashboxScopeMiddleware(OwinMiddleware next, IStashboxContainer container) : base(next)
        {
            this.container = container;
        }

        public override async Task Invoke(IOwinContext context)
        {
            using (var scope = this.container.BeginScope())
            {
                context.Set(Constants.LifetimeScopeKey, scope);
                await base.Next.Invoke(context);
            }
        }
    }
}
