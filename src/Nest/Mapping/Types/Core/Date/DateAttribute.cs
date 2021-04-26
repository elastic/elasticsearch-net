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

using System;

namespace Nest
{
	public class DateAttribute : ElasticsearchDocValuesPropertyAttributeBase, IDateProperty
	{
		public DateAttribute() : base(FieldType.Date) { }

		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

		public string Format
		{
			get => Self.Format;
			set => Self.Format = value;
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

		public DateTime NullValue
		{
			get => Self.NullValue.GetValueOrDefault();
			set => Self.NullValue = value;
		}

		double? IDateProperty.Boost { get; set; }
		INumericFielddata IDateProperty.Fielddata { get; set; }
		string IDateProperty.Format { get; set; }
		bool? IDateProperty.IgnoreMalformed { get; set; }

		bool? IDateProperty.Index { get; set; }
		DateTime? IDateProperty.NullValue { get; set; }
		private IDateProperty Self => this;
	}
}
