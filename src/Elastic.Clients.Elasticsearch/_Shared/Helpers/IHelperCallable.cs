// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// May be applied to helper requests where they may be called by an upstream helper.
/// </summary>
/// <remarks>
/// For example, the reindex helper calls down into the bulk helper and scroll helpers.
/// <see cref="BulkAllRequest{T}"/> therefore implements this interface.
/// </remarks>
internal interface IHelperCallable
{
	/// <summary>
	/// The <see cref="RequestMetaData"/> of the parent helper when this requestis created by a parent
	/// helper.
	/// </summary>
	RequestMetaData ParentMetaData { get; internal set; }
}
