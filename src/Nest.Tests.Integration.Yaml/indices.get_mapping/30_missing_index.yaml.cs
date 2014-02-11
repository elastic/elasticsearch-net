using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping
{
	public partial class IndicesGetMappingTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Raise404WhenIndexDoesntExistTests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenIndexDoesntExistTest()
			{	

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMapping("test_index", "not_test_type"));

			}
		}
	}
}

