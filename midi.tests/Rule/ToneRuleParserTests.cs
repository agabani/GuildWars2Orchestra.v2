using midi.Rule;
using NUnit.Framework;

namespace midi.tests.Rule
{
    [TestFixture]
    internal class ToneRuleParserTests
    {
        [Test]
        [TestCase("C0")]
        [TestCase("C#0")]
        [TestCase("C0,C#0")]
        [TestCase("C0,C#0,D0")]
        [TestCase("C0-C#0")]
        [TestCase("C0-C#0,C#0-D0")]
        [TestCase("C0-C#0,C#0-D0,D0-D#0")]
        [TestCase("C0,C0-C#0")]
        [TestCase("C0-C#0,C#0")]
        [TestCase("C0,C0-C#0,C#0-D0")]
        [TestCase("C0-C#0,C#0-D0,D0")]
        public void Test(string expected)
        {
            Assert.That(ToneRuleParser.FromString(expected).ToString(), Is.EqualTo(expected));
        }
    }
}