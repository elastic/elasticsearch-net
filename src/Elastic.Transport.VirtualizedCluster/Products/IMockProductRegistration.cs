// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Transport.Products;

namespace Elastic.Transport.VirtualizedCluster.Products
{
	public interface IMockProductRegistration
	{
		IProductRegistration ProductRegistration { get; }

		byte[] CreateSniffResponseBytes(IReadOnlyList<Node> nodes, string stackVersion, string publishAddressOverride, bool returnFullyQualifiedDomainNames);
	}
}
