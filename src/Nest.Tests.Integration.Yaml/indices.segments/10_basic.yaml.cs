using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesSegments
{
	public partial class IndicesSegments10BasicYaml10Tests
	{
		
		public class SegmentsTest10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public SegmentsTest10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void SegmentsTestTests()
			{

				//do indices.segments 
				
				this._client.IndicesSegmentsGet(nv=>nv);
			}
		}
	}
}
