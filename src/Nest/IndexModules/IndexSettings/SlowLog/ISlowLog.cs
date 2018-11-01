using System;

namespace Nest
{
	public interface ISlowLog
	{
		ISlowLogIndexing Indexing { get; set; }
		ISlowLogSearch Search { get; set; }
	}

	public class SlowLog : ISlowLog
	{
		/// <inheritdoc />
		public ISlowLogIndexing Indexing { get; set; }

		/// <inheritdoc />
		public ISlowLogSearch Search { get; set; }
	}

	public class SlowLogDescriptor : DescriptorBase<SlowLogDescriptor, ISlowLog>, ISlowLog
	{
		ISlowLogIndexing ISlowLog.Indexing { get; set; }
		ISlowLogSearch ISlowLog.Search { get; set; }

		/// <inheritdoc />
		public SlowLogDescriptor Indexing(Func<SlowLogIndexingDescriptor, ISlowLogIndexing> selector) =>
			Assign(a => a.Indexing = selector?.Invoke(new SlowLogIndexingDescriptor()));

		/// <inheritdoc />
		public SlowLogDescriptor Search(Func<SlowLogSearchDescriptor, ISlowLogSearch> selector) =>
			Assign(a => a.Search = selector?.Invoke(new SlowLogSearchDescriptor()));
	}
}
