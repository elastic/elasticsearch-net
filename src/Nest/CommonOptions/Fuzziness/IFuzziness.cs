// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(FuzzinessInterfaceFormatter))]
	public interface IFuzziness
	{
		bool Auto { get; }
		int? Low { get; }
		int? High { get; }
		int? EditDistance { get; }
		double? Ratio { get; }
	}
}
