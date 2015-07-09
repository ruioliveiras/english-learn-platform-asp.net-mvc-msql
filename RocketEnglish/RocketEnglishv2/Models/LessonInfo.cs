using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using RocketEnglishv2.DAO;

namespace RocketEnglishv2.Models
{
    public class LessonInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //LessonInfo status: 0-Available 1-Done
        public int Status { get; set; }

        public LessonInfo() : this(0,"","",0) { }

        public LessonInfo(int pId, string pName, string pDescription, int pStatus)
        {
            Id = pId;
            Name = pName;
            Description = pDescription;
            Status = pStatus;
        }

        //returns corresponding Lesson Object
        public Lesson fetchLesson()
        {
            RocketEnglish rem = new RocketEnglish();
            Licao lDB = rem.Licao.Single(b => b.id == Id);

            Lesson l = new Lesson(lDB.id,lDB.nivel,lDB.descricao,lDB.nome);
            l.pollQuestions();

            return l;
        }

        public bool IsAvailable() { return Status == 0; }
        public bool IsDone() { return Status == 1; }
    }
}