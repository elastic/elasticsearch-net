#region License
// Copyright (c) .NET Foundation and Contributors
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// https://github.com/xunit/samples.xunit/blob/master/UseCulture/UseCultureAttribute.cs
#endregion

using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Xunit.Sdk;

namespace Tests.Reproduce
{
	/// <summary>
	/// Apply this attribute to your test method to replace the
	/// <see cref="Thread.CurrentThread" /> <see cref="CultureInfo.CurrentCulture" /> and
	/// <see cref="CultureInfo.CurrentUICulture" /> with another culture.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class UseCultureAttribute : BeforeAfterTestAttribute
	{
		private readonly Lazy<CultureInfo> _culture;
		private readonly Lazy<CultureInfo> _uiCulture;

		private CultureInfo _originalCulture;
		private CultureInfo _originalUiCulture;

		/// <summary>
		/// Replaces the culture and UI culture of the current thread with
		/// <paramref name="culture" />
		/// </summary>
		/// <param name="culture">The name of the culture.</param>
		/// <remarks>
		/// <para>
		/// This constructor overload uses <paramref name="culture" /> for both
		/// <see cref="Culture" /> and <see cref="UiCulture" />.
		/// </para>
		/// </remarks>
		public UseCultureAttribute(string culture)
			: this(culture, culture) { }

		/// <summary>
		/// Replaces the culture and UI culture of the current thread with
		/// <paramref name="culture" /> and <paramref name="uiCulture" />
		/// </summary>
		/// <param name="culture">The name of the culture.</param>
		/// <param name="uiCulture">The name of the UI culture.</param>
		public UseCultureAttribute(string culture, string uiCulture)
		{
			_culture = new Lazy<CultureInfo>(() => new CultureInfo(culture, false));
			_uiCulture = new Lazy<CultureInfo>(() => new CultureInfo(uiCulture, false));
		}

		/// <summary>
		/// Gets the culture.
		/// </summary>
		public CultureInfo Culture => _culture.Value;

		/// <summary>
		/// Gets the UI culture.
		/// </summary>
		public CultureInfo UiCulture => _uiCulture.Value;

		/// <summary>
		/// Stores the current <see cref="Thread.CurrentPrincipal" />
		/// <see cref="CultureInfo.CurrentCulture" /> and <see cref="CultureInfo.CurrentUICulture" />
		/// and replaces them with the new cultures defined in the constructor.
		/// </summary>
		/// <param name="methodUnderTest">The method under test</param>
		public override void Before(MethodInfo methodUnderTest)
		{
			_originalCulture = Thread.CurrentThread.CurrentCulture;
			_originalUiCulture = Thread.CurrentThread.CurrentUICulture;

			Thread.CurrentThread.CurrentCulture = Culture;
			Thread.CurrentThread.CurrentUICulture = UiCulture;

			CultureInfo.CurrentCulture.ClearCachedData();
			CultureInfo.CurrentUICulture.ClearCachedData();
		}

		/// <summary>
		/// Restores the original <see cref="CultureInfo.CurrentCulture" /> and
		/// <see cref="CultureInfo.CurrentUICulture" /> to <see cref="Thread.CurrentPrincipal" />
		/// </summary>
		/// <param name="methodUnderTest">The method under test</param>
		public override void After(MethodInfo methodUnderTest)
		{
			Thread.CurrentThread.CurrentCulture = _originalCulture;
			Thread.CurrentThread.CurrentUICulture = _originalUiCulture;

			CultureInfo.CurrentCulture.ClearCachedData();
			CultureInfo.CurrentUICulture.ClearCachedData();
		}
	}
}
