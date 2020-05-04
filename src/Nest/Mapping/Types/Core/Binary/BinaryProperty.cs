// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The binary type accepts a binary value as a Base64 encoded string.
	/// The field is not stored by default and is not searchable
	/// </summary>
	[InterfaceDataContract]
	public interface IBinaryProperty : IDocValuesProperty { }

	[DebuggerDisplay("{DebugDisplay}")]
	public class BinaryProperty : DocValuesPropertyBase, IBinaryProperty
	{
		public BinaryProperty() : base(FieldType.Binary) { }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BinaryPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<BinaryPropertyDescriptor<T>, IBinaryProperty, T>, IBinaryProperty
		where T : class
	{
		public BinaryPropertyDescriptor() : base(FieldType.Binary) { }
	}
}
