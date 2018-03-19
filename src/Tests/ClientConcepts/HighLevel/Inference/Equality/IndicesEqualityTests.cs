using FluentAssertions;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class IndicesEqualityTests
	{
		[U] public void Eq()
		{
			Nest.Indices types = "foo,bar";
			Nest.Indices[] equal = {"foo,bar", "bar,foo", "foo,  bar", "bar,  foo   "};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}

			(Nest.Indices.All == "_all").Should().BeTrue();
		}


		[U] public void NotEq()
		{
			Nest.Indices types = "foo,bar";
			Nest.Indices[] notEqual = {"foo,bar,x", "foo", typeof(Project)};
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void TypedEq()
		{
			Nest.Indices t1 = typeof(Project), t2 = typeof(Project);
			(t1 == t2).ShouldBeTrue(t2);
		}

		[U] public void TypedNotEq()
		{
			Nest.Indices t1 = typeof(Project), t2 = typeof(CommitActivity);
			(t1 != t2).ShouldBeTrue(t2);
		}
		[U] public void Null()
		{
			Nest.Indices value = typeof(Project);
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
