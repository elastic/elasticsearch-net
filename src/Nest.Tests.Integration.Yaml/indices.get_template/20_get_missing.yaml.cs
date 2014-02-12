using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetTemplate
{
	public partial class IndicesGetTemplateTests
	{	
	
		public class IndicesGetTemplate20GetMissingYamlBase : YamlTestsBase
		{
			public IndicesGetTemplate20GetMissingYamlBase() : base()
			{	

				//do indices.delete_template 
				this.Do(()=> this._client.IndicesDeleteTemplate("*", nv=>nv
					.Add("ignore", 404)
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMissingTemplatePost0903Tests : IndicesGetTemplate20GetMissingYamlBase
		{
			[Test]
			public void GetMissingTemplatePost0903Test()
			{	

				//skip 0 - 0.90.2; 
				this.Skip("0 - 0.90.2", "Missing templates throw 404 from 0.90.3");

				//do indices.get_template 
				this.Do(()=> this._client.IndicesGetTemplate("test"));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMissingTemplatePre0903Tests : IndicesGetTemplate20GetMissingYamlBase
		{
			[Test]
			public void GetMissingTemplatePre0903Test()
			{	

				//skip 0.90.3 - 999; 
				this.Skip("0.90.3 - 999", "Missing templates didn&#39;t throw 404 before 0.90.3");

				//do indices.delete_template 
				this.Do(()=> this._client.IndicesDeleteTemplate("test", nv=>nv
					.Add("ignore", 404)
				));

				//do indices.get_template 
				this.Do(()=> this._client.IndicesGetTemplate("test"));

				//match this._status: 
				this.IsMatch(this._status, new {});

			}
		}
	}
}

