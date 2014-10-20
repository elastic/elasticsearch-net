using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStats2
{
	public partial class IndicesStats2YamlTests
	{	
	
		public class IndicesStats211MetricYamlBase : YamlTestsBase
		{
			public IndicesStats211MetricYamlBase() : base()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test1", "bar", "1", _body));

				//do index 
				_body = new {
					foo= "baz"
				};
				this.Do(()=> _client.Index("test2", "baz", "1", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MetricBlank2Tests : IndicesStats211MetricYamlBase
		{
			[Test]
			public void MetricBlank2Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll());

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response._all.total.store; 
				this.IsTrue(_response._all.total.store);

				//is_true _response._all.total.indexing; 
				this.IsTrue(_response._all.total.indexing);

				//is_true _response._all.total.get; 
				this.IsTrue(_response._all.total.get);

				//is_true _response._all.total.search; 
				this.IsTrue(_response._all.total.search);

				//is_true _response._all.total.merges; 
				this.IsTrue(_response._all.total.merges);

				//is_true _response._all.total.refresh; 
				this.IsTrue(_response._all.total.refresh);

				//is_true _response._all.total.flush; 
				this.IsTrue(_response._all.total.flush);

				//is_true _response._all.total.warmer; 
				this.IsTrue(_response._all.total.warmer);

				//is_true _response._all.total.filter_cache; 
				this.IsTrue(_response._all.total.filter_cache);

				//is_true _response._all.total.id_cache; 
				this.IsTrue(_response._all.total.id_cache);

				//is_true _response._all.total.fielddata; 
				this.IsTrue(_response._all.total.fielddata);

				//is_true _response._all.total.percolate; 
				this.IsTrue(_response._all.total.percolate);

				//is_true _response._all.total.completion; 
				this.IsTrue(_response._all.total.completion);

				//is_true _response._all.total.segments; 
				this.IsTrue(_response._all.total.segments);

				//is_true _response._all.total.translog; 
				this.IsTrue(_response._all.total.translog);

				//is_true _response._all.total.suggest; 
				this.IsTrue(_response._all.total.suggest);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MetricAll3Tests : IndicesStats211MetricYamlBase
		{
			[Test]
			public void MetricAll3Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("_all"));

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response._all.total.store; 
				this.IsTrue(_response._all.total.store);

				//is_true _response._all.total.indexing; 
				this.IsTrue(_response._all.total.indexing);

				//is_true _response._all.total.get; 
				this.IsTrue(_response._all.total.get);

				//is_true _response._all.total.search; 
				this.IsTrue(_response._all.total.search);

				//is_true _response._all.total.merges; 
				this.IsTrue(_response._all.total.merges);

				//is_true _response._all.total.refresh; 
				this.IsTrue(_response._all.total.refresh);

				//is_true _response._all.total.flush; 
				this.IsTrue(_response._all.total.flush);

				//is_true _response._all.total.warmer; 
				this.IsTrue(_response._all.total.warmer);

				//is_true _response._all.total.filter_cache; 
				this.IsTrue(_response._all.total.filter_cache);

				//is_true _response._all.total.id_cache; 
				this.IsTrue(_response._all.total.id_cache);

				//is_true _response._all.total.fielddata; 
				this.IsTrue(_response._all.total.fielddata);

				//is_true _response._all.total.percolate; 
				this.IsTrue(_response._all.total.percolate);

				//is_true _response._all.total.completion; 
				this.IsTrue(_response._all.total.completion);

				//is_true _response._all.total.segments; 
				this.IsTrue(_response._all.total.segments);

				//is_true _response._all.total.translog; 
				this.IsTrue(_response._all.total.translog);

				//is_true _response._all.total.suggest; 
				this.IsTrue(_response._all.total.suggest);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MetricOne4Tests : IndicesStats211MetricYamlBase
		{
			[Test]
			public void MetricOne4Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("docs"));

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_false _response._all.total.store; 
				this.IsFalse(_response._all.total.store);

				//is_false _response._all.total.indexing; 
				this.IsFalse(_response._all.total.indexing);

				//is_false _response._all.total.get; 
				this.IsFalse(_response._all.total.get);

				//is_false _response._all.total.search; 
				this.IsFalse(_response._all.total.search);

				//is_false _response._all.total.merges; 
				this.IsFalse(_response._all.total.merges);

				//is_false _response._all.total.refresh; 
				this.IsFalse(_response._all.total.refresh);

				//is_false _response._all.total.flush; 
				this.IsFalse(_response._all.total.flush);

				//is_false _response._all.total.warmer; 
				this.IsFalse(_response._all.total.warmer);

				//is_false _response._all.total.filter_cache; 
				this.IsFalse(_response._all.total.filter_cache);

				//is_false _response._all.total.id_cache; 
				this.IsFalse(_response._all.total.id_cache);

				//is_false _response._all.total.fielddata; 
				this.IsFalse(_response._all.total.fielddata);

				//is_false _response._all.total.percolate; 
				this.IsFalse(_response._all.total.percolate);

				//is_false _response._all.total.completion; 
				this.IsFalse(_response._all.total.completion);

				//is_false _response._all.total.segments; 
				this.IsFalse(_response._all.total.segments);

				//is_false _response._all.total.translog; 
				this.IsFalse(_response._all.total.translog);

				//is_false _response._all.total.suggest; 
				this.IsFalse(_response._all.total.suggest);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MetricMulti5Tests : IndicesStats211MetricYamlBase
		{
			[Test]
			public void MetricMulti5Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("store,get,merge"));

				//is_false _response._all.total.docs; 
				this.IsFalse(_response._all.total.docs);

				//is_true _response._all.total.store; 
				this.IsTrue(_response._all.total.store);

				//is_false _response._all.total.indexing; 
				this.IsFalse(_response._all.total.indexing);

				//is_true _response._all.total.get; 
				this.IsTrue(_response._all.total.get);

				//is_false _response._all.total.search; 
				this.IsFalse(_response._all.total.search);

				//is_true _response._all.total.merges; 
				this.IsTrue(_response._all.total.merges);

				//is_false _response._all.total.refresh; 
				this.IsFalse(_response._all.total.refresh);

				//is_false _response._all.total.flush; 
				this.IsFalse(_response._all.total.flush);

				//is_false _response._all.total.warmer; 
				this.IsFalse(_response._all.total.warmer);

				//is_false _response._all.total.filter_cache; 
				this.IsFalse(_response._all.total.filter_cache);

				//is_false _response._all.total.id_cache; 
				this.IsFalse(_response._all.total.id_cache);

				//is_false _response._all.total.fielddata; 
				this.IsFalse(_response._all.total.fielddata);

				//is_false _response._all.total.percolate; 
				this.IsFalse(_response._all.total.percolate);

				//is_false _response._all.total.completion; 
				this.IsFalse(_response._all.total.completion);

				//is_false _response._all.total.segments; 
				this.IsFalse(_response._all.total.segments);

				//is_false _response._all.total.translog; 
				this.IsFalse(_response._all.total.translog);

				//is_false _response._all.total.suggest; 
				this.IsFalse(_response._all.total.suggest);

			}
		}
	}
}

