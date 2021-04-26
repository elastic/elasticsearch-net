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

using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A range of single-precision 32-bit IEEE 754 floating point values.
	/// </summary>
	[InterfaceDataContract]
	public interface IFloatRangeProperty : IRangeProperty { }

	/// <inheritdoc cref="IFloatRangeProperty"/>
	public class FloatRangeProperty : RangePropertyBase, IFloatRangeProperty
	{
		public FloatRangeProperty() : base(RangeType.FloatRange) { }
	}

	/// <inheritdoc cref="IFloatRangeProperty"/>
	public class FloatRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<FloatRangePropertyDescriptor<T>, IFloatRangeProperty, T>, IFloatRangeProperty
		where T : class
	{
		public FloatRangePropertyDescriptor() : base(RangeType.FloatRange) { }
	}
}
