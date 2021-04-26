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
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(RuntimeField))]
	public interface IRuntimeField
	{
		/// <summary>
		/// Runtime fields with a type of date can accept the format parameter exactly as the date field type.
		/// <see cref="DateFormat" />
		/// </summary>
		[DataMember(Name = "format")]
		string Format { get; set; }

		/// <summary>
		/// The script to be evaluated for field calculation at search time.
		/// </summary>
		[DataMember(Name = "script")]
		IInlineScript Script { get; set; }

		/// <summary>
		/// The datatype of the runtime field.
		/// </summary>
		[DataMember(Name = "type")]
		FieldType Type { get; set; }
	}

	public class RuntimeField : IRuntimeField
	{
		/// <inheritdoc />
		public string Format { get; set; }
		/// <inheritdoc />
		public IInlineScript Script { get; set; }
		/// <inheritdoc />
		public FieldType Type { get; set; }
	}

	public class RuntimeFieldDescriptor
		: DescriptorBase<RuntimeFieldDescriptor, IRuntimeField>, IRuntimeField
	{
		public RuntimeFieldDescriptor(FieldType fieldType) => Self.Type = fieldType;

		string IRuntimeField.Format { get; set; }
		IInlineScript IRuntimeField.Script { get; set; }
		FieldType IRuntimeField.Type { get; set; }

		/// <inheritdoc cref="IRuntimeField.Format" />
		public RuntimeFieldDescriptor Format(string format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc cref="IRuntimeField.Script" />
		public RuntimeFieldDescriptor Script(IInlineScript script) => Assign(script, (a, v) => a.Script = v);

		/// <inheritdoc cref="IRuntimeField.Script" />
		public RuntimeFieldDescriptor Script(string source) => Assign(source, (a, v) => a.Script = new InlineScript(source));

		/// <inheritdoc cref="IRuntimeField.Script" />
		public RuntimeFieldDescriptor Script(Func<InlineScriptDescriptor, IInlineScript> selector) => Assign(selector, (a, v) => a.Script = v?.Invoke(new InlineScriptDescriptor()));
	}
}
