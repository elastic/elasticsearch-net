using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class Profile
	{
		[DataMember(Name = "shards")]
		public IReadOnlyCollection<ShardProfile> Shards { get; internal set; } =
			EmptyReadOnly<ShardProfile>.Collection;
	}
}
