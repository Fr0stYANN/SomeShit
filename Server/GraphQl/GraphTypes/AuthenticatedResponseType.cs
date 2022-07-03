using GraphQL.Types;

namespace Server.GraphQl.GraphTypes
{
    public class AuthenticatedResponseType : ObjectGraphType<AuthenticatedResponse>
    {
        public AuthenticatedResponseType()
        {
            Field(_ => _.Token, type: typeof(StringGraphType));
            Field(_ => _.RefreshToken, type: typeof(StringGraphType));
            Field(_ => _.ValidTo, type: typeof(DateTimeGraphType));
        }
    }
}
