// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
		IStoredScript Script { get; set; }

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
		public IStoredScript Script { get; set; }
		/// <inheritdoc />
		public FieldType Type { get; set; }
	}

	public class RuntimeFieldDescriptor
		: DescriptorBase<RuntimeFieldDescriptor, IRuntimeField>, IRuntimeField
	{
		public RuntimeFieldDescriptor(FieldType fieldType) => Self.Type = fieldType;

		string IRuntimeField.Format { get; set; }
		IStoredScript IRuntimeField.Script { get; set; }
		FieldType IRuntimeField.Type { get; set; }

		public RuntimeFieldDescriptor Format(string format) => Assign(format, (a, v) => a.Format = v);
		
		public RuntimeFieldDescriptor Script(IStoredScript script) => Assign(script, (a, v) => a.Script = v);

		public RuntimeFieldDescriptor Script(string source) => Assign(source, (a, v) => a.Script = new PainlessScript(source));
	}
}
