using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Bulk
{
	public partial class Bulk10BasicYaml10Tests
	{
		
		public class ArrayOfObjects10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				_body = @"{""index"":{""_index"":""test_index"",""_type"":""test_type"",""_id"":""test_id""}}
{""f1"":""v1"",""f2"":""42""}
{""index"":{""_index"":""test_index"",""_type"":""test_type"",""_id"":""test_id2""}}
{""f1"":""v2"",""f2"":""47""}";
				_status = this._client.BulkPost(_body, nv=>nv
					.Add("refresh","true")
				);
				_response = _status.Deserialize<dynamic>();

				//do count 
				
				_status = this._client.CountGet("test_index");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
