/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Configuration;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Methods 
{
	public class BoundFluentMethod : FluentSyntaxBase
	{
		public BoundFluentMethod(CsharpNames names, IReadOnlyCollection<UrlPart> parts, bool selectorIsOptional, string link, string summary) 
			: base(names, parts, selectorIsOptional, link, summary) { }

		private string DescriptorTypeParams => string.Join(", ", CsharpNames.DescriptorGenerics
			.Select(e => CsharpNames.DescriptorBoundDocumentGeneric));
		
		private string RequestTypeParams => string.Join(", ", CsharpNames.SplitGeneric(CsharpNames.GenericsDeclaredOnRequest)
			.Select(e => CsharpNames.DescriptorBoundDocumentGeneric));

		private string SelectorReturn => string.IsNullOrWhiteSpace(CsharpNames.GenericsDeclaredOnRequest)
			|| !CodeConfiguration.GenericOnlyInterfaces.Contains(CsharpNames.RequestInterfaceName)
				? CsharpNames.RequestInterfaceName
				: $"{CsharpNames.RequestInterfaceName}<{RequestTypeParams}>";
		
		public override string DescriptorName => $"{CsharpNames.DescriptorName}<{DescriptorTypeParams}>";
		public override string GenericWhereClause => $"where {CsharpNames.DescriptorBoundDocumentGeneric} : class";
		public override string MethodGenerics => $"<{CsharpNames.DescriptorBoundDocumentGeneric}>";
		
		public override string RequestMethodGenerics => !string.IsNullOrWhiteSpace(RequestTypeParams) 
			? $"<{RequestTypeParams}>"
			: base.RequestMethodGenerics;
		
		public override string Selector => $"Func<{DescriptorName}, {SelectorReturn}>";
		

	}
}