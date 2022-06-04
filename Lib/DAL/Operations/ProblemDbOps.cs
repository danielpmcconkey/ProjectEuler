using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib.DAL.Operations
{
    public static class ProblemDbOps
    {
        public static Problem[] ReadSolved()
        {
            using (var context = new EulerContext())
            {
                return ReadSolved(context);
            }
        }
        public static Problem[] ReadSolved(EulerContext existingContext)
        {            
            return existingContext.Problems
                .Where(p => p.solution != null)
                .OrderBy(x => x.id)
                .ToArray();            
        }
    }
}
