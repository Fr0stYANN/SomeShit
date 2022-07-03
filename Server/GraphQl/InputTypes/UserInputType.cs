using GraphQL.Types;

namespace Server.GraphQl.InputTypes
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "userInput";
            Field<StringGraphType>("email");
            Field<StringGraphType>("password");
            Field<StringGraphType>("lastname");
            Field<StringGraphType>("firstname");
        }
    }
}
