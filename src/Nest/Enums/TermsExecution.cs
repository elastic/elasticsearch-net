using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TermsExecution
	{
		plain,
		@bool,
		and,
		or,
		fielddata
	}
}
