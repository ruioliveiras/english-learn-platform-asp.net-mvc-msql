using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketEnglishv2.Models
{
    public class Unlockable
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> Dialog;

        public Unlockable() : this(0,"","","") { }

        public Unlockable(int pId, string pImage, string pName, string pType)
        {
            Id = pId;
            Image = pImage;
            Name = pName;
            Type = pType;
        }
        public static Dictionary<int, Unlockable> FindAll()
        {
            DAO.RocketEnglish a = new DAO.RocketEnglish();
            
            return a.Desbloqueavel.Join(
                a.TipoDesbloqueavel,
                x => x.tipo,
                x => x.id,
                (d, td) => new { id = d.id, image = d.imagem, nome = d.nome, type = td.nome }
            )
            .ToList()
            .Select(x => new Unlockable(x.id, x.image, x.nome, x.type))
            .ToDictionary(x => x.Id);
        }
    }
}
