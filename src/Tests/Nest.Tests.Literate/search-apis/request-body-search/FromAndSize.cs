using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Literate.search_apis.request_body_search
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
			public Serializes() : base(
				ExpectedJson: new
				{
					from = 10,
					size = 12
				},
				InitializerExample: new SearchRequest<object>
				{
					From = 10,
					Size = 12
				},
				FluentExample: s => s
					.Size(12)
					.From(10)
			) {}
		}
	}
}
