// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Core;

internal interface IEnumStruct<TSelf> where TSelf : struct, IEnumStruct<TSelf>
{
	// TODO: Can be made static when targeting .NET 7 and higher
	TSelf Create(string value);
}
