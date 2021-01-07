// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<RuntimeFields, IRuntimeFields, string, IRuntimeField>))]
	public interface IRuntimeFields : IIsADictionary<string, IRuntimeField> { }

	public class RuntimeFields : IsADictionaryBase<string, IRuntimeField>, IRuntimeFields
	{
		public RuntimeFields() { }

		public RuntimeFields(IDictionary<string, IRuntimeField> container) : base(container) { }

		public RuntimeFields(Dictionary<string, IRuntimeField> container) : base(container) { }

		public void Add(string name, IRuntimeField runtimeField) => BackingDictionary.Add(name, runtimeField);
	}

	public class RuntimeFieldsDescriptor
		: IsADictionaryDescriptorBase<RuntimeFieldsDescriptor, RuntimeFields, string, IRuntimeField>
	{
		public RuntimeFieldsDescriptor() : base(new RuntimeFields()) { }

		public RuntimeFieldsDescriptor RuntimeField(string name, FieldType type, Func<RuntimeFieldDescriptor, IRuntimeField> selector) =>
			Assign(name, selector?.Invoke(new RuntimeFieldDescriptor(type)));

		public RuntimeFieldsDescriptor RuntimeField(string name, FieldType type) =>
			Assign(name, new RuntimeFieldDescriptor(type));
	}
}
