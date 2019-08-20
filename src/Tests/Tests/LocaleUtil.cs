using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Tests
{
	internal sealed class LocaleUtil : IDisposable
	{
		public static IDisposable UseLocale(string locale) => new LocaleUtil(CultureInfo.GetCultureInfo(locale));

		private readonly CultureInfo previous;
		private readonly CultureInfo previousUi;

		private LocaleUtil(CultureInfo locale)
		{
			previous = Thread.CurrentThread.CurrentCulture;
			previousUi = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentCulture = locale;
			Thread.CurrentThread.CurrentUICulture = locale;
		}

		void IDisposable.Dispose()
		{
			Thread.CurrentThread.CurrentCulture = previous;
			Thread.CurrentThread.CurrentUICulture = previousUi;
		}
	}
}
