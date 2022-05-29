using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
    internal class Euler0024 : Euler
    {
        public Euler0024() : base()
        {
            title = "Lexicographic permutations";
            problemNumber = 24;
            PrintTitle();
        }
        public override void Run()
        {
			/*
             * I just want to show off the rad SQL I wrote
             * to first solve this problem. I wrote a linq
             * version of this, but it never finished. The
             * below SQL run in Postgres runs in 8.6 seconds
             * I know there's a better way though.
             * */

            #region radSQL
			const string radSQL = @"
                                 * 
                    drop table numerals;
                    create temporary table numerals (
					    digit int
                    );

                    insert into numerals (digit) values (	0	);
                    insert into numerals (digit) values (	1	);
                    insert into numerals (digit) values (	2	);
                    insert into numerals (digit) values (	3	);
                    insert into numerals (digit) values (	4	);
                    insert into numerals (digit) values (	5	);
                    insert into numerals (digit) values (	6	);
                    insert into numerals (digit) values (	7	);
                    insert into numerals (digit) values (	8	);
                    insert into numerals (digit) values (	9	);



                    with permutations as (
                    select 
	                    a.digit as a, 
	                    b.digit as b, 
	                    c.digit as c,
	                    d.digit as d,
 	                    e.digit as e,
	                    f.digit as f,
	                    g.digit as g,
	                    h.digit as h,
	                    i.digit as i,
	                    j.digit as j
                    from numerals a
	                    cross join numerals b
	                    cross join numerals c
	                    cross join numerals d
	                    cross join numerals e
	                    cross join numerals f
	                    cross join numerals g
	                    cross join numerals h
	                    cross join numerals i
	                    cross join numerals j
                    where 
	                    a.digit <> b.digit
	                    and a.digit <> c.digit
	                    and a.digit <> d.digit
	                    and a.digit <> e.digit
	                    and a.digit <> f.digit
	                    and a.digit <> g.digit
	                    and a.digit <> h.digit
	                    and a.digit <> i.digit
	                    and a.digit <> j.digit
	                    and b.digit <> c.digit
	                    and b.digit <> d.digit
	                    and b.digit <> e.digit
	                    and b.digit <> f.digit
	                    and b.digit <> g.digit
	                    and b.digit <> h.digit
	                    and b.digit <> i.digit
	                    and b.digit <> j.digit
	                    and c.digit <> d.digit
	                    and c.digit <> e.digit
	                    and c.digit <> f.digit
	                    and c.digit <> g.digit
	                    and c.digit <> h.digit
	                    and c.digit <> i.digit
	                    and c.digit <> j.digit
	                    and d.digit <> e.digit
	                    and d.digit <> f.digit
	                    and d.digit <> g.digit
	                    and d.digit <> h.digit
	                    and d.digit <> i.digit
	                    and d.digit <> j.digit
	                    and e.digit <> f.digit
	                    and e.digit <> g.digit
	                    and e.digit <> h.digit
	                    and e.digit <> i.digit
	                    and e.digit <> j.digit
	                    and f.digit <> g.digit
	                    and f.digit <> h.digit
	                    and f.digit <> i.digit
	                    and f.digit <> j.digit
	                    and g.digit <> h.digit
	                    and g.digit <> i.digit
	                    and g.digit <> j.digit
	                    and h.digit <> i.digit
	                    and h.digit <> j.digit
	                    and i.digit <> j.digit
                    order by a, b, c, d, e, f, g, h, i, j
                    limit 1000000
                    )

                    select * from permutations 
                    order by 
	                    cast(
		                    concat(a,b,c,d,e,f,g,h,i,j)
		                    as numeric(10,0)) desc
                     limit 1
                    ;
				";
			#endregion

			int[] numerals = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int[][] permutations = CommonAlgorithms.GetAllLexicographicPermutationsOfIntArray(numerals);
			
			Console.WriteLine(permutations.Length);

			string answer = string.Empty;
			for (int i = 0; i < 10; i++)
            {
				answer += permutations[999999][i];
			}

            PrintSolution(answer.ToString());
            return;
        }
		
	}
}
