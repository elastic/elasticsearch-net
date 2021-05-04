// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Specialized.ConstantKeyword
{
	public class ConstantKeywordTest
	{
		[ConstantKeyword(Value = "constant_string")]
		public string ConstantString { get; set; }

		[ConstantKeyword(Value = 42)]
		public int ConstantInt { get; set; }
	}

	[SkipVersion("<7.7.0", "introduced in 7.7.0")]
	public class ConstantKeywordAttributeTests : AttributeTestsBase<ConstantKeywordTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				constantString = new
				{
					type = "constant_keyword",
					value = "constant_string"
				},
				constantInt = new
				{
					type = "constant_keyword",
					value = 42
				}
			}
		};
	}
}
