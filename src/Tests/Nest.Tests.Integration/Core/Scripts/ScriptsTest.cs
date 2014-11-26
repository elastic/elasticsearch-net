using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Scripts
{
    [TestFixture]
    public class ScriptsTest : IntegrationTests
    {
        [Test]
		[SkipVersion("0 - 1.2.9", "Indexed scripts introduced in ES 1.3")]
        public void AddScriptWithNotSupportedLangParameterShouldBeInvalid()
        {
            var putScriptResponse = this.Client.PutScript(s => s.Lang("lang").Id("id").Script("1*1"));
            putScriptResponse.IsValid.Should().BeFalse();
        }

        [Test]
		[SkipVersion("0 - 1.2.9", "Indexed scripts introduced in ES 1.3")]
        public void AddScript()
        {
            var putScriptResponse = this.Client.PutScript(s => s.Lang(ScriptLang.Groovy).Id("id").Script("1*1"));
            putScriptResponse.IsValid.Should().BeTrue();
        }

        [Test]
		[SkipVersion("0 - 1.2.9", "Indexed scripts introduced in ES 1.3")]
        public void GetScriptShouldReturnScriptContent()
        {
            var script = "1*1";

            this.Client.PutScript(s => s.Lang(ScriptLang.Groovy).Id("id").Script(script));

            var getScriptResponse = this.Client.GetScript(s => s.Lang(ScriptLang.Groovy).Id("id"));
            getScriptResponse.IsValid.Should().BeTrue();
            getScriptResponse.Script.ShouldBeEquivalentTo(script);
        }

        [Test]
		[SkipVersion("0 - 1.2.9", "Indexed scripts introduced in ES 1.3")]
        public void AddAndDeleteScript()
        {
            var putScriptResponse = this.Client.PutScript(s => s.Lang(ScriptLang.Groovy).Id("id").Script("1*1"));
            putScriptResponse.IsValid.Should().BeTrue();

            var getScriptResponse = this.Client.GetScript(s => s.Lang(ScriptLang.Groovy).Id("id"));
            getScriptResponse.IsValid.Should().BeTrue();

            var deleteScriptResponse = this.Client.DeleteScript(s => s.Lang(ScriptLang.Groovy).Id("id"));
            deleteScriptResponse.IsValid.Should().BeTrue();
        }
    }
}
