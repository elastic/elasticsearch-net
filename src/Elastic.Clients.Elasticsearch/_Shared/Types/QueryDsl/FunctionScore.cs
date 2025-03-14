// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.QueryDsl;

// A quirk of the function score type which is valid without a variant.

public partial class FunctionScore
{
	/// <summary>
	/// The weight score allows you to multiply the score by the provided weight.
	/// </summary>
	public static FunctionScore WeightScore(double weight) => new() { Weight = weight };
}

public partial class FunctionScoreDescriptor<TDocument>
{
	public FunctionScoreDescriptor<TDocument> WeightScore(double weight) => Set(null, null).Weight(weight);
}

public partial class FunctionScoreDescriptor
{
	public FunctionScoreDescriptor WeightScore(double weight) => Set(null, null).Weight(weight);
}
