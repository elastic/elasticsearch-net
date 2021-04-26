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
using Nest.Utf8Json;

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
