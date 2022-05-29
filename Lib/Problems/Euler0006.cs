namespace EulerProblems.Lib.Problems
{
    public class Euler0006 : Euler
    {
        public Euler0006() : base()
        {
            title = "Sum square difference";
            problemNumber = 6;
            
        }
        protected override void Run()
        {
            long limit = 100;
            long sumOfSquares = CommonAlgorithms.GetSumOfSquares(limit);
            long squareOfSum = CommonAlgorithms.GetSquareOfSum(limit);

            long difference = squareOfSum - sumOfSquares;
            PrintSolution(difference.ToString());
            return;
        }

    }
}
