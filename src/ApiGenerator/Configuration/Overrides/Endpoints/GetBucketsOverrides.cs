// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	public class GetBucketsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"expand",
			"exclude_interim",
			"from",
			"size",
			"start",
			"timestamp",
			"end",
			"anomaly_score",
			"sort",
			"desc"
		};
	}
}
