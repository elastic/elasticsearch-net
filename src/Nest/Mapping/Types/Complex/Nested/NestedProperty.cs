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
	/// A specialised version of the <see cref="IObjectProperty"/> datatype that allows arrays of objects
	/// to be indexed in a way that they can be queried independently of each other, using nested queries
	/// and aggregations.
	/// </summary>
	[InterfaceDataContract]
	public interface INestedProperty : IObjectProperty
	{
		/// <summary>
		/// Whether to also index nested objects as flattened values on the parent document.
		/// </summary>
		[DataMember(Name = "include_in_parent")]
		bool? IncludeInParent { get; set; }

		/// <summary>
		/// Whether to also index nested objects as flattened values on the root document.
		/// </summary>
		[DataMember(Name = "include_in_root")]
		bool? IncludeInRoot { get; set; }
	}

	/// <inheritdoc cref="INestedProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class NestedProperty : ObjectProperty, INestedProperty
	{
		public NestedProperty() : base(FieldType.Nested) { }

		/// <inheritdoc />
		public bool? IncludeInParent { get; set; }
		/// <inheritdoc />
		public bool? IncludeInRoot { get; set; }
	}

	/// <inheritdoc cref="INestedProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class NestedPropertyDescriptor<TParent, TChild>
		: ObjectPropertyDescriptorBase<NestedPropertyDescriptor<TParent, TChild>, INestedProperty, TParent, TChild>
			, INestedProperty
		where TParent : class
		where TChild : class
	{
		public NestedPropertyDescriptor() : base(FieldType.Nested) { }

		bool? INestedProperty.IncludeInParent { get; set; }
		bool? INestedProperty.IncludeInRoot { get; set; }

		/// <inheritdoc cref="INestedProperty.IncludeInParent"/>
		public NestedPropertyDescriptor<TParent, TChild> IncludeInParent(bool? includeInParent = true) =>
			Assign(includeInParent, (a, v) => a.IncludeInParent = v);

		/// <inheritdoc cref="INestedProperty.IncludeInRoot"/>
		public NestedPropertyDescriptor<TParent, TChild> IncludeInRoot(bool? includeInRoot = true) =>
			Assign(includeInRoot, (a, v) => a.IncludeInRoot = v);
	}
}
