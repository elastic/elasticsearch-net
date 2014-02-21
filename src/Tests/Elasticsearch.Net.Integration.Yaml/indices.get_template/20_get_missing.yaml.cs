using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetTemplate2
{
	public partial class IndicesGetTemplate2YamlTests
	{	
	
		public class IndicesGetTemplate220GetMissingYamlBase : YamlTestsBase
		{
			public IndicesGetTemplate220GetMissingYamlBase() : base()
			{	

				//do indices.delete_template 
				this.Do(()=> _client.IndicesDeleteTemplateForAll("*", nv=>nv
					.Add("ignore", 404)
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMissingTemplate2Tests : IndicesGetTemplate220GetMissingYamlBase
		{
			[Test]
			public void GetMissingTemplate2Test()
			{	

				//do indices.get_template 
				this.Do(()=> _client.IndicesGetTemplateForAll("test"), shouldCatch: @"missing");

			}
		}
	}
}

