using EulerProblems.Lib;
using System.Text.RegularExpressions;

namespace EulerProblems.Problems
{
	internal class Euler0031 : Euler
	{


		public Euler0031() : base()
		{
			title = "Coin sums";
			problemNumber = 31;
			PrintTitle();
		}
		public override void Run()
		{

			Run_bruteForce();

		}
		private void Run_bruteForce()
		{
			int target = 200;

			int onePence = 1;
			int twoPence = 2;
			int fivePence = 5;
			int tenPence = 10;
			int twentyPence = 20;
			int fiftyPence = 50;
			int onePound = 100;
			int twoPound = 200;

			int maxOnePence = target / onePence;
			int maxTwoPence = target / twoPence;
			int maxFivePence = target / fivePence;
			int maxTenPence = target / tenPence;
			int maxTwentyPence = target / twentyPence;
			int maxFiftyPence = target / fiftyPence;
			int maxOnePound = target / onePound;
			int maxTwoPound = target / twoPound;

			int answer = 0;
			int count = 0;
			// 128MM possibilities, run in  23804.2229 milliseconds
			// make it backward (largest denomination to smallest) and check if we've gone over
			// 14,810,082 possibilities, run in 110.0447 milliseconds
			for (int h = 0; h <= maxTwoPound; h++)
			{
				for (int g = 0; g <= maxOnePound; g++)
				{
					if (h * twoPound + g * onePound <= target)
					{
						for (int f = 0; f <= maxFiftyPence; f++)
						{
							if (
								h * twoPound
								+ g * onePound
								+ f * fiftyPence
								<= target)
							{
								for (int e = 0; e <= maxTwentyPence; e++)
								{
									if (
										h * twoPound
										+ g * onePound
										+ f * fiftyPence
										+ e * twentyPence
										<= target)
									{
										for (int d = 0; d <= maxTenPence; d++)
										{
											if (
												h * twoPound
												+ g * onePound
												+ f * fiftyPence
												+ e * twentyPence
												+ d * tenPence
												<= target)
											{
												for (int c = 0; c <= maxFivePence; c++)
												{
													if (
														h * twoPound
														+ g * onePound
														+ f * fiftyPence
														+ e * twentyPence
														+ d * tenPence
														+ c * fivePence
														<= target)
													{

														for (int b = 0; b <= maxTwoPence; b++)
														{
															if (
																h * twoPound
																+ g * onePound
																+ f * fiftyPence
																+ e * twentyPence
																+ d * tenPence
																+ c * fivePence
																+ b * twoPence
																<= target)
															{
																for (int a = 0; a <= maxOnePence; a++)
																{
																	if (
																		a * onePence
																		+ b * twoPence
																		+ c * fivePence
																		+ d * tenPence
																		+ e * twentyPence
																		+ f * fiftyPence
																		+ g * onePound
																		+ h * twoPound
																		== target)
																	{
																		answer++;
																	}
																	count++;
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
						}
					}
				}
			}

			Console.WriteLine(count);
			PrintSolution(answer.ToString());
			return;
		}



	}
}
