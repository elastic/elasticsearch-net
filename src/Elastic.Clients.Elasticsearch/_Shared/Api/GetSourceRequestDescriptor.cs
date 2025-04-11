// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public readonly partial struct GetSourceRequestDescriptor
{
	/// <summary>
	/// A shortcut into calling Index(typeof(TOther)).
	/// </summary>
	public GetSourceRequestDescriptor Index<TOther>()
	{
		Instance.Index = typeof(TOther);
		return this;
	}
}
