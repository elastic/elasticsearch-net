using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesOptimize
{
	public partial class IndicesOptimize10BasicYaml10Tests
	{
		
		public class OptimizeIndexTests10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public OptimizeIndexTests10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void OptimizeIndexTestsTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("testing", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.optimize 
				
				_status = this._client.IndicesOptimizeGet("testing", nv=>nv
					.Add("max_num_segments","1")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
