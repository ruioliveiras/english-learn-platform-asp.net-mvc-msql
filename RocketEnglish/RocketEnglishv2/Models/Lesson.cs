using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using RocketEnglishv2.DAO;

namespace RocketEnglishv2.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Question> questions;

        public Lesson() : this(-1, -1, "No description", "Lesson name")
        {
        }

        public Lesson(int pId, int pLevel, string pDescription, string pName)
        {
            Id = pId;
            Level = pLevel;
            Description = pDescription;
            Name = pName;
            questions = new List<Question>();
        }

        public void pollQuestions()
        {
            RocketEnglish re = new RocketEnglish();

            List<PerguntaEscolhaMultipla> multipleChoiceQuestions = new List<PerguntaEscolhaMultipla>();
            List<PerguntaPalavra> fillQuestions = new List<PerguntaPalavra>();

            //polls questions from database
            multipleChoiceQuestions = re.PerguntaEscolhaMultipla
                .Where(b => b.idLicao == Id)
                .ToList();

            fillQuestions = re.PerguntaPalavra
                .Where(b => b.idLicao == Id)
                .ToList();


            //adds questions to questions List
            multipleChoiceQuestions.ForEach(delegate(PerguntaEscolhaMultipla pem)
            {
                questions.Add(new MultipleChoice(pem.id, pem.pergunta, pem.dica, "", pem.resposta, pem.imagem, pem.video, pem.errada1,
                    pem.errada2, pem.errada3));
            });

            fillQuestions.ForEach(delegate(PerguntaPalavra pp)
            {
                questions.Add(new Fill(pp.id, pp.pergunta, pp.dica, "", pp.resposta, pp.imagem, pp.video));
            });

            //randomize question order
            randomizeQuestions();
        }

        public void randomizeQuestions()
        {
            Random rnd = new Random();
            int ind1, ind2;
            int listSize = questions.Count;

            if (listSize <= 0)
            {
                throw new Exception("Lesson don't exist or don't has questions");
            }

            for (int i = 0; i < 5; i++)
            {
                ind1 = rnd.Next(listSize);

                do
                {
                    ind2 = rnd.Next(listSize);
                } while (ind1 == ind2);

                questionSwap(ind1, ind2);
            }
        }

        public void questionSwap(int ind1, int ind2)
        {
            Question tmpQ = questions[ind1];
            questions[ind1] = questions[ind2];
            questions[ind2] = tmpQ;
        }

        public List<Question> getWrongQuestions()
        {
            List<Question> wrongQuestions = new List<Question>();
            questions.ForEach(delegate(Question q)
            {
                if (!q.isCorrect())
                    wrongQuestions.Add(q);
            });
            return wrongQuestions;
        }

        public bool saveResults(int userId)
        {
            int correctCount = 0;
            bool isCompleted = false;

            RocketEnglish re = new RocketEnglish();

            foreach (Question q in questions)
            {
                correctCount += (q.isCorrect()) ? 1 : 0;

                if (q.GetType() == typeof (MultipleChoice))
                    processMCQ(re,q,userId);
                else
                    processFQ(re,q,userId);


            }

            isCompleted = (correctCount > 6) ? true : false;

            if (isCompleted)
            {
                re.Utilizador.Find(userId)
                  .Licao
                  .Add(re.Licao.Find(Id));
            }

            re.SaveChanges();

            return isCompleted;
        }

        private void processMCQ(RocketEnglish re, Question q, int userId)
        {
            UtilizadorPerguntaEscolhaMultipla upem = re.UtilizadorPerguntaEscolhaMultipla.Find(q.ID, userId);
            if (upem == null)
            {
                upem = new UtilizadorPerguntaEscolhaMultipla();
                upem.idPerguntaEscolhaMultipla = q.ID;
                upem.idUtilizador = userId;
                upem.correta = q.isCorrect();
                re.UtilizadorPerguntaEscolhaMultipla.Add(upem);
            }
            else
            {
                upem.correta = q.isCorrect();
                re.Entry(upem).CurrentValues.SetValues(upem);
            }

        }

        public void processFQ(RocketEnglish re, Question q, int userId)
        {
            UtilizadorPerguntaPalavra upp = re.UtilizadorPerguntaPalavra.Find(q.ID, userId);
            if (upp == null)
            {
                upp = new UtilizadorPerguntaPalavra();
                upp.idPerguntaPalavra = q.ID;
                upp.idUtilizador = userId;
                upp.correta = q.isCorrect();
                re.UtilizadorPerguntaPalavra.Add(upp);
            }
            else
            {
                upp.correta = q.isCorrect();
                re.Entry(upp).CurrentValues.SetValues(upp);   
            }
        }
    }
}