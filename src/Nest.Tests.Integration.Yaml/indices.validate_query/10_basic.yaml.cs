using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesValidateQuery
{
	public partial class IndicesValidateQuery10BasicYaml10Tests
	{
		
		public class ValidateQueryApi10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public ValidateQueryApi10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ValidateQueryApiTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("testing", null);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.validate_query 
				
				_status = this._client.IndicesValidateQueryGet(nv=>nv
					.Add("q","query string")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true .valid; 
				this.IsTrue(_response.valid);

				//do indices.validate_query 
				_body = new {
					query= new {
						invalid_query= new {}
					}
				};
				_status = this._client.IndicesValidateQueryPost(_body);
				_response = _status.Deserialize<dynamic>();

				//is_false .valid; 
				this.IsFalse(_response.valid);
			}
		}
	}
}
