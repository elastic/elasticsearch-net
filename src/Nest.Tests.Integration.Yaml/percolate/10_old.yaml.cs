using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Percolate
{
	public partial class Percolate10OldYaml10Tests
	{
		
		public class BasicPercolationTests10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicPercolationTests10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicPercolationTestsTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				_status = this._client.IndexPost("_percolator", "test_index", "test_percolator", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do percolate 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				_status = this._client.PercolatePost("test_index", "test_type", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);
			}
		}
	}
}
