using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//TODO needs IFDEF?

namespace System.Runtime.CompilerServices
{
	public class FormattableStringFactory
	{
		public static FormattableString Create(string messageFormat, params object[] args)
		{
			return new FormattableString(messageFormat, args);
		}

		public static FormattableString Create(string messageFormat, DateTime bad, params object[] args)
		{
			var realArgs = new object[args.Length + 1];
			realArgs[0] = "Please don't use DateTime";
			Array.Copy(args, 0, realArgs, 1, args.Length);
			return new FormattableString(messageFormat, realArgs);
		}
	}
}

namespace System
{
	public class FormattableString
	{
		private readonly string messageFormat;
		private readonly object[] args;

		public FormattableString(string messageFormat, object[] args)
		{
			this.messageFormat = messageFormat;
			this.args = args;
		}
		public override string ToString()
		{
			return string.Format(messageFormat, args);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			return string.Format(formatProvider, messageFormat, args);
		}

	}
}
