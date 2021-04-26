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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An alias mapping defines an alternate name for a field in the index. The alias can be used in place
	/// of the target field in search requests, and selected other APIs like field capabilities.
	/// </summary>
	[InterfaceDataContract]
	public interface IFieldAliasProperty : IProperty
	{
		/// <summary> The full path to alias </summary>
		[DataMember(Name ="path")]
		Field Path { get; set; }
	}

	/// <inheritdoc cref="IFieldAliasProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class FieldAliasProperty : PropertyBase, IFieldAliasProperty
	{
		public FieldAliasProperty() : base(FieldType.Alias) { }

		/// <inheritdoc cref="IFieldAliasProperty.Path" />
		public Field Path { get; set; }
	}

	/// <inheritdoc cref="IFieldAliasProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class FieldAliasPropertyDescriptor<T>
		: PropertyDescriptorBase<FieldAliasPropertyDescriptor<T>, IFieldAliasProperty, T>, IFieldAliasProperty
		where T : class
	{
		public FieldAliasPropertyDescriptor() : base(FieldType.Alias) { }

		Field IFieldAliasProperty.Path { get; set; }

		/// <inheritdoc cref="IFieldAliasProperty.Path" />
		public FieldAliasPropertyDescriptor<T> Path<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Path = v);

		/// <inheritdoc cref="IFieldAliasProperty.Path" />
		public FieldAliasPropertyDescriptor<T> Path(Field field) => Assign(field, (a, v) => a.Path = v);
	}
}
