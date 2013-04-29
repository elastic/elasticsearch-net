using Nest.Tests.MockData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.ProfilerHelper.Actions
{
	public class SearchActionArgs
	{

	}
	public static class SearchAction
	{
		public static void Search(SearchActionArgs args)
		{
			var settings = BaseAction.Settings();
			var client = new ElasticClient(settings, new InMemoryConnection(settings));
			for (var i = 0; i < 10000; i++)
			{
				var term = i.ToString();
				var result = client.Search<ElasticSearchProject>(s => s
						.Query(q=>
							q.Term(p=>p.Name, term) || q.Term(p=>p.Followers.First().FirstName, term)
						)
					);
			}
		}
	}
}
