using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketEnglishv2.Models
{
    public abstract class Question
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Tip { get; set; }
        public string Answer { get; set; }
        public string Correct { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }

        public Question() : this(0, "", "", "", "", "", "") { }
        public Question(int pId, string pText, string pTip, string pAnswer, string pCorrect) : this(pId, pText, pTip, pAnswer, pCorrect, "", "") { }
        public Question(Question q) : this(q.ID, q.Text, q.Tip, q.Answer, q.Correct, "", "") { }
        public Question(int pId, string pText, string pTip, string pAnswer, string pCorrect, string pImage, string pVideo)
        {
            ID = pId;
            Text = pText;
            Tip = pTip;
            Answer = pAnswer;
            Correct = pCorrect;
            Image = pImage;
            Video = pVideo;
        }

        public bool isCorrect()
        {
            return Answer.Equals(Correct, StringComparison.Ordinal);
        }

        public void answer(string a)
        {
            Answer = a;
        }
    }


}