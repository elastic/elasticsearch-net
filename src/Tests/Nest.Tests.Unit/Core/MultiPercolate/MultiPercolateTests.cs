using System;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.MultiPercolate
{
	[TestFixture]
	public class MultiPercolateTests : BaseJsonTests
	{
		[Test]
		public void MultiSearchNonFixed()
		{
			var result = this._client.MultiPercolate(m => m
				.Percolate<ElasticsearchProject>(pp=>pp.QueryString("hello"))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/_mpercolate");
		}
		[Test]
		public void MultiSearchFixedIndex()
		{
			var result = this._client.MultiPercolate(b => b
				.FixedPath("myindex")
				.Percolate<ElasticsearchProject>(pp=>pp.QueryString("hello"))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/_mpercolate");
		}
		[Test]
		public void MultiSearchFixedIndexAndType()
		{
			var result = this._client.MultiPercolate(b => b
				.FixedPath("myindex", "mytype")
				.Percolate<ElasticsearchProject>(pp=>pp.QueryString("hello"))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_mpercolate");
		}
		[Test]
		public void MultiPercolateJson()
		{
			var searchResult = this._client.Search<ElasticsearchProject>(s => s
				.IgnoreUnavailable()
				.MatchAll()
			);

			var result = this._client.MultiPercolate(b => b
				.Percolate<ElasticsearchProject>(pp=>pp
					.QueryString("hello")
					.Aggregations(aggs=>aggs
						.Terms("name", ta=>ta.Field(p=>p.Name))
					)
					.AllowNoIndices()
					.ExpandWildcards(ExpandWildcards.Open)
					.Filter(f=>f.Term("field", "value"))
					.IgnoreUnavailable()
					.PercolateIndex("percolate_index")
					.PercolateType("alternative_type")
					.Preference("local")
					.Routing("value")
					.Sort(s=>s.Ascending().OnField(p=>p.LOC))
					.TrackScores()
					.Version(123)
					.VersionType(VersionType.Force)
				)
				.Count<ElasticsearchProject>(pp=>pp
					.Document(new ElasticsearchProject()
					{
						Name = "nest"
					})
					.QueryString("hello")
				)
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/_mpercolate");

			var json = status.Request.Utf8String();
			//Assert.Fail(json);
			this.BulkJsonEquals(json, MethodBase.GetCurrentMethod());

		}

	}
}
