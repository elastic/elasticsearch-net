// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A sentinel type used to mark internal JSON constructors.
/// </summary>
internal sealed class JsonConstructorSentinel
{
	public static readonly JsonConstructorSentinel Instance = new();
}
