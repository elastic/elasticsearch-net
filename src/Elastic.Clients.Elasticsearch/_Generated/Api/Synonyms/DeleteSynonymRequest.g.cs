// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Synonyms;

public sealed partial class DeleteSynonymRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Delete a synonym set.
/// </para>
/// <para>
/// You can only delete a synonyms set that is not in use by any index analyzer.
/// </para>
/// <para>
/// Synonyms sets can be used in synonym graph token filters and synonym token filters.
/// These synonym filters can be used as part of search analyzers.
/// </para>
/// <para>
/// Analyzers need to be loaded when an index is restored (such as when a node starts, or the index becomes open).
/// Even if the analyzer is not used on any field mapping, it still needs to be loaded on the index recovery phase.
/// </para>
/// <para>
/// If any analyzers cannot be loaded, the index becomes unavailable and the cluster status becomes red or yellow as index shards are not available.
/// To prevent that, synonyms sets that are used in analyzers can't be deleted.
/// A delete request in this case will return a 400 response code.
/// </para>
/// <para>
/// To remove a synonyms set, you must first remove all indices that contain analyzers using it.
/// You can migrate an index by creating a new index that does not contain the token filter with the synonyms set, and use the reindex API in order to copy over the index data.
/// Once finished, you can delete the index.
/// When the synonyms set is not used in analyzers, you will be able to delete it.
/// </para>
/// </summary>
public sealed partial class DeleteSynonymRequest : PlainRequest<DeleteSynonymRequestParameters>
{
	public DeleteSynonymRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SynonymsDeleteSynonym;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "synonyms.delete_synonym";
}

/// <summary>
/// <para>
/// Delete a synonym set.
/// </para>
/// <para>
/// You can only delete a synonyms set that is not in use by any index analyzer.
/// </para>
/// <para>
/// Synonyms sets can be used in synonym graph token filters and synonym token filters.
/// These synonym filters can be used as part of search analyzers.
/// </para>
/// <para>
/// Analyzers need to be loaded when an index is restored (such as when a node starts, or the index becomes open).
/// Even if the analyzer is not used on any field mapping, it still needs to be loaded on the index recovery phase.
/// </para>
/// <para>
/// If any analyzers cannot be loaded, the index becomes unavailable and the cluster status becomes red or yellow as index shards are not available.
/// To prevent that, synonyms sets that are used in analyzers can't be deleted.
/// A delete request in this case will return a 400 response code.
/// </para>
/// <para>
/// To remove a synonyms set, you must first remove all indices that contain analyzers using it.
/// You can migrate an index by creating a new index that does not contain the token filter with the synonyms set, and use the reindex API in order to copy over the index data.
/// Once finished, you can delete the index.
/// When the synonyms set is not used in analyzers, you will be able to delete it.
/// </para>
/// </summary>
public sealed partial class DeleteSynonymRequestDescriptor<TDocument> : RequestDescriptor<DeleteSynonymRequestDescriptor<TDocument>, DeleteSynonymRequestParameters>
{
	internal DeleteSynonymRequestDescriptor(Action<DeleteSynonymRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DeleteSynonymRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SynonymsDeleteSynonym;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "synonyms.delete_synonym";

	public DeleteSynonymRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Delete a synonym set.
/// </para>
/// <para>
/// You can only delete a synonyms set that is not in use by any index analyzer.
/// </para>
/// <para>
/// Synonyms sets can be used in synonym graph token filters and synonym token filters.
/// These synonym filters can be used as part of search analyzers.
/// </para>
/// <para>
/// Analyzers need to be loaded when an index is restored (such as when a node starts, or the index becomes open).
/// Even if the analyzer is not used on any field mapping, it still needs to be loaded on the index recovery phase.
/// </para>
/// <para>
/// If any analyzers cannot be loaded, the index becomes unavailable and the cluster status becomes red or yellow as index shards are not available.
/// To prevent that, synonyms sets that are used in analyzers can't be deleted.
/// A delete request in this case will return a 400 response code.
/// </para>
/// <para>
/// To remove a synonyms set, you must first remove all indices that contain analyzers using it.
/// You can migrate an index by creating a new index that does not contain the token filter with the synonyms set, and use the reindex API in order to copy over the index data.
/// Once finished, you can delete the index.
/// When the synonyms set is not used in analyzers, you will be able to delete it.
/// </para>
/// </summary>
public sealed partial class DeleteSynonymRequestDescriptor : RequestDescriptor<DeleteSynonymRequestDescriptor, DeleteSynonymRequestParameters>
{
	internal DeleteSynonymRequestDescriptor(Action<DeleteSynonymRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteSynonymRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SynonymsDeleteSynonym;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "synonyms.delete_synonym";

	public DeleteSynonymRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}