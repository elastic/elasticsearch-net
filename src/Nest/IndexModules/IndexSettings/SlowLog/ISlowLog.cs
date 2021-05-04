// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
		public SlowLogDescriptor Search(Func<SlowLogSearchDescriptor, ISlowLogSearch> selector) =>
			Assign(selector, (a, v) => a.Search = v?.Invoke(new SlowLogSearchDescriptor()));

		/// <inheritdoc />
		public SlowLogDescriptor Indexing(Func<SlowLogIndexingDescriptor, ISlowLogIndexing> selector) =>
			Assign(selector, (a, v) => a.Indexing = v?.Invoke(new SlowLogIndexingDescriptor()));
	}
}
