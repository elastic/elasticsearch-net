using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nest
{
	public enum ParentScoreType
	{
		[EnumMember(Value = "none")]
		None = 0,
		[EnumMember(Value = "score")]
		Score
	}
}
