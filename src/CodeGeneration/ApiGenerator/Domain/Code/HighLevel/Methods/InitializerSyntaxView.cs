using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CsQuery.Engine.PseudoClassSelectors;
using Microsoft.CodeAnalysis;

namespace ApiGenerator.Domain
{
	public class InitializerSyntaxView
	{
		public InitializerSyntaxView(InitializerMethod  syntax, bool async) => (Syntax , Async) = (syntax, async);

		public InitializerMethod Syntax { get; }

		public bool Async { get; }
	}

}
