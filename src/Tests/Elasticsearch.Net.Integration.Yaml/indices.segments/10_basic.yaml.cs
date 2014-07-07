using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesSegments1
{
	public partial class IndicesSegments1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SegmentsTest1Tests : YamlTestsBase
		{
			[Test]
			public void SegmentsTest1Test()
			{	

				//do indices.segments 
				this.Do(()=> _client.IndicesSegmentsForAll(nv=>nv
					.AddQueryString("allow_no_indices", @"true")
				));

			}
		}
	}
}

