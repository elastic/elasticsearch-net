using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetFieldMapping4
{
	public partial class IndicesGetFieldMapping4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Raise404WhenIndexDoesntExist1Tests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenIndexDoesntExist1Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("test_index", "type", "field"), shouldCatch: @"missing");

			}
		}
	}
}

