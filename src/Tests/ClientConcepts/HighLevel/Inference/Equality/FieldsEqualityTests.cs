using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class FieldsEqualityTests
	{
		[U] public void Eq()
		{
			Fields name = "foo, bar";
			Fields[] equal = {"foo, bar", "  foo,   bar", "bar, foo", "   bar  ,   foo  "};
			foreach (var t in equal)
			{
				(t == name).ShouldBeTrue(t);
				t.Should().BeEquivalentTo(name);
			}
		}

		[U] public void NotEq()
		{
			Fields name = "foo, bar";
			Fields[] notEqual = {"bar", "foo, bar, x", "", "   ", " foo ", Infer.Field<Project>(p=>p.Name)};
			foreach (var t in notEqual)
			{
				(t != name).ShouldBeTrue(t);
				if (t != null) t.Should().NotBeEquivalentTo(name);
			}
		}

		[U] public void TypedEq()
		{
			Fields t1 = Infer.Field<Project>(p => p.Name), t2 = Infer.Field<Project>(p => p.Name);
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);

			t1 = Infer.Field<Project>(p => p.Name, 1.1); t2 = Infer.Field<Project>(p => p.Name, 1.1);
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);
		}

		[U] public void TypedNotEq()
		{
			Fields t1 = Infer.Field<Developer>(p => p.Id), t2 = Infer.Field<CommitActivity>(p => p.Id);
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBeEquivalentTo(t2);

			t1 = Infer.Field<Project>(p => p.Name); t2 = Infer.Field<Project>(p => p.Name, 2.0);
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBeEquivalentTo(t2);

			t1 = Infer.Field<Project>(p => p.Name, 1.0); t2 = Infer.Field<Project>(p => p.Name, 2.0);
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBeEquivalentTo(t2);
		}

		[U] public void ReflectedEq()
		{
			Fields t1 = typeof(Project).GetProperty(nameof(Project.Name)),
				  t2 = typeof(Project).GetProperty(nameof(Project.Name));
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);
		}

		[U] public void ReflectedNotEq()
		{
			Fields t1 = typeof(CommitActivity).GetProperty(nameof(CommitActivity.Id)),
				  t2 = typeof(Developer).GetProperty(nameof(Developer.Id));
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBeEquivalentTo(t2);
		}

		[U] public void Null()
		{
			Fields value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
