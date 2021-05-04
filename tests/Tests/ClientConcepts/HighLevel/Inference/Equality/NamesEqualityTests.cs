// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class NamesEqualityTests
	{
		[U] public void Eq()
		{
			Names types = "foo,bar";
			Names[] equal = { "foo,bar", "bar,foo", "foo,  bar", "bar,  foo   " };
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			Names types = "foo,bar";
			Names[] notEqual = { "foo,bar,x", "foo" };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void Null()
		{
			Names types = "foo,bar";
			(types == null).Should().BeFalse();
			(null == types).Should().BeFalse();
		}
	}
}
