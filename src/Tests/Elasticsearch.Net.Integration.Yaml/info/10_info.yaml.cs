using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Info1
{
	public partial class Info1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class InfoReturnsBodyTests : YamlTestsBase
		{
			[Test]
			public void InfoReturnsBodyTest()
			{	

				//do info 
				this.Do(()=> _client.Info());

				//match _response.status: 
				this.IsMatch(_response.status, 200);

				//is_true _response.name; 
				this.IsTrue(_response.name);

				//is_true _response.tagline; 
				this.IsTrue(_response.tagline);

				//is_true _response.version; 
				this.IsTrue(_response.version);

				//is_true _response.version.number; 
				this.IsTrue(_response.version.number);

			}
		}
	}
}

