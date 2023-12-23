using NUnit.Framework;

namespace BulletinBoard.BL.UnitTests
{
    [TestFixture]
    public class TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        [Test]
        [TestCase(" 8 919 ")]
        [TestCase(null)]
        [TestCase("")]
        [Category("Integration")]
        public void TestPhoneValidation(string phone)
        {
            Assert.Pass();
        }
    }
}