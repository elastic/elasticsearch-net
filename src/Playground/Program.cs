using System;
using Nest;

namespace Playground
{
	internal class Program
    {
	    private static void Main(string[] args)
	    {
		    var client = new ElasticClient(new Uri("http://localhost:9200"));
			
	    }
    }
}
