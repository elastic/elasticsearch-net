// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using Nest.Utf8Json;

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
