using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.ScriptFields
{
	[TestFixture]
	public class ScriptFieldTests : BaseJsonTests
	{
		[Test]
		public void TestScriptFields()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.ScriptFields(sf=>sf
					.Add("test1", sff=>sff
						.Script("doc['loc'].value * multiplier")
						.Params(sp=>sp
							.Add("multiplier", 4)
						)
					)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

	    [Test]
        [ExpectedException]
	    public void TestIndexedScriptFieldNullScriptIdShouldThrowException()
	    {
            new SearchDescriptor<ElasticsearchProject>()
                .From(0)
                .Size(10)
                .ScriptFields(sf => sf
                    .Add("test1", sff => sff
                        .ScriptId(null)
                        .Lang(ScriptLang.Groovy)
                        .Params(sp => sp
                            .Add("multiplier", 4)
                        )
                    )
                );
	    }

	    [Test]
	    public void TestIndexedScriptField()
	    {
            var s = new SearchDescriptor<ElasticsearchProject>()
                .From(0)
                .Size(10)
                .ScriptFields(sf => sf
                    .Add("test1", sff => sff
                        .ScriptId("scriptId")
                        .Lang("groovy")
                        .Params(sp => sp
                            .Add("multiplier", 4)
                        )
                    )
                );

            this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
	    }

        [Test]
        public void TestIndexedScriptFieldWithScriptLangEnum()
        {
            var s = new SearchDescriptor<ElasticsearchProject>()
                .From(0)
                .Size(10)
                .ScriptFields(sf => sf
                    .Add("test1", sff => sff
                        .ScriptId("scriptId")
                        .Lang(ScriptLang.Groovy)
                        .Params(sp => sp
                            .Add("multiplier", 4)
                        )
                    )
                );

            this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
        }
	}
}
