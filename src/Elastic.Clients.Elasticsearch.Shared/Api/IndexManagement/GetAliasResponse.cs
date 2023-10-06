// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public partial class GetAliasResponse
{
	/// <summary>
	/// A dictionary containing the <see cref="IndexAliases"/> organised by <see cref="IndexName"/>.
	/// </summary>
	[JsonIgnore]
	public IReadOnlyDictionary<IndexName, IndexAliases> Aliases => BackingDictionary;

	/// <summary>
	/// Checks if a response is functionally valid or not.
	/// This is a client abstraction to have a single property to check whether there was something wrong with a request.
	/// <para>
	/// The aliases endpoint returns a 404 when some of the specified alias names for an index cannot be found. For such partial responses,
	/// the client considers the response to be valid.
	/// </para>
	/// </summary>
	public override bool IsValidResponse => base.IsValidResponse || Aliases.Count > 0;
}
