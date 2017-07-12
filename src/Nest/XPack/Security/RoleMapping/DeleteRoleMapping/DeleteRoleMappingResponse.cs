using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteRoleMappingResponse : IResponse
	{
		[JsonProperty("found")]
		bool Found { get; }
	}

	public class DeleteRoleMappingResponse : ResponseBase, IDeleteRoleMappingResponse
	{
		public bool Found { get; internal set; }
	}

}
