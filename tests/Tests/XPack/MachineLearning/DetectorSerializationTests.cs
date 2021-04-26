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
