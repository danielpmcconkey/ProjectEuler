namespace EulerProblems.Lib.Problems
{
    internal enum WordsUpToNineteen
    {
        zero, one, two, three, four, five, six, seven, eight, nine, ten,    // don't use zero
        eleven, twelve, thirteen, fourteen, fifteen, sixteen, seventeen,
        eighteen, nineteen
    }
    internal enum TensWords
    {
        zero, teen, twenty, thirty, forty, fifty, sixty, seventy,   // don't use zero or teen
        eighty, ninety
    }
    public class Euler0017 : Euler
    {
        public Euler0017() : base()
        {
            title = "Number letter counts";
            problemNumber = 17;
            
        }
        protected override void Run()
        {
            long numberOfLetters = 0;
            int firstNum = 1;
            int lastNum = 1000;

            //string test = PrintNumberAsWord(342);
            //long test2 = CountLetters(test);
            //Console.WriteLine(test.PadRight(20) + test2.ToString().PadLeft(10));

            for (int i = firstNum; i <= lastNum; i++)
            {
                string words = PrintNumberAsWord(i);
                long lettersCount = CountLetters(words);
                //Console.WriteLine(words.PadRight(20) + lettersCount.ToString().PadLeft(10));
                numberOfLetters += lettersCount;
            }

            PrintSolution(numberOfLetters.ToString());
            return;
        }
        private string PrintNumberAsWord(int n)
        {
            // rather than work out a program for something that'll only 
            // happen once, just hard wire "one thousand"
            if (n == 1000) return "one thousand";    

            string word = "";

            // convert to string (string containing numerical digits)
            // and get teh individual digits
            char[] nAsCharArray = n.ToString().PadLeft(4, '0').ToCharArray();
            short hundreds = Int16.Parse(nAsCharArray[1].ToString());
            short tens = Int16.Parse(nAsCharArray[2].ToString());
            short ones = Int16.Parse(nAsCharArray[3].ToString());

            if(n < 1000 & n >= 100)
            {
                // add the hundreds 
                if(hundreds > 0)
                {
                    word += (WordsUpToNineteen)hundreds;
                    word += "hundred";
                }
                if(tens > 0 || ones > 0) word += "and"; // avoids it saying one hundred and zero
            }
            // now convert the tens and ones to words
            if (tens >= 2)
            {
                word += (TensWords)tens;
                if (ones > 0) word += (WordsUpToNineteen)ones;
            }
            else if (tens > 0 || ones > 0) // avoids it saying one hundred and zero
            {
                string teenString = tens.ToString() + ones.ToString();
                short teen = Int16.Parse(teenString);
                word += (WordsUpToNineteen)teen;
            }
            return word;
        }
        private long CountLetters(string x)
        {
            return x.Replace(" ", "").Length;
        }



    }
}
