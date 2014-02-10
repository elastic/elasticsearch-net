using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Bulk
{
	public partial class Bulk10BasicYaml10Tests
	{
		
		public class ArrayOfObjects10Tests
		{
			private readonly RawElasticClient _client;
		
			public ArrayOfObjects10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ArrayOfObjectsTests()
			{

				//do bulk 
				this._client.BulkPost("SERIALIZED BODY HERE", nv=>nv);

				//do count 
				this._client.CountGet("test_index", nv=>nv);
			}
		}
	}
}