using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;

namespace Tests.CodeStandards
{
	[ProjectReferenceOnly]
	public class Analysis
	{
		/**
		* Every analyzer interface should attribute properties with JsonPropertyAttribute
		*/
		[U()]
		public void AnalyzerPropertiesAreAttributedWithJsonPropertyAttribute() =>
			PropertiesOfTypeAreAttributedWithJsonPropertyAttribute(typeof(IAnalyzer));

		/**
		* Every normalizer interface should attribute properties with JsonPropertyAttribute
		*/
		[U]
		public void NormalizerPropertiesAreAttributedWithJsonPropertyAttribute() =>
			PropertiesOfTypeAreAttributedWithJsonPropertyAttribute(typeof(INormalizer));

		/**
		* Every char filter interface should attribute properties with JsonPropertyAttribute
		*/
		[U]
		public void CharFilterPropertiesAreAttributedWithJsonPropertyAttribute() =>
			PropertiesOfTypeAreAttributedWithJsonPropertyAttribute(typeof(ICharFilter));

		/**
		* Every tokenizer interface should attribute properties with JsonPropertyAttribute
		*/
		[U]
		public void TokenizerPropertiesAreAttributedWithJsonPropertyAttribute() =>
			PropertiesOfTypeAreAttributedWithJsonPropertyAttribute(typeof(ITokenizer));

		/**
		* Every token filter interface should attribute properties with JsonPropertyAttribute
		*/
		[U]
		public void TokenFilterPropertiesAreAttributedWithJsonPropertyAttribute() =>
			PropertiesOfTypeAreAttributedWithJsonPropertyAttribute(typeof(ITokenFilter));

		private static void PropertiesOfTypeAreAttributedWithJsonPropertyAttribute(Type type)
		{
			var types =
				from t in type.Assembly().Types()
				where t.IsInterface() && type.IsAssignableFrom(t)
				let properties = t.GetProperties()
				from p in properties
				where p.GetCustomAttribute(typeof(JsonPropertyAttribute)) == null
				select $"{p.Name} on {t.Name} does not have {nameof(JsonPropertyAttribute)} applied";

			types.Should().BeEmpty();
		}


		[U] public void CharFiltersShouldBeInSync()
		{
			var analyzeMethods = PublicMethodsOf<AnalyzeCharFiltersDescriptor>(0, nameof(AnalyzeCharFiltersDescriptor.Name));
			var indexSettingsMethods = PublicMethodsOf<CharFiltersDescriptor>(1, nameof(CharFiltersDescriptor.UserDefined));

			analyzeMethods.Select(a => a.Item1).Should().BeEquivalentTo(indexSettingsMethods.Select(a => a.Item1),
				"Char filter methods are not in sync");
			analyzeMethods.Select(a => a.Item2.ParameterType).Should().BeEquivalentTo(indexSettingsMethods.Select(a => a.Item2.ParameterType),
				"Char filter selector funcs are not in sync");
		}

		[U] public void TokenFiltersShouldBeInSync()
		{
			var analyzeMethods = PublicMethodsOf<AnalyzeTokenFiltersDescriptor>(0, nameof(AnalyzeTokenFiltersDescriptor.Name));
			var indexSettingsMethods = PublicMethodsOf<TokenFiltersDescriptor>(1, nameof(TokenFiltersDescriptor.UserDefined));

			analyzeMethods.Select(a => a.Item1).Should().BeEquivalentTo(indexSettingsMethods.Select(a => a.Item1),
				"Token filter methods are not in sync");
			analyzeMethods.Select(a => a.Item2.ParameterType).Should().BeEquivalentTo(indexSettingsMethods.Select(a => a.Item2.ParameterType),
				"Token filter selector funcs are not in sync");
		}

		[U] public void TokenizersShouldBeInSync()
		{
			var analyzeMethods = PublicMethodsOf<AnalyzeTokenizersSelector>(0, typeof(ITokenizer), null);
			var indexSettingsMethods = PublicMethodsOf<TokenizersDescriptor>(1, nameof(TokenizersDescriptor.UserDefined));

			analyzeMethods.Select(a => a.Item1).Should().BeEquivalentTo(indexSettingsMethods.Select(a => a.Item1),
				"tokenizer methods are not in sync");
			analyzeMethods.Select(a => a.Item2.ParameterType).Should().BeEquivalentTo(indexSettingsMethods.Select(a => a.Item2.ParameterType),
				"tokenizer selector funcs are not in sync");
		}

		private static readonly string[] IgnoreObjectMethods = {nameof(GetHashCode), nameof(object.Equals), nameof(ToString), nameof(ReferenceEquals), nameof(GetType)};

		private static IList<Tuple<string, ParameterInfo>> PublicMethodsOf<T>(int selectorParam, params string[] except) =>
			PublicMethodsOf<T>(selectorParam, typeof(T), except);

		private static IList<Tuple<string, ParameterInfo>> PublicMethodsOf<T>(int selectorParam, Type returnType, params string[] except)
		{
			var ignore = (except ?? new string[] {}).Concat(IgnoreObjectMethods).ToArray();

			var methods = (from m in typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance)
				where m.ReturnType == returnType
				where !ignore.Contains(m.Name)
				select Tuple.Create(m.Name, m.GetParameters()[selectorParam])).ToList();
			methods.Should().NotBeEmpty("Expected public methods on {0}", typeof(T).Name);
			return methods.ToList();
		}
	}
}
