using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPutUserResponse : IResponse
	{
		[JsonProperty("user")]
		PutUserStatus User { get; }
	}

	public class PutUserResponse : ResponseBase, IPutUserResponse
	{
		public PutUserStatus User { get; internal set; }
	}

	public class PutUserStatus
	{
		[JsonProperty("created")]
		public bool Created { get; internal set; }
	}
}
