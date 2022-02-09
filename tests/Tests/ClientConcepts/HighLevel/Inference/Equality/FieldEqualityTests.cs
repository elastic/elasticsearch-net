// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class FieldEqualityTests
	{
		[U] public void Eq()
		{
			Field name = "foo";
			Field[] equal = { "foo" };
			foreach (var t in equal)
			{
				(t == name).ShouldBeTrue(t);
				t.Should().Be(name);
			}
		}

		[U] public void NotEq()
		{
			Field name = "foo";
			Field[] notEqual = { "bar", "x", "", "   ", " foo ", Infer.Field<Project>(p => p.Name) };
			foreach (var t in notEqual)
			{
				(t != name).ShouldBeTrue(t);
				t.Should().NotBe(name);
			}
		}

		[U] public void TypedEq()
		{
			Field t1 = Infer.Field<Project>(p => p.Name), t2 = Infer.Field<Project>(p => p.Name);
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().Be(t2);
		}

		[U] public void TypedNotEq()
		{
			Field t1 = Infer.Field<Developer>(p => p.Id), t2 = Infer.Field<CommitActivity>(p => p.Id);
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBe(t2);
		}

		[U] public void ReflectedEq()
		{
			Field t1 = typeof(Project).GetProperty(nameof(Project.Name)),
				t2 = typeof(Project).GetProperty(nameof(Project.Name));
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().Be(t2);
		}

		[U] public void ReflectedNotEq()
		{
			Field t1 = typeof(CommitActivity).GetProperty(nameof(CommitActivity.Id)),
				t2 = typeof(Developer).GetProperty(nameof(Developer.Id));
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBe(t2);
		}

		[U] public void Null()
		{
			Field value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
