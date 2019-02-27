using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IHasPrivilegesResponse : IResponse
	{
		[JsonProperty("username")]
		string Username { get; }

		[JsonProperty("has_all_requested")]
		bool HasAllRequested { get; }

		[JsonProperty("cluster")]
		IReadOnlyDictionary<string, IReadOnlyDictionary<string, bool>> Cluster { get; }

		[JsonProperty("index")]
		IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyDictionary<string, bool>>> Index { get; }

		[JsonProperty("application")]
		IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyDictionary<string, bool>>> Application { get; }
	}

	public class HasPrivilegesResponse : ResponseBase, IHasPrivilegesResponse
	{
		public string Username { get; internal set;  }
		public bool HasAllRequested { get; internal set; }
		public IReadOnlyDictionary<string, IReadOnlyDictionary<string, bool>> Cluster { get;internal set;  }
		public IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyDictionary<string, bool>>> Index { get;internal set;  }
		public IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyDictionary<string, bool>>> Application
		{
			get;
			internal set;
		}
	}
}
