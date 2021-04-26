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
	/// The join datatype is a special field that creates parent/child relation within documents of the same index.
	/// </summary>
	[InterfaceDataContract]
	public interface IJoinProperty : IProperty
	{
		/// <summary>
		/// Defines a set of possible relations within the documents,
		/// each relation being a parent name and a child name.
		/// </summary>
		[DataMember(Name = "relations")]
		IRelations Relations { get; set; }
	}

	/// <inheritdoc cref="IJoinProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class JoinProperty : PropertyBase, IJoinProperty
	{
		public JoinProperty() : base(FieldType.Join) { }

		public IRelations Relations { get; set; }
	}

	/// <inheritdoc cref="IJoinProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class JoinPropertyDescriptor<T> : PropertyDescriptorBase<JoinPropertyDescriptor<T>, IJoinProperty, T>, IJoinProperty
		where T : class
	{
		public JoinPropertyDescriptor() : base(FieldType.Join) { }

		IRelations IJoinProperty.Relations { get; set; }

		/// <inheritdoc cref="IJoinProperty.Relations"/>
		public JoinPropertyDescriptor<T> Relations(Func<RelationsDescriptor, IPromise<IRelations>> selector) =>
			Assign(selector, (a, v) => a.Relations = v?.Invoke(new RelationsDescriptor())?.Value);
	}
}
