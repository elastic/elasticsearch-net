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
	/// An ip field can index/store either IPv4 or IPv6 addresses.
	/// </summary>
	[InterfaceDataContract]
	public interface IIpProperty : IDocValuesProperty
	{
		[Obsolete("The server always treated this as a noop and has been removed in 7.10")]
		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="null_value")]
		string NullValue { get; set; }
	}

	/// <inheritdoc cref="IIpProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class IpProperty : DocValuesPropertyBase, IIpProperty
	{
		public IpProperty() : base(FieldType.Ip) { }

		public double? Boost { get; set; }
		public bool? Index { get; set; }
		public string NullValue { get; set; }
	}

	/// <inheritdoc cref="IIpProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class IpPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<IpPropertyDescriptor<T>, IIpProperty, T>, IIpProperty
		where T : class
	{
		public IpPropertyDescriptor() : base(FieldType.Ip) { }

		double? IIpProperty.Boost { get; set; }
		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }

		public IpPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		[Obsolete("The server always treated this as a noop and has been removed in 7.10")]
		public IpPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public IpPropertyDescriptor<T> NullValue(string nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);
	}
}
