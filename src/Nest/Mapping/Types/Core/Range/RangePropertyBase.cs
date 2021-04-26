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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRangeProperty : IDocValuesProperty
	{
		/// <summary>
		/// Try to convert strings to numbers and truncate fractions for integers. Accepts true (default) and false.
		/// </summary>
		[DataMember(Name ="coerce")]
		bool? Coerce { get; set; }

		/// <summary>
		/// Should the field be searchable? Accepts true (default) and false.
		/// </summary>
		[DataMember(Name ="index")]
		bool? Index { get; set; }
	}

	public abstract class RangePropertyBase : DocValuesPropertyBase, IRangeProperty
	{
		protected RangePropertyBase(RangeType rangeType) : base(rangeType.ToFieldType()) { }

		/// <inheritdoc />
		public bool? Coerce { get; set; }

		/// <inheritdoc />
		public bool? Index { get; set; }
	}

	public abstract class RangePropertyDescriptorBase<TDescriptor, TInterface, T>
		: DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, IRangeProperty
		where TDescriptor : RangePropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IRangeProperty
		where T : class
	{
		protected RangePropertyDescriptorBase(RangeType type) : base(type.ToFieldType()) { }

		bool? IRangeProperty.Coerce { get; set; }
		bool? IRangeProperty.Index { get; set; }

		/// <inheritdoc cref="IRangeProperty.Coerce" />
		public TDescriptor Coerce(bool? coerce = true) => Assign(coerce, (a, v) => a.Coerce = v);

		/// <inheritdoc cref="IRangeProperty.Index" />
		public TDescriptor Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);
	}
}
