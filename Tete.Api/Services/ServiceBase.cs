using Tete.Api.Contexts;
using Tete.Models.Authentication;

namespace Tete.Api.Services
{
    public abstract class ServiceBase
    {
        protected MainContext mainContext;

        protected UserVM Actor;
    }
}