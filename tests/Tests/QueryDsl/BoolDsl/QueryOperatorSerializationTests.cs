// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.QueryDsl.BoolDsl;

[UsesVerify]
public class QueryOperatorSerializationTests : SerializerTestBase
{
	[U]
	public async Task SearchQueriesCombinedWithOrOperator_SerializeAsShouldClauses()
	{
		var search = new SearchRequest<Project>
		{
			Query = new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } ||
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueriesCombinedWithAndOperator_SerializeAsMustClauses()
	{
		var search = new SearchRequest<Project>
		{
			Query = new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } &&
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task MultipleSearchQueriesCombinedWithAndOperator_SerializeAsManyMustClauses()
	{
		var search = new SearchRequest<Project>
		{
			Query = new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } &&
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" } &&
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "z" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryWithUnaryNegationOperator_SerializeAsMustNotClause()
	{
		var search = new SearchRequest<Project>
		{
			Query = !new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryWithUnaryNegationOperator_CombinedWithAnd_SerializesCorrectly()
	{
		var search = new SearchRequest<Project>
		{
			Query = !new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } &&
					!new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryWithUnaryAddOperator_SerializeAsFilterClause()
	{
		var search = new SearchRequest<Project>
		{
			Query = +new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryWithUnaryAddOperator_CombinedWithAnd_SerializesCorrectly()
	{
		var search = new SearchRequest<Project>
		{
			Query = +new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } &&
					+new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryWithMustAndMustNot_SerializesCorrectly()
	{
		var search = new SearchRequest<Project>
		{
			Query = new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } &&
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" } &&
					!new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "z" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryWithMustAndMustNotAndFilter_SerializesCorrectly()
	{
		var search = new SearchRequest<Project>
		{
			Query = new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } &&
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" } &&
					!new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "z" } &&
					+new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "a" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryMixAndMatch_SerializesCorrectly()
	{
		var search = new SearchRequest<Project>
		{
			Query = new BoolQuery { Must = new Query[]
			{
				new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" },
				new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" },
				new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "z" } } } &&
				!new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "a" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryJoinsWithShouldClauses_SerializesCorrectly()
	{
		var search = new SearchRequest<Project>
		{
			Query = new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } &&
			(
				new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } ||
				new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } ||
				new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
			)
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryMixAndMatchMinimumShouldMatch_SerializesCorrectly()
	{
		var search = new SearchRequest<Project>
		{
			Query = new BoolQuery
			{
				Should = new Query[]
				{
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" },
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" },
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" },
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
				},
				MinimumShouldMatch = 2
			} || !new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } || new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task DoNotCombineLockedBools()
	{
		var search = new SearchRequest<Project>
		{
			Query = new BoolQuery
			{
				Should = new Query[]
				{
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
				},
				QueryName = "leftBool"
			} || new BoolQuery
			{
				Should = new Query[]
				{
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
				},
				QueryName = "rightBool"
			}
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task DoNotCombineRightLockedBool()
	{
		var search = new SearchRequest<Project>
		{
			Query = new BoolQuery
			{
				Should = new Query[]
				{
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
				}
			} || new BoolQuery
			{
				Should = new Query[]
				{
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
				},
				QueryName = "rightBool"
			}
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task DoNotCombineLeftLockedBool()
	{
		var search = new SearchRequest<Project>
		{
			Query = new BoolQuery
			{
				Should = new Query[]
				{
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
				},
				QueryName = "leftBool"
			} || new BoolQuery
			{
				Should = new Query[]
				{
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" }
				}
			}
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}
}
