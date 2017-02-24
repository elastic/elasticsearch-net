using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using System.Reflection;

namespace Tests.Ingest
{
	public class ProcessorSerializationTests : SerializationTestBase
	{
		[U]
		public void CanSerializeAndDeserializeAllProcessors()
		{
			var processorTypes =
				from t in typeof(IProcessor).Assembly().Types()
				where typeof(ProcessorBase).IsAssignableFrom(t) && !t.IsAbstract()
				select t;

			var processors = processorTypes
				.Select(processorType => (IProcessor)Activator.CreateInstance(processorType))
				.ToList();

			var pipeline = new Pipeline { Processors = processors };
			var deserializedPipeline = this.Deserialize<Pipeline>(this.Serialize(pipeline));

			deserializedPipeline.Processors.Should().HaveCount(pipeline.Processors.Count());
			deserializedPipeline.Processors.Select(p => p.Name).Distinct().Should().HaveCount(pipeline.Processors.Count());
		}
	}
}
