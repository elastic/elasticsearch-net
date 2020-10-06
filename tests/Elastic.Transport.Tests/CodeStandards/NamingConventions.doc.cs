// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Elastic.Transport;
using FluentAssertions;
using Xunit;

namespace Elastic.Transport.Tests.CodeStandards
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
		[Fact] public void AbstractClassNamesEndWithBase()
		{
			var exceptions = new Type[]
			{
				typeof(PostData), typeof(RequestParameters<>)
			};

			var abstractClassesNotEndingInBase = typeof(ITransport<>).Assembly.GetTypes()
				.Where(t => t.IsClass && t.IsAbstract && !t.IsSealed && !exceptions.Contains(t))
				.Select(t => t.Name.Split('`')[0])
				.Where(n => n != "DelegatingStream")
				.Where(n => !n.EndsWith("Base"))
				.ToList();

			abstractClassesNotEndingInBase.Should().BeEmpty();
		}

		/**
		* Class names that end with `Base` suffix are abstract
		*/
		[Fact] public void ClassNameContainsBaseShouldBeAbstract()
		{
			var exceptions = new Type[] { };

			var baseClassesNotAbstract = typeof(ITransport<>).Assembly.GetTypes()
				.Where(t => t.IsClass && !exceptions.Contains(t))
				.Where(t => t.Name.Split('`')[0].EndsWith("Base"))
				.Where(t => !t.IsAbstract)
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			baseClassesNotAbstract.Should().BeEmpty();
		}

		private List<Type> Scan()
		{
			var assembly = typeof(ITransport<>).Assembly;

			var exceptions = new List<Type>
			{
				assembly.GetType("System.AssemblyVersionInformation", throwOnError: false),
				assembly.GetType("System.Runtime.Serialization.Formatters.FormatterAssemblyStyle", throwOnError: false),
				assembly.GetType("System.ComponentModel.Browsable", throwOnError: false),
				assembly.GetType("Microsoft.CodeAnalysis.EmbeddedAttribute", throwOnError: false),
				assembly.GetType("System.Runtime.CompilerServices.IsReadOnlyAttribute", throwOnError: false),
			};

			var types = assembly.GetTypes();
			var typesNotInNestNamespace = types
				.Where(t => t != null)
				.Where(t => t.Namespace != null)
				.Where(t => !exceptions.Contains(t))
				.ToList();
			return typesNotInNestNamespace;
		}

		[Fact] public void AllTransportTypesAreInTheRoot()
		{
			// We want all types in the Elastic.Transport namespaces, unless its relates to Diagnostics/Extensions or product specific integration
			var root = "Elastic.Transport";
			var allowedNamespaces = new[]
			{
				$"{root}.Diagnostics", $"{root}.Products", $"{root}.Extensions"
			};
			var transportTypes = Scan().Where(t => t.Namespace != root && !allowedNamespaces.Any(a => t.Namespace.StartsWith(a)))
				.Where(t => !t.Name.StartsWith("<"))
				.Where(t => IsValidTypeNameOrIdentifier(t.Name, true))
				.ToList();

			transportTypes.Should().BeEmpty();
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


