using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStats5
{
	public partial class IndicesStats5YamlTests
	{	
	
		public class IndicesStats514GroupsYamlBase : YamlTestsBase
		{
			public IndicesStats514GroupsYamlBase() : base()
			{	

				//do index 
				_body = new {
					bar= "bar",
					baz= "baz"
				};
				this.Do(()=> _client.Index("test1", "bar", "1", _body));

				//do search 
				_body = new {
					stats= new [] {
						"bar",
						"baz"
					}
				};
				this.Do(()=> _client.Search(_body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsBlank2Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsBlank2Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll());

				//gt _response._all.total.search.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.query_total, 0);

				//is_false _response._all.total.search.groups; 
				this.IsFalse(_response._all.total.search.groups);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsOne3Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsOne3Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("groups", @"bar")
				));

				//gt _response._all.total.search.groups.bar.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.bar.query_total, 0);

				//is_false _response._all.total.search.groups.baz; 
				this.IsFalse(_response._all.total.search.groups.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsMulti4Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsMulti4Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("groups", @"bar,baz")
				));

				//gt _response._all.total.search.groups.bar.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.bar.query_total, 0);

				//gt _response._all.total.search.groups.baz.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.baz.query_total, 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsStar5Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsStar5Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("groups", @"*")
				));

				//gt _response._all.total.search.groups.bar.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.bar.query_total, 0);

				//gt _response._all.total.search.groups.baz.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.baz.query_total, 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsPattern6Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsPattern6Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("groups", @"*r")
				));

				//gt _response._all.total.search.groups.bar.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.bar.query_total, 0);

				//is_false _response._all.total.search.groups.baz; 
				this.IsFalse(_response._all.total.search.groups.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsAllMetric7Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsAllMetric7Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("_all", nv=>nv
					.AddQueryString("groups", @"bar")
				));

				//gt _response._all.total.search.groups.bar.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.bar.query_total, 0);

				//is_false _response._all.total.search.groups.baz; 
				this.IsFalse(_response._all.total.search.groups.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsSearchMetric8Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsSearchMetric8Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("search", nv=>nv
					.AddQueryString("groups", @"bar")
				));

				//gt _response._all.total.search.groups.bar.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.bar.query_total, 0);

				//is_false _response._all.total.search.groups.baz; 
				this.IsFalse(_response._all.total.search.groups.baz);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GroupsMultiMetric9Tests : IndicesStats514GroupsYamlBase
		{
			[Test]
			public void GroupsMultiMetric9Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("indexing,search", nv=>nv
					.AddQueryString("groups", @"bar")
				));

				//gt _response._all.total.search.groups.bar.query_total: 0; 
				this.IsGreaterThan(_response._all.total.search.groups.bar.query_total, 0);

				//is_false _response._all.total.search.groups.baz; 
				this.IsFalse(_response._all.total.search.groups.baz);

			}
		}
	}
}

