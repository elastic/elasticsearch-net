using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteRoleResponse : IResponse
	{
		[JsonProperty("found")]
		bool Found { get; }
	}

	public class DeleteRoleResponse : ResponseBase, IDeleteRoleResponse
	{
		public bool Found { get; internal set; }
	}
}
