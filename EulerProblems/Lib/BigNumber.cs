using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProblems.Lib
{
    public class BigNumber : IComparable
    {
        public int[] digits;
        /// <summary>
        /// the count of digits that are to the right of the decimal
        /// </summary>
        public int decimalDigitCount;
        public bool isPositive;
        public int orderOfMagnitude { get { return GetOrderOfMagnitude(); }  }
        public BigNumber(int[] digits)
        { 
            this.digits = digits;
            decimalDigitCount = 0;
            isPositive = true;
        }
        public BigNumber(int[] digits, int decimalDigitCount)
        {
            this.digits = digits;
            this.decimalDigitCount = decimalDigitCount;
            isPositive = true;
        }
        public BigNumber(int[] digits, int decimalDigitCount, bool isPositive)
        {
            this.digits = digits;
            this.decimalDigitCount = decimalDigitCount;
            this.isPositive = isPositive;
        }
        public BigNumber(long n)
        {
            ConvertFromLong(n);
        }
        /// <summary>
        /// converts a string of integers into a BigNumber
        /// Assumes positive and no decimals
        /// </summary>
        /// <param name="s"></param>
        public BigNumber(string s)
        {
            digits = new int[s.Length];
            for (int j = 0; j < s.Length; j++)
            {
                digits[j] = Int16.Parse(s[j].ToString());
            }
            isPositive = true;
            decimalDigitCount = 0;
        }
        public BigNumber(int n)
        {
            ConvertFromLong((long)n);
        }
        public int CompareTo(object? other)
        {
            /*
             * less than zero means that this should preceed other in an ascending sort
             * zero means the two are equal
             * greater than zero means that this should follow other in an ascending sort
             * */

            if (other == null) return 1;    // does null go first in ascending query?

            BigNumber otherBigNumber = (BigNumber)other;

            // check signs
            if (this.isPositive && !otherBigNumber.isPositive) return 1;
            if (!this.isPositive && otherBigNumber.isPositive) return -1;
            // check order of magnitude
            int thisOOM = this.orderOfMagnitude;
            int otherOOM = otherBigNumber.orderOfMagnitude;
            if (thisOOM > otherOOM) return 1;
            if (thisOOM < otherOOM) return -1;
            /*
             * check digits
             * find the "start" position and check left to right
             *  ex 1: { 0, 0, 1, 2, 3, 4 }; dec cnt is 0; oom is 3; 1s pos is 5; start pos is 2
             *  ex 2: { 6, 7, 8, 9, 1, 0 }; dec cnt is 2; oom is 3; 1s pos is 3; start pos is 0
             *  ex 3: { 0, 7, 8, 9, 1, 0 }; dec cnt is 4; oom is 0; 1s pos is 1; start pos is 1
             *  ex 4: { 0, 0, 1, 2, 3, 4 }; dec cnt is 5; oom is -2; 1s pos is 0; start pos is 0
             *  ex 5: { 0, 0, 1, 2, 3, 4 }; dec cnt is 4; oom is -1; 1s pos is 1; start pos is 1
             * */
            int onesPosThis = this.digits.Length - this.decimalDigitCount - 1;
            int startPosThis = (thisOOM < 0) ? onesPosThis : onesPosThis - thisOOM;
            
            int onesPosOther = otherBigNumber.digits.Length - otherBigNumber.decimalDigitCount - 1;
            int startPosOther = (otherOOM < 0) ? onesPosOther : onesPosOther - otherOOM;
            
            for(int i = 0; i < Math.Max(this.digits.Length, otherBigNumber.digits.Length); i++)
            {
                int thisValAtPos = ((startPosThis + i) > this.digits.Length -1) ?
                    0 : this.digits[startPosThis + i];

                int otherValAtPos = ((startPosOther + i) > otherBigNumber.digits.Length - 1) ?
                    0 : otherBigNumber.digits[startPosOther + i];

                if(thisValAtPos > otherValAtPos) return 1;
                if (thisValAtPos < otherValAtPos) return -1;
            }
            return 0;   // both are even
        }
        public override string ToString()
        {
            // if string is all zeros return "0"
            if (digits.Sum() == 0) return "0";

            StringBuilder sb = new StringBuilder();
            bool hasHitNonZero = false;
            for (int i = 0; i < this.digits.Length; i++)
            {
                int digit = this.digits[i];
                if (digit == 0)
                {
                    if (hasHitNonZero) sb.Append(digit.ToString());
                    
                    if(decimalDigitCount > 0 && decimalDigitCount == digits.Length - i - 1)
                    {
                        sb.Append(digit.ToString());
                        sb.Append(".");
                        hasHitNonZero = true;   // all zeros after a decimal are meaningful
                    }
                }
                else
                {
                    hasHitNonZero = true;
                    sb.Append(digit.ToString());
                    if (decimalDigitCount > 0 && decimalDigitCount == digits.Length - i - 1)
                    {
                        sb.Append(".");
                    }
                }                
            }
            return sb.ToString();
        }
        private void ConvertFromLong(long n)
        {
            int ordersOfMagnitudeToSupport = 12;
            List<int> digitsInReverse = new List<int>();
            for (int i = 0; i < ordersOfMagnitudeToSupport; i++)
            {
                if (n >= Math.Pow(10, i))
                {
                    digitsInReverse.Add(
                       (int)(Math.Floor(
                            n % Math.Pow(10, i + 1)
                            /
                            Math.Pow(10, i)
                            )));
                }
            }
            // now turn it to an array and reverse
            this.digits = digitsInReverse.ToArray().Reverse().ToArray();
            decimalDigitCount = 0;
            isPositive = (n > 0) ? true : false;
        }
        private int GetOrderOfMagnitude()
        {
            // go left to right and find
            // the first non-zero number. 
            for(int i = 0; i < digits.Length; i++)
            {
                if(digits[i] != 0)
                {
                    // { 0, 0, 4, 3, 9, 7 }
                    // total digits is 6
                    // first non-zero is i = 2
                    // OOM = 3
                    // OOM = total digits minus i minus 1
                    return digits.Length - i - 1 - decimalDigitCount;
                }
            }
            // the only way we can be here is if all numbers are zero
            return 0;
            
        }

    }
}
