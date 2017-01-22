using midi.Rule;
using NUnit.Framework;

namespace midi.tests.Rule
{
    [TestFixture]
    internal class ToneRuleParserTests
    {
        [Test]
        public void Test()
        {
            var rule = ToneRuleParser.FromString("C0");
            Assert.That(rule.ToString(), Is.EqualTo("C0"));
        }
    }
}