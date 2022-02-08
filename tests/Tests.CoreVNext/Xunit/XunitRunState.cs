// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Concurrent;

namespace Tests.Core.Xunit
{
	internal static class XunitRunState
	{
		public static ConcurrentBag<string> SeenDeprecations { get; } = new ConcurrentBag<string>();
	}
}
