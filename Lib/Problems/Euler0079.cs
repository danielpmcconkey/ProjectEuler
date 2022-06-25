//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0079 : Euler
	{
        const string filePath = @"E:\ProjectEuler\ExternalFiles\p079_keylog.txt";

        public Euler0079() : base()
		{
			title = "Passcode derivation";
			problemNumber = 79;
		}
		protected override void Run()
		{
            /*
             * This one was strikingly easy. I could've solved by hand I 
             * believe. But, since I was worried it'd be harder than I had 
             * thought I approached through code. I first wanted to see the 
             * statistical view on how often digit x preceeded digit y. I was
             * surprised to see that it was always black or white. If x 
             * preceeded y once, it always did. That made it a sorting 
             * algorithm. 
             * */
            Func<int[][]> readKeylogsIn = () =>
            {
                var lines = File.ReadLines(filePath).ToArray();
                var numLines = lines.Length;
                int[][] keylogs = new int[numLines][];
                Func<char[], int[]> toIntArray = (chars) =>
                {
                    var ints = new int[chars.Length];
                    var zeroPosition = ASCIIHelper.zero;
                    for (int i = 0; i < chars.Length; i++) { ints[i] = chars[i] - zeroPosition; }
                    return ints;
                };
                for (int i = 0; i < numLines; i++)
                {
                    keylogs[i] = toIntArray(lines[i].ToCharArray());
                }
                return keylogs;
            };
            Func<int[][], int[]> getDistinctKeys = (logs) =>
            {
                List<int> keys = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    if (logs.Where(x => x[0] == i || x[1] == i || x[2] == i).Any())
                    {
                        keys.Add(i);
                    }
                }
                return keys.ToArray();
            };

            var keylogs = readKeylogsIn();
            var distinctKeys = getDistinctKeys(keylogs);
            var orderedKeys = new int[distinctKeys.Length];
            Array.Copy(distinctKeys, orderedKeys, distinctKeys.Length);

            for (int i = 0; i < distinctKeys.Length - 1; i++)
            {
                for (int j = i+1; j < distinctKeys.Length; j++)
                {
                    var iVal = distinctKeys[i];
                    var jVal = distinctKeys[j];
                    var preceed = 0;
                    var succeed = 0;
                    // how often does i preceed j
                    // and how often does i suceed j
                    foreach (var l in keylogs)
                    {
                        int iPos = Array.IndexOf(l, iVal);
                        int jPos = Array.IndexOf(l, jVal);
                        if(jPos > -1 && iPos > -1)
                        {
                            if (iPos < jPos) preceed++;
                            if (iPos > jPos) succeed++;
                        }
                    }
                    if (preceed > 0 && succeed == 0)
                    {
#if VERBOSEOUTPUT
                        Console.WriteLine("{0} always preceeds {1}", iVal, jVal); 
#endif
                    }
                    else if (preceed == 0 && succeed > 0)
                    {
#if VERBOSEOUTPUT
                        Console.WriteLine("{0} always preceeds {1}", jVal, iVal); 
#endif
                        var orderedIndexI = Array.IndexOf(orderedKeys, iVal);
                        var orderedIndexJ = Array.IndexOf(orderedKeys, jVal);
                        orderedKeys = CommonAlgorithms.ArraySwap(orderedKeys, orderedIndexI, orderedIndexJ);
                    }
                    else
                    {
                        throw new NotImplementedException("need to implement duplicated numbers in passcode");
                    }
                }
            }

            var answer = string.Join("", orderedKeys);
			PrintSolution(answer);
			return;
		}
        
	}
}
