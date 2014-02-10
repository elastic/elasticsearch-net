using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesStatus
{
	public partial class IndicesStatus10BasicYaml10Tests
	{
		
		public class IndicesStatusTest10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public IndicesStatusTest10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void IndicesStatusTestTests()
			{

				//do indices.status 
				
				_status = this._client.IndicesStatusGet();
				_response = _status.Deserialize<dynamic>();

				//do indices.status 
				
				_status = this._client.IndicesStatusGet("not_here");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
