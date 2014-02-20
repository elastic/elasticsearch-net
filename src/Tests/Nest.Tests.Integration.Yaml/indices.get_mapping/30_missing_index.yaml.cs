using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping3
{
	public partial class IndicesGetMapping3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Raise404WhenIndexDoesntExist1Tests : YamlTestsBase
		{
			[Test]
			public void Raise404WhenIndexDoesntExist1Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_index", "not_test_type"), shouldCatch: @"missing");

			}
		}
	}
}

