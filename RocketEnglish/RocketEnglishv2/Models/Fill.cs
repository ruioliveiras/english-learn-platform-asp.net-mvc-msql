using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketEnglishv2.Models
{
    public class Fill : Question
    {
        public Fill() { }
        public Fill(int pId, string pText, string pTip, string pAnswer, string pCorrect, string pImage, string pVideo)
            : base(pId, pText, pTip, pAnswer, pCorrect, pImage, pVideo) { }
    }
}