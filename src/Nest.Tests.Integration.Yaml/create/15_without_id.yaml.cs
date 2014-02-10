using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Create
{
	public partial class Create15WithoutIdYaml15Tests
	{
		
		public class CreateWithoutId15Tests
		{
			private readonly RawElasticClient _client;
		
			public CreateWithoutId15Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void CreateWithoutIdTests()
			{

				//do create 
				this._client.IndexPost("test_1", "test", "SERIALIZED BODY HERE", nv=>nv);

				//do get 
				this._client.Get("test_1", "test", "$id", nv=>nv);
			}
		}
	}
}