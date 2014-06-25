using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nest
{
	public enum ChildScoreType
	{
		[EnumMember(Value = "none")]
		None,
		[EnumMember(Value = "avg")]
		Average,
		[EnumMember(Value = "sum")]
		Sum,
		[EnumMember(Value = "max")]
		Max
	}
}
