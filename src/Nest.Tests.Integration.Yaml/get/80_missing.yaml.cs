using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get
{
	public partial class GetTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithCatchTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithCatchTest()
			{	

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithIgnoreTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithIgnoreTest()
			{	

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("ignore", 404)
				));

			}
		}
	}
}

