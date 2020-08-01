using GraphQL.API.Types;
using GraphQL.Types;
using NHLStats.Api.Helpers;

namespace GraphQL.API.Models
{
    public class NHLStatsQuery : ObjectGraphType
    {
        public NHLStatsQuery(ContextServiceLocator contextServiceLocator)
        {
            Field<PlayerType>(
                "player",
                arguments: new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id", Description = "Id del jugador a consultar" }),
                resolve: context => contextServiceLocator.PlayerRepository.Get(context.GetArgument<int>("id")));

            Field<ListGraphType<PlayerType>>(
            "players",
            resolve: context => contextServiceLocator.PlayerRepository.All());
        }
    }
}
