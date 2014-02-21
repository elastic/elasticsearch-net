using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IStringifier
	{
		string Stringify(object o);
	}

	public class Stringifier : IStringifier
	{
		public string Stringify(object o)
		{
			var s = o as string;
			if (s != null)
				return s;
			var ss = o as string[];
			if (ss != null)
				return string.Join(",", ss);

			var pns = o as IEnumerable<object>;
			if (pns != null)
				return string.Join(",", pns.ToString());

			var e = o as Enum;
			if (e != null) return KnownEnums.Resolve(e);
			if (o is bool)
				return ((bool) o) ? "true" : "false";
			return o.ToString();
		}
	
	}
}
