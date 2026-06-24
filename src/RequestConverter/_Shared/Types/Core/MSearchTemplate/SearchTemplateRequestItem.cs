// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Elastic.Clients.Elasticsearch.Core.MSearchTemplate;

public sealed partial class SearchTemplateRequestItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// SearchTemplateRequestItem has no parameterless constructor (Header/Body are init-only and set via the
		// ctor), so emit a constructor call rather than an object initializer.
		writer.Write("new SearchTemplateRequestItem(");
		if (Header is not null)
		{
			Header.FormatCode(writer);
			writer.Write(", ");
		}

		Body.FormatCode(writer);
		writer.Write(")");
	}
}
