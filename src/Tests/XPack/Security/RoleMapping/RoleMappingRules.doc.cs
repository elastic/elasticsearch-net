using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Tests.Framework.RoundTripper;

namespace Tests.Search
{
	/**[[role-mapping-rules]]
	 * === Role Mapping Rules
	 *
	 * X-Pack Security allows you to map role rules through the API as of 5.5
	 * NEST exposes this role rules DSL in a strongy typed fashion
	 *
	 */
	public class RoleMappingRulesTests : SerializationTestBase
	{
		//this serializes the expected json in a way that preserves null values
		protected override bool NoClientSerializeOfExpected => true;


        /**==== Rule Conjunction
         *
         * You can create a conjuction of many rules using either `+` or `&` which are both overloaded to produce an `all` rule.
         */
		[U] public void RulesConjunction()
		{
			var allRule = new UsernameRule("u1") + new UsernameRule("u2") & new UsernameRule("u3");
			allRule.All.Should().NotBeEmpty().And.HaveCount(3);


			/** using `+=` assignments you can build rules more dynamically **/
			RoleMappingRuleBase rules = null;
			for(var i = 0;i<10;i++)
				rules += new UsernameRule($"user_{i}");
			rules.Should().BeOfType<AllRoleMappingRule>();

			allRule = (AllRoleMappingRule) rules;
			allRule.All.Should().NotBeEmpty().And.HaveCount(10);


		}

        /**==== Rule Disjunction
         *
         * The produce an `any` disjunction rule over many rules you can use the overloaded `|`
         */
		[U] public void RulesDisjunction()
		{
			var anyRule = new UsernameRule("u1") | new UsernameRule("u2") | new UsernameRule("u3");
			anyRule.Any.Should().NotBeEmpty().And.HaveCount(3);

			/** using `|=` assignments you can build disjunction rules more dynamically **/
			RoleMappingRuleBase rules = null;
			for(var i = 0;i<10;i++)
				rules |= new UsernameRule($"user_{i}");
			rules.Should().BeOfType<AnyRoleMappingRule>();

			anyRule = (AnyRoleMappingRule) rules;
			anyRule.Any.Should().NotBeEmpty().And.HaveCount(10);

		}

        /**==== Rule Negation
         *
         * You can automatically negate any rule by using the `!` infix operator
         */
		[U] public void RuleNegation()
		{
			var exceptRule = !new UsernameRule("user_1");
			exceptRule.Should().BeOfType<ExceptRoleMappingRole>();
		}

        /**==== Full Example
         *
         * Combining all of these you can build role mapping rules quite elegantly
         */
		[U] public void FullExample()
		{
			var dn = "*,ou=admin,dc=example,dc=com";
			var username = "mpdreamz";
			var realm = "some_realm";
			var metadata = Tuple.Create("a", "b");
			var groups = new [] {"group1", "group2"};

			/** using the operators we can succintly combine field rules  */
			var dsl =
				(new DistinguishedNameRule(dn) | new UsernameRule(username) | new RealmRule(realm))
				& new MetadataRule(metadata.Item1, metadata.Item2)
				& !new GroupsRule(groups);

			/** We can also use an explicit object initializer syntax to compose the same graph */
			var ois = new AllRoleMappingRule(
				new AnyRoleMappingRule(
					new DistinguishedNameRule(dn),
					new UsernameRule(username),
					new RealmRule(realm)
				)
				, new MetadataRule(metadata.Item1, metadata.Item2)
				, new ExceptRoleMappingRole(new GroupsRule(groups))
			);

			var writingStyles = Setup(dsl, ois);

			Assert(writingStyles, json: new {
				all = new object[] {
					new {
						any = new object[] {
							new { field = new { dn = "*,ou=admin,dc=example,dc=com" } },
							new { field = new { username = "mpdreamz" } },
							new { field = new Dictionary<string, object>(){ {"realm.name", "some_realm" } } }
						}
					},
					new { field = new Dictionary<string, object>(){ {"metadata.a", "b" } } },
					new { except = new { field = new { groups = new [] { "group1", "group2" } } } }
				}
            });
		}

		private static Tuple<RoleMappingRuleBase, RoleMappingRuleBase> Setup(RoleMappingRuleBase dsl, RoleMappingRuleBase ois) =>
			Tuple.Create(dsl, ois);


		private void Assert(Tuple<RoleMappingRuleBase, RoleMappingRuleBase> rules, object json)
		{
			Assert(json, rules.Item1);
			Assert(json, rules.Item2);
		}

		private void Assert(object json, RoleMappingRuleBase rules)
		{
			Expect(new
			{
				enabled = true,
				roles = new[] {"admin"},
				rules = json,
				metadata = new
				{
					x = "y",
					//TODO test for null here again, limitation of Expect()
					//z = (object) null
				}
			}).WhenSerializing(new PutRoleMappingRequest("x")
			{
				Enabled = true,
				Roles = new[] {"admin"},
				Rules = rules,
				Metadata = new Dictionary<string, object>
				{
					{"x", "y"},
					//{"z", null}
				}
			});
		}
	}
}
