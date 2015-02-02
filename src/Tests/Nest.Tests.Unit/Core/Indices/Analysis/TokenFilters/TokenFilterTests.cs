using Nest.Tests.Unit.Core.Indices.Analysis.Tokenizers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Core.Indices.Analysis.TokenFilters
{
	[TestFixture]
	public class TokenFilterTests : BaseAnalysisTests
	{
		[Test]
		public void TestStopTokenFilterWithArray()
		{
			var result = this.Analysis(a => a
				.TokenFilters(t => t
					.Add("my_stop", new StopTokenFilter
					{
						Stopwords = new string[] { "and", "is", "the" }
					})
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void TestStopTokenFilterWithPredefinedLanguageList()
		{
			var result = this.Analysis(a => a
				.TokenFilters(t => t
					.Add("my_stop", new StopTokenFilter
					{
						Stopwords = "_english_"
					})
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
