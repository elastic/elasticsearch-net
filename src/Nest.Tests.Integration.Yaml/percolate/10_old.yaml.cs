using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Percolate
{
	public partial class Percolate10OldYaml10Tests
	{
		
		public class BasicPercolationTests10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
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
				
				this._client.IndicesCreatePost("test_index", null, nv=>nv);

				//do index 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this._client.IndexPost("_percolator", "test_index", "test_percolator", _body, nv=>nv);

				//do indices.refresh 
				
				this._client.IndicesRefreshGet(nv=>nv);

				//do percolate 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this._client.PercolatePost("test_index", "test_type", _body, nv=>nv);
			}
		}
	}
}
