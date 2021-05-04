// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	public interface ISlowLogSearch
	{
		ISlowLogSearchFetch Fetch { get; set; }
		LogLevel? LogLevel { get; set; }
		ISlowLogSearchQuery Query { get; set; }
	}

	public class SlowLogSearch : ISlowLogSearch
	{
		public ISlowLogSearchFetch Fetch { get; set; }

		public LogLevel? LogLevel { get; set; }

		public ISlowLogSearchQuery Query { get; set; }
	}

	public class SlowLogSearchDescriptor : DescriptorBase<SlowLogSearchDescriptor, ISlowLogSearch>, ISlowLogSearch
	{
		ISlowLogSearchFetch ISlowLogSearch.Fetch { get; set; }
		LogLevel? ISlowLogSearch.LogLevel { get; set; }
		ISlowLogSearchQuery ISlowLogSearch.Query { get; set; }

		/// <inheritdoc />
		public SlowLogSearchDescriptor LogLevel(LogLevel? level) => Assign(level, (a, v) => a.LogLevel = v);

		/// <inheritdoc />
		public SlowLogSearchDescriptor Query(Func<SlowLogSearchQueryDescriptor, ISlowLogSearchQuery> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new SlowLogSearchQueryDescriptor()));

		/// <inheritdoc />
		public SlowLogSearchDescriptor Fetch(Func<SlowLogSearchFetchDescriptor, ISlowLogSearchFetch> selector) =>
			Assign(selector, (a, v) => a.Fetch = v?.Invoke(new SlowLogSearchFetchDescriptor()));
	}
}
