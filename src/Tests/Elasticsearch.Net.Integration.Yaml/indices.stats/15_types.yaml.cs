using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStats6
{
	public partial class IndicesStats6YamlTests
	{	
	
		public class IndicesStats615TypesYamlBase : YamlTestsBase
		{
			public IndicesStats615TypesYamlBase() : base()
			{	

				//do index 
				_body = new {
					bar= "bar",
					baz= "baz"
				};
				this.Do(()=> _client.Index("test1", "bar", "1", _body));

				//do index 
				_body = new {
					bar= "bar",
					baz= "baz"
				};
				this.Do(()=> _client.Index("test2", "baz", "1", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesBlank2Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesBlank2Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll());

				//match _response._all.primaries.indexing.index_total: 
				this.IsMatch(_response._all.primaries.indexing.index_total, 2);

				//is_false _response._all.primaries.indexing.types; 
				this.IsFalse(_response._all.primaries.indexing.types);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesOne3Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesOne3Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("types", @"bar")
				));

				//match _response._all.primaries.indexing.types.bar.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.bar.index_total, 1);

				//is_false _response._all.primaries.indexing.types.baz; 
				this.IsFalse(_response._all.primaries.indexing.types.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesMulti4Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesMulti4Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("types", @"bar,baz")
				));

				//match _response._all.primaries.indexing.types.bar.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.bar.index_total, 1);

				//match _response._all.primaries.indexing.types.baz.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.baz.index_total, 1);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesStar5Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesStar5Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("types", @"*")
				));

				//match _response._all.primaries.indexing.types.bar.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.bar.index_total, 1);

				//match _response._all.primaries.indexing.types.baz.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.baz.index_total, 1);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesPattern6Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesPattern6Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("types", @"*r")
				));

				//match _response._all.primaries.indexing.types.bar.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.bar.index_total, 1);

				//is_false _response._all.primaries.indexing.types.baz; 
				this.IsFalse(_response._all.primaries.indexing.types.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesAllMetric7Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesAllMetric7Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("_all", nv=>nv
					.AddQueryString("types", @"bar")
				));

				//match _response._all.primaries.indexing.types.bar.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.bar.index_total, 1);

				//is_false _response._all.primaries.indexing.types.baz; 
				this.IsFalse(_response._all.primaries.indexing.types.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesIndexingMetric8Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesIndexingMetric8Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("indexing", nv=>nv
					.AddQueryString("types", @"bar")
				));

				//match _response._all.primaries.indexing.types.bar.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.bar.index_total, 1);

				//is_false _response._all.primaries.indexing.types.baz; 
				this.IsFalse(_response._all.primaries.indexing.types.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TypesMultiMetric9Tests : IndicesStats615TypesYamlBase
		{
			[Test]
			public void TypesMultiMetric9Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("indexing,search", nv=>nv
					.AddQueryString("types", @"bar")
				));

				//match _response._all.primaries.indexing.types.bar.index_total: 
				this.IsMatch(_response._all.primaries.indexing.types.bar.index_total, 1);

				//is_false _response._all.primaries.indexing.types.baz; 
				this.IsFalse(_response._all.primaries.indexing.types.baz);

			}
		}
	}
}

