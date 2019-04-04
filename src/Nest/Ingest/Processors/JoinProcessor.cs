﻿using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<JoinProcessor>))]
	public interface IJoinProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("separator")]
		string Separator { get; set; }
	}

	public class JoinProcessor : ProcessorBase, IJoinProcessor
	{
		public Field Field { get; set; }

		public string Separator { get; set; }
		protected override string Name => "join";
	}

	public class JoinProcessorDescriptor<T>
		: ProcessorDescriptorBase<JoinProcessorDescriptor<T>, IJoinProcessor>, IJoinProcessor
		where T : class
	{
		protected override string Name => "join";

		Field IJoinProcessor.Field { get; set; }
		string IJoinProcessor.Separator { get; set; }

		public JoinProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public JoinProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public JoinProcessorDescriptor<T> Separator(string separator) => Assign(separator, (a, v) => a.Separator = v);
	}
}
