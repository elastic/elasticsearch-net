using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;

namespace Tests.CodeStandards
{
	public class Analysis
	{
		/**
		* Every analyzer interface should attribute properties with JsonPropertyAttribute
		*/
		[U]
		public void AnalyzerPropertiesAreAttributedWithJsonPropertyAttribute() =>
			PropertiesOfTypeAreAttributedWithJsonPropertyAttribute(typeof(IAnalyzer));

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
	}
}
