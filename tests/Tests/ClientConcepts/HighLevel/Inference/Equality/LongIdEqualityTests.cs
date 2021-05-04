// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class LongIdEqualityTests
	{
		[U] public void Eq()
		{
			LongId types = 2;
			LongId[] equal = { 2L, 2 };
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}

			LongId l1 = 2, l2 = 2;
			(l1 == l2).ShouldBeTrue(l2);
			(l1 == 2).ShouldBeTrue(l1);
			l1.Should().Be(l2);
			l1.Should().Be(2);
		}

		[U] public void NotEq()
		{
			LongId types = 3;
			LongId[] notEqual = { 4L, 4 };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
			LongId l1 = 2, l2 = 3;
			(l1 != l2).ShouldBeTrue(l2);
			(l1 != 3).ShouldBeTrue(l1);
			l1.Should().NotBe(l2);
			l1.Should().NotBe(3);
		}

		[U] public void Null()
		{
			LongId c = 10;
			(c == null).Should().BeFalse();
			(null == c).Should().BeFalse();
		}
	}
}
