using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetRequest { }
	public interface IGetRequest<T> : IGetRequest where T : class { }

	public partial class GetRequest 
	{
	}

	public partial class GetRequest<T> 
		where T : class
	{
	}

	public partial class GetDescriptor<T> where T : class
	{
		public GetDescriptor(Id id)
			: base(p => p.Required(Indices.Single<T>()).Required(Types.Single<T>()).Required(Ids.Single(id))) { }

		public GetDescriptor(T document) : base(r => r.Required(Ids.Single(document))) { }

		public GetDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public GetDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}
	}
}
