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
	/// Joins each element of an array into a single string using a separator
	/// character between each element. Throws an error when the field is not an array.
	/// </summary>
	[InterfaceDataContract]
	public interface IJoinProcessor : IProcessor
	{
		/// <summary>
		/// The field to be joined
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The separator character
		/// </summary>
		[DataMember(Name ="separator")]
		string Separator { get; set; }

		/// <summary>
		/// The field to assign the joined value to, by default field is updated in-place
		/// </summary>
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc cref="IJoinProcessor"/>
	public class JoinProcessor : ProcessorBase, IJoinProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public string Separator { get; set; }
		/// <inheritdoc />
		public Field TargetField { get; set; }
		protected override string Name => "join";
	}

	/// <inheritdoc cref="IJoinProcessor"/>
	public class JoinProcessorDescriptor<T>
		: ProcessorDescriptorBase<JoinProcessorDescriptor<T>, IJoinProcessor>, IJoinProcessor
		where T : class
	{
		protected override string Name => "join";

		Field IJoinProcessor.Field { get; set; }
		Field IJoinProcessor.TargetField { get; set; }
		string IJoinProcessor.Separator { get; set; }

		/// <inheritdoc cref="IJoinProcessor.Field"/>
		public JoinProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IJoinProcessor.Field"/>
		public JoinProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IJoinProcessor.TargetField"/>
		public JoinProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IJoinProcessor.TargetField"/>
		public JoinProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IJoinProcessor.Separator"/>
		public JoinProcessorDescriptor<T> Separator(string separator) => Assign(separator, (a, v) => a.Separator = v);
	}
}
