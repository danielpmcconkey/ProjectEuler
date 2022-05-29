

namespace EulerProblems.Lib
{
    public static class EulerProblemFactory
    {
        public static Euler GetEulerProblemClassByNumber(int problemNumber)
        {
            string className = string.Format(
                "EulerProblems.Lib.Problems.Euler{0}", problemNumber.ToString("000#"));
            Type type = Type.GetType(className);
            object instance = Activator.CreateInstance(type);
            return (Euler)instance;
        }
    }
}
