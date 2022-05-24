// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// Marker for types which may also act as a key in a dictionary.
	/// </summary>
	internal interface IDictionaryKey
	{
		string Key(IElasticsearchClientSettings settings);
	}
}
