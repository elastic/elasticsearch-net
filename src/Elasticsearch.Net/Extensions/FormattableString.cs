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
		private readonly string _messageFormat;
		private readonly object[] _args;

		public FormattableString(string messageFormat, object[] args)
		{
			this._messageFormat = messageFormat;
			this._args = args;
		}
		public override string ToString() => string.Format(_messageFormat, _args);

		public string ToString(IFormatProvider formatProvider) => string.Format(formatProvider, _messageFormat, _args);
	}
}
