using Xunit.Abstractions;

namespace TestProject
{
    public class Test
    {
        private ITestOutputHelper _helper;

        public Test(ITestOutputHelper helper)
        {
            _helper = helper;
        }

        [Theory]
        [InlineData("uuueeeenzzzzz", "Succesfully compressed: u3e4nz5")]
        [InlineData("uuueeeennzzzzz", "Succesfully compressed: u3e4nnz5")]
        [InlineData("uuuuuuuuuueeeennzzzzz", "Succesfully compressed: u10e4nnz5")] 
        public void StringCompression_ExpectedOutputs(string input, string expectedOutput)
        {
            // arrange
            var test = new Program();

            // act
            var output = test.StringCompression(input);

            // assert
            _helper.WriteLine($"Input: {input}\nOutput: {output}\nExpected: {expectedOutput}");
            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [InlineData("aabbcc", "Not compressed: aabbcc")]
        [InlineData("abc", "Not compressed: abc")]
        [InlineData("a", "Not compressed: a")]
        public void StringCompression_NotCompressed(string input, string expectedOutput)
        {
            // arrange
            var test = new Program();

            // act
            var output = test.StringCompression(input);

            // assert
            _helper.WriteLine($"Input: {input}\nOutput: {output}\nExpected: {expectedOutput}");
            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [InlineData("", "Not a valid string!")]
        [InlineData(" ", "Not a valid string!")]
        [InlineData(null, "Not a valid string!")]
        public void StringCompression_NullOrEmptyString(string input, string expectedOutput)
        {
            // arrange
            var program = new Program();

            // act
            var output = program.StringCompression(input);

            // assert
            _helper.WriteLine($"Input: {input}\nOutput: {output}\nExpected: {expectedOutput}");
            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [InlineData("hh555", "Succesfully compressed: hh/3")]
        [InlineData("bf77", "Not compressed: bf))")]
        [InlineData("ab43333", "Succesfully compressed: ab#^4")]
        [InlineData("abbbb3333", "Succesfully compressed: ab4^4")]
        [InlineData("ab4444444444444444444444444444444444", "Succesfully compressed: ab#34")]
        public void StringCompression_NumericCharacters(string input, string expectedOutput)
        {
            // arrange
            var program = new Program();

            // act
            var output = program.StringCompression(input);

            // assert
            _helper.WriteLine($"Input: {input}\nOutput: {output}\nExpected: {expectedOutput}");
            Assert.Equal(expectedOutput, output);
        }
    }

}