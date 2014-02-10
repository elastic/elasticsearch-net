using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetTemplate
{
	public partial class IndicesGetTemplate20GetMissingYaml20Tests
	{
		
		public class Setup20Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Setup20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void SetupTests()
			{

				//do indices.delete_template 
				
				_status = this._client.IndicesDeleteTemplate("*", nv=>nv
					.Add("ignore","404")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetMissingTemplatePost090320Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetMissingTemplatePost090320Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetMissingTemplatePost0903Tests()
			{

				//do indices.get_template 
				
				_status = this._client.IndicesGetTemplate("test");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetMissingTemplatePre090320Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetMissingTemplatePre090320Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetMissingTemplatePre0903Tests()
			{

				//do indices.delete_template 
				
				_status = this._client.IndicesDeleteTemplate("test", nv=>nv
					.Add("ignore","404")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_template 
				
				_status = this._client.IndicesGetTemplate("test");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
