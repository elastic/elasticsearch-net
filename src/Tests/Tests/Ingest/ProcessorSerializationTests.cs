using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Ingest
{
	public class ProcessorSerializationTests
	{
		[U]
		public void CanSerializeAndDeserializeAllProcessors()
		{
			var processorTypes =
				from t in typeof(IProcessor).Assembly.Types()
				where typeof(ProcessorBase).IsAssignableFrom(t) && !t.IsAbstract()
				select t;

			var processors = processorTypes
				.Select(processorType => (IProcessor)Activator.CreateInstance(processorType))
				.ToList();

			var pipeline = new Pipeline { Processors = processors };
			var deserializedPipeline = Object(pipeline).RoundTrips();
			deserializedPipeline.Processors.Should().HaveCount(pipeline.Processors.Count());
			deserializedPipeline.Processors.Select(p => p.Name).Distinct().Should().HaveCount(pipeline.Processors.Count());
		}
	}
}
