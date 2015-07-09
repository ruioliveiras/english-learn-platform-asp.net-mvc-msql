using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketEnglishv2.Models
{
    public class MultipleChoice : Question
    {
        public string Wrong1 { get; set; }
        public string Wrong2 { get; set; }
        public string Wrong3 { get; set; }
        public string[] All { get { return new String[] {Correct,Wrong1, Wrong2, Wrong3 }; } }

        public MultipleChoice(string wa1, string wa2, string wa3)
        {
            Wrong1 = wa1;
            Wrong2 = wa2;
            Wrong3 = wa3;
        }

        public MultipleChoice(int pId, string pText, string pTip, string pAnswer, string pCorrect, string wa1, string wa2, string wa3)
            : base(pId, pText, pTip, pAnswer, pCorrect)
        {
            Wrong1 = wa1;
            Wrong2 = wa2;
            Wrong3 = wa3;
        }

        public MultipleChoice(int pId, string pText, string pTip, string pAnswer, string pCorrect, string pImage, string pVideo,
            string wa1, string wa2, string wa3)
            : base(pId, pText, pTip, pAnswer, pCorrect, pImage, pVideo)
        {
            Wrong1 = wa1;
            Wrong2 = wa2;
            Wrong3 = wa3;
        }

        public MultipleChoice(MultipleChoice mc) : base(mc)
        {
            Wrong1 = mc.Wrong1;
            Wrong2 = mc.Wrong2;
            Wrong3 = mc.Wrong3;
        }
    }
}