using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.GetSource8
{
	public partial class GetSource8YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithCatch1Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithCatch1Test()
			{	

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithIgnore2Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithIgnore2Test()
			{	

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.Add("ignore", 404)
				));

			}
		}
	}
}

