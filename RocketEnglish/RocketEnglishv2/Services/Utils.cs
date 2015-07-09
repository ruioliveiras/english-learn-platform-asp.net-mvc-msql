using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketEnglishv2.Services
{
    public class Utils
    {
        private static Random random = new Random();
        public static IEnumerable<T> shuffle<T>(IEnumerable<T> sequence)
        {
            T[] retArray = sequence.ToArray();


            for (int i = 0; i < retArray.Length - 1; i += 1)
            {
                int swapIndex = random.Next(i, retArray.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
        }
    }
}