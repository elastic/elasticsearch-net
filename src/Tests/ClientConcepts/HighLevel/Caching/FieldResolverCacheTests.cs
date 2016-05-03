using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
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

		public class Fields
		{
			[U]
			public void Expression()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<Project>(p => p.Name));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<Project>(p => p.Name));
				resolver.CachedFields.Should().Be(1);
				// Boost values should have no baring on cached field names
				resolver.Resolve(Field<Project>(p => p.Name, 1.1));
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
			public void ExpressionWithVariableSuffix()
			{
				var suffix = "raw";
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(suffix)));
				resolver.CachedFields.Should().Be(1);
				suffix = "foo";
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(suffix)));
				resolver.CachedFields.Should().Be(2);
			}

			[U]
			public void ExpressionWithDictionarySuffix()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var key = "key1";
				var d = new Dictionary<string, string> { { "key1", "raw" }, { "key2", "foo" } };
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d[key])));
				resolver.CachedFields.Should().Be(1);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d["key1"])));
				resolver.CachedFields.Should().Be(1);
				key = "key2";
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d[key])));
				resolver.CachedFields.Should().Be(2);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d["key2"])));
				resolver.CachedFields.Should().Be(2);
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
				resolver.Resolve(new Field(typeof(Project).GetProperty(nameof(Project.Name)), 1.1));
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
		}

		public class PropertyNames
		{
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
			public void EquivalentExpressionsOnDifferentTypes()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				resolver.Resolve(Property<CommitActivity>(c => c.Id));
				resolver.CachedProperties.Should().Be(1);
				resolver.Resolve(Property<Person>(p => p.Id));
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
		}

		public class CachePerformance
		{
			public class HitTiming
			{
				public Field Field { get; set; }
				public double FirstHit { get; set; }
				public double CachedHit { get; set; }

				public override string ToString() => $"First hit took {FirstHit}ms, Cached hit took {CachedHit}ms ({FirstHit / CachedHit}x faster).";
			}

			private List<HitTiming> _timings = new List<HitTiming>();
			private Stopwatch _stopwatch;
			private FieldResolver _resolver;

			[U]
			public void CachedVsNonCached()
			{

				_resolver = new FieldResolver(new ConnectionSettings());
				_stopwatch = Stopwatch.StartNew();

				AddTiming(Field<Project>(p => p.Name));
				AddTiming(Field<Project>(p => p.Description));
				AddTiming(Field<Project>(p => p.NumberOfCommits));
				AddTiming(Field<Project>(p => p.LastActivity));
				AddTiming(Field<Project>(p => p.LeadDeveloper));
				AddTiming(Field<Project>(p => p.Metadata));
				AddTiming(Field<Project>(p => p.Tags));
				AddTiming(Field<Project>(p => p.CuratedTags));

				AddTiming(Field<CommitActivity>(p => p.Id));
				AddTiming(Field<CommitActivity>(p => p.Message));
				AddTiming(Field<CommitActivity>(p => p.ProjectName));
				AddTiming(Field<CommitActivity>(p => p.StringDuration));
			}

			private void AddTiming(Field field)
			{
				var timing = new HitTiming { Field = field };
				_timings.Add(timing);

				_stopwatch = Stopwatch.StartNew();

				_resolver.Resolve(field);
				timing.FirstHit = _stopwatch.Elapsed.TotalMilliseconds;

				_stopwatch.Restart();

				_resolver.Resolve(field);
				timing.CachedHit = _stopwatch.Elapsed.TotalMilliseconds;

				_stopwatch.Stop();
			}
		}
	}
}
