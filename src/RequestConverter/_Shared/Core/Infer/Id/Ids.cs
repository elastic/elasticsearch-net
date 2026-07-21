// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Ids : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer) =>
		writer.WriteImplicitArray(_ids, static (w, id) => ((RequestConverter.ICodeFormattable)id).FormatCode(w));
}
