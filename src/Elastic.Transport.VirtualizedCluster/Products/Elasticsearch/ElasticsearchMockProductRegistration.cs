// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Transport.Products;
using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Transport.VirtualizedCluster.Products.Elasticsearch
{
	public class ElasticsearchMockProductRegistration : IMockProductRegistration
	{
		public static IMockProductRegistration Default { get; } = new ElasticsearchMockProductRegistration();

		public IProductRegistration ProductRegistration { get; } = ElasticsearchProductRegistration.Default;

		public byte[] CreateSniffResponseBytes(IReadOnlyList<Node> nodes, string stackVersion, string publishAddressOverride, bool returnFullyQualifiedDomainNames) =>
			SniffResponseBytes.Create(nodes, stackVersion, publishAddressOverride, returnFullyQualifiedDomainNames);
	}
}
