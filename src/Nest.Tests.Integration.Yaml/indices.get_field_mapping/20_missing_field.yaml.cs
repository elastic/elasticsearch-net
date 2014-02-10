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
	public partial class IndicesGetFieldMapping20MissingFieldYaml20Tests
	{
		
		public class Raise404WhenFieldDoesntExist20Tests
		{
			private readonly RawElasticClient _client;
		
			public Raise404WhenFieldDoesntExist20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void Raise404WhenFieldDoesntExistTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_index", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_field_mapping 
				this._client.IndicesGetFieldMapping("test_index", "test_type", "not_text", nv=>nv);
			}
		}
	}
}