// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Sets one field and associates it with the specified value.
	/// If the field already exists, its value will be replaced with the provided one.
	/// </summary>
	[InterfaceDataContract]
	public interface ISetProcessor : IProcessor
	{
		/// <summary>
		/// The field to insert, upsert, or update. Supports template snippets.
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The value to be set for the field. Supports template snippets.
		/// </summary>
		[DataMember(Name ="value")]
		[JsonFormatter(typeof(SourceWriteFormatter<>))]
		object Value { get; set; }

		/// <summary>
		/// If processor will update fields with pre-existing non-null-valued field.
		/// When set to false, such fields will not be touched.
		/// Default is <c>true</c>
		/// </summary>
		[DataMember(Name = "override")]
		bool? Override { get; set; }
	}

	/// <inheritdoc cref="ISetProcessor" />
	public class SetProcessor : ProcessorBase, ISetProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public object Value { get; set; }
		/// <inheritdoc />
		public bool? Override { get; set; }
		protected override string Name => "set";
	}

	/// <inheritdoc cref="ISetProcessor" />
	public class SetProcessorDescriptor<T> : ProcessorDescriptorBase<SetProcessorDescriptor<T>, ISetProcessor>, ISetProcessor
		where T : class
	{
		protected override string Name => "set";
		Field ISetProcessor.Field { get; set; }
		object ISetProcessor.Value { get; set; }
		bool? ISetProcessor.Override { get; set; }

		/// <inheritdoc cref="ISetProcessor.Field"/>
		public SetProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetProcessor.Field"/>
		public SetProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetProcessor.Value"/>
		public SetProcessorDescriptor<T> Value<TValue>(TValue value) => Assign(value, (a, v) => a.Value = v);

		/// <inheritdoc cref="ISetProcessor.Override"/>
		public SetProcessorDescriptor<T> Override(bool? @override = true) => Assign(@override, (a, v) => a.Override = v);
	}
}
