using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMappingTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Raise404WhenIndexDoesntExistTests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenIndexDoesntExistTest()
			{	

				//skip 0 - 0.90.5; 
				this.Skip("0 - 0.90.5", "get field mapping was added in 0.90.6");

				//do indices.get_field_mapping 
				this.Do(()=> this._client.IndicesGetFieldMapping("test_index", "type", "field"));

			}
		}
	}
}

