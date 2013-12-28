using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;

namespace Nest.Tests.Unit.Search.SearchType
{
	[TestFixture]
	public class SearchTypeTests : BaseJsonTests
	{
		[Test]
		public void SearchTypeDoesNotPolluteQueryObject()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.QueryAndFetch);
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void UrlParamSetWhenQueryAndFetch()
		{
			var p = new PathResolver(this._settings);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.QueryAndFetch);

			var path = p.GetSearchPathForTyped(s);
			StringAssert.Contains("search_type=query_and_fetch", path);
		}
		[Test]
		public void UrlParamSetWhenQueryThenFetch()
		{
			var p = new PathResolver(this._settings);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.QueryThenFetch);

			var path = p.GetSearchPathForTyped(s);
			StringAssert.Contains("search_type=query_then_fetch", path);
		}
		[Test]
		public void UrlParamSetWhenDfsQueryAndFetch()
		{
			var p = new PathResolver(this._settings);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.DfsQueryAndFetch);

			var path = p.GetSearchPathForTyped(s);
			StringAssert.Contains("search_type=dfs_query_and_fetch", path);
		}
		[Test]
		public void UrlParamSetWhenDfsQueryThenFetch()
		{
			var p = new PathResolver(this._settings);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.DfsQueryThenFetch);

			var path = p.GetSearchPathForTyped(s);
			StringAssert.Contains("search_type=dfs_query_then_fetch", path);
		}
		[Test]
		public void UrlParamSetWhenCount()
		{
			var p = new PathResolver(this._settings);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.Count);

			var path = p.GetSearchPathForTyped(s);
			StringAssert.Contains("search_type=count", path);
		}
		[Test]
		public void UrlParamSetScan()
		{
			var p = new PathResolver(this._settings);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.Scan);

			var path = p.GetSearchPathForTyped(s);
			StringAssert.Contains("search_type=scan", path);
		}
		[Test]
		public void UrlParamSetScanAndScroll()
		{
			var p = new PathResolver(this._settings);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(Nest.SearchType.Scan)
				.Scroll("5s");

			var path = p.GetSearchPathForTyped(s);
			StringAssert.Contains("search_type=scan", path);
			StringAssert.Contains("scroll=5s", path);
		}
	}
}
