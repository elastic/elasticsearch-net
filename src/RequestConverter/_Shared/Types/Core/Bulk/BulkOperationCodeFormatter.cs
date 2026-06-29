// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Elastic.Clients.Elasticsearch.Core.Search;

using RequestConverter;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

/// <summary>
/// Hand-crafted request-converter formatter for the <see cref="IBulkOperation"/> hierarchy. The bulk operation types
/// are not schema types (they model the NDJSON action/source pairs in the C# client by hand), so the generator emits
/// no <c>FormatCode</c> for them. Each operation is rendered as a constructor call (document for index/create, id for
/// update/delete) plus an object initializer for the metadata it carries, the mirror image of how
/// <see cref="BulkRequestConverter"/> reads them, so a materialized request and the request rebuilt from the emitted
/// code serialize to identical NDJSON.
/// </summary>
internal static class BulkOperationCodeFormatter
{
	public static void FormatCode(IBulkOperation operation, CodeWriter writer)
	{
		switch (operation)
		{
			case BulkIndexOperation<object> index:
				FormatIndexOrCreate("BulkIndexOperation", index, index.Document, index.Pipeline, index.DynamicTemplates, writer);
				break;
			case BulkCreateOperation<object> create:
				FormatIndexOrCreate("BulkCreateOperation", create, create.Document, create.Pipeline, create.DynamicTemplates, writer);
				break;
			case BulkUpdateOperation<object, object> update:
				FormatUpdate(update, writer);
				break;
			case BulkDeleteOperation delete:
				FormatDelete(delete, writer);
				break;
			default:
				throw new NotSupportedException(
					$"The request converter cannot format bulk operation '{operation.GetType()}'.");
		}
	}

	private static void FormatIndexOrCreate(string typeName, BulkOperation op, object? document, string? pipeline, IDictionary<string, string>? dynamicTemplates, CodeWriter writer)
	{
		writer.Write("new ").WriteTypeRef("Elastic.Clients.Elasticsearch.Core.Bulk." + typeName + "<object>").Write("(");
		writer.WriteValue(document);
		writer.Write(")");

		var properties = new List<(string Name, Action<CodeWriter> Write)>();
		AppendMetadata(properties, op, includeId: true);
		if (pipeline is not null)
			properties.Add(("Pipeline", w => w.WriteString(pipeline)));
		if (dynamicTemplates is not null)
			properties.Add(("DynamicTemplates", w => WriteStringDictionary(dynamicTemplates, w)));

		WriteInitializer(properties, writer);
	}

	private static void FormatUpdate(BulkUpdateOperation<object, object> op, CodeWriter writer)
	{
		writer.Write("new ").WriteTypeRef("Elastic.Clients.Elasticsearch.Core.Bulk.BulkUpdateOperation<object, object>").Write("(");
		WriteId(op.Id, writer);
		writer.Write(")");

		var properties = new List<(string Name, Action<CodeWriter> Write)>();
		AppendMetadata(properties, op, includeId: false);
		if (CodeWriter.ShouldFormat(op.Doc))
			properties.Add(("Doc", w => w.WriteValue(op.Doc)));
		if (CodeWriter.ShouldFormat(op.Upsert))
			properties.Add(("Upsert", w => w.WriteValue(op.Upsert)));
		if (op.Script is not null)
			properties.Add(("Script", w => op.Script.FormatCode(w)));
		if (op.DocAsUpsert is not null)
			properties.Add(("DocAsUpsert", w => w.WriteValue(op.DocAsUpsert.Value)));
		if (op.ScriptedUpsert is not null)
			properties.Add(("ScriptedUpsert", w => w.WriteValue(op.ScriptedUpsert.Value)));
		if (op.Source is not null)
			properties.Add(("Source", w => WriteSource(op.Source, w)));
		if (op.RetryOnConflict is not null)
			properties.Add(("RetryOnConflict", w => w.WriteValue(op.RetryOnConflict.Value)));

		WriteInitializer(properties, writer);
	}

	private static void FormatDelete(BulkDeleteOperation op, CodeWriter writer)
	{
		writer.Write("new ").WriteTypeRef("Elastic.Clients.Elasticsearch.Core.Bulk.BulkDeleteOperation").Write("(");
		WriteId(op.Id, writer);
		writer.Write(")");

		var properties = new List<(string Name, Action<CodeWriter> Write)>();
		AppendMetadata(properties, op, includeId: false);
		WriteInitializer(properties, writer);
	}

	// The base-class metadata shared by every operation. 'Id' is passed to the constructor for update/delete (they
	// have no parameterless constructor), so it is emitted in the initializer only for index/create.
	private static void AppendMetadata(List<(string Name, Action<CodeWriter> Write)> properties, BulkOperation op, bool includeId)
	{
		if (includeId && op.Id is not null)
			properties.Add(("Id", w => op.Id!.FormatCode(w)));
		if (op.Index is not null)
			properties.Add(("Index", w => op.Index!.FormatCode(w)));
		if (op.Routing is not null)
			properties.Add(("Routing", w => op.Routing!.FormatCode(w)));
		if (op.Version is not null)
			properties.Add(("Version", w => w.WriteValue(op.Version.Value)));
		if (op.VersionType is not null)
			properties.Add(("VersionType", w => w.WriteValue(op.VersionType.Value)));
		if (op.IfSequenceNumber is not null)
			properties.Add(("IfSequenceNumber", w => w.WriteValue(op.IfSequenceNumber.Value)));
		if (op.IfPrimaryTerm is not null)
			properties.Add(("IfPrimaryTerm", w => w.WriteValue(op.IfPrimaryTerm.Value)));
		if (op.RequireAlias is not null)
			properties.Add(("RequireAlias", w => w.WriteValue(op.RequireAlias.Value)));
	}

	private static void WriteInitializer(List<(string Name, Action<CodeWriter> Write)> properties, CodeWriter writer)
	{
		if (properties.Count == 0)
			return;

		// The constructor (and its argument) is already written; attach the metadata as a multi-line object
		// initializer so it matches the block style used elsewhere.
		using var initializer = writer.BeginInitializer();
		foreach (var (name, write) in properties)
			write(initializer.Property(name));
	}

	private static void WriteId(Id? id, CodeWriter writer)
	{
		if (id is null)
			writer.Write("null");
		else
			id.FormatCode(writer);
	}

	private static void WriteStringDictionary(IDictionary<string, string> dictionary, CodeWriter writer)
	{
		writer.Write("new ").WriteTypeRef("System.Collections.Generic.Dictionary<string, string>").Write("()");
		writer.WriteBlockList(
			dictionary,
			static (w, kvp) => { w.Write("{ "); w.WriteString(kvp.Key); w.Write(", "); w.WriteString(kvp.Value); w.Write(" }"); });
	}

	private static void WriteSource(Union<bool, SourceFilter> source, CodeWriter writer)
	{
		writer.Write("new ").WriteTypeRef("Elastic.Clients.Elasticsearch.Union<bool, Elastic.Clients.Elasticsearch.Core.Search.SourceFilter>").Write("(");
		if (source.Tag == UnionTag.T2)
			writer.WriteValue(source.Value2);
		else
			writer.WriteValue(source.Value1);

		writer.Write(")");
	}
}
