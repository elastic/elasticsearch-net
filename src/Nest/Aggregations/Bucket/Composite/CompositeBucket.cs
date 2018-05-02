using System;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// A bucket with a composite key
	/// </summary>
	public class CompositeBucket : BucketBase
	{
		public CompositeBucket(IReadOnlyDictionary<string, IAggregate> dict, ILazyDocument key) : base(dict) =>
			Key = key;

		/// <summary>
		/// The bucket key
		/// </summary>
		public ILazyDocument Key { get; }

		/// <summary>
		/// The count of documents
		/// </summary>
		public long? DocCount { get; set; }
	}
}
