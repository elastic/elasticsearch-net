// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch
{
	public class PropertyMapping : IPropertyMapping
	{
		public static PropertyMapping Ignored = new() { Ignore = true };

		///// <inheritdoc />
		public bool Ignore { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }
	}
}
