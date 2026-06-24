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
		var __init = writer.BeginObjectInitializer("BulkRequest", false);
		if (Index is not null)
		{
			__init.Property("Index");
			Index.FormatCode(writer);
		}

		if (IncludeSourceOnError is not null)
		{
			__init.Property("IncludeSourceOnError");
			writer.WriteValue(IncludeSourceOnError.Value);
		}

		if (ListExecutedPipelines is not null)
		{
			__init.Property("ListExecutedPipelines");
			writer.WriteValue(ListExecutedPipelines.Value);
		}

		if (Pipeline is not null)
		{
			__init.Property("Pipeline");
			writer.WriteString(Pipeline);
		}

		if (Refresh is not null)
		{
			__init.Property("Refresh");
			Elastic.Clients.Elasticsearch.RefreshCodeFormatter.FormatCode(Refresh.Value, writer);
		}

		if (RequireAlias is not null)
		{
			__init.Property("RequireAlias");
			writer.WriteValue(RequireAlias.Value);
		}

		if (RequireDataStream is not null)
		{
			__init.Property("RequireDataStream");
			writer.WriteValue(RequireDataStream.Value);
		}

		if (Routing is not null)
		{
			__init.Property("Routing");
			Routing.FormatCode(writer);
		}

		if (Source is not null)
		{
			__init.Property("Source");
			Source.FormatCode(writer);
		}

		if (SourceExcludes is not null)
		{
			__init.Property("SourceExcludes");
			SourceExcludes.FormatCode(writer);
		}

		if (SourceIncludes is not null)
		{
			__init.Property("SourceIncludes");
			SourceIncludes.FormatCode(writer);
		}

		if (Timeout is not null)
		{
			__init.Property("Timeout");
			Timeout.FormatCode(writer);
		}

		if (WaitForActiveShards is not null)
		{
			__init.Property("WaitForActiveShards");
			WaitForActiveShards.FormatCode(writer);
		}

		// The bulk body: a 'BulkOperationsCollection' (an IList<IBulkOperation>) rendered as a collection
		// initializer — 'new BulkOperationsCollection { op0, op1, ... }'. A collection-expression ('[ ... ]')
		// would not bind to 'BulkOperationsCollection', so an explicit 'new' with a '{ }' block is used.
		if (Operations is not null)
		{
			__init.Property("Operations");
			writer.Write("new global::Elastic.Clients.Elasticsearch.Core.Bulk.BulkOperationsCollection ");
			writer.WriteInlineList(
				Operations,
				static (w, operation) => global::Elastic.Clients.Elasticsearch.Core.Bulk.BulkOperationCodeFormatter.FormatCode(operation, w),
				"{ ", " }", ", ");
		}

		__init.Dispose();
	}
}
