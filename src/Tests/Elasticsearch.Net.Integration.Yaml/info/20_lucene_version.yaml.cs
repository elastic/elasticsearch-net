using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Info2
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
				this.Do(()=> _client.Info());

				//match _response.status: 
				this.IsMatch(_response.status, 200);

				//is_true _response.version.lucene_version; 
				this.IsTrue(_response.version.lucene_version);

			}
		}
	}
}

