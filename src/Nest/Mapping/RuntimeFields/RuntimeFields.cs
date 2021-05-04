// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<RuntimeFields, IRuntimeFields, Field, IRuntimeField>))]
	public interface IRuntimeFields : IIsADictionary<Field, IRuntimeField> { }

	public class RuntimeFields : IsADictionaryBase<Field, IRuntimeField>, IRuntimeFields
	{
		public RuntimeFields() { }

		public RuntimeFields(IDictionary<Field, IRuntimeField> container) : base(container) { }

		public RuntimeFields(Dictionary<Field, IRuntimeField> container) : base(container) { }

		public void Add(Field name, IRuntimeField runtimeField) => BackingDictionary.Add(name, runtimeField);
	}

	public class RuntimeFieldsDescriptor<TDocument>
		: IsADictionaryDescriptorBase<RuntimeFieldsDescriptor<TDocument>, RuntimeFields, Field, IRuntimeField> where TDocument : class
	{
		public RuntimeFieldsDescriptor() : base(new RuntimeFields()) { }

		public RuntimeFieldsDescriptor<TDocument> RuntimeField(string name, FieldType type, Func<RuntimeFieldDescriptor, IRuntimeField> selector) =>
			Assign(name, selector?.Invoke(new RuntimeFieldDescriptor(type)));

		public RuntimeFieldsDescriptor<TDocument> RuntimeField(Expression<Func<TDocument, Field>> field, FieldType type, Func<RuntimeFieldDescriptor, IRuntimeField> selector) =>
			Assign(field, selector?.Invoke(new RuntimeFieldDescriptor(type)));

		public RuntimeFieldsDescriptor<TDocument> RuntimeField(string name, FieldType type) =>
			Assign(name, new RuntimeFieldDescriptor(type));

		public RuntimeFieldsDescriptor<TDocument> RuntimeField(Expression<Func<TDocument, Field>> field, FieldType type) =>
			Assign(field, new RuntimeFieldDescriptor(type));
	}

	public class RuntimeFieldsDescriptor
		: IsADictionaryDescriptorBase<RuntimeFieldsDescriptor, RuntimeFields, Field, IRuntimeField>
	{
		public RuntimeFieldsDescriptor() : base(new RuntimeFields()) { }

		public RuntimeFieldsDescriptor RuntimeField(string name, FieldType type, Func<RuntimeFieldDescriptor, IRuntimeField> selector) =>
			Assign(name, selector?.Invoke(new RuntimeFieldDescriptor(type)));

		public RuntimeFieldsDescriptor RuntimeField(string name, FieldType type) =>
			Assign(name, new RuntimeFieldDescriptor(type));
	}
}
