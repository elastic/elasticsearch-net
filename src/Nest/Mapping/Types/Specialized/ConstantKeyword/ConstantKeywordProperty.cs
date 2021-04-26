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

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Constant keyword is a specialization of the keyword field for the case that all documents in the index have the same value.
	/// <para />
	/// Available in Elasticsearch 7.7.0+ with at least a basic license level
	/// </summary>
	[InterfaceDataContract]
	public interface IConstantKeywordProperty : IProperty
	{
		/// <summary>
		/// The value to associate with all documents in the index.
		/// If this parameter is not provided, it is set based on the first document that gets indexed.
		/// <para />
		/// Value must be a string or a numeric value
		/// </summary>
		[DataMember(Name ="value")]
		object Value { get; set; }
	}

	/// <inheritdoc cref="IConstantKeywordProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class ConstantKeywordProperty : PropertyBase, IConstantKeywordProperty
	{
		public ConstantKeywordProperty() : base(FieldType.ConstantKeyword) { }

		/// <inheritdoc cref="IConstantKeywordProperty.Value" />
		public object Value { get; set; }
	}

	/// <inheritdoc cref="IConstantKeywordProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class ConstantKeywordPropertyDescriptor<T>
		: PropertyDescriptorBase<ConstantKeywordPropertyDescriptor<T>, IConstantKeywordProperty, T>, IConstantKeywordProperty
		where T : class
	{
		public ConstantKeywordPropertyDescriptor() : base(FieldType.ConstantKeyword) { }

		object IConstantKeywordProperty.Value { get; set; }

		/// <inheritdoc cref="IConstantKeywordProperty.Value" />
		public ConstantKeywordPropertyDescriptor<T> Value(object value) => Assign(value, (a, v) => a.Value = v);
	}
}
