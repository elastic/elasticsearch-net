// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace ApiGenerator.Domain.Code.HighLevel.Methods 
{
	public class FluentSyntaxView
	{
		public FluentSyntaxView(FluentSyntaxBase syntax, bool async) => (Syntax , Async) = (syntax, async);

		public FluentSyntaxBase Syntax { get; }

		public bool Async { get; }
	}
}