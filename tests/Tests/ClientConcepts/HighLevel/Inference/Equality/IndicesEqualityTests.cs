// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class IndicesEqualityTests
	{
		[U] public void Eq()
		{
			Indices types = "foo,bar";
			Indices[] equal = { "foo,bar", "bar,foo", "foo,  bar", "bar,  foo   " };
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}

			(Indices.All == "_all").Should().BeTrue();
		}


		[U] public void NotEq()
		{
			Indices types = "foo,bar";
			Indices[] notEqual = { "foo,bar,x", "foo", typeof(Project) };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void TypedEq()
		{
			Indices t1 = typeof(Project), t2 = typeof(Project);
			(t1 == t2).ShouldBeTrue(t2);
		}

		[U] public void TypedNotEq()
		{
			Indices t1 = typeof(Project), t2 = typeof(CommitActivity);
			(t1 != t2).ShouldBeTrue(t2);
		}

		[U] public void Null()
		{
			Indices value = typeof(Project);
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
