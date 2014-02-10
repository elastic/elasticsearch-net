using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Get
{
	public partial class Get80MissingYaml80Tests
	{
		
		public class MissingDocumentWithCatch80Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public MissingDocumentWithCatch80Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void MissingDocumentWithCatchTests()
			{

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);
			}
		}
		
		public class MissingDocumentWithIgnore80Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public MissingDocumentWithIgnore80Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void MissingDocumentWithIgnoreTests()
			{

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);
			}
		}
	}
}
