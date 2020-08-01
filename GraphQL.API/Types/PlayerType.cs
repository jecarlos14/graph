using GraphQL.Types;
using NHLStats.Api.Helpers;
using NHLStats.Core.Models;

namespace GraphQL.API.Types
{
    public class PlayerType : ObjectGraphType<Player>
    {
        public PlayerType(ContextServiceLocator contextServiceLocator)
        {
            Field(x => x.Id);
            // El true nos define si acepta o no nulo
            Field(x => x.Name, true);
            Field(x => x.BirthPlace);
            Field(x => x.Height);
            Field(x => x.WeightLbs);

        }
    }
}
