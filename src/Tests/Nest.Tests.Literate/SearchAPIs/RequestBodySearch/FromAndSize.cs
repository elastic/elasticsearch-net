using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Nest.Tests.Literate;
using Ploeh.AutoFixture;
using Xunit;

namespace SearchApis.RequestBody
{
	public class FromAndSize 
	{
		/**
		 * Pagination of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */

		public class Serializes : SerializationTests<ISearchRequest, SearchDescriptor<object>, SearchRequest<object>>
		{
			static int from = Create<int>();
			static int size = Create<int>();

			public Serializes() : base(
				ExpectedJson: new { from = from, size = size },
				Initializer: new SearchRequest<object>
				{
					From = from,
					Size = size
				},
				Fluent: s => s.Size(size).From(from)
			) {}
		}

		public class Usage : EndpointUsageTests<ISearchRequest, SearchDescriptor<object>, SearchRequest<object>, ISearchResponse<object>>
		{
			public override int ExpectStatusCode() { return 200; }

			public override bool ExpectIsValid() { return true; }

			public override void AssertUrl(string url)
			{
				throw new NotImplementedException();
			}

			protected override ISearchResponse<object> Initializer(IElasticClient client)
			{
				return client.Search<object>(new SearchRequest()
				{
					From = 10,
					Size = 12
				});
			}

			protected override ISearchResponse<object> Fluent(IElasticClient client)
			{
				return client.Search<object>(s => s
					.From(10)
					.Size(12)
				);
			}
		}
	}
}
