using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBucket { }

	public abstract class BucketBase : AggregationsHelper, IBucket
	{
		internal abstract bool Matches(JToken source);
	}
}
