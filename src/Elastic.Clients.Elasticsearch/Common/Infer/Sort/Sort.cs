using System;
using System.Collections.Generic;
using System.Text;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Global.Search
{
	public partial class Sort
	{
		// This is temporary
		public Sort(IEnumerable<SortCombinations> sortCombinations) => _sortCombinationsList.AddRange(sortCombinations);

		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}
}
