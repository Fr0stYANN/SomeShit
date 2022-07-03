using GraphQL.Types;
using Server.Business.Entities;

namespace Server.GraphQl.GraphTypes
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(_ => _.Id, type: typeof(IdGraphType));
            Field(_ => _.Email, type: typeof(StringGraphType));
            Field(_ => _.Lastname,type: typeof(StringGraphType));
            Field(_ => _.Firstname,type: typeof(StringGraphType));
            Field(_ => _.Password, type: typeof(StringGraphType));
        }
    }
}
