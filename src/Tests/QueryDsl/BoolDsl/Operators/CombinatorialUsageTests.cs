using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.QueryDsl.BoolDsl.Operators
{
	public class CombinationUsageTests : OperatorUsageBase
	{
		[U] void DoesNotJoinTwoShouldsUsingAnd() => ReturnsBool(
			(Query || Query) && (Query || Query),
			q => (q.Query() || q.Query()) && (q.Query() || q.Query()),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();

			});

		[U] void DoesJoinTwoShouldsUsingOr() => ReturnsBool(
			(Query || Query) || (Query || Query),
			q => (q.Query() || q.Query()) || (q.Query() || q.Query()),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(4);
				b.Must.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

		[U] void DoesNotJoinTwoMustsUsingOr() => ReturnsBool(
			(Query && Query) || (Query && Query),
			q => (q.Query() && q.Query()) || (q.Query() && q.Query()),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
				b.Must.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

		[U] void DoesJoinTwoMustsUsingAnd() => ReturnsBool(
			(Query && Query) && (Query && Query),
			q => (q.Query() && q.Query()) && (q.Query() && q.Query()),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(4);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

		[U] void AndJoinsMustNot() => ReturnsBool(
			Query && !Query,
			q => q.Query() && !q.Query(),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(1);
				b.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			});

		[U] void OrDoesNotJoinMustNot() => ReturnsBool(
			Query || !Query,
			q => q.Query() || !q.Query(),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
			});

		[U] void OrDoesNotJoinFilter() => ReturnsBool(
			Query || !Query,
			q => q.Query() || +q.Query(),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
				b.Filter.Should().BeNull();
			});

		[U] void AndJoinsFilter() => ReturnsBool(
			Query && +Query,
			q => q.Query() && +q.Query(),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(1);
				b.Filter.Should().NotBeEmpty().And.HaveCount(1);
			});
	}
}
