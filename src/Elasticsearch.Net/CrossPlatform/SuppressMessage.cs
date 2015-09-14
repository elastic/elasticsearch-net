#if DOTNETCORE
using System;

namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(
	AttributeTargets.All,
	Inherited = false,
	AllowMultiple = true
	)
	]
	[Conditional("CODE_ANALYSIS")]
	public sealed class SuppressMessageAttribute : Attribute
	{
		public SuppressMessageAttribute(string category, string checkId) { }

		public string Category => null;
		public string CheckId => null;

		public string Scope { get; set; }

		public string Target { get; set; }

		public string MessageId { get; set; }

		public string Justification { get; set; }
	}
}
#endif