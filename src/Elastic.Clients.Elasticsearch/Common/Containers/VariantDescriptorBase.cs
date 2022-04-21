// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public abstract class VariantDescriptorBase<T> : DescriptorBase<T> where T : DescriptorBase<T>
{
	internal string VariantName { get; private set; }

	protected void SetVariantName(string name) => VariantName = name;
}
