using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nest
{
	public enum TermsOrder
	{
		[EnumMember(Value = "count")]
		Count = 0,
		[EnumMember(Value = "term")]
		Term,
		[EnumMember(Value = "reverse_count")]
		ReverseCount,
		[EnumMember(Value = "reverse_term")]
		ReverseTerm
	}
}
