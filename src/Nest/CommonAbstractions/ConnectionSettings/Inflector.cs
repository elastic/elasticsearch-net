using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nest
{
	/// <summary>
	/// Pluralizes or singularizes words.
	/// </summary>
	public static class Inflector
	{
		private static readonly ConcurrentDictionary<string, string> _memoization = new ConcurrentDictionary<string, string>(); 
		private static readonly List<InflectorRule> _plurals = new List<InflectorRule>();
		private static readonly List<InflectorRule> _singulars = new List<InflectorRule>();
		private static readonly List<string> _uncountables = new List<string>();

		/// <summary>
		/// Initializes the <see cref="Inflector"/> class.
		/// </summary>
		static Inflector()
		{
			AddPluralRule("$", "s");
			AddPluralRule("s$", "s");
			AddPluralRule("(ax|test)is$", "$1es");
			AddPluralRule("(octop|vir)us$", "$1i");
			AddPluralRule("(alias|status)$", "$1es");
			AddPluralRule("(bu)s$", "$1ses");
			AddPluralRule("(buffal|tomat)o$", "$1oes");
			AddPluralRule("([ti])um$", "$1a");
			AddPluralRule("sis$", "ses");
			AddPluralRule("(?:([^f])fe|([lr])f)$", "$1$2ves");
			AddPluralRule("(hive)$", "$1s");
			AddPluralRule("([^aeiouy]|qu)y$", "$1ies");
			AddPluralRule("(x|ch|ss|sh)$", "$1es");
			AddPluralRule("(matr|vert|ind)ix|ex$", "$1ices");
			AddPluralRule("([m|l])ouse$", "$1ice");
			AddPluralRule("^(ox)$", "$1en");
			AddPluralRule("(quiz)$", "$1zes");

			AddSingularRule("s$", String.Empty);
			AddSingularRule("ss$", "ss");
			AddSingularRule("(n)ews$", "$1ews");
			AddSingularRule("([ti])a$", "$1um");
			AddSingularRule("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
			AddSingularRule("(^analy)ses$", "$1sis");
			AddSingularRule("([^f])ves$", "$1fe");
			AddSingularRule("(hive)s$", "$1");
			AddSingularRule("(tive)s$", "$1");
			AddSingularRule("([lr])ves$", "$1f");
			AddSingularRule("([^aeiouy]|qu)ies$", "$1y");
			AddSingularRule("(s)eries$", "$1eries");
			AddSingularRule("(m)ovies$", "$1ovie");
			AddSingularRule("(x|ch|ss|sh)es$", "$1");
			AddSingularRule("([m|l])ice$", "$1ouse");
			AddSingularRule("(bus)es$", "$1");
			AddSingularRule("(o)es$", "$1");
			AddSingularRule("(shoe)s$", "$1");
			AddSingularRule("(cris|ax|test)es$", "$1is");
			AddSingularRule("(octop|vir)i$", "$1us");
			AddSingularRule("(alias|status)$", "$1");
			AddSingularRule("(alias|status)es$", "$1");
			AddSingularRule("^(ox)en", "$1");
			AddSingularRule("(vert|ind)ices$", "$1ex");
			AddSingularRule("(matr)ices$", "$1ix");
			AddSingularRule("(quiz)zes$", "$1");

			AddIrregularRule("person", "people");
			AddIrregularRule("man", "men");
			AddIrregularRule("child", "children");
			AddIrregularRule("sex", "sexes");
			AddIrregularRule("tax", "taxes");
			AddIrregularRule("move", "moves");

			AddUnknownCountRule("equipment");
			AddUnknownCountRule("information");
			AddUnknownCountRule("rice");
			AddUnknownCountRule("money");
			AddUnknownCountRule("species");
			AddUnknownCountRule("series");
			AddUnknownCountRule("fish");
			AddUnknownCountRule("sheep");
		}

		/// <summary>
		/// Adds the irregular rule.
		/// </summary>
		/// <param name="singular">The singular.</param>
		/// <param name="plural">The plural.</param>
		private static void AddIrregularRule(string singular, string plural)
		{
			AddPluralRule(String.Concat("(", singular[0], ")", singular.Substring(1), "$"),
						  String.Concat("$1", plural.Substring(1)));
			AddSingularRule(String.Concat("(", plural[0], ")", plural.Substring(1), "$"),
							String.Concat("$1", singular.Substring(1)));
		}

		/// <summary>
		/// Adds the unknown count rule.
		/// </summary>
		/// <param name="word">The word.</param>
		private static void AddUnknownCountRule(string word)
		{
			_uncountables.Add(word.ToLower());
		}

		/// <summary>
		/// Adds the plural rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <param name="replacement">The replacement.</param>
		private static void AddPluralRule(string rule, string replacement)
		{
			_plurals.Add(new InflectorRule(rule, replacement));
		}

		/// <summary>
		/// Adds the singular rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <param name="replacement">The replacement.</param>
		private static void AddSingularRule(string rule, string replacement)
		{
			_singulars.Add(new InflectorRule(rule, replacement));
		}

		/// <summary>
		/// Makes the plural.
		/// </summary>
		/// <param name="word">The word.</param>
		/// <returns></returns>
		public static string MakePlural(this string word)
		{
			string plural;
			if (!_memoization.TryGetValue(word, out plural))
			{
				plural = ApplyRules(_plurals, word);
				_memoization.TryAdd(word, plural);
			}
			return plural;
		}

		/// <summary>
		/// Makes the singular.
		/// </summary>
		/// <param name="word">The word.</param>
		/// <returns></returns>
		public static string MakeSingular(this string word)
		{
			return ApplyRules(_singulars, word);
		}

		/// <summary>
		/// Applies the rules.
		/// </summary>
		/// <param name="rules">The rules.</param>
		/// <param name="word">The word.</param>
		/// <returns></returns>
		private static string ApplyRules(IList<InflectorRule> rules, string word)
		{
			string result = word;

			if (!_uncountables.Contains(word.ToLower()))
			{
				for (int i = rules.Count - 1; i >= 0; i--)
				{
					string currentPass = rules[i].Apply(word);
					if (currentPass != null)
					{
						result = currentPass;
						break;
					}
				}
			}

			return result;
		}

		#region Nested type: InflectorRule

		/// <summary>
		/// Summary for the InflectorRule class
		/// </summary>
		private class InflectorRule
		{
			private readonly Regex regex;
			private readonly string replacement;

			/// <summary>
			/// Initializes a new instance of the <see cref="InflectorRule"/> class.
			/// </summary>
			/// <param name="regexPattern">The regex pattern.</param>
			/// <param name="replacementText">The replacement text.</param>
			public InflectorRule(string regexPattern, string replacementText)
			{
				regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
				replacement = replacementText;
			}

			/// <summary>
			/// Applies the tule to the specified word.
			/// </summary>
			/// <param name="word">The word.</param>
			/// <returns></returns>
			public string Apply(string word)
			{
				if (!regex.IsMatch(word))
					return null;

				string replace = regex.Replace(word, replacement);
				if (word == word.ToUpper())
					replace = replace.ToUpper();

				return replace;
			}
		}

		#endregion
	}
}