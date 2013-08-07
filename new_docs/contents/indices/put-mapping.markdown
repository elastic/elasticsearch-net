---
template: layout.jade
title: Connecting
menusection: indices
menuitem: put-mapping
---


# Put Mapping

The put mapping API allows to register specific mapping definition for a specific type.

## Attribute based mapping

You can decorate your classes with `ElasticProperty` and `ElasticType` attributes to describe how they should be mapped in ES.

	[ElasticType(
		Name = "elasticsearchprojects2",
		DateDetection = true,
		NumericDetection = true,
		SearchAnalyzer = "standard",
		IndexAnalyzer = "standard",
		DynamicDateFormats = new[] { "dateOptionalTime", "yyyy/MM/dd HH:mm:ss Z||yyyy/MM/dd Z" }
	)]
	public class ElasticSearchProject
	{
		public int Id { get; set;  }
		public string Name { get; set; }
		[ElasticProperty(OmitNorms = true, Index = FieldIndexOption.not_analyzed)]
		public string Country { get; set; }
		public string Content { get; set; }
		[ElasticProperty(Name="loc")]
		public int LOC { get; set; }
		public List<Person> Followers { get; set; }

		[ElasticProperty(Type=FieldType.geo_point)]
		public GeoLocation Origin { get; set; }
		public DateTime StartedOn { get; set; }


		//excuse the lame properties i needed some numerics !
		public long LongValue { get; set; }
		public float FloatValue { get; set; }
		public double DoubleValue { get; set; }

		[ElasticProperty(NumericType=NumericType.Long)]
		public int StupidIntIWantAsLong { get; set; }
	}


You can persist this mapping by simpling calling 

	var response = this.ConnectedClient.Map<ElasticSearchProject>();


**NOTE**: Whenever the client needs to infer the typename for `ElasticSearchProject` it will resolve nicely to `"elasticsearchprojects2"` now too. This gives you great control over naming without having to specify the typename on each call.

**ALSO NOTE**: `Map<T>()` will also explicitly map string properties as strings with elasticsearch even if they do not have an attribute on them. It does this for all the value types (string, int, float, double, DateTime).

## Code based mapping

You can also create mappings on the fly:

	var typeMapping = new TypeMapping(Guid.NewGuid().ToString("n"));
	var property = new TypeMappingProperty
	{
		Type = "multi_field"
	};

	var primaryField = new TypeMappingProperty
	{
		Type = "string", 
		Index = "not_analyzed"
	};

	var analyzedField = new TypeMappingProperty
	{
		Type = "string", 
		Index = "analyzed"
	};

	property.Fields = new Dictionary<string, TypeMappingProperty>();
	property.Fields.Add("name", primaryField);
	property.Fields.Add("name_analyzed", analyzedField);

	typeMapping.Properties.Add("name", property);
	this.ConnectedClient.Map(typeMapping);
