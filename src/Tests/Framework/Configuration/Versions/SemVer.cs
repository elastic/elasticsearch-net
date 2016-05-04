/*
Copyright (c) Adam Reeve ("Author")
All rights reserved.

The BSD License

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright
notice, this list of conditions and the following disclaimer in the
documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR AND CONTRIBUTORS ``AS IS'' AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS
BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

//TODO TAKEN FROM https://github.com/adamreeve/semver.net, we need to be good OSS citizens and open a PR for CoreCLR support
//With rc2 in such a flux at the moment its best to defer this untill we are ready to move to rc2 ourselves

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tests.Framework.Versions
{
	internal class SemverComparator : IEquatable<SemverComparator>
	{
		public readonly Operator ComparatorType;

		public readonly SemanticVersion Version;

		private const string pattern = @"
            \s*
            ([=<>]*)                # SemverComparator type (can be empty)
            \s*
            ([0-9a-zA-Z\-\+\.\*]+)  # Version (potentially partial version)
            \s*
            ";

		public SemverComparator(string input)
		{
			var regex = new Regex(String.Format("^{0}$", pattern),
					RegexOptions.IgnorePatternWhitespace);
			var match = regex.Match(input);
			if (!match.Success)
			{
				throw new ArgumentException(String.Format("Invalid comparator string: {0}", input));
			}

			ComparatorType = ParseComparatorType(match.Groups[1].Value);
			var partialVersion = new PartialVersion(match.Groups[2].Value);

			if (!partialVersion.IsFull())
			{
				// For Operator.Equal, partial versions are handled by the StarRange
				// desugarer, and desugar to multiple comparators.

				switch (ComparatorType)
				{
					// For <= with a partial version, eg. <=1.2.x, this
					// means the same as < 1.3.0, and <=1.x means <2.0
					case Operator.LessThanOrEqual:
						ComparatorType = Operator.LessThan;
						if (!partialVersion.Major.HasValue)
						{
							// <=* means >=0.0.0
							ComparatorType = Operator.GreaterThanOrEqual;
							Version = new SemanticVersion(0, 0, 0);
						}
						else if (!partialVersion.Minor.HasValue)
						{
							Version = new SemanticVersion(partialVersion.Major.Value + 1, 0, 0);
						}
						else
						{
							Version = new SemanticVersion(partialVersion.Major.Value, partialVersion.Minor.Value + 1, 0);
						}
						break;
					case Operator.GreaterThan:
						ComparatorType = Operator.GreaterThanOrEqual;
						if (!partialVersion.Major.HasValue)
						{
							// >* is unsatisfiable, so use <0.0.0
							ComparatorType = Operator.LessThan;
							Version = new SemanticVersion(0, 0, 0);
						}
						else if (!partialVersion.Minor.HasValue)
						{
							// eg. >1.x -> >=2.0
							Version = new SemanticVersion(partialVersion.Major.Value + 1, 0, 0);
						}
						else
						{
							// eg. >1.2.x -> >=1.3
							Version = new SemanticVersion(partialVersion.Major.Value, partialVersion.Minor.Value + 1, 0);
						}
						break;
					default:
						// <1.2.x means <1.2.0
						// >=1.2.x means >=1.2.0
						Version = partialVersion.ToZeroVersion();
						break;
				}
			}
			else
			{
				Version = partialVersion.ToZeroVersion();
			}
		}

		public SemverComparator(Operator comparatorType, SemanticVersion comparatorVersion)
		{
			if (comparatorVersion == null)
			{
				throw new NullReferenceException("Null comparator version");
			}
			ComparatorType = comparatorType;
			Version = comparatorVersion;
		}

		public static Tuple<int, SemverComparator> TryParse(string input)
		{
			var regex = new Regex(String.Format("^{0}", pattern),
					RegexOptions.IgnorePatternWhitespace);

			var match = regex.Match(input);

			return match.Success ?
				Tuple.Create(
					match.Length,
					new SemverComparator(match.Value))
				: null;
		}

		private static Operator ParseComparatorType(string input)
		{
			switch (input)
			{
				case (""):
				case ("="):
					return Operator.Equal;
				case ("<"):
					return Operator.LessThan;
				case ("<="):
					return Operator.LessThanOrEqual;
				case (">"):
					return Operator.GreaterThan;
				case (">="):
					return Operator.GreaterThanOrEqual;
				default:
					throw new ArgumentException(String.Format("Invalid comparator type: {0}", input));
			}
		}

		public bool IsSatisfied(SemanticVersion version)
		{
			switch (ComparatorType)
			{
				case (Operator.Equal):
					return version == Version;
				case (Operator.LessThan):
					return version < Version;
				case (Operator.LessThanOrEqual):
					return version <= Version;
				case (Operator.GreaterThan):
					return version > Version;
				case (Operator.GreaterThanOrEqual):
					return version >= Version;
				default:
					throw new InvalidOperationException("SemverComparator type not recognised.");
			}
		}

		public enum Operator
		{
			Equal = 0,
			LessThan,
			LessThanOrEqual,
			GreaterThan,
			GreaterThanOrEqual,
		}

		public override string ToString()
		{
			string operatorString = null;
			switch (ComparatorType)
			{
				case (Operator.Equal):
					operatorString = "=";
					break;
				case (Operator.LessThan):
					operatorString = "<";
					break;
				case (Operator.LessThanOrEqual):
					operatorString = "<=";
					break;
				case (Operator.GreaterThan):
					operatorString = ">";
					break;
				case (Operator.GreaterThanOrEqual):
					operatorString = ">=";
					break;
				default:
					throw new InvalidOperationException("SemverComparator type not recognised.");
			}
			return String.Format("{0}{1}", operatorString, Version);
		}

		public bool Equals(SemverComparator other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			return ComparatorType == other.ComparatorType && Version == other.Version;
		}

		public override bool Equals(object other)
		{
			return Equals(other as SemverComparator);
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}

	internal static class PreReleaseVersion
	{
		public static int Compare(string a, string b)
		{
			if (a == null && b == null)
			{
				return 0;
			}
			else if (a == null)
			{
				// No pre-release is > having a pre-release version
				return 1;
			}
			else if (b == null)
			{
				return -1;
			}
			else
			{
				foreach (var c in IdentifierComparisons(Identifiers(a), Identifiers(b)))
				{
					if (c != 0)
					{
						return c;
					}
				}
				return 0;
			}
		}

		public static string Clean(string input)
		{
			var identifierStrings = Identifiers(input).Select(i => i.Clean());
			return String.Join(".", identifierStrings.ToArray());
		}

		private class Identifier
		{
			public bool IsNumeric { get; set; }
			public int IntValue { get; set; }
			public string Value { get; set; }

			public Identifier(string input)
			{
				Value = input;
				SetNumeric();
			}

			public string Clean()
			{
				return IsNumeric ? IntValue.ToString() : Value;
			}

			private void SetNumeric()
			{
				int x;
				bool couldParse = Int32.TryParse(Value, out x);
				IsNumeric = couldParse && x >= 0;
				IntValue = x;
			}
		}

		private static IEnumerable<Identifier> Identifiers(string input)
		{
			foreach (var identifier in input.Split('.'))
			{
				yield return new Identifier(identifier);
			}
		}

		private static IEnumerable<int> IdentifierComparisons(
				IEnumerable<Identifier> aIdentifiers, IEnumerable<Identifier> bIdentifiers)
		{
			foreach (var identifiers in ZipIdentifiers(aIdentifiers, bIdentifiers))
			{
				var a = identifiers.Item1;
				var b = identifiers.Item2;
				if (a == b)
				{
					yield return 0;
				}
				else if (a == null)
				{
					yield return -1;
				}
				else if (b == null)
				{
					yield return 1;
				}
				else
				{
					if (a.IsNumeric && b.IsNumeric)
					{
						yield return a.IntValue.CompareTo(b.IntValue);
					}
					else if (!a.IsNumeric && !b.IsNumeric)
					{
						yield return String.CompareOrdinal(a.Value, b.Value);
					}
					else if (a.IsNumeric && !b.IsNumeric)
					{
						yield return -1;
					}
					else // !a.IsNumeric && b.IsNumeric
					{
						yield return 1;
					}
				}
			}
		}

		// Zip identifier sets until both have been exhausted, returning null
		// for identifier components not in one set.
		private static IEnumerable<Tuple<Identifier, Identifier>> ZipIdentifiers(
				IEnumerable<Identifier> first, IEnumerable<Identifier> second)
		{
			using (var ie1 = first.GetEnumerator())
			using (var ie2 = second.GetEnumerator())
			{
				while (ie1.MoveNext())
				{
					if (ie2.MoveNext())
					{
						yield return Tuple.Create(ie1.Current, ie2.Current);
					}
					else
					{
						yield return Tuple.Create<Identifier, Identifier>(ie1.Current, null);
					}
				}
				while (ie2.MoveNext())
				{
					yield return Tuple.Create<Identifier, Identifier>(null, ie2.Current);
				}
			}
		}
	}

	// A version that might not have a minor or patch
	// number, for use in ranges like "^1.2" or "2.x"
	internal class PartialVersion
	{
		public int? Major { get; set; }

		public int? Minor { get; set; }

		public int? Patch { get; set; }

		public string PreRelease { get; set; }

		private static Regex regex = new Regex(@"^
                [v=\s]*
                (\d+|[Xx\*])                      # major version
                (
                    \.
                    (\d+|[Xx\*])                  # minor version
                    (
                        \.
                        (\d+|[Xx\*])              # patch version
                        (\-?([0-9A-Za-z\-\.]+))?  # pre-release version
                        (\+([0-9A-Za-z\-\.]+))?   # build version (ignored)
                    )?
                )?
                $",
			RegexOptions.IgnorePatternWhitespace);

		public PartialVersion(string input)
		{

			string[] xValues = { "X", "x", "*" };

			if (input.Trim() == "")
			{
				// Empty input means any version
				return;
			}

			var match = regex.Match(input);
			if (!match.Success)
			{
				throw new ArgumentException(String.Format("Invalid version string: \"{0}\"", input));
			}

			if (xValues.Contains(match.Groups[1].Value))
			{
				Major = null;
			}
			else
			{
				Major = Int32.Parse(match.Groups[1].Value);
			}

			if (match.Groups[2].Success)
			{
				if (xValues.Contains(match.Groups[3].Value))
				{
					Minor = null;
				}
				else
				{
					Minor = Int32.Parse(match.Groups[3].Value);
				}
			}

			if (match.Groups[4].Success)
			{
				if (xValues.Contains(match.Groups[5].Value))
				{
					Patch = null;
				}
				else
				{
					Patch = Int32.Parse(match.Groups[5].Value);
				}
			}

			if (match.Groups[6].Success)
			{
				PreRelease = match.Groups[7].Value;
			}
		}

		public SemanticVersion ToZeroVersion()
		{
			return new SemanticVersion(
					Major ?? 0,
					Minor ?? 0,
					Patch ?? 0,
					PreRelease);
		}

		public bool IsFull()
		{
			return Major.HasValue && Minor.HasValue && Patch.HasValue;
		}
	}

	internal static class Desugarer
	{
		private const string versionChars = @"[0-9a-zA-Z\-\+\.\*]";

		// Allows patch-level changes if a minor version is specified
		// on the comparator. Allows minor-level changes if not.
		public static Tuple<int, SemverComparator[]> TildeRange(string spec)
		{
			string pattern = String.Format(@"^\s*~\s*({0}+)\s*", versionChars);

			var regex = new Regex(pattern);
			var match = regex.Match(spec);
			if (!match.Success)
			{
				return null;
			}

			SemanticVersion minVersion = null;
			SemanticVersion maxVersion = null;

			var version = new PartialVersion(match.Groups[1].Value);
			if (version.Minor.HasValue)
			{
				// Doesn't matter whether patch version is null or not,
				// the logic is the same, min patch version will be zero if null.
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(version.Major.Value, version.Minor.Value + 1, 0);
			}
			else
			{
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(version.Major.Value + 1, 0, 0);
			}

			return Tuple.Create(
					match.Length,
					minMaxComparators(minVersion, maxVersion));
		}

		// Allows changes that do not modify the left-most non-zero digit
		// in the [major, minor, patch] tuple.
		public static Tuple<int, SemverComparator[]> CaretRange(string spec)
		{
			string pattern = String.Format(@"^\s*\^\s*({0}+)\s*", versionChars);

			var regex = new Regex(pattern);
			var match = regex.Match(spec);
			if (!match.Success)
			{
				return null;
			}

			SemanticVersion minVersion = null;
			SemanticVersion maxVersion = null;

			var version = new PartialVersion(match.Groups[1].Value);

			if (version.Major.Value > 0)
			{
				// Don't allow major version change
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(version.Major.Value + 1, 0, 0);
			}
			else if (!version.Minor.HasValue)
			{
				// Don't allow major version change, even if it's zero
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(version.Major.Value + 1, 0, 0);
			}
			else if (!version.Patch.HasValue)
			{
				// Don't allow minor version change, even if it's zero
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(0, version.Minor.Value + 1, 0);
			}
			else if (version.Minor > 0)
			{
				// Don't allow minor version change
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(0, version.Minor.Value + 1, 0);
			}
			else
			{
				// Only patch non-zero, don't allow patch change
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(0, 0, version.Patch.Value + 1);
			}

			return Tuple.Create(
					match.Length,
					minMaxComparators(minVersion, maxVersion));
		}

		public static Tuple<int, SemverComparator[]> HyphenRange(string spec)
		{
			string pattern = String.Format(@"^\s*({0}+)\s+\-\s+({0}+)\s*", versionChars);

			var regex = new Regex(pattern);
			var match = regex.Match(spec);
			if (!match.Success)
			{
				return null;
			}

			PartialVersion minPartialVersion = null;
			PartialVersion maxPartialVersion = null;

			// Parse versions from lower and upper ranges, which might
			// be partial versions.
			try
			{
				minPartialVersion = new PartialVersion(match.Groups[1].Value);
				maxPartialVersion = new PartialVersion(match.Groups[2].Value);
			}
			catch (ArgumentException)
			{
				return null;
			}

			// Lower range has any non-supplied values replaced with zero
			var minVersion = minPartialVersion.ToZeroVersion();

			SemverComparator.Operator maxOperator = maxPartialVersion.IsFull()
				? SemverComparator.Operator.LessThanOrEqual : SemverComparator.Operator.LessThan;

			SemanticVersion maxVersion = null;

			// Partial upper range means supplied version values can't change
			if (!maxPartialVersion.Major.HasValue)
			{
				// eg. upper range = "*", then maxVersion remains null
				// and there's only a minimum
			}
			else if (!maxPartialVersion.Minor.HasValue)
			{
				maxVersion = new SemanticVersion(maxPartialVersion.Major.Value + 1, 0, 0);
			}
			else if (!maxPartialVersion.Patch.HasValue)
			{
				maxVersion = new SemanticVersion(maxPartialVersion.Major.Value, maxPartialVersion.Minor.Value + 1, 0);
			}
			else
			{
				// Fully specified max version
				maxVersion = maxPartialVersion.ToZeroVersion();
			}
			return Tuple.Create(
					match.Length,
					minMaxComparators(minVersion, maxVersion, maxOperator));
		}

		public static Tuple<int, SemverComparator[]> StarRange(string spec)
		{
			// Also match with an equals sign, eg. "=0.7.x"
			string pattern = String.Format(@"^\s*=?\s*({0}+)\s*", versionChars);

			var regex = new Regex(pattern);
			var match = regex.Match(spec);

			if (!match.Success)
			{
				return null;
			}

			PartialVersion version = null;
			try
			{
				version = new PartialVersion(match.Groups[1].Value);
			}
			catch (ArgumentException)
			{
				return null;
			}

			// If partial version match is actually a full version,
			// then this isn't a star range, so return null.
			if (version.IsFull())
			{
				return null;
			}

			SemanticVersion minVersion = null;
			SemanticVersion maxVersion = null;
			if (!version.Major.HasValue)
			{
				minVersion = version.ToZeroVersion();
				// no max version
			}
			else if (!version.Minor.HasValue)
			{
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(version.Major.Value + 1, 0, 0);
			}
			else
			{
				minVersion = version.ToZeroVersion();
				maxVersion = new SemanticVersion(version.Major.Value, version.Minor.Value + 1, 0);
			}

			return Tuple.Create(
					match.Length,
					minMaxComparators(minVersion, maxVersion));
		}

		private static SemverComparator[] minMaxComparators(SemanticVersion minVersion, SemanticVersion maxVersion,
				SemverComparator.Operator maxOperator = SemverComparator.Operator.LessThan)
		{
			var minComparator = new SemverComparator(
					SemverComparator.Operator.GreaterThanOrEqual,
					minVersion);
			if (maxVersion == null)
			{
				return new[] { minComparator };
			}
			else
			{
				var maxComparator = new SemverComparator(
						maxOperator, maxVersion);
				return new[] { minComparator, maxComparator };
			}
		}
	}

	internal class ComparatorSet : IEquatable<ComparatorSet>
	{
		private readonly List<SemverComparator> _comparators;

		public ComparatorSet(string spec)
		{
			_comparators = new List<SemverComparator> { };

			spec = spec.Trim();
			if (spec == "")
			{
				spec = "*";
			}

			int position = 0;
			int end = spec.Length;

			while (position < end)
			{
				int iterStartPosition = position;

				// A comparator set might be an advanced range specifier
				// like ~1.2.3, ^1.2, or 1.*.
				// Check for these first before standard comparators:
				foreach (var desugarer in new Func<string, Tuple<int, SemverComparator[]>>[] {
						Desugarer.HyphenRange,
						Desugarer.TildeRange,
						Desugarer.CaretRange,
						Desugarer.StarRange,
						})
				{
					var result = desugarer(spec.Substring(position));
					if (result != null)
					{
						position += result.Item1;
						_comparators.AddRange(result.Item2);
					}
				}

				// Check for standard comparator with operator and version:
				var comparatorResult = SemverComparator.TryParse(spec.Substring(position));
				if (comparatorResult != null)
				{
					position += comparatorResult.Item1;
					_comparators.Add(comparatorResult.Item2);
				}

				if (position == iterStartPosition)
				{
					// Didn't manage to read any valid comparators
					throw new ArgumentException(String.Format("Invalid range specification: \"{0}\"", spec));
				}
			}
		}

		public bool IsSatisfied(SemanticVersion version)
		{
			bool satisfied = _comparators.All(c => c.IsSatisfied(version));
			if (version.PreRelease != null)
			{
				// If the version is a pre-release, then one of the
				// comparators must have the same version and also include
				// a pre-release tag.
				return satisfied && _comparators.Any(c =>
						c.Version.PreRelease != null &&
						c.Version.BaseVersion() == version.BaseVersion());
			}
			else
			{
				return satisfied;
			}
		}

		public bool Equals(ComparatorSet other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			var thisSet = new HashSet<SemverComparator>(_comparators);
			return thisSet.SetEquals(other._comparators);
		}

		public override bool Equals(object other)
		{
			return Equals(other as ComparatorSet);
		}

		public override int GetHashCode()
		{
			// XOR is commutative, so this hash code is independent
			// of the order of comparators.
			return _comparators.Aggregate(0, (accum, next) => accum ^ next.GetHashCode());
		}
	}

	/// <summary>
	/// A semantic version.
	/// </summary>
	public class SemanticVersion : IComparable<SemanticVersion>, IEquatable<SemanticVersion>
	{
		private readonly string _inputString;
		private readonly int _major;
		private readonly int _minor;
		private readonly int _patch;
		private readonly string _preRelease;
		private readonly string _build;

		/// <summary>
		/// The major component of the version.
		/// </summary>
		public int Major { get { return _major; } }

		/// <summary>
		/// The minor component of the version.
		/// </summary>
		public int Minor { get { return _minor; } }

		/// <summary>
		/// The patch component of the version.
		/// </summary>
		public int Patch { get { return _patch; } }

		/// <summary>
		/// The pre-release string, or null for no pre-release version.
		/// </summary>
		public string PreRelease { get { return _preRelease; } }

		/// <summary>
		/// The build string, or null for no build version.
		/// </summary>
		public string Build { get { return _build; } }

		private static Regex strictRegex = new Regex(@"^
            \s*v?
            (\d+)                     # major version
            \.
            (\d+)                     # minor version
            \.
            (\d+)                     # patch version
            (\-([0-9A-Za-z\-\.]+))?   # pre-release version
            (\+([0-9A-Za-z\-\.]+))?   # build metadata
            \s*
            $",
			RegexOptions.IgnorePatternWhitespace);

		private static Regex looseRegex = new Regex(@"^
            [v=\s]*
            (\d+)                     # major version
            \.
            (\d+)                     # minor version
            \.
            (\d+)                     # patch version
            (\-?([0-9A-Za-z\-\.]+))?  # pre-release version
            (\+([0-9A-Za-z\-\.]+))?   # build metadata
            \s*
            $",
			RegexOptions.IgnorePatternWhitespace);

		/// <summary>
		/// Construct a new semantic version from a version string.
		/// </summary>
		/// <param name="input">The version string.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <exception cref="System.ArgumentException">Thrown when the version string is invalid.</exception>
		public SemanticVersion(string input, bool loose = false)
		{
			_inputString = input;

			var regex = loose ? looseRegex : strictRegex;

			var match = regex.Match(input);
			if (!match.Success)
			{
				throw new ArgumentException(String.Format("Invalid version string: {0}", input));
			}

			_major = Int32.Parse(match.Groups[1].Value);

			_minor = Int32.Parse(match.Groups[2].Value);

			_patch = Int32.Parse(match.Groups[3].Value);

			if (match.Groups[4].Success)
			{
				var inputPreRelease = match.Groups[5].Value;
				var cleanedPreRelease = PreReleaseVersion.Clean(inputPreRelease);
				if (!loose && inputPreRelease != cleanedPreRelease)
				{
					throw new ArgumentException(String.Format(
								"Invalid pre-release version: {0}", inputPreRelease));
				}
				_preRelease = cleanedPreRelease;
			}

			if (match.Groups[6].Success)
			{
				_build = match.Groups[7].Value;
			}
		}

		/// <summary>
		/// Construct a new semantic version from version components.
		/// </summary>
		/// <param name="major">The major component of the version.</param>
		/// <param name="minor">The minor component of the version.</param>
		/// <param name="patch">The patch component of the version.</param>
		/// <param name="preRelease">The pre-release version string, or null for no pre-release version.</param>
		/// <param name="build">The build version string, or null for no build version.</param>
		public SemanticVersion(int major, int minor, int patch,
				string preRelease = null, string build = null)
		{
			_major = major;
			_minor = minor;
			_patch = patch;
			_preRelease = preRelease;
			_build = build;
		}

		/// <summary>
		/// Returns this version without any pre-release or build version.
		/// </summary>
		/// <returns>The base version</returns>
		public SemanticVersion BaseVersion()
		{
			return new SemanticVersion(Major, Minor, Patch);
		}

		/// <summary>
		/// Returns the original input string the version was constructed from or
		/// the cleaned version if the version was constructed from version components.
		/// </summary>
		/// <returns>The version string</returns>
		public override string ToString()
		{
			return _inputString ?? Clean();
		}

		/// <summary>
		/// Return a cleaned, normalised version string.
		/// </summary>
		/// <returns>The cleaned version string.</returns>
		public string Clean()
		{
			var preReleaseString = PreRelease == null ? ""
				: String.Format("-{0}", PreReleaseVersion.Clean(PreRelease));
			var buildString = Build == null ? ""
				: String.Format("+{0}", Build);

			return String.Format("{0}.{1}.{2}{3}{4}",
					Major, Minor, Patch, preReleaseString, buildString);
		}

		/// <summary>
		/// Calculate a hash code for the version.
		/// </summary>
		public override int GetHashCode()
		{
			// The build version isn't included when calculating the hash,
			// as two versions with equal properties except for the build
			// are considered equal.

			unchecked  // Allow integer overflow with wrapping
			{
				int hash = 17;
				hash = hash * 23 + Major.GetHashCode();
				hash = hash * 23 + Minor.GetHashCode();
				hash = hash * 23 + Patch.GetHashCode();
				if (PreRelease != null)
				{
					hash = hash * 23 + PreRelease.GetHashCode();
				}
				return hash;
			}
		}

		// Implement IEquatable<Version>
		/// <summary>
		/// Test whether two versions are semantically equivalent.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(SemanticVersion other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			return CompareTo(other) == 0;
		}

		// Implement IComparable<Version>
		public int CompareTo(SemanticVersion other)
		{
			if (ReferenceEquals(other, null))
			{
				return 1;
			}

			foreach (var c in PartComparisons(other))
			{
				if (c != 0)
				{
					return c;
				}
			}

			return PreReleaseVersion.Compare(this.PreRelease, other.PreRelease);
		}

		private IEnumerable<int> PartComparisons(SemanticVersion other)
		{
			yield return Major.CompareTo(other.Major);
			yield return Minor.CompareTo(other.Minor);
			yield return Patch.CompareTo(other.Patch);
		}

		public override bool Equals(object other)
		{
			return Equals(other as SemanticVersion);
		}

		public static bool operator ==(SemanticVersion a, SemanticVersion b)
		{
			if (ReferenceEquals(a, null))
			{
				return ReferenceEquals(b, null);
			}
			return a.Equals(b);
		}

		public static bool operator !=(SemanticVersion a, SemanticVersion b)
		{
			return !(a == b);
		}

		public static bool operator >(SemanticVersion a, SemanticVersion b)
		{
			if (ReferenceEquals(a, null))
			{
				return false;
			}
			return a.CompareTo(b) > 0;
		}

		public static bool operator >=(SemanticVersion a, SemanticVersion b)
		{
			if (ReferenceEquals(a, null))
			{
				return ReferenceEquals(b, null) ? true : false;
			}
			return a.CompareTo(b) >= 0;
		}

		public static bool operator <(SemanticVersion a, SemanticVersion b)
		{
			if (ReferenceEquals(a, null))
			{
				return ReferenceEquals(b, null) ? false : true;
			}
			return a.CompareTo(b) < 0;
		}

		public static bool operator <=(SemanticVersion a, SemanticVersion b)
		{
			if (ReferenceEquals(a, null))
			{
				return true;
			}
			return a.CompareTo(b) <= 0;
		}
	}

	/// <summary>
	/// Specifies valid versions.
	/// </summary>
	public class VersionRange : IEquatable<VersionRange>
	{
		private readonly ComparatorSet[] _comparatorSets;

		private readonly string _rangeSpec;

		/// <summary>
		/// Construct a new range from a range specification.
		/// </summary>
		/// <param name="rangeSpec">The range specification string.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <exception cref="System.ArgumentException">Thrown when the range specification is invalid.</exception>
		public VersionRange(string rangeSpec, bool loose = false)
		{
			_rangeSpec = rangeSpec;
			var comparatorSetSpecs = rangeSpec.Split(new[] { "||" }, StringSplitOptions.None);
			_comparatorSets = comparatorSetSpecs.Select(s => new ComparatorSet(s)).ToArray();
		}

		/// <summary>
		/// Determine whether the given version satisfies this range.
		/// </summary>
		/// <param name="version">The version to check.</param>
		/// <returns>true if the range is satisfied by the version.</returns>
		public bool IsSatisfied(SemanticVersion version)
		{
			return _comparatorSets.Any(s => s.IsSatisfied(version));
		}

		/// <summary>
		/// Determine whether the given version satisfies this range.
		/// With an invalid version this method returns false.
		/// </summary>
		/// <param name="versionString">The version to check.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <returns>true if the range is satisfied by the version.</returns>
		public bool IsSatisfied(string versionString, bool loose = false)
		{
			try
			{
				var version = new SemanticVersion(versionString, loose);
				return IsSatisfied(version);
			}
			catch (ArgumentException)
			{
				return false;
			}
		}

		/// <summary>
		/// Return the set of versions that satisfy this range.
		/// </summary>
		/// <param name="versions">The versions to check.</param>
		/// <returns>An IEnumerable of satisfying versions.</returns>
		public IEnumerable<SemanticVersion> Satisfying(IEnumerable<SemanticVersion> versions)
		{
			return versions.Where(IsSatisfied);
		}

		/// <summary>
		/// Return the set of version strings that satisfy this range.
		/// Invalid version specifications are skipped.
		/// </summary>
		/// <param name="versions">The version strings to check.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <returns>An IEnumerable of satisfying version strings.</returns>
		public IEnumerable<string> Satisfying(IEnumerable<string> versions, bool loose = false)
		{
			return versions.Where(v => IsSatisfied(v, loose));
		}

		/// <summary>
		/// Return the maximum version that satisfies this range.
		/// </summary>
		/// <param name="versions">The versions to select from.</param>
		/// <returns>The maximum satisfying version, or null if no versions satisfied this range.</returns>
		public SemanticVersion MaxSatisfying(IEnumerable<SemanticVersion> versions)
		{
			return Satisfying(versions).Max();
		}

		/// <summary>
		/// Return the maximum version that satisfies this range.
		/// </summary>
		/// <param name="versionStrings">The version strings to select from.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <returns>The maximum satisfying version string, or null if no versions satisfied this range.</returns>
		public string MaxSatisfying(IEnumerable<string> versionStrings, bool loose = false)
		{
			var versions = ValidVersions(versionStrings, loose);
			var maxVersion = MaxSatisfying(versions);
			return maxVersion == null ? null : maxVersion.ToString();
		}

		/// <summary>
		/// Returns the range specification string used when constructing this range.
		/// </summary>
		/// <returns>The range string</returns>
		public override string ToString()
		{
			return _rangeSpec;
		}

		public bool Equals(VersionRange other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			var thisSet = new HashSet<ComparatorSet>(_comparatorSets);
			return thisSet.SetEquals(other._comparatorSets);
		}

		public override bool Equals(object other)
		{
			return Equals(other as VersionRange);
		}

		public override int GetHashCode()
		{
			// XOR is commutative, so this hash code is independent
			// of the order of comparators.
			return _comparatorSets.Aggregate(0, (accum, next) => accum ^ next.GetHashCode());
		}

		// Static convenience methods

		/// <summary>
		/// Determine whether the given version satisfies a given range.
		/// With an invalid version this method returns false.
		/// </summary>
		/// <param name="rangeSpec">The range specification.</param>
		/// <param name="versionString">The version to check.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <returns>true if the range is satisfied by the version.</returns>
		public static bool IsSatisfied(string rangeSpec, string versionString, bool loose = false)
		{
			var range = new VersionRange(rangeSpec);
			return range.IsSatisfied(versionString);
		}

		/// <summary>
		/// Return the set of version strings that satisfy a given range.
		/// Invalid version specifications are skipped.
		/// </summary>
		/// <param name="rangeSpec">The range specification.</param>
		/// <param name="versions">The version strings to check.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <returns>An IEnumerable of satisfying version strings.</returns>
		public static IEnumerable<string> Satisfying(string rangeSpec, IEnumerable<string> versions, bool loose = false)
		{
			var range = new VersionRange(rangeSpec);
			return range.Satisfying(versions);
		}

		/// <summary>
		/// Return the maximum version that satisfies a given range.
		/// </summary>
		/// <param name="rangeSpec">The range specification.</param>
		/// <param name="versionStrings">The version strings to select from.</param>
		/// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
		/// <returns>The maximum satisfying version string, or null if no versions satisfied this range.</returns>
		public static string MaxSatisfying(string rangeSpec, IEnumerable<string> versionStrings, bool loose = false)
		{
			var range = new VersionRange(rangeSpec);
			return range.MaxSatisfying(versionStrings);
		}

		private IEnumerable<SemanticVersion> ValidVersions(IEnumerable<string> versionStrings, bool loose)
		{
			foreach (var v in versionStrings)
			{
				SemanticVersion version = null;

				try
				{
					version = new SemanticVersion(v, loose);
				}
				catch (ArgumentException) { } // Skip

				if (version != null)
				{
					yield return version;
				}
			}
		}
	}
}
