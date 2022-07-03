using GraphQL.Types;
using Server.GraphQl.Queries;

namespace Server.GraphQl.AppSchema
{
    public class AppSchema : Schema, ISchema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<UserQueries>();
        }    
    }
}
