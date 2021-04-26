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
	public class IpAttribute : ElasticsearchDocValuesPropertyAttributeBase, IIpProperty
	{
		public IpAttribute() : base(FieldType.Ip) { }

		[Obsolete("The server always treated this as a noop and has been removed in 7.10")]
		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public string NullValue
		{
			get => Self.NullValue;
			set => Self.NullValue = value;
		}

		double? IIpProperty.Boost { get; set; }
		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }
		private IIpProperty Self => this;
	}
}
