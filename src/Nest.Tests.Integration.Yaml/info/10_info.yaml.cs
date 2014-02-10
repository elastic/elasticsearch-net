using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Info
{
	public partial class Info10InfoYaml10Tests
	{
		
		public class Info10Tests : YamlTestsBase
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

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//is_true .name; 
				this.IsTrue(_response.name);

				//is_true .tagline; 
				this.IsTrue(_response.tagline);

				//is_true .version; 
				this.IsTrue(_response.version);

				//is_true .version.number; 
				this.IsTrue(_response.version.number);
			}
		}
	}
}
