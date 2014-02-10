using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Delete
{
	public partial class Delete60MissingYaml60Tests
	{
		
		public class MissingDocumentWithCatch60Tests
		{
			private readonly RawElasticClient _client;
		
			public MissingDocumentWithCatch60Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void MissingDocumentWithCatchTests()
			{

				//do delete 
				this._client.Delete("test_1", "test", "1", nv=>nv);
			}
		}
		
		public class MissingDocumentWithIgnore60Tests
		{
			private readonly RawElasticClient _client;
		
			public MissingDocumentWithIgnore60Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void MissingDocumentWithIgnoreTests()
			{

				//do delete 
				this._client.Delete("test_1", "test", "1", nv=>nv);
			}
		}
	}
}