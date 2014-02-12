using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.GetSource
{
	public partial class GetSourceTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithCatchTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithCatchTest()
			{	

				//skip 0 - 0.90.0; 
				this.Skip("0 - 0.90.0", "Get source not supported in pre 0.90.1 versions.");

				//do get_source 
				this.Do(()=> this._client.GetSource("test_1", "test", "1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingDocumentWithIgnoreTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentWithIgnoreTest()
			{	

				//skip 0 - 0.90.0; 
				this.Skip("0 - 0.90.0", "Get source not supported in pre 0.90.1 versions.");

				//do get_source 
				this.Do(()=> this._client.GetSource("test_1", "test", "1", nv=>nv
					.Add("ignore", 404)
				));

			}
		}
	}
}

