using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IDeleteUserResponse : IResponse
	{
		[JsonProperty("found")]
		bool Found { get; }
	}

	public class DeleteUserResponse : ResponseBase, IDeleteUserResponse
	{
		public bool Found { get; internal set; }
	}
}
