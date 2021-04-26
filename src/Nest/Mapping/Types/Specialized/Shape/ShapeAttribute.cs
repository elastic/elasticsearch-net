/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace Nest
{
	/// <inheritdoc cref="IShapeProperty" />
	public class ShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IShapeProperty
	{
		public ShapeAttribute() : base(FieldType.Shape) { }

		bool? IShapeProperty.IgnoreMalformed { get; set; }
		bool? IShapeProperty.IgnoreZValue { get; set; }
		ShapeOrientation? IShapeProperty.Orientation { get; set; }
		private IShapeProperty Self => this;
		bool? IShapeProperty.Coerce { get; set; }

		/// <inheritdoc cref="IShapeProperty.IgnoreMalformed" />
		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault(false);
			set => Self.IgnoreMalformed = value;
		}

		/// <inheritdoc cref="IShapeProperty.IgnoreZValue" />
		public bool IgnoreZValue
		{
			get => Self.IgnoreZValue.GetValueOrDefault(true);
			set => Self.IgnoreZValue = value;
		}

		/// <inheritdoc cref="IShapeProperty.Orientation" />
		public ShapeOrientation Orientation
		{
			get => Self.Orientation.GetValueOrDefault(ShapeOrientation.CounterClockWise);
			set => Self.Orientation = value;
		}

		/// <inheritdoc cref="IShapeProperty.Coerce" />
		public bool Coerce
		{
			get => Self.Coerce.GetValueOrDefault(true);
			set => Self.Coerce = value;
		}
	}
}
