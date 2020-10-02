// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Tests.Core.Extensions
{
	public static class ReadOnlyDictionaryShouldExtensions
	{
		public static ReadOnlyDictionaryAssertions<TKey, TValue> Should<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> actualValue) =>
			new ReadOnlyDictionaryAssertions<TKey, TValue>(actualValue);

		public static GenericDictionaryAssertions<TKey, TValue> Should<TKey, TValue>(this Dictionary<TKey, TValue> actualValue) =>
			new GenericDictionaryAssertions<TKey, TValue>(actualValue);
	}

	public class WhichValueConstraint<TKey, TValue> : AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>
	{
		public WhichValueConstraint(ReadOnlyDictionaryAssertions<TKey, TValue> parentConstraint, TValue value)
			: base(parentConstraint) => WhichValue = value;

		/// <summary>Gets the value of the object referred to by the key.</summary>
		public TValue WhichValue { get; private set; }
	}

	/// <summary>
	/// Contains a number of methods to assert that an <see cref="IDictionary{TKey,TValue}" /> is in the expected state.
	/// </summary>
	[DebuggerNonUserCode]
	public class ReadOnlyDictionaryAssertions<TKey, TValue>
		: ReferenceTypeAssertions<IReadOnlyDictionary<TKey, TValue>, ReadOnlyDictionaryAssertions<TKey, TValue>>
	{
		public ReadOnlyDictionaryAssertions(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary != null) Subject = dictionary;
		}

		protected override string Identifier => "dictionary";

		/// <summary>
		/// Asserts that the number of items in the dictionary matches the supplied <paramref name="expected" /> amount.
		/// </summary>
		/// <param name="expected">The expected number of items.</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> HaveCount(int expected,
			string because = "", params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {0} item(s){reason}, but found {1}.", expected, Subject);

			var actualCount = Subject.Count;

			Execute.Assertion
				.ForCondition(actualCount == expected)
				.BecauseOf(because, becauseArgs)
				.FailWith("Expected {context:dictionary} {0} to have {1} item(s){reason}, but found {2}.", Subject, expected, actualCount);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the number of items in the dictionary matches a condition stated by a predicate.
		/// </summary>
		/// <param name="countPredicate">The predicate which must be satisfied by the amount of items.</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> HaveCount(Expression<Func<int, bool>> countPredicate,
			string because = "", params object[] becauseArgs
		)
		{
			if (countPredicate == null) throw new NullReferenceException("Cannot compare dictionary count against a <null> predicate.");

			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to have {0} items{reason}, but found {1}.", countPredicate.Body, Subject);

			var compiledPredicate = countPredicate.Compile();

			var actualCount = Subject.Count;

			if (!compiledPredicate(actualCount))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} {0} to have a count {1}{reason}, but count is {2}.",
						Subject, countPredicate.Body, actualCount);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the dictionary does not contain any items.
		/// </summary>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> BeEmpty(string because = "", params object[] becauseArgs)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to be empty{reason}, but found {0}.", Subject);

			Execute.Assertion
				.ForCondition(!Subject.Any())
				.BecauseOf(because, becauseArgs)
				.FailWith("Expected {context:dictionary} to not have any items{reason}, but found {0}.", Subject.Count);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the dictionary contains at least 1 item.
		/// </summary>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotBeEmpty(string because = "",
			params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} not to be empty{reason}, but found {0}.", Subject);

			Execute.Assertion
				.ForCondition(Subject.Any())
				.BecauseOf(because, becauseArgs)
				.FailWith("Expected one or more items{reason}, but found none.");

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the current dictionary contains all the same key-value pairs as the
		/// specified <paramref name="expected" /> dictionary. Keys and values are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected dictionary</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> Equal(IReadOnlyDictionary<TKey, TValue> expected,
			string because = "", params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but found {1}.", expected, Subject);

			if (expected == null) throw new ArgumentNullException("expected", "Cannot compare dictionary with <null>.");

			var missingKeys = expected.Keys.Except(Subject.Keys);
			var additionalKeys = Subject.Keys.Except(expected.Keys);

			if (missingKeys.Any())
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but could not find keys {1}.", expected,
						missingKeys);

			if (additionalKeys.Any())
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but found additional keys {1}.", expected,
						additionalKeys);

			foreach (var key in expected.Keys)
				Execute.Assertion
					.ForCondition(Subject[key].IsSameOrEqualTo(expected[key]))
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but {1} differs at key {2}.",
						expected, Subject, key);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts the current dictionary not to contain all the same key-value pairs as the
		/// specified <paramref name="unexpected" /> dictionary. Keys and values are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="unexpected">The unexpected dictionary</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotEqual(IReadOnlyDictionary<TKey, TValue> unexpected,
			string because = "", params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected dictionaries not to be equal{reason}, but found {0}.", Subject);

			if (unexpected == null) throw new ArgumentNullException("unexpected", "Cannot compare dictionary with <null>.");

			var missingKeys = unexpected.Keys.Except(Subject.Keys);
			var additionalKeys = Subject.Keys.Except(unexpected.Keys);

			var foundDifference = missingKeys.Any()
				|| additionalKeys.Any()
				|| Subject.Keys.Any(key => !Subject[key].IsSameOrEqualTo(unexpected[key]));

			if (!foundDifference)
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Did not expect dictionaries {0} and {1} to be equal{reason}.", unexpected, Subject);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the dictionary contains the specified key. Keys are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected key</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public WhichValueConstraint<TKey, TValue> ContainKey(TKey expected,
			string because = "", params object[] becauseArgs
		)
		{
			var andConstraint = ContainKeys(new[] { expected }, because, becauseArgs);

			return new WhichValueConstraint<TKey, TValue>(andConstraint.And, Subject[expected]);
		}

		/// <summary>
		/// Asserts that the dictionary contains all of the specified keys. Keys are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected keys</param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> ContainKeys(params TKey[] expected) => ContainKeys(expected, string.Empty);

		/// <summary>
		/// Asserts that the dictionary contains all of the specified keys. Keys are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected keys</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> ContainKeys(IEnumerable<TKey> expected,
			string because = "", params object[] becauseArgs
		)
		{
			if (expected == null) throw new NullReferenceException("Cannot verify key containment against a <null> collection of keys");

			var enumerable = expected as TKey[] ?? expected.ToArray();
			var expectedKeys = enumerable.ToArray();

			if (!expectedKeys.Any()) throw new ArgumentException("Cannot verify key containment against an empty sequence");

			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to contain keys {0}{reason}, but found {1}.", expected, Subject);

			var missingKeys = expectedKeys.Where(key => !Subject.ContainsKey(key));

			if (missingKeys.Any())
			{
				if (expectedKeys.Count() > 1)
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to contain key {1}{reason}, but could not find {2}.", Subject,
							expected, missingKeys);
				else
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to contain key {1}{reason}.", Subject,
							enumerable.Cast<object>().First());
			}

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the current dictionary does not contain the specified <paramref name="unexpected" /> key.
		/// Keys are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="unexpected">The unexpected key</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContainKey(TKey unexpected,
			string because = "", params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} not to contain key {0}{reason}, but found {1}.", unexpected, Subject);

			if (Subject.ContainsKey(unexpected))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("{context:Dictionary} {0} should not contain key {1}{reason}, but found it anyhow.", Subject, unexpected);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the dictionary does not contain any of the specified keys. Keys are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="unexpected">The unexpected keys</param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContainKeys(params TKey[] unexpected) =>
			NotContainKeys(unexpected, string.Empty);

		/// <summary>
		/// Asserts that the dictionary does not contain any of the specified keys. Keys are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="unexpected">The unexpected keys</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContainKeys(IEnumerable<TKey> unexpected,
			string because = "", params object[] becauseArgs
		)
		{
			if (unexpected == null) throw new NullReferenceException("Cannot verify key containment against a <null> collection of keys");

			var enumerable = unexpected as TKey[] ?? unexpected.ToArray();
			var unexpectedKeys = enumerable.ToArray();

			if (!unexpectedKeys.Any()) throw new ArgumentException("Cannot verify key containment against an empty sequence");

			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to contain keys {0}{reason}, but found {1}.", unexpected, Subject);

			var foundKeys = unexpectedKeys.Intersect(Subject.Keys);

			if (foundKeys.Any())
			{
				if (unexpectedKeys.Count() > 1)
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to not contain key {1}{reason}, but found {2}.", Subject,
							unexpected, foundKeys);
				else
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to not contain key {1}{reason}.", Subject,
							enumerable.Cast<object>().First());
			}

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the dictionary contains the specified value. Values are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndWhichConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>, TValue> ContainValue(TValue expected,
			string because = "", params object[] becauseArgs
		)
		{
			var innerConstraint =
				ContainValuesAndWhich(new[] { expected }, because, becauseArgs);

			return
				new AndWhichConstraint
					<ReadOnlyDictionaryAssertions<TKey, TValue>, TValue>(
						innerConstraint.And, innerConstraint.Which);
		}

		/// <summary>
		/// Asserts that the dictionary contains all of the specified values. Values are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected values</param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> ContainValues(params TValue[] expected) =>
			ContainValues(expected, string.Empty);

		/// <summary>
		/// Asserts that the dictionary contains all of the specified values. Values are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected values</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> ContainValues(IEnumerable<TValue> expected,
			string because = "", params object[] becauseArgs
		) => ContainValuesAndWhich(expected, because, becauseArgs);

		private AndWhichConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>, IEnumerable<TValue>> ContainValuesAndWhich(
			IEnumerable<TValue> expected, string because = "",
			params object[] becauseArgs
		)
		{
			if (expected == null) throw new NullReferenceException("Cannot verify value containment against a <null> collection of values");

			var enumerable = expected as TValue[] ?? expected.ToArray();
			var expectedValues = enumerable.ToArray();

			if (!expectedValues.Any()) throw new ArgumentException("Cannot verify value containment with an empty sequence");

			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to contain value {0}{reason}, but found {1}.", expected, Subject);

			var missingValues = expectedValues.Except(Subject.Values);
			if (missingValues.Any())
			{
				if (expectedValues.Length > 1)
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to contain value {1}{reason}, but could not find {2}.", Subject,
							expected, missingValues);
				else
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to contain value {1}{reason}.", Subject,
							enumerable.Cast<object>().First());
			}

			return
				new AndWhichConstraint
				<ReadOnlyDictionaryAssertions<TKey, TValue>,
					IEnumerable<TValue>>(this,
					RepetitionPreservingIntersect(Subject.Values, expectedValues));
		}

		/// <summary>
		/// Returns an enumerable consisting of all items in the first collection also appearing in the second.
		/// </summary>
		/// <remarks>Enumerable.Intersect is not suitable because it drops any repeated elements.</remarks>
		private IEnumerable<TValue> RepetitionPreservingIntersect(
			IEnumerable<TValue> first, IEnumerable<TValue> second
		)
		{
			var secondSet = new HashSet<TValue>(second);
			return first.Where(secondSet.Contains);
		}

		/// <summary>
		/// Asserts that the current dictionary does not contain the specified <paramref name="unexpected" /> value.
		/// Values are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="unexpected">The unexpected value</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContainValue(TValue unexpected,
			string because = "", params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} not to contain value {0}{reason}, but found {1}.", unexpected, Subject);

			if (Subject.Values.Contains(unexpected))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("{context:Dictionary} {0} should not contain value {1}{reason}, but found it anyhow.", Subject, unexpected);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the dictionary does not contain any of the specified values. Values are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="unexpected">The unexpected values</param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContainValues(params TValue[] unexpected) =>
			NotContainValues(unexpected, string.Empty);

		/// <summary>
		/// Asserts that the dictionary does not contain any of the specified values. Values are compared using
		/// their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="unexpected">The unexpected values</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContainValues(IEnumerable<TValue> unexpected,
			string because = "", params object[] becauseArgs
		)
		{
			if (unexpected == null) throw new NullReferenceException("Cannot verify value containment against a <null> collection of values");

			var enumerable = unexpected as TValue[] ?? unexpected.ToArray();
			var unexpectedValues = enumerable.ToArray();

			if (!unexpectedValues.Any()) throw new ArgumentException("Cannot verify value containment with an empty sequence");

			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to not contain value {0}{reason}, but found {1}.", unexpected, Subject);

			var foundValues = unexpectedValues.Intersect(Subject!.Values);
			if (foundValues.Any())
			{
				if (unexpectedValues.Length > 1)
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to not contain value {1}{reason}, but found {2}.", Subject,
							unexpected, foundValues);
				else
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to not contain value {1}{reason}.", Subject,
							enumerable.Cast<object>().First());
			}

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the current dictionary contains the specified <paramref name="expected" />.
		/// Keys and values are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected key/value pairs.</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> Contain(IEnumerable<KeyValuePair<TKey, TValue>> expected,
			string because = "", params object[] becauseArgs
		)
		{
			if (expected == null) throw new ArgumentNullException("expected", "Cannot compare dictionary with <null>.");

			var expectedKeyValuePairs = expected.ToArray();

			if (!expectedKeyValuePairs.Any())
				throw new ArgumentException("Cannot verify key containment against an empty collection of key/value pairs");

			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to contain key/value pairs {0}{reason}, but dictionary is {1}.", expected, Subject);

			var expectedKeys = expectedKeyValuePairs.Select(keyValuePair => keyValuePair.Key).ToArray();
			var missingKeys = expectedKeys.Where(key => !Subject.ContainsKey(key));

			if (missingKeys.Any())
			{
				if (expectedKeyValuePairs.Count() > 1)
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to contain key(s) {1}{reason}, but could not find keys {2}.", Subject,
							expectedKeys, missingKeys);
				else
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} {0} to contain key {1}{reason}.", Subject,
							expectedKeys.Cast<object>().First());
			}

			var keyValuePairsNotSameOrEqualInSubject = expectedKeyValuePairs
				.Where(keyValuePair => !Subject[keyValuePair.Key].IsSameOrEqualTo(keyValuePair.Value))
				.ToArray();

			if (keyValuePairsNotSameOrEqualInSubject.Any())
			{
				if (keyValuePairsNotSameOrEqualInSubject.Count() > 1)
					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} to contain {0}{reason}, but {context:dictionary} differs at keys {1}.",
							expectedKeyValuePairs, keyValuePairsNotSameOrEqualInSubject.Select(keyValuePair => keyValuePair.Key));
				else
				{
					var expectedKeyValuePair = keyValuePairsNotSameOrEqualInSubject.First();
					var actual = Subject![expectedKeyValuePair.Key];

					Execute.Assertion
						.BecauseOf(because, becauseArgs)
						.FailWith("Expected {context:dictionary} to contain value {0} at key {1}{reason}, but found {2}.", expectedKeyValuePair.Value,
							expectedKeyValuePair.Key, actual);
				}
			}

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the current dictionary contains the specified <paramref name="expected" />.
		/// Keys and values are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="expected">The expected <see cref="KeyValuePair{TKey,TValue}" /></param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> Contain(KeyValuePair<TKey, TValue> expected,
			string because = "", params object[] becauseArgs
		) => Contain(expected.Key, expected.Value, because, becauseArgs);

		/// <summary>
		/// Asserts that the current dictionary contains the specified <paramref name="value" /> for the supplied
		/// <paramref
		///     name="key" />
		/// . Values are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="key">The key for which to validate the value</param>
		/// <param name="value">The value to validate</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> Contain(TKey key, TValue value,
			string because = "", params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to contain value {0} at key {1}{reason}, but dictionary is {2}.", value, key,
						Subject);

			if (Subject.ContainsKey(key))
			{
				var actual = Subject[key];

				Execute.Assertion
					.ForCondition(actual.IsSameOrEqualTo(value))
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to contain value {0} at key {1}{reason}, but found {2}.", value, key, actual);
			}
			else
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to contain value {0} at key {1}{reason}, but the key was not found.", value,
						key);

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the current dictionary does not contain the specified <paramref name="items" />.
		/// Keys and values are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="items">The unexpected key/value pairs</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContain(IEnumerable<KeyValuePair<TKey, TValue>> items,
			string because = "", params object[] becauseArgs
		)
		{
			if (items == null) throw new ArgumentNullException("items", "Cannot compare dictionary with <null>.");

			var keyValuePairs = items.ToArray();

			if (!keyValuePairs.Any()) throw new ArgumentException("Cannot verify key containment against an empty collection of key/value pairs");

			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} to not contain key/value pairs {0}{reason}, but dictionary is {1}.", items, Subject);

			var keyValuePairsFound = keyValuePairs.Where(keyValuePair => Subject.ContainsKey(keyValuePair.Key)).ToArray();

			if (keyValuePairsFound.Any())
			{
				var keyValuePairsSameOrEqualInSubject =
					keyValuePairsFound.Where(keyValuePair => Subject[keyValuePair.Key].IsSameOrEqualTo(keyValuePair.Value)).ToArray();

				if (keyValuePairsSameOrEqualInSubject.Any())
				{
					if (keyValuePairsSameOrEqualInSubject.Count() > 1)
						Execute.Assertion
							.BecauseOf(because, becauseArgs)
							.FailWith("Expected {context:dictionary} to not contain key/value pairs {0}{reason}, but found them anyhow.",
								keyValuePairs);
					else
					{
						var keyValuePair = keyValuePairsSameOrEqualInSubject.First();

						Execute.Assertion
							.BecauseOf(because, becauseArgs)
							.FailWith("Expected {context:dictionary} to not contain value {0} at key {1}{reason}, but found it anyhow.",
								keyValuePair.Value, keyValuePair.Key);
					}
				}
			}

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}

		/// <summary>
		/// Asserts that the current dictionary does not contain the specified <paramref name="item" />.
		/// Keys and values are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="item">The unexpected <see cref="KeyValuePair{TKey,TValue}" /></param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContain(KeyValuePair<TKey, TValue> item,
			string because = "", params object[] becauseArgs
		) => NotContain(item.Key, item.Value, because, becauseArgs);

		/// <summary>
		/// Asserts that the current dictionary does not contain the specified <paramref name="value" /> for the
		/// supplied <paramref name="key" />. Values are compared using their <see cref="object.Equals(object)" /> implementation.
		/// </summary>
		/// <param name="key">The key for which to validate the value</param>
		/// <param name="value">The value to validate</param>
		/// <param name="because">
		/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
		/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
		/// </param>
		/// <param name="becauseArgs">
		/// Zero or more objects to format using the placeholders in <see cref="because" />.
		/// </param>
		public AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>> NotContain(TKey key, TValue value,
			string because = "", params object[] becauseArgs
		)
		{
			if (ReferenceEquals(Subject, null))
				Execute.Assertion
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} not to contain value {0} at key {1}{reason}, but dictionary is {2}.", value,
						key, Subject);

			if (Subject!.ContainsKey(key))
			{
				var actual = Subject[key];

				Execute.Assertion
					.ForCondition(!actual.IsSameOrEqualTo(value))
					.BecauseOf(because, becauseArgs)
					.FailWith("Expected {context:dictionary} not to contain value {0} at key {1}{reason}, but found it anyhow.", value, key);
			}

			return new AndConstraint<ReadOnlyDictionaryAssertions<TKey, TValue>>(this);
		}
	}
}
