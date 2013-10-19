using Nest.Tests.MockData.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.ProfilerHelper.Actions
{
	public class SerializationActionArgs
	{

	}
	public static class SerializationAction
	{
		public static void DoLoadsOfSerializations(SerializationActionArgs args)
		{
			var settings = BaseAction.Settings();
			var client = new ElasticClient(settings, new InMemoryConnection(settings));
			for (var i = 0; i < 10000; i++)
			{
				var term = i.ToString();
				var result = client.Search<ElasticSearchProject>(s => s
						.Query(q =>
							q.Term(p => p.Name, term) || q.Term(p => p.Followers.First().FirstName, term)
						)
					);
			}
		

			//var json = File.ReadAllText(@"Json\searchresponse.json");
			//for (var i = 0; i < 100; i++)
			//{
			//	var results = client.Serializer.Deserialize<QueryResponse<ElasticSearchProject>>(json);
			//	if (results.Documents.Count() != 10 || results.Documents.Any(d => string.IsNullOrEmpty(d.Name)))
			//		throw new Exception("we did not map properly");
			//}
		}

		public class TempResponse<T> where T : class
		{
			public int Took { get; set; }
		}
	}
}
