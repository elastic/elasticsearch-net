// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Aggregation response for an aggregation request
	/// </summary>
	[JsonFormatter(typeof(AggregateFormatter))]
	public interface IAggregate
	{
		/// <summary>
		/// Metadata for the aggregation
		/// </summary>
		IReadOnlyDictionary<string, object> Meta { get; }
	}
}
