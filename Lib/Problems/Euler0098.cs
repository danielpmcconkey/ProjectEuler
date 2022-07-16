//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0098 : Euler
	{
		public Euler0098() : base()
		{
			title = "Anagramic squares";
			problemNumber = 98;
		}
		protected override void Run()
		{
            /*
             * I enjoyed this one. It certainly wasn't easy and, while hard, I 
             * didn't have to do any research. The path to solution was rather 
             * circuitous for me. My original attempt in C# was lacking. I was
             * certain I misunderstood the problem. I actually googled "Euler 
             * problem clarification" to see if I could check the rules without
             * spoiling the answer and came across this lovely forum:
             * 
             * https://projecteuler.chat/viewforum.php?f=50
             * 
             * That would've been nice on more than one occasion. In the end, 
             * my understanding was incorrect, but my implementation was lousy.
             * So I shifted gears. I took the problem into SQL, thinking I 
             * could use set-based logic to whittle down to the answer. And 
             * that worked. I never did produce an SQL query that gave me one
             * right answer, But I got down to less than 10 and was able to 
             * spot the correct one manually. 
             * 
             * Once I had the answer, it was pretty easy to do a C# version 
             * that worked.
             * 
             * */
            const string filePath = @"E:\ProjectEuler\ExternalFiles\p098_words.txt";
            var words = File.ReadAllText(filePath).Replace("\"","").Split(",");


            /* 
             * step 1 is to find all of the word anagrams. Make it easier by 
             * grouping on word length first.
             * */
            var wordsGroupedByLength = from word in words
                                       group word by word.Length into g
                                       orderby g.Key
                                       select new { length = g.Key, words = g.ToList() };
            var anagramWords = new List<(string w1, string w2, int length)>();
            var longestAnagram = 0;
            foreach(var wordGroup in wordsGroupedByLength)
            {
                for(int i = 0; i < wordGroup.words.Count(); i++)
                {
                    for (int j = i+1; j < wordGroup.words.Count(); j++)
                    {
                        var word1 = wordGroup.words[i];
                        var word2 = wordGroup.words[j];
                        var chars1 = word1.ToCharArray();
                        var chars2 = word2.ToCharArray();
                        Array.Sort(chars1);
                        Array.Sort(chars2);
                        var isAnagram = true;
                        for(int k = 0; k < chars1.Length; k++)
                        {
                            if(chars1[k] != chars2[k])
                            {
                                isAnagram = false; 
                                break;
                            }
                        }
                        if(isAnagram)
                        {
                            longestAnagram = wordGroup.length;
                            anagramWords.Add((word1, word2, chars1.Length));
                            Console.WriteLine("{0} {1} are anagrams", word1, word2);
                        }
                    }
                }
            }

            /*
             * step 2 is to create a list of all the perfect square whose 
             * number of digits is <= the largest word anagram. Don't bother
             * trying to find all the number anagrams as there are a LOT more
             * numbers than words to deal with
             * */

            List<int> squares = new List<int>();
            var limit = Math.Pow(10, longestAnagram);
            for(int i = 1; i * i < limit; i++)
            {
                squares.Add(i * i);
            }

            /*
             * step 3 is to iterate through all your anagram words, find all 
             * the squares of same length, and test those combos for matches to
             * the problem requirements
             * 
             * */

            // write the function for determining anagram-ness
            Func<int, int, string, string, bool> areAnagramsToWords = (n1, n2, s1, s2) =>
            {
                var charsS1 = s1.ToString().ToCharArray();
                var charsN1 = n1.ToString().ToCharArray();
                var charsN2 = n2.ToString().ToCharArray();
                var charsS2 = s2.ToString().ToCharArray();

                var replacedS1 = s1;
                var replacedS2 = s2;

                // apply the orphan annie decoder ring
                for (int i = 0; i < charsS1.Length; i++)
                {
                    replacedS1 = replacedS1.Replace(charsS1[i], charsN1[i]);
                    replacedS2 = replacedS2.Replace(charsS1[i], charsN1[i]);
                }

                if (replacedS1 != n1.ToString()) return false;
                if (replacedS2 != n2.ToString()) return false;

                return true;
            };

            int answer = 0;
            for (int k = 0; k < anagramWords.Count; k++)
            {
                var wordAnagram = anagramWords[k];
                int distinctCharCount = wordAnagram.w1.ToCharArray().Distinct().Count();
                var squaresThisLength = squares
                    .Where(x => 
                        x.ToString().Length == wordAnagram.w1.Length 
                        && x.ToString().ToCharArray().Distinct().Count() == distinctCharCount
                        )
                    .OrderByDescending(y => y)
                    .ToArray();

                for (int i = 0; i < squaresThisLength.Count(); i++)
                {
                    for (int j = 0; j < squaresThisLength.Count(); j++)
                    {
                        int n = squaresThisLength[i];
                        int m = squaresThisLength[j];
                        if (areAnagramsToWords(n, m, wordAnagram.w1, wordAnagram.w2))
                        {
                            Console.WriteLine("{0} {1} {2} {3}", n, m, wordAnagram.w1, wordAnagram.w2);
                            var biggerNum = Math.Max(m, n);
                            answer = Math.Max(answer, biggerNum);
                        }
                    }
                }
            }
            PrintSolution(answer.ToString());
            return;
        }
	}
}
