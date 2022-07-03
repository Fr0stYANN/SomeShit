using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Server.GraphQl;

namespace Server.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly ISchema schema;
        private readonly IDocumentExecuter documentExecuter;
        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            this.schema = schema;
            this.documentExecuter = documentExecuter;
        }

        [HttpPost] async Task<OkObjectResult> Post([FromBody] QueryDTO query)
        {
            var result = await documentExecuter.ExecuteAsync(_ =>
               {
                   _.Schema = schema;
                   _.Query = query.Query;
               }
             ).ConfigureAwait(false);
            return Ok(result.Data);
        }
    }
}
