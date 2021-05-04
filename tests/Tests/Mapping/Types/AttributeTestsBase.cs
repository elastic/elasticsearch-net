// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Mapping.Types
{
	public abstract class AttributeTestsBase<T>
		where T : class
	{
		protected abstract object ExpectJson { get; }


		[U]
		protected virtual void Serializes() =>
			Expect(ExpectJson)
				.WhenSerializing(new PutMappingDescriptor<T>().AutoMap() as IPutMappingRequest);
	}
}
