using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Suggest
{
	[TestFixture]
	public class SuggestRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public SuggestRequestTests()
		{
			var request = new SuggestRequest()
			{
				AllIndices = true,
				AllowNoIndices = true,
				ExpandWildcards = ExpandWildcards.Closed,
				GlobalText = "global suggest text",
				Suggest = new Dictionary<string, ISuggester>
				{
					{ "terms_sug", new CompletionSuggester
					{
						Analyzer = "standard",
						Field = Property.Path<ElasticsearchProject>(p=>p.Content),
						Size = 4,
						ShardSize = 10,
						Fuzzy = new FuzzySuggester
						{
							Fuzziness = Fuzziness.Ratio(0.3),
							PrefixLength = 4
						}
					} 
					}
				}
			};
			var response = this._client.Suggest(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_all/_suggest?allow_no_indices=true&expand_wildcards=closed");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void SuggestBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
