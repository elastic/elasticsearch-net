using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Common;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit.Abstractions;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Caching
{
	public class FieldResolverCacheTests
	{
		public class TestableFieldResolver : FieldResolver
		{
			public TestableFieldResolver(IConnectionSettingsValues settings) : base(settings) { }

			public long CachedFields => Fields.Count;
			public long CachedProperties => Properties.Count;
		}

		private class TestFieldDocument
		{
			public Project ProjectA { get; set; }
			public Project ProjectB { get; set; }
		}

		private class FirstTestFieldDocument
		{
			public Project Project { get; set; }
		}

		private class SecondTestFieldDocument
		{
			public Project Project { get; set; }
		}

		public class Fields
		{
			[U]
			public void ExpressionEquality()
			{
				Field first = Field<Project>(p => p.Name);
				Field second = Field<Project>(p => p.Name);

				first.Should().Be(second);
			}

			[U]
			public void ExpressionEqualityWithDifferentParams()
			{
				Field first = Field<Project>(p => p.Name);
				Field second = Field<Project>(project => project.Name);

				first.Should().Be(second);
			}

			[U]
			public void PropertyInfoEquality()
			{
				Field first = typeof(Project).GetProperty(nameof(Project.Name));
				Field second = typeof(Project).GetProperty(nameof(Project.Name));

				first.Should().Be(second);
			}

			[U]
			public void StringEquality()
			{
				Field first = "Name";
				Field second = "Name";

				first.Should().Be(second);
			}

			[U]
			public void StringInequality()
			{
				Field first = "Name";
				Field second = "name";

				first.Should().NotBe(second);
			}

			[U]
			public void Expression()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<Project>(p => p.Name));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<Project>(p => p.Name));
				resolver.CachedFields.Should().Be(1);
				// Boost values should have no bearing on cached field names
				resolver.Resolve(Field<Project>(p => p.Name, 1.1));
				resolver.CachedFields.Should().Be(1);
			}

			[U]
			public void ExpressionWithDifferentParameters()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<Project>(p => p.Name));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<Project>(d => d.Name));
				resolver.CachedFields.Should().Be(1);
				// Boost values should have no bearing on cached field names
				resolver.Resolve(Field<Project>(e => e.Name, 1.1));
				resolver.CachedFields.Should().Be(1);
			}

			[U]
			public void ExpressionWithSuffix()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<Project>(p => p.Name));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<Project>(p => p.Name));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix("raw")));
				resolver.CachedFields.Should().Be(2);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix("raw"), 1.1));
				resolver.CachedFields.Should().Be(2);
			}

			[U]
			public void ExpressionWithConstantSuffix()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix("raw")));
				resolver.CachedFields.Should().Be(1);
				resolved.Should().EndWith("raw");
				resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix("foo")));
				resolver.CachedFields.Should().Be(2);
				resolved.Should().EndWith("foo");
			}

			[U]
			public void ExpressionWithVariableSuffix()
			{
				var suffix = "raw";
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix(suffix)));
				resolved.Should().EndWith("raw");
				resolver.CachedFields.Should().Be(0);
				suffix = "foo";
				resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix(suffix)));
				resolved.Should().EndWith("foo");
				resolver.CachedFields.Should().Be(0);
			}

			[U]
			public void ExpressionWithDictionaryItemVariableExpression()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var key = "key1";
				var resolved = resolver.Resolve(Field<Project>(p => p.Metadata[key]));
				resolver.CachedFields.Should().Be(0);
				resolved.Should().Contain(key);
				key = "key2";
				resolved = resolver.Resolve(Field<Project>(p => p.Metadata[key]));
				resolver.CachedFields.Should().Be(0);
				resolved.Should().Contain(key);
			}

			[U] public void ExpressionWithDictionaryItemConstantExpression()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var resolved = resolver.Resolve(Field<Project>(p => p.Metadata["key1"]));
				resolver.CachedFields.Should().Be(1);
				resolved.Should().Contain("key1");
				resolved = resolver.Resolve(Field<Project>(p => p.Metadata["key2"]));
				resolver.CachedFields.Should().Be(2);
				resolved.Should().Contain("key2");
			}

			[U] public void ExpressionWithDictionaryItemConstantExpressionAndVariableSuffix()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var suffix = "x";
				var resolved = resolver.Resolve(Field<Project>(p => p.Metadata["key1"].Suffix(suffix)));
				resolver.CachedFields.Should().Be(0);
				resolved.Should().Contain("key1").And.EndWith(".x");
				suffix = "y";
				resolved = resolver.Resolve(Field<Project>(p => p.Metadata["key2"].Suffix(suffix)));
				resolver.CachedFields.Should().Be(0);
				resolved.Should().Contain("key2").And.EndWith(".y");
			}

			[U]
			public void ExpressionWithDictionarySuffix()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var key = "key1";
				var d = new Dictionary<string, string> { { "key1", "raw" }, { "key2", "foo" } };
				var resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix(d[key])));
				resolved.Should().EndWith("raw");
				resolver.CachedFields.Should().Be(0);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d["key1"])));
				resolver.CachedFields.Should().Be(0);

				key = "key2";
				resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix(d[key])));
				resolved.Should().EndWith("foo");
				resolver.CachedFields.Should().Be(0);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d["key2"])));
				resolver.CachedFields.Should().Be(0);
			}

			[U]
			public void EquivalentExpressionsOnDifferentTypes()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<CommitActivity>(c => c.Id));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<Person>(p => p.Id));
				resolver.CachedFields.Should().Be(2);
			}

			[U]
			public void String()
			{
				// Explicit strings are not cached since they are returned directly by the resolver
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field("name"));
				resolver.CachedFields.Should().Be(0);
			}

			[U]
			public void PropertyInfo()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve((Field)typeof(Project).GetProperty(nameof(Project.Name)));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve((Field)typeof(Project).GetProperty(nameof(Project.Name)));
				resolver.CachedFields.Should().Be(1);
			}

			[U]
			public void EquivalentPropertiesOnDifferentTypes()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve((Field)typeof(Person).GetProperty(nameof(Person.Id)));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve((Field)typeof(CommitActivity).GetProperty(nameof(CommitActivity.Id)));
				resolver.CachedFields.Should().Be(2);
			}

			[U]
			public void SamePropertyTypesOnDifferentTypes()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<FirstTestFieldDocument>(c => c.Project.Name));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<SecondTestFieldDocument>(c => c.Project.Name));
				resolver.CachedFields.Should().Be(2);
			}

			[U]
			public void SamePropertyTypesWithDifferentNames()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<TestFieldDocument>(c => c.ProjectA));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<TestFieldDocument>(c => c.ProjectB));
				resolver.CachedFields.Should().Be(2);
			}
		}

		public class PropertyNames
		{
			[U]
			public void ExpressionEquality()
			{
				PropertyName first = Property<Project>(p => p.Name);
				PropertyName second = Property<Project>(p => p.Name);

				first.Should().Be(second);
			}

			[U]
			public void ExpressionEqualityWithDifferentParameters()
			{
				PropertyName first = Property<Project>(p => p.Name);
				PropertyName second = Property<Project>(project => project.Name);

				first.Should().Be(second);
			}

			[U]
			public void PropertyInfoEquality()
			{
				PropertyName first = typeof(Project).GetProperty(nameof(Project.Name));
				PropertyName second = typeof(Project).GetProperty(nameof(Project.Name));

				first.Should().Be(second);
			}

			[U]
			public void PropertyInfoInequality()
			{
				PropertyName first = typeof(Project).GetProperty(nameof(Project.Name));
				PropertyName second = typeof(Project).GetProperty(nameof(Project.NumberOfCommits));

				first.Should().NotBe(second);
			}
			[U]
			public void StringEquality()
			{
				PropertyName first = "Name";
				PropertyName second = "Name";

				first.Should().Be(second);
			}

			[U]
			public void Expression()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Property<Project>(p => p.Name));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve(Property<Project>(p => p.Name));
				resolver.CachedProperties.Should().Be(1);
			}

			[U]
			public void ExpressionWithDifferentParameter()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Property<Project>(p => p.Name));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve(Property<Project>(d => d.Name));
				resolver.CachedProperties.Should().Be(1);
			}

			[U]
			public void EquivalentExpressionsOnDifferentTypes()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Property<CommitActivity>(c => c.Id));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve(Property<Person>(c => c.Id));
				resolver.CachedProperties.Should().Be(2);
			}

			[U]
			public void String()
			{
				// Explicit strings are not cached since they are returned directly by the resolver
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Property("name"));
				resolver.CachedProperties.Should().Be(0);
			}

			[U]
			public void PropertyInfo()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve((PropertyName)typeof(Project).GetProperty(nameof(Project.Name)));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve((PropertyName)typeof(Project).GetProperty(nameof(Project.Name)));
				resolver.CachedProperties.Should().Be(1);
			}

			[U]
			public void EquivalentPropertiesOnDifferentTypes()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve((PropertyName)typeof(Person).GetProperty(nameof(Person.Id)));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve((PropertyName)typeof(CommitActivity).GetProperty(nameof(CommitActivity.Id)));
				resolver.CachedProperties.Should().Be(2);
			}

			[U]
			public void SamePropertyTypesOnDifferentTypes()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Property<FirstTestFieldDocument>(c => c.Project.Name));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve(Property<SecondTestFieldDocument>(c => c.Project.Name));
				resolver.CachedProperties.Should().Be(2);
			}

			[U]
			public void SamePropertyTypesWithDifferentNames()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Property<TestFieldDocument>(c => c.ProjectA));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve(Property<TestFieldDocument>(c => c.ProjectB));
				resolver.CachedProperties.Should().Be(2);
			}
		}

		public class CachePerformance
		{
			private readonly ITestOutputHelper output;

			public CachePerformance(ITestOutputHelper output)
			{
				this.output = output;
			}

			public class HitTiming
			{
				public string Name { get; set; }
				public Func<Field> Field { get; set; }
				public double FirstHit { get; set; }
				public double CachedHit { get; set; }

				public override string ToString() => $"First hit for {Name} took {FirstHit}ms, Cached hit took {CachedHit}ms ({FirstHit / CachedHit}x faster).";
			}

			private readonly List<HitTiming> _timings = new List<HitTiming>();
			private Stopwatch _stopwatch;
			private FieldResolver _resolver;

			[U]
			public void CachedVsNonCached()
			{
				_resolver = new FieldResolver(new ConnectionSettings());

				AddTiming(()=>Field<Project>(p => p.Metadata["fixed"]));
				var x = "dynamic";
				AddTiming(()=>Field<Project>(p => p.Metadata[x]));
				AddTiming(()=>Field<Project>(p => p.Name));
				AddTiming(()=>Field<Project>(p => p.Description));
				AddTiming(()=>Field<Project>(p => p.NumberOfCommits));
				AddTiming(()=>Field<Project>(p => p.LastActivity));
				AddTiming(()=>Field<Project>(p => p.LeadDeveloper));
				AddTiming(()=>Field<Project>(p => p.Metadata));
				AddTiming(()=>Field<Project>(p => p.Tags));
				AddTiming(()=>Field<Project>(p => p.CuratedTags));

				AddTiming(()=>Field<CommitActivity>(p => p.Id));
				AddTiming(()=>Field<CommitActivity>(p => p.Message));
				AddTiming(()=>Field<CommitActivity>(p => p.ProjectName));
				AddTiming(()=>Field<CommitActivity>(p => p.StringDuration));

				output.WriteLine(_timings.Aggregate(new StringBuilder().AppendLine(), (sb, s) => sb.AppendLine(s.ToString()), sb => sb.ToString()));
			}

			private void AddTiming(Func<Field> field)
			{
				var timing = new HitTiming { Field = field };
				_timings.Add(timing);

				_stopwatch = Stopwatch.StartNew();

				timing.Name = _resolver.Resolve(field());
				timing.FirstHit = _stopwatch.Elapsed.TotalMilliseconds;

				_stopwatch.Restart();

				_resolver.Resolve(field());
				timing.CachedHit = _stopwatch.Elapsed.TotalMilliseconds;

				_stopwatch.Stop();
			}
		}
	}
}
