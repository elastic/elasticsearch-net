// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Core.MatchOnlyText
{
	public class MatchOnlyTextTest
	{
		[MatchOnlyText]
		public string MatchOnlyText { get; set; }
	}

	[SkipVersion("<7.14.0", "Match only text property added in 7.14.0")]
	public class MatchOnlyTextAttributeTests : AttributeTestsBase<MatchOnlyTextTest>
	{
		protected override object ExpectJson => new { properties = new { matchOnlyText = new { type = "match_only_text" } } };
	}
}
