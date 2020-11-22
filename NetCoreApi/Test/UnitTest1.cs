using NUnit.Framework;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var annoyCla1 = new
            {
                sercet = "123456789123456789",
                issuer = "test.cn",
                audience = "test",
                accessExpiration=30,
                refreshExpiration=60

            };


        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }

    internal class UserModel
    {
        public UserModel()
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}