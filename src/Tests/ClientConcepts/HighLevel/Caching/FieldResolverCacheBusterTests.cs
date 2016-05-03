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
	public class FieldResolverCacheBusterTests
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
			public void ExpressionWithVariableSuffix()
			{
				var suffix = "raw";
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix(suffix)));
				resolver.CachedFields.Should().Be(0);
				resolved.Should().EndWith("raw");
				suffix = "foo";
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(suffix)));
				resolved = resolver.Resolve(Field<Project>(p => p.Name.Suffix(suffix)));
				resolver.CachedFields.Should().Be(0);
				resolved.Should().EndWith("foo");
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

			[U]
			public void ExpressionWithDictionaryItemConstantExpression()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var resolved = resolver.Resolve(Field<Project>(p => p.Metadata["key1"]));
				resolver.CachedFields.Should().Be(1);
				resolved.Should().Contain("key1");
				resolved = resolver.Resolve(Field<Project>(p => p.Metadata["key2"]));
				resolver.CachedFields.Should().Be(2);
				resolved.Should().Contain("key2");
			}

			[U]
			public void ExpressionWithDictionarySuffix()
			{
				var resolver = new TestableFieldResolver(new ConnectionSettings());
				var key = "key1";
				var d = new Dictionary<string, string> { { "key1", "raw" }, { "key2", "foo" } };
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d[key])));
				resolver.CachedFields.Should().Be(0);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d["key1"])));
				resolver.CachedFields.Should().Be(0);
				key = "key2";
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d[key])));
				resolver.CachedFields.Should().Be(0);
				resolver.Resolve(Field<Project>(p => p.Name.Suffix(d["key2"])));
				resolver.CachedFields.Should().Be(0);
			}
		}

	}
}
