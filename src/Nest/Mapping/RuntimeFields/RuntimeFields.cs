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
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Utf8Json;

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
