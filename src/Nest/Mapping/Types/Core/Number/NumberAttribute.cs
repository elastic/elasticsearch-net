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
	/// <summary>
	/// Maps a property as a number type. If no type is specified,
	/// the default type is float (single precision floating point).
	/// </summary>
	public class NumberAttribute : ElasticsearchDocValuesPropertyAttributeBase, INumberProperty
	{
		public NumberAttribute() : base(FieldType.Float) { }

		public NumberAttribute(NumberType type) : base(type.ToFieldType()) { }

		public double Boost
		{
#pragma warning disable 618
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
#pragma warning restore 618
		}

		public bool Coerce
		{
			get => Self.Coerce.GetValueOrDefault();
			set => Self.Coerce = value;
		}

		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault();
			set => Self.IgnoreMalformed = value;
		}

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public double NullValue
		{
			get => Self.NullValue.GetValueOrDefault();
			set => Self.NullValue = value;
		}

		public double ScalingFactor
		{
			get => Self.ScalingFactor.GetValueOrDefault();
			set => Self.ScalingFactor = value;
		}

		public IInlineScript Script
		{
			get => Self.Script;
			set => Self.Script = value;
		}

		public OnScriptError OnScriptError
		{
			get => Self.OnScriptError.GetValueOrDefault();
			set => Self.OnScriptError = value;
		}

		double? INumberProperty.Boost { get; set; }
		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }
		bool? INumberProperty.Index { get; set; }
		double? INumberProperty.NullValue { get; set; }
		double? INumberProperty.ScalingFactor { get; set; }
		IInlineScript INumberProperty.Script { get; set; }
		OnScriptError? INumberProperty.OnScriptError { get; set; }

		private INumberProperty Self => this;
	}
}
