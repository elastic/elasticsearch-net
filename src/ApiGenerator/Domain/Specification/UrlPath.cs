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

using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Domain.Specification 
{
	public class UrlPath
	{
		private readonly List<UrlPart> _additionalPartsForConstructor;
		public string Path { get; }
		public DeprecatedPath Deprecation { get; }


		public List<UrlPart> Parts { get; }

		//TODO mark the parts that are deprecated
		//TODO this will all go away once https://github.com/elastic/elasticsearch/pull/42346 lands 
		public UrlPath(DeprecatedPath path, IDictionary<string, UrlPart> originalParts, IReadOnlyCollection<UrlPath> allNonDeprecatedPaths) 
			: this(path.Path, originalParts)
		{
			Deprecation = path;
			foreach (var part in Parts)
			{
				if (!part.Deprecated && !allNonDeprecatedPaths.Any(p => p.Path.Contains($"{{{part.Name}}}")))
					part.Deprecated = true;
			}
		}
		public UrlPath(string path, IDictionary<string, UrlPart> allParts, List<UrlPart> additionalPartsForConstructor = null)
		{
			_additionalPartsForConstructor = additionalPartsForConstructor ?? new List<UrlPart>();
			Path = LeadingBackslash(path);
			if (allParts == null)
			{
				Parts = new List<UrlPart>();
				return;
			}
			var parts =
				from p in allParts
				//so deliciously side effect-y but at least its more isolated then in ApiEndpoint.CsharpMethods
				let name = p.Value.Name = p.Key
				where path.Contains($"{{{name}}}")
				orderby path.IndexOf($"{{{name}}}", StringComparison.Ordinal)
				select p.Value;
			Parts = parts.ToList();
		}

		public string ConstructorArguments => string.Join(", ", Parts.Select(p => $"{p.HighLevelTypeName} {p.NameAsArgument}"));
		public string RequestBaseArguments =>
			!Parts.Any() ? string.Empty
				: "r => r." + string.Join(".", Parts.Select(p => $"{(p.Required ? "Required" : "Optional")}(\"{p.Name}\", {p.NameAsArgument})"));

		public string TypedSubClassBaseArguments => string.Join(", ", Parts.Select(p => p.NameAsArgument));

		private static readonly string[] ResolvableFromT = { "index"};

		public bool HasResolvableArguments => Parts.Any(p => ResolvableFromT.Contains(p.Name));
		public string AutoResolveConstructorArguments => string.Join(", ", Parts.Where(p  => !ResolvableFromT.Contains(p.Name)).Select(p => $"{p.HighLevelTypeName} {p.NameAsArgument}"));

		public string AutoResolveBaseArguments(string generic) => string.Join(", ", Parts.Select(p => !ResolvableFromT.Contains(p.Name) ? p.Name : $"typeof({generic})"));

		public string DocumentPathBaseArgument(string generic) => string.Join(", ",
			_additionalPartsForConstructor.Select(p => p.Name =="id" ? $"id ?? Nest.Id.From(documentWithId)"
				: ResolvableFromT.Contains(p.Name) ? $"{p.Name} ?? typeof({generic})" : p.Name));

		public string DocumentPathConstructorArgument(string generic) => string.Join(", ",
			new [] { $"{generic} documentWithId" }.Concat(_additionalPartsForConstructor.Select(p => $"{p.HighLevelTypeName} {p.NameAsArgument} = null")));

		public string GetXmlDocs(string indent, bool skipResolvable = false, bool documentConstructor = false)
		{
			var doc = $@"///<summary>{Path}</summary>";
			var parts = Parts.Where(p => !skipResolvable || !ResolvableFromT.Contains(p.Name)).ToList();
			if (!parts.Any()) return doc;

			doc += indent;
			doc += string.Join(indent, parts.Select(ParamDoc));
			return doc;

			string ParamDoc(UrlPart p) => P(p.NameAsArgument, GetDescription(p));

			string GetDescription(UrlPart p)
			{
				if (documentConstructor) return "The document used to resolve the path from";
				return p.Required ? "this parameter is required" : "Optional, accepts null";
			}
		}

		private string P(string name, string description) => $"///<param name=\"{name}\">{description}</param>";

		private string LeadingBackslash(string p) => p.StartsWith("/") ? p : $"/{p}";
	}
}
