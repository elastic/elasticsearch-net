// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Hand-crafted request-converter formatter for <see cref="BulkRequest"/>. The parameter block mirrors what the
/// generator emits for any request (and is kept in sync with it); the difference is the <c>Operations</c> body, which
/// the generator deliberately erases (it cannot model the NDJSON action/source pairs), so it is appended here. The
/// type is marked <c>[Codegen(GenerateFormatCode = false)]</c> so the generator skips its FormatCode and this one wins.
/// </summary>
public partial class BulkRequest : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// Bulk has no fluent-descriptor form here (the generator cannot model the NDJSON Operations), so this
		// hand-crafted body always emits object-initializer syntax; force that mode for the whole subtree so a
		// descriptor-capable nested value (e.g. an update operation's Script) renders as a value, not a chain.
		using var _objectInitializer = writer.ForceObjectInitializer();

		var initializer = writer.BeginObjectInitializer("Elastic.Clients.Elasticsearch.BulkRequest", false);
		if (Index is not null)
		{
			initializer.Property("Index");
			Index.FormatCode(writer);
		}

		if (IncludeSourceOnError is not null)
		{
			initializer.Property("IncludeSourceOnError");
			writer.WriteValue(IncludeSourceOnError.Value);
		}

		if (ListExecutedPipelines is not null)
		{
			initializer.Property("ListExecutedPipelines");
			writer.WriteValue(ListExecutedPipelines.Value);
		}

		if (Pipeline is not null)
		{
			initializer.Property("Pipeline");
			writer.WriteString(Pipeline);
		}

		if (Refresh is not null)
		{
			initializer.Property("Refresh");
			Elastic.Clients.Elasticsearch.RefreshCodeFormatter.FormatCode(Refresh.Value, writer);
		}

		if (RequireAlias is not null)
		{
			initializer.Property("RequireAlias");
			writer.WriteValue(RequireAlias.Value);
		}

		if (RequireDataStream is not null)
		{
			initializer.Property("RequireDataStream");
			writer.WriteValue(RequireDataStream.Value);
		}

		if (Routing is not null)
		{
			initializer.Property("Routing");
			Routing.FormatCode(writer);
		}

		if (Source is not null)
		{
			initializer.Property("Source");
			Source.FormatCode(writer);
		}

		if (SourceExcludes is not null)
		{
			initializer.Property("SourceExcludes");
			SourceExcludes.FormatCode(writer);
		}

		if (SourceIncludes is not null)
		{
			initializer.Property("SourceIncludes");
			SourceIncludes.FormatCode(writer);
		}

		if (Timeout is not null)
		{
			initializer.Property("Timeout");
			Timeout.FormatCode(writer);
		}

		if (WaitForActiveShards is not null)
		{
			initializer.Property("WaitForActiveShards");
			WaitForActiveShards.FormatCode(writer);
		}

		// The bulk body: a 'BulkOperationsCollection' (an IList<IBulkOperation>) rendered as a collection
		// initializer: 'new BulkOperationsCollection { op0, op1, ... }'. A collection-expression ('[ ... ]')
		// would not bind to 'BulkOperationsCollection', so an explicit 'new' with a '{ }' block is used.
		if (Operations is not null)
		{
			initializer.Property("Operations");
			writer.Write("new ").WriteTypeRef("Elastic.Clients.Elasticsearch.Core.Bulk.BulkOperationsCollection");
			writer.WriteBlockList(
				Operations,
				static (w, operation) => global::Elastic.Clients.Elasticsearch.Core.Bulk.BulkOperationCodeFormatter.FormatCode(operation, w));
		}

		initializer.Dispose();
	}
}
