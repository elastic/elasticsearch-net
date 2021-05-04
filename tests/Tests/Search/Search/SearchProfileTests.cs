// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

#if FEATURE_PROFILING
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
#endif
