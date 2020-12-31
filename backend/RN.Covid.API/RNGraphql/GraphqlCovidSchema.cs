using GraphQL.Types;

namespace RN.Covid.API.RNGraphql
{
    public class GraphqlCovidSchema : Schema
    {
        public GraphqlCovidSchema()
        {
            Query = new GraphqlCovidQuery();
        }
    }
}
