// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class IndexName : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// Emit the cluster-qualified name (e.g. "cluster_one:my-index") so cross-cluster index names
		// round-trip: the literal is implicitly parsed back into Cluster + Name. ToString() applies the
		// same Cluster-prefix logic the URL resolver uses (and handles the Name/Type cases); WriteString
		// quotes and escapes it. Emitting just the Name would silently drop the remote-cluster qualifier.
		writer.WriteString(ToString());
	}
}
