// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 namespace Nest
{
	/// <inheritdoc cref="IPointProperty" />
	public class PointAttribute : ElasticsearchPropertyAttributeBase, IPointProperty
	{
		public PointAttribute() : base(FieldType.Point) { }

		bool? IPointProperty.IgnoreMalformed { get; set; }
		bool? IPointProperty.IgnoreZValue { get; set; }
		CartesianPoint IPointProperty.NullValue { get; set; }

		private IPointProperty Self => this;

		/// <inheritdoc cref="IPointProperty.IgnoreMalformed" />
		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault(false);
			set => Self.IgnoreMalformed = value;
		}

		/// <inheritdoc cref="IPointProperty.IgnoreZValue" />
		public bool IgnoreZValue
		{
			get => Self.IgnoreZValue.GetValueOrDefault(true);
			set => Self.IgnoreZValue = value;
		}

		/// <inheritdoc cref="IPointProperty.NullValue" />
		public CartesianPoint NullValue
		{
			get => Self.NullValue;
			set => Self.NullValue = value;
		}
	}
}
