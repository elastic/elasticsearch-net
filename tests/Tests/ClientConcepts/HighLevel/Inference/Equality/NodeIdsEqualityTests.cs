// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Tests.Core.Extensions;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class NodeIdsEqualityTests
	{
		[U] public void Eq()
		{
			NodeIds types = "foo,bar";
			NodeIds[] equal = { "foo,bar", "bar,foo", "foo,  bar", "bar,  foo   " };
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			NodeIds types = "foo,bar";
			NodeIds[] notEqual = { "foo,bar,x", "foo" };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void Null()
		{
			NodeIds value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
