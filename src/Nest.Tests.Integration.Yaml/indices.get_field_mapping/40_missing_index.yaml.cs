using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMapping40MissingIndexYaml40Tests
	{
		
		public class Raise404WhenIndexDoesntExist40Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Raise404WhenIndexDoesntExist40Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void Raise404WhenIndexDoesntExistTests()
			{

				//do indices.get_field_mapping 
				
				_status = this._client.IndicesGetFieldMapping("test_index", "type", "field");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
