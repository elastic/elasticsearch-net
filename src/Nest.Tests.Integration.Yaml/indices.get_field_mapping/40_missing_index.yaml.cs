using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMapping40MissingIndexYaml40Tests
	{
		
		public class Raise404WhenIndexDoesntExist40Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Raise404WhenIndexDoesntExist40Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void Raise404WhenIndexDoesntExistTests()
			{

				//do indices.get_field_mapping 
				
				this._client.IndicesGetFieldMapping("test_index", "type", "field", nv=>nv);
			}
		}
	}
}
