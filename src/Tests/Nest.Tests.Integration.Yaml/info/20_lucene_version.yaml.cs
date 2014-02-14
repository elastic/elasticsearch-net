using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Info2
{
	public partial class Info2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class LuceneVersion1Tests : YamlTestsBase
		{
			[Test]
			public void LuceneVersion1Test()
			{	

				//do info 
				this.Do(()=> this._client.InfoGet());

				//match _response.status: 
				this.IsMatch(_response.status, 200);

				//is_true _response.version.lucene_version; 
				this.IsTrue(_response.version.lucene_version);

			}
		}
	}
}

