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
	/// A mapping for an inner object
	/// </summary>
	[InterfaceDataContract]
	public interface IObjectProperty : ICoreProperty
	{
		/// <summary>
		/// Whether or not new properties should be added dynamically to an existing object.
		/// Default is <c>true</c>
		/// </summary>
		[DataMember(Name = "dynamic")]
		[JsonFormatter(typeof(DynamicMappingFormatter))]
		Union<bool, DynamicMapping> Dynamic { get; set; }

		/// <summary>
		/// Whether the JSON value given for this field should be parsed and indexed. Default is <c>true</c>
		/// </summary>
		[DataMember(Name = "enabled")]
		bool? Enabled { get; set; }

		/// <summary>
		/// The fields within the object
		/// </summary>
		[DataMember(Name = "properties")]
		IProperties Properties { get; set; }
	}

	/// <summary>
	/// A object datatype mapping for an inner object
	/// </summary>
	[DebuggerDisplay("{DebugDisplay}")]
	public class ObjectProperty : CorePropertyBase, IObjectProperty
	{
		public ObjectProperty() : base(FieldType.Object) { }

		protected ObjectProperty(FieldType type) : base(type) { }

		/// <inheritdoc />
		public Union<bool, DynamicMapping> Dynamic { get; set; }

		/// <inheritdoc />
		public bool? Enabled { get; set; }

		/// <inheritdoc />
		public IProperties Properties { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class ObjectTypeDescriptor<TParent, TChild>
		: ObjectPropertyDescriptorBase<ObjectTypeDescriptor<TParent, TChild>, IObjectProperty, TParent, TChild>, IObjectProperty
		where TParent : class
		where TChild : class { }

	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class ObjectPropertyDescriptorBase<TDescriptor, TInterface, TParent, TChild>
		: CorePropertyDescriptorBase<TDescriptor, TInterface, TParent>, IObjectProperty
		where TDescriptor : ObjectPropertyDescriptorBase<TDescriptor, TInterface, TParent, TChild>, TInterface
		where TInterface : class, IObjectProperty
		where TParent : class
		where TChild : class
	{
		protected ObjectPropertyDescriptorBase() : base(FieldType.Object) { }

		protected ObjectPropertyDescriptorBase(FieldType fieldType) : base(fieldType) { }

		Union<bool, DynamicMapping> IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		IProperties IObjectProperty.Properties { get; set; }

		/// <summary>
		/// Whether or not new properties should be added dynamically to an existing object.
		/// Default is <c>true</c>
		/// </summary>
		public TDescriptor Dynamic(Union<bool, DynamicMapping> dynamic) => Assign(dynamic, (a, v) => a.Dynamic = v);

		/// <summary>
		/// Whether or not new properties should be added dynamically to an existing object.
		/// Default is <c>true</c>
		/// </summary>
		public TDescriptor Dynamic(bool dynamic = true) => Assign(dynamic, (a, v) => a.Dynamic = v);

		/// <summary>
		/// Whether the JSON value given for this field should be parsed and indexed. Default is <c>true</c>
		/// </summary>
		public TDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);

		/// <summary>
		/// The fields within the object
		/// </summary>
		public TDescriptor Properties(Func<PropertiesDescriptor<TChild>, IPromise<IProperties>> selector) =>
			Assign(selector, (a, v) => a.Properties = v?.Invoke(new PropertiesDescriptor<TChild>(a.Properties))?.Value);

		public TDescriptor AutoMap(IPropertyVisitor visitor = null, int maxRecursion = 0) =>
			Assign(Self.Properties.AutoMap<TChild>(visitor, maxRecursion), (a, v) => a.Properties = v);

		public TDescriptor AutoMap(int maxRecursion) => AutoMap(null, maxRecursion);
	}
}
