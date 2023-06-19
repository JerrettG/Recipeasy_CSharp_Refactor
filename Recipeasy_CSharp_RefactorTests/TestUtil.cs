using System;
using Bogus;
using Moq;

namespace Recipeasy_CSharp_RefactorTests
{
	public class TestUtil
	{
        public static List<T> CreateMockList<T>(Dict>) where T : class
        {
            var faker = new Faker<T>();

            return faker.Generate(5)
                .Select(_ =>
                {
                    try
                    {
                        return CreateMock<T>();
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        throw new Exception(e.Message, e);
                    }
                })
                .ToList();
        }

        public static T CreateMock<T>() where T : class
        {
            var mock = Mock.Of<T>();
            return mock;
        }
    }
}

