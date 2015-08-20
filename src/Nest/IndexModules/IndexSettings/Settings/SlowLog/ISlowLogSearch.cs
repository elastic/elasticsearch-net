using System;

namespace Nest
{
	public interface ISlowLogSearch
	{
		ISlowLogSearchQuery Query { get; set; }
		ISlowLogSearchFetch Fetch { get; set; }
		SlowLogLevel? LogLevel { get; set; }
	}

	public class SlowLogSearch : ISlowLogSearch
	{
	 	public ISlowLogSearchFetch Fetch { get; set; }

		public SlowLogLevel? LogLevel { get; set; }

		public ISlowLogSearchQuery Query { get; set; }
	}

	public class SlowLogSearchDescriptor : ISlowLogSearch
	{
		SlowLogSearchDescriptor Assign(Action<ISlowLogSearch> assigner) => Fluent.Assign(this, assigner);

		ISlowLogSearchQuery ISlowLogSearch.Query { get; set; }
		ISlowLogSearchFetch ISlowLogSearch.Fetch { get; set; }
		SlowLogLevel? ISlowLogSearch.LogLevel { get; set; }

		/// </inheritdoc>
		public SlowLogSearchDescriptor LogLevel(SlowLogLevel? level) => Assign(a => a.LogLevel = level);

		/// </inheritdoc>
		public SlowLogSearchDescriptor Query(Func<SlowLogSearchQueryDescriptor, ISlowLogSearchQuery> selector) =>
			Assign(a => a.Query = selector?.Invoke(new SlowLogSearchQueryDescriptor()));

		/// </inheritdoc>
		public SlowLogSearchDescriptor Fetch(Func<SlowLogSearchFetchDescriptor, ISlowLogSearchFetch> selector) =>
			Assign(a => a.Fetch = selector?.Invoke(new SlowLogSearchFetchDescriptor()));
	}
}