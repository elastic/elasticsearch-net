using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping4
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

