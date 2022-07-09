//#define VERBOSEOUTPUT
namespace EulerProblems.Lib.Problems
{
	public class Euler0090 : Euler
	{
        HashSet<string> checkedVals;
		public Euler0090() : base()
		{
			title = "Cube digit pairs";
			problemNumber = 90;
		}
		protected override void Run()
		{
            checkedVals = new HashSet<string>();

            bool[] left = new bool[10];
            bool[] right = new bool[10];
            Func<int, bool> addLeft = (n) =>
            {
                left[n] = true;
                right[n] = false;
                return true;
            };
            Func<int, bool> addRight = (n) =>
            {
                left[n] = false;
                right[n] = true;
                return true;
            };
            Func<int, bool> addBoth = (n) =>
            {
                left[n] = true;
                right[n] = true;
                return true;
            };
            Func<int, bool> addNeither = (n) =>
            {
                left[n] = false;
                right[n] = false;
                return true;
            };

            int answer = 0;
#if VERBOSEOUTPUT
            int numChecks = 0; 
#endif

            for (int i_0 = 0; i_0 < 3; i_0++)
            {
                switch (i_0)
                {
                    // 0 can either be in left, right, or both
                    case 0: addLeft(0); break;
                    case 1: addRight(0); break;
                    case 2: addBoth(0); break;
                }
                for (int i_1 = 0; i_1 < 2; i_1++)
                {
                    switch (i_1)
                    {
                        // 1 can either be in right or both
                        case 0: addRight(1); break;
                        case 1: addBoth(1); break;
                    }
                    for (int i_2 = 0; i_2 < 3; i_2++)
                    {
                        switch (i_2)
                        {
                            // 2 can be left, right, or both
                            case 0: addLeft(2); break;
                            case 1: addRight(2); break;
                            case 2: addBoth(2); break;
                        }
                        for (int i_3 = 0; i_3 < 3; i_3++)
                        {
                            switch (i_3)
                            {
                                // 3 can be left, right, or both
                                case 0: addLeft(3); break;
                                case 1: addRight(3); break;
                                case 2: addBoth(3); break;
                            }
                            for (int i_4 = 0; i_4 < 3; i_4++)
                            {
                                switch (i_4)
                                {
                                    case 0: addLeft(4); break;
                                    case 1: addRight(4); break;
                                    case 2: addBoth(4); break;
                                }
                                for (int i_5 = 0; i_5 < 3; i_5++)
                                {
                                    switch (i_5)
                                    {
                                        case 0: addLeft(5); break;
                                        case 1: addRight(5); break;
                                        case 2: addBoth(5); break;
                                    }
                                    for (int i_6 = 0; i_6 < 4; i_6++)
                                    {
                                        switch (i_6)
                                        {
                                            // 6 can be either, both, or neither
                                            case 0: addLeft(6); break;
                                            case 1: addRight(6); break;
                                            case 2: addBoth(6); break;
                                            case 3: addNeither(6); break;
                                        }
                                        for (int i_7 = 0; i_7 < 4; i_7++)
                                        {
                                            switch (i_7)
                                            {
                                                // 7 can be either, both, or neither
                                                case 0: addLeft(7); break;
                                                case 1: addRight(7); break;
                                                case 2: addBoth(7); break;
                                                case 3: addNeither(7); break;
                                            }
                                            for (int i_8 = 0; i_8 < 2; i_8++)
                                            {
                                                switch (i_8)
                                                {
                                                    // 8 can be in left or both
                                                    case 0: addLeft(8); break;
                                                    case 1: addBoth(8); break;
                                                }
                                                for (int i_9 = 0; i_9 < 4; i_9++)
                                                {
                                                    switch (i_9)
                                                    {
                                                        // 9 can be either, both, or neither
                                                        case 0: addLeft(9); break;
                                                        case 1: addRight(9); break;
                                                        case 2: addBoth(9); break;
                                                        case 3: addNeither(9); break;
                                                    }

                                                    if (IsValid(left, right)) answer++;

#if VERBOSEOUTPUT
                                                    numChecks++;
                                                    if (numChecks % 1000 == 0) Console.WriteLine("{0}", numChecks); 
#endif
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            PrintSolution(answer.ToString());
			return;
		}
        private bool IsValid(bool[] left, bool[] right)
        {
            List<int> d1 = new List<int>();
            List<int> d2 = new List<int>();
            for(int i = 0; i < 10; i++)
            {
                if (left[i]) d1.Add(i);
                if (right[i]) d2.Add(i);
            }
            if(d1.Count != 6) return false;
            if(d2.Count != 6) return false;

            // see if we've already checked this combo and return false if so
            var str1 = string.Join(",", d1);
            var str2 = string.Join(",", d2);
            var key1 = str1 + str2;
            var key2 = str2 + str1;
            if(checkedVals.Contains(key1)) return false;
            else if(checkedVals.Contains(key2)) return false;
            // add to the hash set so we don't check this combo again
            checkedVals.Add(key1);
            checkedVals.Add(key2);

            Func<List<int>, List<int>, bool> canMake01 = (d1, d2) =>
            {
                if ((d1.Contains(0) && d2.Contains(1)) ||
                    (d2.Contains(0) && d1.Contains(1))) return true;
                return false;
            };
            Func<List<int>, List<int>, bool> canMake04 = (d1, d2) =>
            {
                if ((d1.Contains(0) && d2.Contains(4)) ||
                    (d2.Contains(0) && d1.Contains(4))) return true;
                return false;
            };
            Func<List<int>, List<int>, bool> canMake09 = (d1, d2) =>
            {
                if ((d1.Contains(0) && d2.Contains(9)) ||
                    (d2.Contains(0) && d1.Contains(9)) ||
                    (d1.Contains(0) && d2.Contains(6)) ||
                    (d2.Contains(0) && d1.Contains(6)) 
                    ) return true;
                return false;
            };
            Func<List<int>, List<int>, bool> canMake16 = (d1, d2) =>
            {
                if ((d1.Contains(1) && d2.Contains(9)) ||
                    (d2.Contains(1) && d1.Contains(9)) ||
                    (d1.Contains(1) && d2.Contains(6)) ||
                    (d2.Contains(1) && d1.Contains(6))
                    ) return true;
                return false;
            };
            Func<List<int>, List<int>, bool> canMake25 = (d1, d2) =>
            {
                if ((d1.Contains(2) && d2.Contains(5)) ||
                    (d2.Contains(2) && d1.Contains(5))) return true;
                return false;
            };
            Func<List<int>, List<int>, bool> canMake36 = (d1, d2) =>
            {
                if ((d1.Contains(3) && d2.Contains(9)) ||
                    (d2.Contains(3) && d1.Contains(9)) ||
                    (d1.Contains(3) && d2.Contains(6)) ||
                    (d2.Contains(3) && d1.Contains(6))
                    ) return true;
                return false;
            };
            Func<List<int>, List<int>, bool> canMake49and64 = (d1, d2) =>
            {
                // given the 6/9 wild card, making 49 and 64 require the same 
                // combo of digits
                if ((d1.Contains(4) && d2.Contains(9)) ||
                    (d2.Contains(4) && d1.Contains(9)) ||
                    (d1.Contains(4) && d2.Contains(6)) ||
                    (d2.Contains(4) && d1.Contains(6))
                    ) return true;
                return false;
            };
            Func<List<int>, List<int>, bool> canMake81 = (d1, d2) =>
            {
                if ((d1.Contains(8) && d2.Contains(1)) ||
                    (d2.Contains(8) && d1.Contains(1))) return true;
                return false;
            };
            if (!canMake01(d1, d2)) return false;
            if (!canMake04(d1, d2)) return false;
            if (!canMake09(d1, d2)) return false;
            if (!canMake16(d1, d2)) return false;
            if (!canMake25(d1, d2)) return false;
            if (!canMake36(d1, d2)) return false;
            if (!canMake49and64(d1, d2)) return false;
            if (!canMake81(d1, d2)) return false;
            return true;
        }
	}
}
