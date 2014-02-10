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
				this._client.IndicesStatusGet(nv=>nv);

				//do indices.status 
				this._client.IndicesStatusGet("not_here", nv=>nv);
			}
		}
	}
}