using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Profiling;
using Tests.Framework.Profiling.Timeline;

namespace Tests.Search.Search
{
    public class SearchProfileTests
    {
        private readonly IElasticClient _client;

        public SearchProfileTests(ProfilingCluster cluster)
        {
            _client = cluster.Client;
        }

        [Timeline(Iterations = 1000)]
        public void Deserialization()
        {
            _client.Search<Developer>(s => s.Query(q => q.MatchAll()));
        }

        [Timeline(Iterations = 1000)]
        public void Serialization()
        {
            TestClient.DefaultInMemoryClient.Search<Developer>();
        }
    }

}
