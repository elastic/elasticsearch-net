// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Core.DenseVector
{
	public class DenseVectorTest
	{
		[DenseVector(Dimensions = 2)]
		public string DenseVector { get; set; }
	}

	[SkipVersion("<7.6.0", "Dense Vector property GA in 7.6.0")]
	public class DenseVectorAttributeTests : AttributeTestsBase<DenseVectorTest>
	{
		protected override object ExpectJson => new { properties = new { denseVector = new { type = "dense_vector", dims = 2 } } };
	}
}
