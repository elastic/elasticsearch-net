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
	public abstract class RangePropertyAttributeBase : ElasticsearchDocValuesPropertyAttributeBase, IRangeProperty
	{
		protected RangePropertyAttributeBase(RangeType type) : base(type.ToFieldType()) { }

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

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		double? IRangeProperty.Boost { get; set; }

		bool? IRangeProperty.Coerce { get; set; }
		bool? IRangeProperty.Index { get; set; }
		private IRangeProperty Self => this;
	}
}
