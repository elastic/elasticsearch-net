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
using ApiGenerator.Configuration;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Requests 
{
	public class RequestInterface
	{
		public IReadOnlyCollection<UrlPart> UrlParts { get; set; }

		/// <summary>
		/// Partial parameters are query string parameters we prefer to send over the body of a request.
		/// We declare these on the generated interfaces so that we don't forget to implement them in our request
		/// implementations
		/// </summary>
		public IReadOnlyCollection<QueryParameters> PartialParameters { get; set; }
		
		public string OfficialDocumentationLink { get; set; }
		
		public CsharpNames CsharpNames { get; set; }

		private bool GenerateOnlyGenericInterface => CodeConfiguration.GenericOnlyInterfaces.Contains(CsharpNames.RequestInterfaceName);

		public bool NeedsGenericInterface => !GenerateOnlyGenericInterface && !string.IsNullOrWhiteSpace(CsharpNames.GenericsDeclaredOnRequest);

		public string Name => CsharpNames.GenericOrNonGenericInterfacePreference;
	}
}