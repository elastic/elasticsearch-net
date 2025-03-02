// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Serialization;

internal interface IEnumStruct<out TSelf>
	where TSelf : struct, IEnumStruct<TSelf>
{
	public string Value { get; }

#if NET7_0_OR_GREATER

	public static abstract TSelf Create(string value);

#else

	public TSelf Create(string value);

#endif
}
