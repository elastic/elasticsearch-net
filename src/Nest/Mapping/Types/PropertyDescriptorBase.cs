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
using System.Collections.Generic;
using System.Linq.Expressions;
using Elastic.Transport.Extensions;

namespace Nest
{
	/// <inheritdoc cref="IProperty" />
	public abstract class PropertyDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, IProperty
		where TDescriptor : PropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IProperty
		where T : class
	{
		private string _type;

		protected PropertyDescriptorBase(FieldType type) => Self.Type = type.GetStringValue();

		protected string DebugDisplay => $"Type: {Self.Type ?? "<empty>"}, Name: {Self.Name.DebugDisplay} ";

		protected string TypeOverride
		{
			set => _type = value;
		}

		IDictionary<string, object> IProperty.LocalMetadata { get; set; }
		PropertyName IProperty.Name { get; set; }
		IDictionary<string, string> IProperty.Meta { get; set; }

		string IProperty.Type
		{
			get => _type;
			set => _type = value;
		}

		/// <inheritdoc cref="IProperty.Name" />
		public TDescriptor Name(PropertyName name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="IProperty.Name" />
		public TDescriptor Name<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Name = v);

		/// <inheritdoc cref="IProperty.LocalMetadata" />
		public TDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.LocalMetadata = v?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IProperty.Meta" />
		public TDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector) =>
			Assign(selector, (a, v) => a.Meta = v?.Invoke(new FluentDictionary<string, string>()));
	}
}
