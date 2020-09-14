// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.CodeStandards
{
	/** == Naming Conventions
	*
	* NEST uses the following naming conventions (with _some_ exceptions).
	*/
	public class NamingConventions
	{
		/** === Class Names
		*
		* Abstract class names should end with a `Base` suffix
		*/
		[U] public void AbstractClassNamesEndWithBase()
		{
			var exceptions = new[]
			{
				typeof(DateMath)
			};

			var abstractClassesNotEndingInBase = typeof(IRequest).Assembly.GetTypes()
				.Where(t => t.IsClass && t.IsAbstract && !t.IsSealed && !exceptions.Contains(t))
				//when testing nuget package against merged internalize json.net skip its types.
				.Where(t => !t.Namespace.StartsWith("Nest.Json"))
				.Where(t => !t.Namespace.StartsWith("Elastic.Internal"))
				.Where(t => !t.Name.Split('`')[0].EndsWith("Base"))
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			abstractClassesNotEndingInBase.Should().BeEmpty();
		}

		/**
		* Class names that end with `Base` suffix are abstract
		*/
		[U] public void ClassNameContainsBaseShouldBeAbstract()
		{
			var exceptions = new[] { typeof(DateMath) };

			var baseClassesNotAbstract = typeof(IRequest).Assembly.GetTypes()
				.Where(t => t.IsClass && !exceptions.Contains(t))
				.Where(t => t.Name.Split('`')[0].EndsWith("Base"))
				.Where(t => !t.IsAbstract)
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			baseClassesNotAbstract.Should().BeEmpty();
		}

		/** === Requests and Responses
		*
		* Request class names should end with `Request`
		*/
		[U]
		public void RequestClassNamesEndWithRequest()
		{
			var types = typeof(IRequest).Assembly.GetTypes();
			var requestsNotEndingInRequest = types
				.Where(t => typeof(IRequest).IsAssignableFrom(t) && !t.IsAbstract)
				.Where(t => !typeof(IDescriptor).IsAssignableFrom(t))
				.Where(t => !t.Name.Split('`')[0].EndsWith("Request"))
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			requestsNotEndingInRequest.Should().BeEmpty();
		}

		/**
		* Response class names should end with `Response`
		**/
		[U]
		public void ResponseClassNamesEndWithResponse()
		{
			var types = typeof(IRequest).Assembly.GetTypes();
			var responsesNotEndingInResponse = types
				.Where(t => typeof(IResponse).IsAssignableFrom(t) && !t.IsAbstract)
				.Where(t => !t.Name.Split('`')[0].EndsWith("Response"))
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			responsesNotEndingInResponse.Should().BeEmpty();
		}

		/**
		* Request and Response class names should be one to one in *most* cases.
		* e.g. `ValidateRequest` => `ValidateResponse`, and not `ValidateQueryRequest` => `ValidateResponse`
		* There are a few exceptions to this rule, most notably the `Cat` prefixed requests and
		* the `Exists` requests.
		*/
		[U]
		public void ParityBetweenRequestsAndResponses()
		{
			var exceptions = new[] // <1> _Exceptions to the rule_
			{
				//TODO These are new API's should be removed, also add test that no request or response starts with Xpack
				//only XPack
				//TODO MAP THIS
				//typeof(RankEvalRequest),
				//TODO add unit tests that we have no requests starting with Exists
				typeof(SourceExistsRequest),
				typeof(SourceExistsRequest<>),
				typeof(DocumentExistsRequest),
				typeof(DocumentExistsRequest<>),
				typeof(AliasExistsRequest),
				typeof(IndexExistsRequest),
				typeof(IndexTemplateExistsRequest),
				typeof(SearchTemplateRequest),
				typeof(SearchTemplateRequest<>),
				typeof(ScrollRequest),
				typeof(SourceRequest),
				typeof(SourceRequest<>),
				typeof(ValidateQueryRequest<>),
				typeof(GetAliasRequest),
				typeof(IndicesShardStoresRequest),
				typeof(RenderSearchTemplateRequest),
				typeof(MultiSearchTemplateRequest),
				typeof(CreateRequest<>),
				typeof(DeleteByQueryRethrottleRequest), // uses ListTasksResponse
				typeof(UpdateByQueryRethrottleRequest) // uses ListTasksResponse
			};

			var types = typeof(IRequest).Assembly.GetTypes();

			var requests = new HashSet<string>(types
				.Where(t =>
					t.IsClass &&
					!t.IsAbstract &&
					typeof(IRequest).IsAssignableFrom(t) &&
					!typeof(IDescriptor).IsAssignableFrom(t)
					&& !t.Name.StartsWith("Cat")
					&& !exceptions.Contains(t))
				.Select(t => t.Name.Split('`')[0].Replace("Request", ""))
			);

			var responses = types
				.Where(t => t.IsClass && !t.IsAbstract && typeof(IResponse).IsAssignableFrom(t))
				.Select(t => t.Name.Split('`')[0].Replace("Response", ""));

			requests.Except(responses).Should().BeEmpty();
		}

		[U]
		public void AllNestTypesAreInNestNamespace()
		{
			var nestAssembly = typeof(IElasticClient).Assembly;

			var exceptions = new List<Type>
			{
				nestAssembly.GetType("System.AssemblyVersionInformation", throwOnError: false),
				nestAssembly.GetType("System.Runtime.Serialization.Formatters.FormatterAssemblyStyle", throwOnError: false),
				nestAssembly.GetType("System.ComponentModel.Browsable", throwOnError: false),
				nestAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute", throwOnError: false),
				nestAssembly.GetType("System.Runtime.CompilerServices.IsReadOnlyAttribute", throwOnError: false),
			};

			var types = nestAssembly.GetTypes();
			var typesNotInNestNamespace = types
				.Where(t => t != null)
				.Where(t => !exceptions.Contains(t))
				.Where(t => t.Namespace != "Nest")
				//when testing nuget package against merged internalize json.net skip its types.
				.Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && !t.Namespace.StartsWith("Nest.Json"))
				.Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && !t.Namespace.StartsWith("Elastic.Internal"))
				.Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && !t.Namespace.StartsWith("Nest.Specification"))
				.Where(t => !t.Name.StartsWith("<"))
				.Where(t => IsValidTypeNameOrIdentifier(t.Name, true))
				.ToList();

			typesNotInNestNamespace.Should().BeEmpty();
		}

		[U]
		public void AllElasticsearchNetTypesAreInElasticsearchNetNamespace()
		{
			var elasticsearchNetAssembly = typeof(IElasticLowLevelClient).Assembly;

			var exceptions = new List<Type>
			{
				elasticsearchNetAssembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute"),
				elasticsearchNetAssembly.GetType("System.Runtime.CompilerServices.IsReadOnlyAttribute"),
				elasticsearchNetAssembly.GetType("System.AssemblyVersionInformation"),
				elasticsearchNetAssembly.GetType("System.FormattableString"),
				elasticsearchNetAssembly.GetType("System.Runtime.CompilerServices.FormattableStringFactory"),
				elasticsearchNetAssembly.GetType("System.Runtime.CompilerServices.FormattableStringFactory"),
				elasticsearchNetAssembly.GetType("Purify.Purifier"),
				elasticsearchNetAssembly.GetType("Purify.Purifier+IPurifier"),
				elasticsearchNetAssembly.GetType("Purify.Purifier+PurifierDotNet"),
				elasticsearchNetAssembly.GetType("Purify.Purifier+PurifierMono"),
				elasticsearchNetAssembly.GetType("Purify.Purifier+UriInfo"),
				elasticsearchNetAssembly.GetType("System.ComponentModel.Browsable")
			};

			var types = elasticsearchNetAssembly.GetTypes();
			var typesNotIElasticsearchNetNamespace = types
				.Where(t => !exceptions.Contains(t))
				.Where(t => t.Namespace != null)
				.Where(t => t.Namespace != "Elasticsearch.Net" && !t.Namespace.StartsWith("Elasticsearch.Net.Specification"))
				.Where(t => !t.Namespace.StartsWith("Elasticsearch.Net.Utf8Json"))
				.Where(t => !t.Namespace.StartsWith("Elasticsearch.Net.Extensions"))
				.Where(t => !t.Namespace.StartsWith("Elasticsearch.Net.Diagnostics"))
				.Where(t => !t.Name.StartsWith("<"))
				.Where(t => IsValidTypeNameOrIdentifier(t.Name, true))
				.ToList();

			typesNotIElasticsearchNetNamespace.Should().BeEmpty();
		}

		/// implementation from System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(string value)
		private static bool IsValidTypeNameOrIdentifier(string value, bool isTypeName)
		{
			var nextMustBeStartChar = true;
			if (value.Length == 0)
				return false;
			for (var index = 0; index < value.Length; ++index)
			{
				var character = value[index];
				var unicodeCategory = char.GetUnicodeCategory(character);

				switch (unicodeCategory)
				{
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.LowercaseLetter:
					case UnicodeCategory.TitlecaseLetter:
					case UnicodeCategory.ModifierLetter:
					case UnicodeCategory.OtherLetter:
					case UnicodeCategory.LetterNumber:
						nextMustBeStartChar = false;
						break;
					case UnicodeCategory.NonSpacingMark:
					case UnicodeCategory.SpacingCombiningMark:
					case UnicodeCategory.DecimalDigitNumber:
					case UnicodeCategory.ConnectorPunctuation:
						if (nextMustBeStartChar && (int)character != 95)
							return false;
						nextMustBeStartChar = false;
						break;
					default:
						if (!isTypeName || !IsSpecialTypeChar(character, ref nextMustBeStartChar))
							return false;
						break;
				}
			}
			return true;
		}

		private static bool IsSpecialTypeChar(char ch, ref bool nextMustBeStartChar)
		{
			if ((uint)ch <= 62U)
			{
				switch (ch)
				{
					case '$':
					case '&':
					case '*':
					case '+':
					case ',':
					case '-':
					case '.':
					case ':':
					case '<':
					case '>':
						break;
					default:
						goto label_6;
				}
			}
			else if ((int)ch != 91 && (int)ch != 93)
			{
				if ((int)ch == 96)
					return true;
				goto label_6;
			}
			nextMustBeStartChar = true;
			return true;
			label_6:
			return false;
		}
	}
}


