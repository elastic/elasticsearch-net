using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClearSqlCursorResponse : IResponse
	{
		[JsonProperty("succeeded")]
		bool Succeeded { get; }
	}

	public class ClearSqlCursorResponse : ResponseBase, IClearSqlCursorResponse
	{
		public bool Succeeded { get; internal set; }
	}
}
