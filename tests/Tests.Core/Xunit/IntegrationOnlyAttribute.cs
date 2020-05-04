// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elastic.Xunit.XunitPlumbing;
using Tests.Configuration;

namespace Tests.Core.Xunit
{
	public class IntegrationOnlyAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Inherited unit tests are ignored on this integration test class";
		public override bool Skip => TestConfiguration.Instance.RunUnitTests;
	}
}
