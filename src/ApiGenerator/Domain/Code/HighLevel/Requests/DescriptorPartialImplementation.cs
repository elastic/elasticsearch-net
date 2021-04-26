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
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Requests 
{
	public class DescriptorPartialImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public IReadOnlyCollection<Constructor> Constructors { get; set; }
		public IReadOnlyCollection<UrlPart> Parts { get; set; }
		public IReadOnlyCollection<UrlPath> Paths { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public bool HasBody { get; set; }
		public string MethodName { get; set; }

		public IEnumerable<FluentRouteSetter> GetFluentRouteSetters()
		{
			var setters = new List<FluentRouteSetter>();
			if (!Parts.Any()) return setters;

			var alwaysGenerate = new[] { "index" };
			var parts = Parts
				.Where(p => !p.Required || alwaysGenerate.Contains(p.Name))
				.Where(p => !string.IsNullOrEmpty(p.Name))
				.ToList();
			var returnType = CsharpNames.GenericOrNonGenericDescriptorName;
			foreach (var part in parts)
			{
				var p = part;
				var paramName = p.Name.ToPascalCase();
				if (paramName.Length > 1)
					paramName = paramName.Substring(0, 1).ToLowerInvariant() + paramName.Substring(1);
				else
					paramName = paramName.ToLowerInvariant();

				var routeValue = "v";
				var routeSetter = p.Required ? "Required" : "Optional";

				var code =
					$"public {returnType} {p.InterfaceName}({p.HighLevelTypeName} {paramName}) => Assign({paramName}, (a,v)=>a.RouteValues.{routeSetter}(\"{p.Name}\", {routeValue}));";
				var xmlDoc = $"///<summary>{p.Description}</summary>";
				setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				if (paramName == "index")
				{
					code = $"public {returnType} {p.InterfaceName}<TOther>() where TOther : class ";
					code += $"=> Assign(typeof(TOther), (a,v)=>a.RouteValues.{routeSetter}(\"{p.Name}\", ({p.HighLevelTypeName})v));";
					xmlDoc = $"///<summary>a shortcut into calling {p.InterfaceName}(typeof(TOther))</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "index" && p.Type == "list")
				{
					code = $"public {returnType} AllIndices() => Index(Indices.All);";
					xmlDoc = $"///<summary>A shortcut into calling Index(Indices.All)</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "fields" && p.Type == "list")
				{
					code = $"public {returnType} Fields<T>(params Expression<Func<T, object>>[] fields) ";
					code += $"=> Assign(fields, (a,v)=>a.RouteValues.{routeSetter}(\"fields\", (Fields)v));";
					xmlDoc = $"///<summary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
			}
			return setters;
		}
	}
}