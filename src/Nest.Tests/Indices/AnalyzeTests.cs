using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using Nest.Mapping;

namespace Nest.Tests.Search
{
	[TestFixture]
	public class AnalyzeTest : BaseElasticSearchTests
	{
		private void _defaultAnalyzeAssertations(AnalyzeResponse r)
		{
			Assert.True(r.IsValid);
			Assert.NotNull(r.Tokens);
			Assert.True(r.Tokens.Any());
			Assert.True(r.Tokens.All(t=> {
				return !t.Token.IsNullOrEmpty()
					&& !t.Type.IsNullOrEmpty()
					&& t.EndPostion > 0;
			}));
		}

		[Test]
		public void SimpleAnalyze()
		{
			//analyze text using default index settings
			var text = "this is a string with some spaces and stuff";
			var r = this.ConnectedClient.Analyze(text);
			this._defaultAnalyzeAssertations(r);
			Assert.False(r.Tokens.Count() == text.Split(new[] { ' ' }).Count());
		}
		[Test]
		public void FieldAnalyze()
		{
			//analyze text using elasticssearchprojects content field settings
			var text = "this is a string with some spaces and stuff";
			var r = this.ConnectedClient.Analyze<ElasticSearchProject>(p => p.Content, text);
			this._defaultAnalyzeAssertations(r);
			Assert.False(r.Tokens.Count() == text.Split(new[] { ' ' }).Count());
		}
		[Test]
		public void SimplAnalyzeDifferentIndex()
		{
			//analyze text using a different index and custom analyzer
			var text = "this is a string with some spaces and stuff";
			var analyzer = new AnalyzeParams { Analyzer = "whitespace", Index = Test.Default.DefaultIndex + "_clone" };
			var r = this.ConnectedClient.Analyze(analyzer, text);
			this._defaultAnalyzeAssertations(r);
			Assert.True(r.Tokens.Count() == text.Split(new[] { ' ' }).Count());
		}
		[Test]
		public void AnalyzeDifferentIndex()
		{
			//analyze text using elasticssearchprojects content field but on a different index
			var text = "this is a string with some spaces and stuff";
			var index = Test.Default.DefaultIndex + "_clone";
			var r = this.ConnectedClient.Analyze<ElasticSearchProject>(p => p.Content, index, text);
			this._defaultAnalyzeAssertations(r);
			Assert.False(r.Tokens.Count() == text.Split(new []{' '}).Count());
		}
		[Test]
		public void SimplAnalyzeDifferentIndexAndField()
		{
			//analyze text using a different index and custom analyzer
			var text = "this is a string with some spaces and stuff";
			var analyzer = new AnalyzeParams { Field = "content", Index = Test.Default.DefaultIndex + "_clone" };
			var r = this.ConnectedClient.Analyze(analyzer, text);
			this._defaultAnalyzeAssertations(r);
			Assert.False(r.Tokens.Count() == text.Split(new[] { ' ' }).Count());
		}
	}
}