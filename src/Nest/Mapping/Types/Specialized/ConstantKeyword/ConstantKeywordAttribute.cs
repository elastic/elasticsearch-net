// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <inheritdoc cref="IConstantKeywordProperty"/>
	public class ConstantKeywordAttribute : ElasticsearchPropertyAttributeBase, IConstantKeywordProperty
	{
		public ConstantKeywordAttribute() : base(FieldType.ConstantKeyword) { }

		/// <inheritdoc />
		public object Value { get; set; }
	}
}
