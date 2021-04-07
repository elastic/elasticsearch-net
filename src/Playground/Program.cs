using System;
using System.Threading.Tasks;
using Nest;

namespace Playground
{
	internal class Program
    {
	    private static async Task Main()
	    {
			IElasticClient client = new ElasticClient(new Uri("http://localhost:9600"));

		    var request = new ClusterHealthRequest
		    {
			    Level = Level.Cluster
		    };

		    var response = await client.Cluster.HealthAsync(request);

		    if (response.IsValid)
		    {
			    Console.WriteLine($"Cluster Name: {response.ClusterName}");
				Console.WriteLine($"Status: {response.Status:G}");
				Console.WriteLine($"Active Primaries: {response.ActivePrimaryShards}");
			}

		    // TODO: It might be kinda nice if the client accepts no request here and uses a cached default instance
		    //var searchResponse = await client.SearchAsync(new SearchRequest());

			//if (searchResponse.IsValid)
			//{
			//	Console.WriteLine($"Took: {searchResponse.Took}");
			//}
		}
    }
}
