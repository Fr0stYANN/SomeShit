using GraphQL.Types;

namespace Server.GraphQl.InputTypes
{
    public class LoginInputType : InputObjectGraphType
    {
        public LoginInputType()
        {
            Name = "loginInput";
            Field<StringGraphType>("email");
            Field<StringGraphType>("password");
        }
    }
}
