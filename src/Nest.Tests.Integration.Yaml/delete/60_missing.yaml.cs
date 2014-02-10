using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Delete
{
	public partial class Delete60MissingYaml60Tests
	{
		
		public class MissingDocumentWithCatch60Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				
				_status = this._client.Delete("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class MissingDocumentWithIgnore60Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("ignore","404")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
