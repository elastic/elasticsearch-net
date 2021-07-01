// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <inheritdoc cref="IDenseVectorProperty" />
	public class DenseVectorAttribute : ElasticsearchPropertyAttributeBase, IDenseVectorProperty
	{
		public DenseVectorAttribute() : base(FieldType.DenseVector) {}

		/// <inheritdoc cref="IDenseVectorProperty.Dimensions" />
		public int Dimensions
		{
			get => Self.Dimensions.GetValueOrDefault();
			set => Self.Dimensions = value;
		}

		int? IDenseVectorProperty.Dimensions { get; set; }

		private IDenseVectorProperty Self => this;
	}
}
