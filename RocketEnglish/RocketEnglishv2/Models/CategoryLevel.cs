using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RocketEnglishv2.DAO;

namespace RocketEnglishv2.Models
{
    /// <summary>
    /// Note that attribute Status should have this values: 
    ///     Status = 2 done, and perfect;
    ///     Status = 1 done, but not perfect;
    ///     Status = 0 not done,
    /// </summary>
    public class CategoryLevel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
        public int Unlockable { get; set; }
        public Dictionary<int,LessonInfo>  LessonInfos { get; set; }

        public bool IsDone() { return Status > 0; }
        public bool IsPerfect() { return Status > 1; }
        public bool IsAvailable()
        {
            bool ret = true;
            foreach (var a in LessonInfos.Values)
            {
                ret = ret && a.IsDone();
            }
            return ret; ;
        }


        public CategoryLevel() : this(0,"categorylevel",0,0,0) { }
        public CategoryLevel(int pCategoryId, string pName) : this(pCategoryId, pName, 0, 0, 0) { }
        public CategoryLevel(int pCategoryId, string pName, int pLevel, int pStatus, int pUnlocakble)
        {
            CategoryId = pCategoryId;
            Name = pName;
            Level = pLevel;
            Status = pStatus;
            Unlockable = pUnlocakble;
            LessonInfos = new Dictionary<int, LessonInfo>();
            
            RocketEnglish rem = new RocketEnglish();
            List<Licao> licoes = rem.Licao
                .Where(b => b.nivel == Level)
                .Where(b => b.idCategoria == CategoryId)
                .ToList();
            foreach (Licao l in licoes)
            {
                LessonInfo linf = new LessonInfo(l.id, l.nome, l.descricao, 0);
                LessonInfos.Add(l.id,linf);
            }
        }

        public CategoryTest CreateCategoryTest()
        {
            CategoryTest ct = new CategoryTest(this);
            ct.pollQuestions(CategoryId, Level);
            return ct;
        }

        public static List<List<CategoryLevel>> findAll()
        {
            RocketEnglish rem = new RocketEnglish();
            List<List<CategoryLevel>> ret = rem.NivelCategoria.Join(
                rem.Categoria,
                x => x.idCategoria,
                x => x.id,
                (c, n) => new {id = c.idCategoria,nome = n.nome,nivel = c.nivel}
           ).GroupBy(c => c.id)
           .Select(x => x.ToList())
           .ToList()
           .Select(l => l.Select(c => new CategoryLevel(c.id,c.nome,c.nivel,0,0)).ToList())
           .ToList();

            //List<Licao> licoes = rem.Licao.Where(b => b.nivel == Level).Where(b => b.idCategoria == CategoryId).ToList();
            return ret;
        }

        public string getUnlockableName()
        {
            RocketEnglish re = new RocketEnglish();

            return re.Desbloqueavel.Find(Unlockable).nome;
        }
    }   
}