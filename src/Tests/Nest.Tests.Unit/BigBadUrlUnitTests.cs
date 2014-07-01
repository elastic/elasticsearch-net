using System;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Security;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Cluster
{
	[TestFixture]
	public class BigBadUrlUnitTests
	{
		private void Do(string method, string expectedUrl, Action<IElasticClient> call)
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200/"), "mydefaultindex")
				.SetConnectionStatusHandler(c =>
				{
					new Uri(c.RequestUrl).PathAndQuery.Should().Be(expectedUrl);
					c.RequestMethod.Should().Be(method);
				})
				;
			var client = new ElasticClient(settings, new InMemoryConnection(settings));
			call(client);
		}

		private class Doc
		{
			public string Id { get; set; }
			public string Name { get; set; }
		}

		private class OtherDoc
		{
			public string Name { get; set; }
		}


		[Test]
		public void TestAllTheUrls()
		{
			Do("POST", "/_aliases", c => c.Alias(a => a));
			Do("POST", "/_analyze", c => c.Analyze(a => a.Text("blah")));
			Do("POST", "/myindex/_analyze", c => c.Analyze(a => a.Index("myindex").Text("blah")));
			Do("POST", "/myindex/_bulk", c => c.Bulk(b => b.FixedPath("myindex").Index<Doc>(ib => ib.Document(new Doc { Id = "1" }))));
			Do("POST", "/myindex/mytype/_bulk", c => c.Bulk(b => b.FixedPath("myindex", "mytype").Index<Doc>(ib => ib.Document(new Doc { Id = "1" }))));
			Do("POST", "/myindex/_bulk", c => c.Bulk(b => b.FixedPath("myindex").Index<Doc>(ib => ib.Document(new Doc { Id = "1" }))));
			Do("POST", "/_bulk", c => c.Bulk(b => b.Index<Doc>(ib => ib.Document(new Doc { Id = "1" }))));
			Do("POST", "/_cache/clear", c => c.ClearCache());
			Do("POST", "/mydefaultindex/_cache/clear", c => c.ClearCache(cc => cc.Index<Doc>()));
			Do("POST", "/mydefaultindex/_close", c => c.CloseIndex(ci => ci.Index<Doc>()));
			Do("GET", "/_nodes", c => c.NodesInfo());
			Do("GET", "/_nodes/insert-marvel-character", c => c.NodesInfo(cn => cn.NodeId("insert-marvel-character")));
			Do("GET", "/_nodes/stats", c => c.NodesStats());
			Do("GET", "/_nodes/insert-marvel-character/stats/jvm", c => c
				.NodesStats(cn => cn.NodeId("insert-marvel-character").Metrics(NodesStatsMetric.Jvm)));
			Do("GET", "/_cluster/state", c => c.ClusterState());
			Do("GET", "/_cluster/state?local=true", c => c.ClusterState(cs => cs.Local()));
			Do("POST", "/_count", c => c.Count());
			Do("POST", "/_all/doc/_count", c => c.Count(cc => cc.AllIndices().Type<Doc>()));
			Do("POST", "/mydefaultindex/doc/_count", c => c.Count(cc => cc.Index<Doc>().Type<Doc>()));
			Do("POST", "/mydefaultindex/_count", c => c.Count(cc => cc.Index<Doc>()));
			Do("POST", "/mydefaultindex/doc/_count", c => c.Count<Doc>());
			Do("POST", "/customindex/doc/_count", c => c.Count<Doc>(cc => cc.Index("customindex")));
			Do("POST", "/new-index-name", c => c.CreateIndex("new-index-name"));
			Do("DELETE", "/mydefaultindex/doc/1", c => c.Delete<Doc>(d => d.Id("1")));
			Do("DELETE", "/mydefaultindex/doc/1", c => c.Delete<Doc>(1));
			Do("DELETE", "/customindex/doc/1", c => c.Delete<Doc>(d => d.Index("customindex").Id(1)));
			Do("DELETE", "/customindex/doc/1", c => c.Delete<Doc>(1, d => d.Index("customindex")));
			Do("DELETE", "/mydefaultindex/doc/_query", c => c.DeleteByQuery<Doc>(q => q.MatchAll()));
			Do("DELETE", "/customindex/doc/_query", c => c.DeleteByQuery<Doc>(q => q.Index("customindex").MatchAll()));
			Do("DELETE", "/_all/doc/_query", c => c.DeleteByQuery<Doc>(q => q.AllIndices().MatchAll()));
			Do("DELETE", "/mydefaultindex/_query", c => c.DeleteByQuery<Doc>(q => q.AllTypes().MatchAll()));
			Do("DELETE", "/custom-index/_query", c => c.DeleteByQuery<Doc>(q => q.Index("custom-index").AllTypes().MatchAll()));
			Do("DELETE", "/mydefaultindex", c => c.DeleteIndex(i => i.Index<Doc>()));
			Do("DELETE", "/a%2Cb", c => c.DeleteIndex(i => i.Indices("a", "b")));
			Do("POST", "/_bulk", c => c.DeleteMany(Enumerable.Range(0, 10).Select(i => new Doc { Id = i.ToString() })));
			Do("POST", "/customindex/customtype/_bulk", c => c.DeleteMany(Enumerable.Range(0, 10).Select(i => new Doc { Id = i.ToString() }), index: "customindex", type: "customtype"));
			Do("DELETE", "/mydefaultindex/doc/_mapping", c => c.DeleteMapping(d => d.Index<Doc>().Type<Doc>()));
			Do("DELETE", "/_template/myTemplate", c => c.DeleteTemplate("myTemplate"));
			Do("DELETE", "/_all/_warmer/mywarmer", c => c.DeleteWarmer("mywarmer", w => w.AllIndices()));
			Do("DELETE", "/_all/_warmer/mywarmer", c => c.DeleteWarmer("mywarmer"));
			Do("DELETE", "/mycustomindex/_warmer/mywarmer", c => c.DeleteWarmer("mywarmer", w => w.Index("mycustomindex")));
			Do("POST", "/_all/_flush", c => c.Flush(f => f.AllIndices()));
			Do("POST", "/mycustomindex/_flush", c => c.Flush(f => f.Index("mycustomindex")));
			Do("GET", "/mydefaultindex/doc/1", c => c.Get<Doc>(1));
			Do("GET", "/mycustomindex/mycustomtype/1", c => c.Get<Doc>(1, index: "mycustomindex", type: "mycustomtype"));
			Do("GET", "/mycustomindex/mycustomtype/1", c => c.Get<Doc>(g => g.Id(1).Index("mycustomindex").Type("mycustomtype")));
			Do("GET", "/mydefaultindex/_alias/*", c => c.GetAliases(a => a.Index<Doc>()));
			Do("GET", "/_alias/prefix-*", c => c.GetAliases(a => a.Alias("prefix-*")));
			Do("GET", "/mydefaultindex/_settings", c => c.GetIndexSettings(i => i.Index<Doc>()));
			Do("GET", "/mydefaultindex/_mapping/doc", c => c.GetMapping<Doc>());
			Do("GET", "/mycustomindex/_mapping/doc", c => c.GetMapping<Doc>(m => m.Index("mycustomindex")));
			Do("GET", "/mycustomindex/_mapping/sometype", c => c.GetMapping(m => m.Index("mycustomindex").Type("sometype")));
			Do("GET", "/_template/mytemplate", c => c.GetTemplate("mytemplate"));
			Do("GET", "/_all/_warmer/mywarmer", c => c.GetWarmer("mywarmer"));
			Do("GET", "/mycustomindex/_warmer/mywarmer", c => c.GetWarmer("mywarmer", g => g.Index("mycustomindex")));
			Do("GET", "/_cluster/health?level=indices", c => c.ClusterHealth(h => h.Level(LevelOptions.Indices)));
			Do("GET", "/_cluster/health", c => c.ClusterHealth());
			Do("PUT", "/mydefaultindex/doc/2", c => c.Index(new Doc { Id = "2" }));
			Do("POST", "/mydefaultindex/doc", c => c.Index(new Doc { Name = "2" }));
			Do("PUT", "/customindex/customtype/2?refresh=true", c => c.Index(new Doc { Id = "2" }, i => i.Index("customindex").Type("customtype").Refresh()));
			Do("HEAD", "/mydefaultindex", c => c.IndexExists(h => h.Index<Doc>()));
			Do("POST", "/_bulk", c => c.IndexMany(Enumerable.Range(0, 10).Select(i => new Doc { Id = i.ToString() })));
			Do("POST", "/customindex/customtype/_bulk", c => c.IndexMany(Enumerable.Range(0, 10).Select(i => new Doc { Id = i.ToString() }), index: "customindex", type: "customtype"));
			Do("GET", "/_stats", c => c.IndicesStats());
			Do("GET", "/mydefaultindex/_stats", c => c.IndicesStats(s => s.Index<Doc>()));
			Do("PUT", "/mydefaultindex/doc/_mapping", c => c.Map<Doc>(m => m.MapFromAttributes()));
			Do("PUT", "/mycustomindex/doc/_mapping", c => c.Map<Doc>(m => m.Index("mycustomindex")));
			Do("PUT", "/mycustomindex/customtype/_mapping", c => c.Map<Doc>(m => m.Index("mycustomindex").Type("customtype")));
			Do("GET", "/mydefaultindex/doc/1/_mlt", c => c.MoreLikeThis<Doc>(m => m.Object(new Doc { Id = "1" })));
			Do("GET", "/mydefaultindex/doc/1/_mlt", c => c.MoreLikeThis<Doc>(m => m.Id(1)));
			Do("GET", "/mycustomindex/mycustomtype/1/_mlt", c => c.MoreLikeThis<Doc>(m => m.Id(1).Index("mycustomindex").Type("mycustomtype")));
			Do("POST", "/_msearch", c => c.MultiSearch(m => m.Search<Doc>(s => s.MatchAll())));
			Do("POST", "/mycustomindex/_msearch", c => c.MultiSearch(m => m.FixedPath(index: "mycustomindex").Search<Doc>(s => s.MatchAll())));
			Do("POST", "/mycustomindex/mycustomtype/_msearch", c => c.MultiSearch(m => m.FixedPath(index: "mycustomindex", type: "mycustomtype").Search<Doc>(s => s.MatchAll())));
			Do("POST", "/mydefaultindex/_open", c => c.OpenIndex<Doc>());
			Do("POST", "/mycustomindex/_open", c => c.OpenIndex(i => i.Index("mycustomindex")));
			Do("POST", "/mycustomindex/_open", c => c.OpenIndex("mycustomindex"));
			Do("POST", "/_optimize", c => c.Optimize());
			Do("POST", "/mydefaultindex/_optimize", c => c.Optimize(o => o.Index<Doc>()));
			Do("POST", "/mydefaultindex/doc/_percolate", c => c.Percolate(new Doc { Id = "1" }));
			Do("POST", "/mydefaultindex/doc/_percolate", c => c.Percolate<Doc, OtherDoc>(new OtherDoc { Name = "hello" }));
			Do("POST", "/mycustomindex/mycustomtype/_percolate", c => c.Percolate<Doc, OtherDoc>(new OtherDoc { Name = "hello" }, p => p.Index("mycustomindex").Type("mycustomtype")));
			Do("PUT", "/_template/my-template", c => c.PutTemplate("my-template", pt => pt.Settings(s => s.Add("mysetting", true))));
			Do("PUT", "/_all/_warmer/my-warmer", c => c.PutWarmer("my-warmer", p => p.Search<Doc>(s => s.MatchAll())));
			Do("PUT", "/mycustomindex/_warmer/my-warmer", c => c.PutWarmer("my-warmer", p => p.Index("mycustomindex").Search<Doc>(s => s.MatchAll())));
			Do("POST", "/_refresh", c => c.Refresh());
			Do("POST", "/mycustomindex/_refresh", c => c.Refresh(r => r.Index("mycustomindex")));
			Do("POST", "/mydefaultindex/.percolator/mypercolator", c => c.RegisterPercolator<Doc>("mypercolator", p => p.Query(q => q.MatchAll())));
			Do("GET", "/", c => c.RootNodeInfo());
			Do("POST", "/_search/scroll?scroll=2m", c => c.Scroll<Doc>(s => s.ScrollId("difficulthash").Scroll("2m")));
			Do("POST", "/_search/scroll?scroll=2m", c => c.Scroll<Doc>(scrollId: "difficulthash", scrollTime: "2m"));
			Do("POST", "/_search/scroll?scroll=2m", c => c.Scroll<Doc>(new ScrollRequest { Scroll = "2m", ScrollId = "difficulthash"}));
			Do("POST", "/mydefaultindex/doc/_search", c => c.Search<Doc>(s => s.MatchAll()));
			Do("POST", "/_all/doc/_search", c => c.Search<Doc>(s => s.AllIndices().MatchAll()));
			Do("POST", "/_search", c => c.Search<Doc>(s => s.AllIndices().AllTypes().MatchAll()));
			Do("POST", "/mydefaultindex/_search", c => c.Search<Doc>(s => s.AllTypes().MatchAll()));
			Do("POST", "/prefix-*/a%2Cb/_search", c => c.Search<Doc>(s => s.Index("prefix-*").Types("a", "b").MatchAll()));
			Do("GET", "/_segments", c => c.Segments());
			Do("GET", "/mycustomindex/_segments", c => c.Segments(s => s.Index("mycustomindex")));
			Do("GET", "/mydefaultindex/doc/1/_source", c => c.Source<Doc>(1));
			Do("GET", "/mycustomindex/mytype/1/_source", c => c.Source<Doc>(1, index: "mycustomindex", type: "mytype"));
			Do("GET", "/mycustomindex/doc/1/_source", c => c.Source<Doc>(s => s.Id(1).Index("mycustomindex")));
			Do("GET", "/_status", c => c.Status());
			Do("GET", "/mydefaultindex/_status", c => c.Status(s => s.Index<Doc>()));
			Do("DELETE", "/mydefaultindex/.percolator/mypercolator", c => c.UnregisterPercolator("mypercolator"));
			Do("DELETE", "/mycustomindex/.percolator/mypercolator", c => c.UnregisterPercolator("mypercolator", r => r.Index("mycustomindex")));
			Do("POST", "/mydefaultindex/doc/1/_update", c => c.Update<Doc, OtherDoc>(u => u.Id(1).Document(new OtherDoc { Name = "asd" })));
			Do("POST", "/mydefaultindex/customtype/1/_update", c => c.Update<Doc, OtherDoc>(u => u.Id(1).Type("customtype").Document(new OtherDoc { Name = "asd" })));
			Do("PUT", "/mydefaultindex/_settings", c => c.UpdateSettings(u => u.AutoExpandReplicas(false)));
			Do("PUT", "/mycustomindex/_settings", c => c.UpdateSettings(u => u.Index("mycustomindex").AutoExpandReplicas(false)));
			Do("POST", "/_all/doc/_validate/query", c => c.Validate<Doc>(v => v.AllIndices()));
			Do("POST", "/mydefaultindex/doc/_validate/query", c => c.Validate<Doc>(v => v.Query(q => q.MatchAll())));
			Do("POST", "/mydefaultindex/_validate/query", c => c.Validate<Doc>(v => v.AllTypes()));
			Do("POST", "/_validate/query", c => c.Validate<Doc>(v => v.AllIndices().AllTypes()));
			Do("PUT", "/_cluster/settings", c => c.ClusterSettings(v => v.Transient(p => p)));
			Do("GET", "/_cluster/settings", c => c.ClusterGetSettings());
		}

	}
}