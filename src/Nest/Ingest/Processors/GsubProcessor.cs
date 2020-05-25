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
	/// Converts a string field by applying a regular expression and a replacement.
	/// If the field is not a string, the processor will throw an exception.
	/// </summary>
	[InterfaceDataContract]
	public interface IGsubProcessor : IProcessor
	{
		/// <summary>
		/// The field to apply the replacement to
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The field to assign the converted value to, by default field is updated in-place
		/// </summary>
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// The pattern to be replaced
		/// </summary>
		[DataMember(Name ="pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// The string to replace the matching patterns with
		/// </summary>
		[DataMember(Name ="replacement")]
		string Replacement { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IGsubProcessor" />
	public class GsubProcessor : ProcessorBase, IGsubProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public Field TargetField { get; set; }
		/// <inheritdoc />
		public string Pattern { get; set; }
		/// <inheritdoc />
		public string Replacement { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		protected override string Name => "gsub";
	}

	/// <inheritdoc cref="IGsubProcessor" />
	public class GsubProcessorDescriptor<T>
		: ProcessorDescriptorBase<GsubProcessorDescriptor<T>, IGsubProcessor>, IGsubProcessor
		where T : class
	{
		protected override string Name => "gsub";

		Field IGsubProcessor.Field { get; set; }
		Field IGsubProcessor.TargetField { get; set; }
		string IGsubProcessor.Pattern { get; set; }
		string IGsubProcessor.Replacement { get; set; }
		bool? IGsubProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IGsubProcessor.Field" />
		public GsubProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IGsubProcessor.Field" />
		public GsubProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IGsubProcessor.TargetField" />
		public GsubProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IGsubProcessor.TargetField" />
		public GsubProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IGsubProcessor.Pattern" />
		public GsubProcessorDescriptor<T> Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);

		/// <inheritdoc cref="IGsubProcessor.Replacement" />
		public GsubProcessorDescriptor<T> Replacement(string replacement) => Assign(replacement, (a, v) => a.Replacement = v);

		/// <inheritdoc cref="IGsubProcessor.IgnoreMissing" />
		public GsubProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
