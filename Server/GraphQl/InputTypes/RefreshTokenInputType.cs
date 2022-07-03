using GraphQL.Types;

namespace Server.GraphQl.InputTypes
{
    public class RefreshTokenInputType : InputObjectGraphType
    {
        public RefreshTokenInputType()
        {
            Name = "refreshTokenInput";
            Field<StringGraphType>("accessToken");
            Field<StringGraphType>("refreshToken");
        }
    }
}
