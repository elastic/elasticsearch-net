// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <inheritdoc cref="IWildcardProperty"/>
	public class WildcardAttribute : ElasticsearchPropertyAttributeBase, IWildcardProperty
	{
		public WildcardAttribute() : base(FieldType.Wildcard) { }

		int? IWildcardProperty.IgnoreAbove { get; set; }

		private IWildcardProperty Self => this;

		/// <inheritdoc cref="IWildcardProperty.IgnoreAbove" />
		public int IgnoreAbove
		{
			get => Self.IgnoreAbove.GetValueOrDefault(2147483647);
			set => Self.IgnoreAbove = value;
		}

		/// <inheritdoc cref="IWildcardProperty.NullValue" />
		public string NullValue { get; set; }
	}
}
