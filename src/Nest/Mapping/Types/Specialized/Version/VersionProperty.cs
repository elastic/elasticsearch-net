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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A version field can index/store semver version numbers.
	/// </summary>
	[InterfaceDataContract]
	public interface IVersionProperty : IProperty
	{
	}

	/// <inheritdoc cref="IVersionProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class VersionProperty : PropertyBase, IVersionProperty
	{
		public VersionProperty() : base(FieldType.Version) { }
	}

	/// <inheritdoc cref="IVersionProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class VersionPropertyDescriptor<T>
		: PropertyDescriptorBase<VersionPropertyDescriptor<T>, IVersionProperty, T>, IVersionProperty
		where T : class
	{
		public VersionPropertyDescriptor() : base(FieldType.Version) { }
	}
}
