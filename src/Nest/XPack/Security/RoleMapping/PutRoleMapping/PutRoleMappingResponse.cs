using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPutRoleMappingResponse : IResponse
	{
		[JsonProperty("role_mapping")]
		PutRoleMappingStatus RoleMapping { get; }
	}

	public class PutRoleMappingResponse : ResponseBase, IPutRoleMappingResponse
	{
		public PutRoleMappingStatus RoleMapping { get; internal set; }

		public bool Created => this.RoleMapping?.Created ?? false;
	}

	public class PutRoleMappingStatus
	{
		[JsonProperty("created")]
		public bool Created { get; set; }
	}

}
