using Nest.Tests.Unit.Core.Indices.Analysis.Tokenizers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;

namespace Nest.Tests.Unit.Core.Indices.Analysis.Analyzers.StopWords
{
	[TestFixture]
	public class StopWordsTests : BaseAnalysisTests
	{
		[Test]
		public void SnowballList()
		{
			this.Assert<SnowballAnalyzer>(MethodBase.GetCurrentMethod(), (snowball) =>
			{
				snowball.StopWords.Should().NotBeEmpty();
				var tokens = snowball.StopWords.Split(new[] { ',' }, StringSplitOptions.None);
				tokens.Should().NotBeEmpty().And.OnlyContain(s => !s.StartsWith(" ") && !s.EndsWith(" "));
			});
		}
		[Test]
		public void SnowballString()
		{
			this.Assert<SnowballAnalyzer>(MethodBase.GetCurrentMethod(), snowball =>
			{
				snowball.StopWords.Should().NotBeEmpty();
			});
		}


		[Test]
		public void SnowballNamed()
		{
			this.Assert<SnowballAnalyzer>(MethodBase.GetCurrentMethod(), snowball =>
			{
				snowball.StopWords.Should().NotBeEmpty().And.Be("_none_");
			});
		}



		[Test]
		public void StandardList()
		{
			this.Assert<StandardAnalyzer>(MethodBase.GetCurrentMethod(), standard =>
			{
				standard.StopWords.Should().NotBeEmpty().And.OnlyContain(s => !s.StartsWith(" ") && !s.EndsWith(" "));
			});
		}
		[Test]
		public void StandardString()
		{
			this.Assert<StandardAnalyzer>(MethodBase.GetCurrentMethod(), standard =>
			{
				standard.StopWords.Should().NotBeEmpty().And.OnlyContain(s => !s.StartsWith(" ") && !s.EndsWith(" "));
				standard.StopWords.Should().NotBeEmpty();
			});
		}


		[Test]
		public void StandardNamed()
		{
			this.Assert<StandardAnalyzer>(MethodBase.GetCurrentMethod(), standard =>
			{
				standard.StopWords.Should().NotBeEmpty().And.HaveCount(1).And.Contain("_none_");
			});
		}

		private void Assert<T>(MethodBase method, Action<T> assert) where T : class
		{
			var analyzer = this.Deserialize<T>(method);
			assert(analyzer);
			var json = this.Serialize(analyzer);
			analyzer = this.Deserialize<T>(json);
			assert(analyzer);
		}

	}
}
