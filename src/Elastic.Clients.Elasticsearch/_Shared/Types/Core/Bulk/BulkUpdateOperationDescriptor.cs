// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using Elastic.Clients.Elasticsearch.Core.Search;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class BulkUpdateOperationDescriptor<TDocument, TPartialDocument> :
	BulkOperationDescriptor<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>
{
	internal new BulkUpdateOperation<TDocument, TPartialDocument> Instance => (BulkUpdateOperation<TDocument, TPartialDocument>)base.Instance;

	public BulkUpdateOperationDescriptor() :
		base(new BulkUpdateOperation<TDocument, TPartialDocument>(null!))
	{
	}

	public BulkUpdateOperationDescriptor(Id id) :
		base(new BulkUpdateOperation<TDocument, TPartialDocument>(id))
	{
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument document)
	{
		Instance.Doc = document;
		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool? partialDocumentAsUpsert = true)
	{
		Instance.DocAsUpsert = partialDocumentAsUpsert;
		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> IdFrom(TDocument idFrom, bool useAsUpsert = false)
	{
		Instance.Id = Elasticsearch.Id.From(idFrom);

		if (useAsUpsert)
		{
			Instance.Upsert = idFrom;
		}

		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> RetriesOnConflict(int? retriesOnConflict)
	{
		Instance.RetryOnConflict = retriesOnConflict;
		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Script(Action<ScriptDescriptor> configure)
	{
		Instance.Script = ScriptDescriptor.Build(configure);
		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Script(Script script)
	{
		Instance.Script = script;
		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool? scriptedUpsert = true)
	{
		Instance.ScriptedUpsert = scriptedUpsert;
		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Source(Union<bool, SourceFilter> source)
	{
		Instance.Source = source;
		return Self;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Upsert(TDocument document)
	{
		Instance.Upsert = document;
		return Self;
	}
}
