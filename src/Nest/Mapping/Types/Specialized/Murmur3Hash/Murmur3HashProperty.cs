// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IMurmur3HashProperty : IDocValuesProperty { }

	[DebuggerDisplay("{DebugDisplay}")]
	public class Murmur3HashProperty : DocValuesPropertyBase, IMurmur3HashProperty
	{
		public Murmur3HashProperty() : base(FieldType.Murmur3Hash) { }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class Murmur3HashPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty, T>, IMurmur3HashProperty
		where T : class
	{
		public Murmur3HashPropertyDescriptor() : base(FieldType.Murmur3Hash) { }
	}
}
