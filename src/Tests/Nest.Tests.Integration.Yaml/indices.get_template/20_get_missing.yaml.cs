using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetTemplate2
{
	public partial class IndicesGetTemplate2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.delete_template 
				this.Do(()=> this._client.IndicesDeleteTemplateForAll("*", nv=>nv
					.Add("ignore", 404)
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMissingTemplate2Tests : YamlTestsBase
		{
			[Test]
			public void GetMissingTemplate2Test()
			{	

				//do indices.get_template 
				this.Do(()=> this._client.IndicesGetTemplateForAll("test"), shouldCatch: @"missing");

			}
		}
	}
}

