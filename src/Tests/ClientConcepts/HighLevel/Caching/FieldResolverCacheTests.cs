using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
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

			//
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
	}
}
