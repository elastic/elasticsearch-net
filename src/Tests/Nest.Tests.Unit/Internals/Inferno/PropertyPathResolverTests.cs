using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Nest.Resolvers;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class PropertyPathResolverTests
	{
		private class CustomDict : Dictionary<string, ElasticsearchProject> { }

		private class DomainObject
		{
			public string Name { get; set; }
			public IDictionary<string, ElasticsearchProject> Dictionary { get; set; }
			public CustomDict CustomDict { get; set; }
			public IList<ElasticsearchProject> Collection { get; set; }

		}

		private readonly ElasticInferrer _infer;
		private readonly string _variable = "vari";

		public PropertyPathResolverTests()
		{
			_infer = TestElasticClient.Client.Infer;
		}

		private string P(Expression<Func<DomainObject, object>> path)
		{
			return this._infer.PropertyPath(Property.Path(path));
		}

		[Test]
		public void SimpleProperty()
		{
			P(p => p.Name).Should().Be("name");
		}

		[Test]
		public void SuffixOnPropery()
		{
			P(p => p.Name.Suffix("sort")).Should().Be("name.sort");
		}

		[Test]
		public void IndexOnCollection()
		{
			P(p => p.Collection[0]).Should().Be("collection");
		}

		[Test]
		public void IndexOnCollectionProperty()
		{
			P(p => p.Collection[0].Name).Should().Be("collection.name");
		}

		[Test]
		public void FirstOnCollection()
		{
			P(p => p.Collection.First()).Should().Be("collection");
		}

		[Test]
		public void FirstOnCollectionProperty()
		{
			P(p => p.Collection.First().Name).Should().Be("collection.name");
		}

		[Test]
		public void Dictionary()
		{
			P(p => p.Dictionary["hardcoded"]).Should().Be("dictionary.hardcoded");
		}

		[Test]
		public void DictionaryPropery()
		{
			P(p => p.Dictionary["hardcoded"].Name).Should().Be("dictionary.hardcoded.name");
		}
		
		//Test variables
		[Test]
		public void DictionaryVariableKey()
		{
			P(p => p.Dictionary[_variable]).Should().Be("dictionary.vari");
		}

		[Test]
		public void DictionaryVariableKeyProperty()
		{
			P(p => p.Dictionary[_variable].Name).Should().Be("dictionary.vari.name");
		}

		[Test]
		public void CustomDictionary()
		{
			P(p => p.CustomDict["hardcoded"]).Should().Be("customDict.hardcoded");
		}

		[Test]
		public void CustomDictionaryPropery()
		{
			P(p => p.CustomDict["hardcoded"].Name).Should().Be("customDict.hardcoded.name");
		}
		
		//Test variables

		[Test]
		public void CustomDictionaryVariableKey()
		{
			P(p => p.CustomDict[_variable]).Should().Be("customDict.vari");
		}

		[Test]
		public void CustomDictionaryVariableKeyProperty()
		{
			P(p => p.CustomDict[_variable].Name).Should().Be("customDict.vari.name");
		}

		//Test suffixes
		[Test]
		public void PropertySuffix()
		{
			P(p => p.Name.Suffix("suffix")).Should().Be("name.suffix");
		}

		[Test]
		public void DictionarySuffix()
		{
			P(p => p.Dictionary["hardcoded"].Suffix("suffix")).Should().Be("dictionary.hardcoded.suffix");
		}

		[Test]
		public void FirstOnCollectionSuffix()
		{
			P(p => p.Collection.First().Suffix("suffix")).Should().Be("collection.suffix");
		}

		[Test]
		public void IndexOnCollectionSuffix()
		{
			P(p => p.Collection[0].Suffix("suffix")).Should().Be("collection.suffix");
		}

		[Test]
		public void CollectionSuffix()
		{
			P(p => p.Collection.Suffix("suffix")).Should().Be("collection.suffix");
		}

		[Test]
		public void PropertySuffixVariable()
		{
			P(p => p.Name.Suffix(_variable)).Should().Be("name.vari");
		}

		[Test]
		public void PropertySuffixLocalVariable()
		{
			var prop = "propXY12";
			P(p => p.Name.Suffix(prop)).Should().Be("name." + prop);
		}

		//Fully qualified tests
		//TODO remove in 2.0 as type.properties are gonna be removed in elasticsearch 2.0

		[Test]
		public void PropertySuffixVariableFullyQualified()
		{
			P(p => p.FullyQualified().Name.Suffix(_variable)).Should().Be("domainobject.name.vari");
		}
	}
}
