//  Code Review

//* Design a method that compresses an input string consisting of only alphabetic characters by shortening its length, where possible.
//  The string should be compressed such that consecutive duplicate letters are replaced with the letter followed by the number of consecutive duplicates.
//  For example, uuueeeenzzzzz would compress into u3e4nz5.

//* However, if the compression of a given letter would not shorten the output string, simply output the original letters. For example, uuueeeennzzzzz would compress into u3e4nnz5.

//* Bonus #1: Design the method in such a way that the input string may contain numbers.
//  If the input string does contain numbers, ensure that the input is compressed in an unambiguous way (a way in which the output string can be uncompressed back into the original input string).
//  Would an output of ab434 mean the original string was ab434 or ab43333 or abbbb3333 or ab4444444444444444444444444444444444?

//* Bonus #2: Can the caller of the method know if the input string was compressed or if the input string was simply returned as-is without the caller of the method having to perform a calculation of its own?

using System.Text.RegularExpressions;

public static class ExtensionMethods
{
    public static string ReplaceNumericValues(this string value)
    { 
        var newValue = value.Replace('0', '!');
        newValue = newValue.Replace('1', '$');
        newValue = newValue.Replace('2', '%');
        newValue = newValue.Replace('3', '^');
        newValue = newValue.Replace('4', '#');
        newValue = newValue.Replace('5', '/');
        newValue = newValue.Replace('6', '&');
        newValue = newValue.Replace('7', ')');
        newValue = newValue.Replace('8', '(');
        newValue = newValue.Replace('9', '*');
        return newValue;
    }
}

public class Program
{

    /// <summary>
    /// Method for compress a string
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns> A compressed version of the input or the original instead </returns>
    public string StringCompression(string? inputString)
    {
        if (!string.IsNullOrWhiteSpace(inputString))
        {
            inputString = Regex.Replace(inputString, @"\s+", "");
            inputString = (inputString.Any(Char.IsNumber)) ? inputString.ReplaceNumericValues() : inputString;

            var repetition = inputString // stores characters that repeats > 3 times
              .GroupBy(x => x)
              .Where(r => r.Count() > 2)
              .Select(x => new { Letter = x.First(), Total = x.Count() }).ToArray();

            var same = inputString // stores characters that repeats 2 or less times
              .GroupBy(x => x)
              .Where(r => r.Count() <= 2) 
              .Select(x => new { Letter = x.First(), Total = x.Count() }).ToArray();

            var outputList = new List<char>(); // aux list to generate the output string
            char last = inputString[0];
            int repetitionIndex=0, sameIndex=0;

            foreach (var characters in inputString.ToCharArray().Distinct()) // loop to create the new string based on the input
            {
                if(repetition.Count() > 0)
                {
                    if (repetition[repetitionIndex].Letter == characters)
                    {
                        int ascii = 0;
                        outputList.Add(repetition[repetitionIndex].Letter);
                        if (repetition[repetitionIndex].Total >= 10) // in case a character repeats over 10 times
                        {
                            ascii = (int)(repetition[repetitionIndex].Total.ToString())[0];
                            outputList.Add((char)ascii);
                            ascii = (int)(repetition[repetitionIndex].Total.ToString())[1];
                            outputList.Add((char)ascii);
                        }
                        else
                        {
                            ascii = (int)(repetition[repetitionIndex].Total.ToString())[0];
                            outputList.Add((char)ascii);
                        }
                        
                        repetitionIndex += (repetitionIndex < (repetition.Count() - 1)) ? 1 : 0;

                    }
                }
                if(same.Count() > 0)
                {
                    if (same[sameIndex].Letter == characters)
                    {
                        if (same[sameIndex].Total == 2)
                        {
                            outputList.Add(same[sameIndex].Letter);
                            outputList.Add(same[sameIndex].Letter);
                        }
                        else
                        {
                            outputList.Add(same[sameIndex].Letter);
                        }
                        if (sameIndex < (same.Count() - 1))
                            sameIndex++;
                    }
                }
            }

            string outputString = new string(outputList.ToArray()); 
            return (inputString != outputString) ? $"Succesfully compressed: {outputString}" : $"Not compressed: {outputString}";
        }

        return "Not a valid string!";
    }

    static void Main(string[] args)
    {
        var program = new Program();
        Console.WriteLine(program.StringCompression("ab4444444444444444444444444444444444"));

    }

}



