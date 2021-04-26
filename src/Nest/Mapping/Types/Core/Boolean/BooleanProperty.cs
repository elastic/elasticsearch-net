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
using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The boolean fields accepts true and false values
	/// </summary>
	[InterfaceDataContract]
	public interface IBooleanProperty : IDocValuesProperty
	{
		[DataMember(Name = "boost")]
		double? Boost { get; set; }

		[DataMember(Name = "fielddata")]
		INumericFielddata Fielddata { get; set; }

		[DataMember(Name = "index")]
		bool? Index { get; set; }

		[DataMember(Name = "null_value")]
		bool? NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanProperty : DocValuesPropertyBase, IBooleanProperty
	{
		public BooleanProperty() : base(FieldType.Boolean) { }

		public double? Boost { get; set; }
		public INumericFielddata Fielddata { get; set; }

		public bool? Index { get; set; }
		public bool? NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<BooleanPropertyDescriptor<T>, IBooleanProperty, T>, IBooleanProperty
		where T : class
	{
		public BooleanPropertyDescriptor() : base(FieldType.Boolean) { }

		double? IBooleanProperty.Boost { get; set; }
		INumericFielddata IBooleanProperty.Fielddata { get; set; }
		bool? IBooleanProperty.Index { get; set; }
		bool? IBooleanProperty.NullValue { get; set; }

		public BooleanPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public BooleanPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public BooleanPropertyDescriptor<T> NullValue(bool? nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		public BooleanPropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(selector(new NumericFielddataDescriptor()), (a, v) => a.Fielddata = v);
	}
}
