// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.QueryDsl.BoolDsl;

public class CombinationUsageTests : OperatorUsageBase
{
	[U]
	public void DoesNotJoinTwoShouldsUsingAnd() =>
		ReturnsBool(
			(TermQuery || TermQuery) && (TermQuery || TermQuery),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});
	[U]
	public void DoesJoinTwoShouldsUsingOr() =>
		ReturnsBool(
			TermQuery || TermQuery || (TermQuery || TermQuery),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(4);
				b.Must.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

	[U]
	public void DoesNotJoinTwoMustsUsingOr() =>
		ReturnsBool(
			TermQuery && TermQuery || TermQuery && TermQuery,
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
				b.Must.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

	[U]
	public void DoesJoinTwoMustsUsingAnd() =>
		ReturnsBool(
		TermQuery && TermQuery && (TermQuery && TermQuery),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(4);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

	[U]
	public void AndJoinsMustNot() =>
		ReturnsBool(
			TermQuery && !TermQuery,
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(1);
				b.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			});

	[U]
	public void OrDoesNotJoinMustNot() =>
		ReturnsBool(
			TermQuery || !TermQuery,
			b => { b.Should.Should().NotBeEmpty().And.HaveCount(2); });

	[U]
	public void OrDoesNotJoinFilter() =>
		ReturnsBool(
			TermQuery || !TermQuery,
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
				b.Filter.Should().BeNull();
			});

	[U]
	public void AndJoinsFilter() =>
		ReturnsBool(
			TermQuery && +TermQuery,
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(1);
				b.Filter.Should().NotBeEmpty().And.HaveCount(1);
			});
}
