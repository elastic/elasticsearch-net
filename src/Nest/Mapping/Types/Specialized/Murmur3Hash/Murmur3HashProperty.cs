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
