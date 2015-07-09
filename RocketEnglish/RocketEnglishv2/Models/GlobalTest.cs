using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RocketEnglishv2.DAO;

namespace RocketEnglishv2.Models
{
    public class GlobalTest
    {
        public int Level { get; set; }
        public List<Question> questions {get; private set;}

        public GlobalTest() : this(0)
        {
        }

        public GlobalTest(int lvl)
        {
            questions = new List<Question>();
            Level = lvl;
        }

        public void pollQuestions()
        {
            RocketEnglish rem = new RocketEnglish();

            List<Licao> lessons = rem.Licao.Where(b => b.nivel == Level).ToList();

            List<PerguntaEscolhaMultipla> mcq = new List<PerguntaEscolhaMultipla>();
            List<PerguntaPalavra> fq = new List<PerguntaPalavra>();
            foreach (Licao l in lessons)
            {
                mcq.AddRange(rem.PerguntaEscolhaMultipla.Where(b => b.idLicao == l.id).ToList());
                fq.AddRange(rem.PerguntaPalavra.Where(b => b.idLicao == l.id).ToList());
            }

            foreach (PerguntaEscolhaMultipla pem in mcq)
            {
                questions.Add(new MultipleChoice(pem.id, pem.pergunta, pem.dica, "", pem.resposta,
                    pem.imagem, pem.video, pem.errada1, pem.errada2, pem.errada3));
            }
            foreach (PerguntaPalavra pem in fq)
            {
                questions.Add(new Fill(pem.id, pem.pergunta, pem.dica, "", pem.resposta,pem.imagem,pem.video));
            }

            //selects 25 questions for the test
            /*Random seed = new Random();
            for (int i = 0, r = 0; i < 25; i++)
            {
                r = seed.Next(i);
                questions.Add(new MultipleChoice(mcq[r].pergunta, mcq[r].dica, "", mcq[r].resposta,
                    mcq[r].imagem, mcq[r].video, mcq[r].errada1, mcq[r].errada2, mcq[r].errada3));
                r = seed.Next(i);
                questions.Add(new Fill(fq[r].pergunta, fq[r].dica, "", fq[r].resposta));
            }*/

            //randomize question order
            randomizeQuestions();
        }

        public void randomizeQuestions()
        {
            Random rnd = new Random();
            int ind1, ind2;
            int listSize = questions.Count;

            for (int i = 0; i < 5; i++)
            {
                ind1 = rnd.Next(listSize);

                do { ind2 = rnd.Next(listSize); }
                while (ind1 == ind2);

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

        public bool saveGlobalTestResults(int userId)
        {
            int correctCount = 0;
            bool isCompleted = false;

            RocketEnglish re = new RocketEnglish();

            foreach (Question q in questions)
            {
                correctCount += (q.isCorrect()) ? 1 : 0;

                if (q.GetType() == typeof(MultipleChoice))
                    processMCQ(re, q, userId);
                else
                    processFQ(re, q, userId);
            }

            isCompleted = (correctCount > questions.Count*0.4) ? true : false;

            if (isCompleted)
            {
                Utilizador u = re.Utilizador.Find(userId);
                if (u.nivelGlobal < Level + 1)
                {
                    // when the user completes the test of level 1 earn the level 2
                    u.nivelGlobal = Level + 1;
                    re.Entry(u).CurrentValues.SetValues(u);
                }
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