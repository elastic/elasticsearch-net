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
