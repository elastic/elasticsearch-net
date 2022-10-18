// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

// TODO - Should be added as a rule to the descriptor generator
//public sealed partial class SourceRequestDescriptor<TDocument>
//{
//	public SourceRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) => Doc
//}

public partial class SourceRequestDescriptor
{
	/// <summary>
	/// A shortcut into calling Index(typeof(TOther)).
	/// </summary>
	public SourceRequestDescriptor Index<TOther>()
	{
		RouteValues.Required("index", (IndexName)typeof(TOther));
		return Self;
	}
}
