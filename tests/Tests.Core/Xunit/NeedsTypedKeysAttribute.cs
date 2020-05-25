// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Configuration;

namespace Tests.Core.Xunit
{
	public class NeedsTypedKeysAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Random Configuration dictates no typed keys but this tests relies on it being set";
		public override bool Skip => !TestConfiguration.Instance.Random.TypedKeys;
	}
}
