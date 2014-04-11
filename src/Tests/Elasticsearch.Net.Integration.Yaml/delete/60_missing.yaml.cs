using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Delete10
{
	public partial class Delete10YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithCatch1Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithCatch1Test()
			{	

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithIgnore2Tests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithIgnore2Test()
			{	

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("ignore", 404)
				));

			}
		}
	}
}

