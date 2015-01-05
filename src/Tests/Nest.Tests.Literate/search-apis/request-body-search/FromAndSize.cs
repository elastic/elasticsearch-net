using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Literate.search_apis.request_body_search
{
	public class FromAndSize : LiterateTests<ISearchRequest, SearchDescriptor<object>, SearchRequest<object>>
	{
		/**
		 * Pagination of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */
		public override object ExpectedJson
		{
			get
			{
				return new
				{
					from = 10,
					size = 12
				};
			}
		}

		public override SearchRequest<object> InitializerExample
		{
			get
			{
				return new SearchRequest<object> { Size = 12, From = 10 };
			}
		}

		public override SearchDescriptor<object> FluentExample(SearchDescriptor<object> descriptor)
		{
			return descriptor.Size(12).From(12);
		}
	}
}
