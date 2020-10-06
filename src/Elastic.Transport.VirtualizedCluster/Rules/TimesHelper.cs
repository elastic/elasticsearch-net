// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elastic.Transport.VirtualizedCluster.Rules
{
	public static class TimesHelper
	{
		public static AllTimes Always = new AllTimes();
		public static readonly int Once = 0;
		public static readonly int Twice = 1;

		public static int Times(int n) => Math.Max(0, n - 1);

		public class AllTimes
		{
			internal AllTimes() { }
		}
	}
}
