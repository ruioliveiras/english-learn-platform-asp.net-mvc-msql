using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RocketEnglishv2.DAO;

namespace RocketEnglishv2.Models
{
    public class CategoryTest
    {
        public CategoryLevel CategoryLevel { get; private set; }
        public List<Question> questions {get; private set;}

        public CategoryTest() : this(new CategoryLevel())
        {
            questions = new List<Question>(20);
        }

        public CategoryTest(CategoryLevel c)
        {
            this.CategoryLevel = c;
            questions = new List<Question>(20);
        }

        public void pollQuestions(int categoryId, int level)
        {
            RocketEnglish rem = new RocketEnglish();
            //gets corresponding lessons
            List<Licao> lessons = rem.Licao.Where(b => b.idCategoria == categoryId).Where(b => b.nivel == level).ToList();

            //polls questions from database for each lesson
            List<PerguntaEscolhaMultipla> mcq = new List<PerguntaEscolhaMultipla>();
            List<PerguntaPalavra> fq = new List<PerguntaPalavra>();
            foreach (Licao l in lessons)
            {
                mcq.AddRange(rem.PerguntaEscolhaMultipla.Where(b => b.idLicao == l.id-2).ToList());
                int i = l.id;
                fq.AddRange(rem.PerguntaPalavra.Where(b => b.idLicao == l.id-2).ToList());
            }
            foreach (PerguntaEscolhaMultipla pem in mcq)
            {
                questions.Add(new MultipleChoice(pem.id, pem.pergunta, pem.dica, "", pem.resposta,
                    pem.imagem, pem.video, pem.errada1, pem.errada2, pem.errada3));
            }
            foreach (PerguntaPalavra pem in fq)
            {
                questions.Add(new Fill(pem.id, pem.pergunta, pem.dica, "", pem.resposta, pem.imagem, pem.video));
            }

            //selects 20 questions for the test
            /*Random seed = new Random();
            for (int i = 0, r = 0; i < 20; i++)
            {
                r = seed.Next(i);
                questions.Add(new MultipleChoice(mcq[r].pergunta,mcq[r].dica,"",mcq[r].resposta,
                    mcq[r].imagem,mcq[r].video,mcq[r].errada1,mcq[r].errada2,mcq[r].errada3));
                r = seed.Next(i);
                questions.Add(new Fill(fq[r].pergunta,fq[r].dica,"",fq[r].resposta));
            }*/
            

            //randomize question order
            randomizeQuestions();
        }

        private void randomizeQuestions()
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

        private void questionSwap(int ind1, int ind2)
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
                if(!q.isCorrect())
                    wrongQuestions.Add(q);
            });
            return wrongQuestions;
        }

        public string getPrizeImagePath()
        {
            RocketEnglish re = new RocketEnglish();
            CategoryLevel cl = CategoryLevel;
            return re.Desbloqueavel.Find(CategoryLevel.Unlockable).imagem;
        }

        public bool saveCategoryTestResults(int userId)
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

            isCompleted = (correctCount > 6) ? true : false;

            if (isCompleted)
            {
                UtilizadorNivelCategoria unc = re.UtilizadorNivelCategoria.Find(userId,CategoryLevel.Level,CategoryLevel.CategoryId);
                if (unc == null)
                {
                    unc = new UtilizadorNivelCategoria();
                    unc.idUtilizador = userId;
                    unc.nivel = CategoryLevel.Level;
                    unc.idCategoria = CategoryLevel.CategoryId;
                    unc.testePerfeito = (correctCount == 10)?true:false;
                    re.UtilizadorNivelCategoria.Add(unc);

                    //adds corresponding avatar unlockable next
                    
                    int ulckID = re.NivelCategoria.Find(CategoryLevel.Level,CategoryLevel.CategoryId).idDesbloqueavel;
                    Desbloqueavel dsb = re.Desbloqueavel.Find(ulckID);
                    re.Utilizador.Find(userId).Desbloqueavel.Add(re.Desbloqueavel.Find(ulckID));
                    //re.Utilizador.Where(b => b.id == userId).Select(b => b.Desbloqueavel).First().Add(dsb);
                }
                else
                {
                    if(!unc.testePerfeito)
                        unc.testePerfeito = (correctCount == 10) ? true : false;
                    re.Entry(unc).CurrentValues.SetValues(unc);
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