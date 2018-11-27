using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetDatafeedsResponse : IResponse
	{
		[DataMember(Name ="count")]
		long Count { get; }

		[DataMember(Name ="datafeeds")]
		IReadOnlyCollection<DatafeedConfig> Datafeeds { get; }
	}

	public class GetDatafeedsResponse : ResponseBase, IGetDatafeedsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<DatafeedConfig> Datafeeds { get; internal set; } = EmptyReadOnly<DatafeedConfig>.Collection;
	}
}
