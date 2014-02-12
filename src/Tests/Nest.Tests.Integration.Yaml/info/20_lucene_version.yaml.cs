using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Info
{
	public partial class InfoTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class LuceneVersionTests : YamlTestsBase
		{
			[Test]
			public void LuceneVersionTest()
			{	

				//skip 0 - 0.90.0; 
				this.Skip("0 - 0.90.0", "Lucene version not included in info before 0.90.1: https://github.com/elasticsearch/elasticsearch/issues/2988");

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

