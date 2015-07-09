using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using RocketEnglishv2.DAO;
using WebGrease.Css.Extensions;

namespace RocketEnglishv2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GlobalLvl { get; set; }
        public int MaxDailyLogin { get; set; }
        public int CurrentDailyLogin { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Tutorial { get; set; }
        public Dictionary<int,List<CategoryLevel>> Categories {get;private set;}

        public User() : this(0, "name", "username", "email", "password", 0, 0, 0, DateTime.Now, false) { }
        public User(int id, string name, string username, string email, string password)
            : this(id, name, username, email, password, 0, 0, 0, DateTime.Now, false) { }
        public User(int id, string name, string username, string email, string password, int globalLvl, int maxDailyLogin, int currentDailyLogin,
            DateTime lastLogin, bool tutorial)
        {
            Id = id;
            Name = name;
            Username = username;
            Email = email;
            Password = password;
            GlobalLvl = globalLvl;
            MaxDailyLogin = maxDailyLogin;
            CurrentDailyLogin = currentDailyLogin;
            LastLogin = lastLogin;
            Tutorial = tutorial;
            Categories = new Dictionary<int, List<CategoryLevel>>();

        }

        public void loadCategories()
        {
            RocketEnglish a = new RocketEnglish();
            // do not reload
            Categories.Clear();
                this.Categories = a.NivelCategoria.Join(
                     a.Categoria,
                     x => x.idCategoria,
                     x => x.id,
                     (c, n) => new { idCategoria = c.idCategoria, nome = n.nome, nivel = c.nivel, desb = c.idDesbloqueavel }
                 ).GroupJoin(
                     a.UtilizadorNivelCategoria.Where(A => A.idUtilizador == this.Id),
                     x => new { x.idCategoria, x.nivel },
                     x => new { x.idCategoria, x.nivel},
                     (c, n2) => new { id = c.idCategoria, nome = c.nome, nivel = c.nivel, perfect = n2, desb = c.desb }
                 )
                 .GroupBy(c => c.id)
                 .Select(x => x.ToList())
                 .ToList()
                 .Select(l =>
                     l.Select(c => new CategoryLevel(
                             c.id,
                             c.nome,
                             c.nivel,
                             (c.perfect != null && (c.perfect.Count() > 0)) ? ((c.perfect.First().testePerfeito) ? 2 : 1) : 0,
                             c.desb
                         )
                     ).ToList()
                 )
                 .ToDictionary(x => x.First().CategoryId);


                //*/
                SetLessonInfoStatus();
        }

        public GlobalTest createGlobalTest()
        {
            GlobalTest gt = new GlobalTest();

            gt.pollQuestions();

            return gt;
        }

        private void SetLessonInfoStatus()
        {
            RocketEnglish rem = new RocketEnglish();

            List<int> doneLessons = rem.Utilizador.Where(b => b.id == Id).Select(b => b.Licao).First().Select(x => x.id).ToList();

            foreach (List<CategoryLevel> catLevels in Categories.Values)
            {
                foreach (CategoryLevel cl in catLevels)
                {
                    foreach (LessonInfo li in cl.LessonInfos.Values)
                    {
                        if(doneLessons.Contains(li.Id))
                            li.Status = 1;
                    }
                }
            }
        }

        //returns ratio (Completed*(lessons+tests))/Available*(lessons/tests)
        public int getTotalProgress()
        {
            float total = 0;
            //GlobalLevel indicates number of sucessfully completed global tests
            float done = GlobalLvl;

            RocketEnglish re = new RocketEnglish();

            foreach (List<CategoryLevel> catLevels in Categories.Values)
            {
                foreach (CategoryLevel cl in catLevels)
                {
                    total++;

                    if( re.NivelCategoria.Find(cl.Level,cl.CategoryId) != null)
                        done++;

                    foreach (LessonInfo li in cl.LessonInfos.Values)
                    {
                        total++;

                        if (li.Status == 1)
                            done++;
                    }
                }
            }

            return (int)(done/total);
        }

        public Unlockable GetHead()
        {
            RocketEnglish re = new RocketEnglish();
            int headId = re.TipoDesbloqueavel.Single(b => b.nome == "Head").id;

            Desbloqueavel d = re.Utilizador
                                        .Where(b => b.id == Id)
                                        .Select(b => b.Desbloqueavel)
                                        .First()
                                        .Where(b => b.tipo == headId)
                                        .First();

            return new Unlockable(d.id, d.imagem, d.nome, "Head");
        }

        public void SetHead(string name)
        {
            RocketEnglish re = new RocketEnglish();
            int headId = re.TipoDesbloqueavel.Single(b => b.nome == "Head").id;
            Utilizador u = re.Utilizador.Where(b => b.id == Id).First();
            Desbloqueavel oldHead = re.Utilizador
                               .Where(b => b.id == Id)
                               .Select(b => b.Desbloqueavel)
                               .First()
                               .Where(b => b.tipo == headId)
                               .First();
            Desbloqueavel newHead = re.Desbloqueavel
                                .Where(d => d.nome == name)
                                .Where(b => b.tipo == headId)
                                .First();

            u.Desbloqueavel.Remove(oldHead);
            u.Desbloqueavel.Add(newHead);
            re.SaveChanges();
        }

        public void SetBody(string name)
        {
            RocketEnglish re = new RocketEnglish();
            int bodyId = re.TipoDesbloqueavel.Single(b => b.nome == "Body").id;
            Utilizador u = re.Utilizador.Where(b => b.id == Id).First();
            Desbloqueavel oldHead = re.Utilizador
                               .Where(b => b.id == Id)
                               .Select(b => b.Desbloqueavel)
                               .First()
                               .Where(b => b.tipo == bodyId)
                               .First();
            Desbloqueavel newHead = re.Desbloqueavel
                                .Where(d => d.nome == name)
                                .Where(b => b.tipo == bodyId)
                                .First();

            u.Desbloqueavel.Remove(oldHead);
            u.Desbloqueavel.Add(newHead);
            re.SaveChanges();
        }


        public Unlockable GetBody()
        {
            RocketEnglish re = new RocketEnglish();
            int headId = re.TipoDesbloqueavel.Single(b => b.nome == "Body").id;

            Desbloqueavel d = re.Utilizador
                                        .Where(b => b.id == Id)
                                        .Select(b => b.Desbloqueavel)
                                        .First()
                                        .Where(b => b.tipo == headId)
                                        .First();

            return new Unlockable(d.id, d.imagem, d.nome, "Body");
        }

        public List<Unlockable> GetAvailableHeads()
        {
            RocketEnglish re = new RocketEnglish();
            List<Unlockable> uL = new List<Unlockable>();
            int headId = re.TipoDesbloqueavel.Single(b => b.nome == "Head").id;

           uL = re.UtilizadorNivelCategoria
                   .Where(u => u.idUtilizador == Id)
                   .Select(u => u.NivelCategoria.Desbloqueavel)
                   .Where(b => b.tipo == headId)
                   .ToList()
                   .Select(u => new Unlockable(u.id, u.imagem, u.nome, "Head"))
                   .ToList();


            uL.AddRange(re.Desbloqueavel
                .Where(u => u.nome == "head")
                .ToList()
                .Select(u => new Unlockable(u.id, u.imagem, u.nome, "Head"))
                .ToList());

            return uL;
        }

        public List<Unlockable> GetAvailableBodys()
        {
            RocketEnglish re = new RocketEnglish();
            List<Unlockable> uL = new List<Unlockable>();
            int bodyId = re.TipoDesbloqueavel.Single(b => b.nome == "Body").id;

            uL = re.UtilizadorNivelCategoria
                    .Where(u => u.idUtilizador == Id)
                    .Select(u => u.NivelCategoria.Desbloqueavel)
                    .Where(b => b.tipo == bodyId)
                    .ToList()
                    .Select(u => new Unlockable(u.id, u.imagem, u.nome, "Body"))
                    .ToList();


            uL.AddRange(re.Desbloqueavel
                .Where(u => u.nome == "torso")
                .ToList()
                .Select(u => new Unlockable(u.id, u.imagem, u.nome, "Body"))
                .ToList());
            return uL;
        }

        public List<int> getAvailableUnlockables()
        {
            RocketEnglish re = new RocketEnglish();
            List<int> uList = re.Utilizador.Where(b => b.id == this.Id)
                .Select(b => b.Desbloqueavel)
                .First()
                .Select(x => x.id)
                .ToList();
            return uList;
        }

        public string[] UnlockableJokes()
        {
            RocketEnglish re = new RocketEnglish();
            Random rnd = new Random();



            string[] ret = re.Utilizador
                .Where(b => b.id == this.Id)
                 .Select(b => b.Desbloqueavel)
                 .Select(b => b.ToList())
                 .ToList()
                 .SelectMany(b => b)
                 .Select(x => x.Dialogo)
                 .SelectMany(x => x)
                 .Select(x => x.dialogo1)
                 .ToArray();

            return ret;
        }

        public int getPerfectTestNumber()
        {
            int cnt = 0;
            RocketEnglish re = new RocketEnglish();

            List<bool> cTests = re.UtilizadorNivelCategoria.Where(b => b.idUtilizador == Id).Select(b => b.testePerfeito).ToList();
            foreach (bool b in cTests)
            {
                if(b)
                    cnt++;
            }

            return cnt;
        }
    }
}