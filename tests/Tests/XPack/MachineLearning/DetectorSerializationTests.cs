// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.XPack.MachineLearning
{
	public class DetectorSerializationTests
	{
		[U] public void CanSerializeAndDeserializeAllDetectors()
		{
			var detectorTypes =
				from t in typeof(IDetector).Assembly.Types()
				where typeof(IDetector).IsAssignableFrom(t) &&
					!t.IsAbstract &&
					!typeof(IDescriptor).IsAssignableFrom(t)
				select t;

			var detectors = detectorTypes
				.Select(detectorType => (IDetector)Activator.CreateInstance(detectorType))
				.ToList();

			var analysisConfig = new AnalysisConfig { Detectors = detectors };
			var deserialized = Object(analysisConfig).RoundTrips();
			deserialized.Detectors.Select(d => d.Function).Distinct().Should().HaveCount(detectors.Count);
		}
	}
}
