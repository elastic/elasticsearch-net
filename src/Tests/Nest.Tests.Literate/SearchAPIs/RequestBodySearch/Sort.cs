using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Literate.SearchAPIs.RequestBodySearch
{
    public class Sort
    {
        public class Serializes : SerializationTests<ISearchRequest, SearchDescriptor<object>, SearchRequest<object>>
        {
            public Serializes() : base(
                ExpectedJson: new
                {
                    sort = new object[]
                    {
                        new { post_date = new { order = "asc" } },
                        new { name = new { order = "desc" } },
                        new { age = new { order = "desc" } },
                        new { _score = new { order = "desc" } },
                    }
                },
                Initializer: new SearchRequest<object>
                {
                    Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>
                    {
                        new KeyValuePair<PropertyPathMarker, ISort>("post_date", new Nest.Sort { Order = SortOrder.Ascending }),
                        new KeyValuePair<PropertyPathMarker, ISort>("name", new Nest.Sort { Order = SortOrder.Descending }),
                        new KeyValuePair<PropertyPathMarker, ISort>("age", new Nest.Sort { Order = SortOrder.Descending }),
                        new KeyValuePair<PropertyPathMarker, ISort>("_score", new Nest.Sort())
                    }
                },
                Fluent: s => s
                    .Sort(sort => sort.OnField("post_date").Order(SortOrder.Ascending))
                    .Sort(sort => sort.OnField("name").Order(SortOrder.Descending))
                    .Sort(sort => sort.OnField("age").Order(SortOrder.Descending))
                    .Sort(sort => sort.OnField("_score"))
            )
            { }
        }

        public class Usage : EndpointUsageTests<ISearchRequest, SearchDescriptor<object>, SearchRequest<object>, ISearchResponse<object>>
        {
            public override int ExpectStatusCode => 200;

            public override bool ExpectIsValid => true;

            public override void AssertUrl(string url) => url.Should().EndWith("_search");

            protected override ISearchResponse<object> Initializer(IElasticClient client) =>
                client.Search<object>(new SearchRequest()
                {
                    
                    Size = 12
                });

            protected override ISearchResponse<object> Fluent(IElasticClient client) =>
                client.Search<object>(s => s
                    .From(10)
                    .Size(12)
                );
        }
    }
}
