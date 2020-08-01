using GraphQL.API.Models;
using GraphQL.Types;

namespace GraphQL.API
{
    public class NHLStatsSchema : Schema
    {
        public NHLStatsSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<NHLStatsQuery>();
        }
    }
}
