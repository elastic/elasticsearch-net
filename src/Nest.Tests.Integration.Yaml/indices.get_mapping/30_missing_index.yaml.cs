using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping
{
	public partial class IndicesGetMapping30MissingIndexYaml30Tests
	{
		
		public class Raise404WhenIndexDoesntExist30Tests
		{
			private readonly RawElasticClient _client;
		
			public Raise404WhenIndexDoesntExist30Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void Raise404WhenIndexDoesntExistTests()
			{

				//do indices.get_mapping 
				this._client.IndicesGetMapping("test_index", "not_test_type", nv=>nv);
			}
		}
	}
}