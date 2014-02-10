using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Info
{
	public partial class Info10InfoYaml10Tests
	{
		
		public class Info10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Info10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void InfoTests()
			{

				//do info 
				
				_status = this._client.InfoGet();
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
