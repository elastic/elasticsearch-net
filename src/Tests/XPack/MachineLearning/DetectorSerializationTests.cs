using System;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning
{
	public class DetectorSerializationTests : SerializationTestBase
	{
		[U]
		public void CanSerializeAndDeserializeAllDetectors()
		{
			var detectorTypes =
				from t in typeof(IDetector).Assembly().Types()
				where typeof(IDetector).IsAssignableFrom(t) &&
				      !t.IsAbstract() &&
				      !typeof(IDescriptor).IsAssignableFrom(t)
				select t;

			var detectors = detectorTypes
				.Select(detectorType => (IDetector)Activator.CreateInstance(detectorType))
				.ToList();

			var analysisConfig = new AnalysisConfig { Detectors = detectors };
			var deserialized = this.Deserialize<AnalysisConfig>(this.Serialize(analysisConfig));

			deserialized.Detectors.Select(d => d.Function).Distinct().Should().HaveCount(detectors.Count);
		}
	}
}
