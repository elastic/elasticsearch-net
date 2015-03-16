using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class FunctionScoreQueryTests
	{
		[Test]
		public void FunctionScoreQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.FunctionScore(fs => fs
						.Name("named_query")
						.Query(qq => qq.MatchAll())
						.Functions(
						    f => f.Weight(3.0).Filter(ff => ff.Term(p => p.Name, "elasticsearch")),
							f => f.Gauss(x => x.StartedOn, d => d.Scale("42w")),
							f => f.Linear(x => x.FloatValue, d => d.Scale("0.3")),
							f => f.Exp(x => x.DoubleValue, d => d.Scale("0.5")),
							f => f.BoostFactor(2.0),
							f => f.FieldValueFactor(op => op.Field(ff => ff.DoubleValue).Factor(2.5).Modifier(FieldValueFactorModifier.SquareRoot))
						)
						.ScoreMode(FunctionScoreMode.Sum)
						.BoostMode(FunctionBoostMode.Replace)
					)
				).Fields(x => x.Content);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ 
                from: 0, size: 10, 
                fields: [""content""],
				query : {
                    function_score : { 
						_name: ""named_query"",
                        functions: [
							{weight: 3.0, filter: { term: { 'name': 'elasticsearch' }}},
                            {gauss:  { startedOn  : { scale: '42w'}}},
                            {linear: { floatValue : { scale: '0.3'}}},
                            {exp:    { doubleValue: { scale: '0.5'}}}, 
                            {boost_factor: 2.0 },
							{field_value_factor: { field: 'doubleValue', factor: 2.5, modifier: 'sqrt'}}
                        ],				
						query : { match_all : {} },
                        score_mode: 'sum',
                        boost_mode: 'replace',
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void FunctionScoreQueryWithJustWeight()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq.MatchAll())
						.Weight(2)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ 
                from: 0, size: 10, 
				query : {
                    function_score : { 		
						query : { match_all : {} },
						weight : 2.0
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void FunctionScoreQueryConditionless()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq.Term("", ""))
						.Functions(
							f => f.Gauss(x => x.StartedOn, d => d.Scale("42w")),
							f => f.Linear(x => x.FloatValue, d => d.Scale("0.3")),
							f => f.Exp(x => x.DoubleValue, d => d.Scale("0.5")),
							f => f.BoostFactor(2),
							f => f.FieldValueFactor(db => db.Field(fa => fa.DoubleValue).Factor(3.4).Modifier(FieldValueFactorModifier.Ln))
						)
						.ScoreMode(FunctionScoreMode.Sum)
						.BoostMode(FunctionBoostMode.Replace)
					)
				).Fields(x => x.Content);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ 
                from: 0, size: 10, 
                fields: [""content""],
				query : {
                    function_score : { 
                        functions: [
                            {gauss:  { startedOn  : { scale: '42w'}}},
                            {linear: { floatValue : { scale: '0.3'}}},
                            {exp:    { doubleValue: { scale: '0.5'}}}, 
                            {boost_factor: 2.0 },
							{field_value_factor: { field: 'doubleValue', factor: 3.4, modifier: 'ln'}}
                        ],				
                        score_mode: 'sum',
                        boost_mode: 'replace',
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void ConditionlessFieldValueFactor()
		{
			Assert.Throws<DslException>(() => new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					.FunctionScore(fs => fs
						.Query(qq => qq.Term("", ""))
						.Functions(
							f => f.FieldValueFactor(db => db.Factor(3.4).Modifier(FieldValueFactorModifier.Ln))
						))
				));
		}
	}
}