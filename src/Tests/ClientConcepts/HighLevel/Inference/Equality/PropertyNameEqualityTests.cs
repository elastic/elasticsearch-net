using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class PropertyNameEqualityTests
	{
		[U] public void Eq()
		{
			PropertyName name = "foo";
			PropertyName[] equal = {"foo"};
			foreach (var t in equal)
			{
				(t == name).ShouldBeTrue(t);
				t.Should().Be(name);
			}
		}

		[U] public void NotEq()
		{
			PropertyName name = "foo";
			PropertyName[] notEqual = {"bar", "foo  ", "  foo   ", "x", "", "   ", Infer.Property<Project>(p=>p.Name)};
			foreach (var t in notEqual)
			{
				(t != name).ShouldBeTrue(t);
				t.Should().NotBe(name);
			}
		}

		[U] public void TypedEq()
		{
			PropertyName t1 = Infer.Property<Project>(p => p.Name), t2 = Infer.Property<Project>(p => p.Name);
			(t1 == t2).ShouldBeTrue(t2);
		}

		[U] public void TypedNotEq()
		{
			PropertyName t1 = Infer.Property<Developer>(p => p.Id), t2 = Infer.Property<CommitActivity>(p => p.Id);
			(t1 != t2).ShouldBeTrue(t2);
		}

		[U] public void Null()
		{
			PropertyName value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
