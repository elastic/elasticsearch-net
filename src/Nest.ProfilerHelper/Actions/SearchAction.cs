using Nest.Tests.MockData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.ProfilerHelper.Actions
{
	public class SearchResultActionArgs
	{

	}
	public static class SearchResultAction
	{
		public static void SearchResult(SearchResultActionArgs args)
		{
			try
			{
				BaseAction.Setup();
				var status = GetValidConnectionStatus();
				var settings = BaseAction.Settings();
				var client = new ElasticClient(settings, new InMemoryConnection(settings, status));

				for (var i = 0; i < 100; i++)
				{
					var result = client.Search<object>(s => s
						.Size(2000)
						.AllIndices()
						.Types(typeof (Person), typeof (ElasticSearchProject))
						.MatchAll()
					);
					result.Documents.Count();
				}
			}
			finally
			{
				BaseAction.TearDown();
			}
		}

		private static ConnectionStatus GetValidConnectionStatus()
		{
			var result = BaseAction.Client.Search<object>(s => s
				  .Size(2000)
				  .AllIndices()
				  .Types(typeof(Person), typeof(ElasticSearchProject))
				  .MatchAll()
			);
			return result.ConnectionStatus;
		}
	}
}
