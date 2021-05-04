// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Core.Percolator
{
	public class PercolatorTest
	{
		[Percolator]
		public QueryContainer Query { get; set; }
	}

	public class PercolatorAttributeTests : AttributeTestsBase<PercolatorTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				query = new
				{
					type = "percolator"
				}
			}
		};
	}
}
