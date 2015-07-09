using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RocketEnglishv2.DAO;
using System.IO;

namespace RocketEnglishv2.Services
{
    public class InstallService
    {
        private string contentFolder;

        public InstallService(string contentFolder)
        {
            this.contentFolder = contentFolder;
        }

        public void DBClean()
        {
            RocketEnglish a = new RocketEnglish();
            //a.UtilizadorPerguntaEscolhaMultipla.ToList().RemoveAll(x => true);
            a.Database.ExecuteSqlCommand("delete from UtilizadorPerguntaEscolhaMultipla");
            a.Database.ExecuteSqlCommand("delete from UtilizadorLicao");
            a.Database.ExecuteSqlCommand("delete from UtilizadorPerguntaPalavra");
            a.Database.ExecuteSqlCommand("delete from UtilizadorNivelCategoria");
            a.Database.ExecuteSqlCommand("delete from UtilizadorDesbloqueavel");
            a.Database.ExecuteSqlCommand("delete from Utilizador");
            a.Database.ExecuteSqlCommand("delete from PerguntaPalavra");
            a.Database.ExecuteSqlCommand("delete from PerguntaEscolhaMultipla");
            a.Database.ExecuteSqlCommand("delete from Licao");
            a.Database.ExecuteSqlCommand("delete from NivelCategoria");
            a.Database.ExecuteSqlCommand("delete from Categoria");
            a.Database.ExecuteSqlCommand("delete from Dialogo");
            a.Database.ExecuteSqlCommand("delete from Desbloqueavel");
            a.Database.ExecuteSqlCommand("delete from TipoDesbloqueavel");
            a.SaveChanges();
        }

        public void DBLoadSample()
        {
            RocketEnglish a = new RocketEnglish();

            // ###################   TipoDesbloqueavel ###################################
            TipoDesbloqueavel head = new TipoDesbloqueavel();
            head.id = 1; head.nome = "Head";
            TipoDesbloqueavel body = new TipoDesbloqueavel();
            body.id = 2; body.nome = "Body";

            a.TipoDesbloqueavel.Add(head);
            a.TipoDesbloqueavel.Add(body);

            TipoDesbloqueavel[] tipoArray = { head, body };
            a.SaveChanges();

            // ###################   Utilizador ###################################
            Utilizador u = new Utilizador();
            u.nome = "Miguel Barroso";
            u.email = "mb@rocketenglish.pt";
            u.username = "mbarroso";
            u.passsword = "moedas123";
            u.tutorialFeito = false;
            u.nivelGlobal = 1;
            u.loginSeguidosAtual = 0;
            u.loginSeguidosMaximo = 0;
            u.dataUltimoLogin = DateTime.Now;

            a.Utilizador.Add(u);

            a.SaveChanges();

            // ###################   Desbloqueaveis ###################################
            Desbloqueavel defaultHead = new Desbloqueavel();
            defaultHead.nome = "head";
            defaultHead.imagem = "~/Content/avatar/images_high/head.png";
            defaultHead.tipo = tipoArray[0].id;

            Desbloqueavel defaultBody = new Desbloqueavel();
            defaultBody.nome = "torso";
            defaultBody.imagem = "~/Content/avatar/images_high/torso.png";
            defaultBody.tipo = tipoArray[1].id;

            u.Desbloqueavel.Add(defaultHead);
            u.Desbloqueavel.Add(defaultBody);

            a.SaveChanges();

            // #########################   Dialogos ###################################
            Dialogo jk1 = new Dialogo();
            jk1.dialogo1 = "B to the O to the double T to the Y! That's BOTTY for you!";
            jk1.idDesbloqueavel = a.Desbloqueavel.Single(b => b.nome == "head").id;
            Dialogo jk2 = new Dialogo();
            jk2.dialogo1 = "Fetching auxiliary english modules...\nLoading...\nDone";
            jk2.idDesbloqueavel = a.Desbloqueavel.Single(b => b.nome == "head").id;
            Dialogo jk3 = new Dialogo();
            jk3.dialogo1 = "This is going to be fun! SO FUN WEEEEE!";
            jk3.idDesbloqueavel = a.Desbloqueavel.Single(b => b.nome == "head").id;
            Dialogo jk4 = new Dialogo();
            jk4.dialogo1 = "Hmm I think I need to oil up.";
            jk4.idDesbloqueavel = a.Desbloqueavel.Single(b => b.nome == "torso").id;
            Dialogo jk5 = new Dialogo();
            jk5.dialogo1 = "Of all the friends I have, you are the first";
            jk5.idDesbloqueavel = a.Desbloqueavel.Single(b => b.nome == "torso").id;
            Dialogo jk6 = new Dialogo();
            jk6.dialogo1 = "Well, I don't know about you, but I love me some new English vocabulary!";
            jk6.idDesbloqueavel = a.Desbloqueavel.Single(b => b.nome == "torso").id;

            a.Dialogo.Add(jk1);
            a.Dialogo.Add(jk2);
            a.Dialogo.Add(jk3);
            a.Dialogo.Add(jk4);
            a.Dialogo.Add(jk5);
            a.Dialogo.Add(jk6);

            a.SaveChanges();
        }

        public void UserDoLessons()
        {
            RocketEnglish re = new RocketEnglish();
            Utilizador u = re.Utilizador.First();
            UtilizadorNivelCategoria nc1 = new UtilizadorNivelCategoria();
            UtilizadorNivelCategoria nc2 = new UtilizadorNivelCategoria();

            nc1.Utilizador = u;
            nc2.Utilizador = u;

            nc1.NivelCategoria = re.NivelCategoria
                .Where(n => n.Categoria.nome == "Grammar")
                .First();
            nc2.NivelCategoria = re.NivelCategoria
                .Where(n => n.Categoria.nome == "Comprehension")
                .First();

            re.UtilizadorNivelCategoria.Add(nc1);
            re.UtilizadorNivelCategoria.Add(nc2);

            foreach(Licao l in nc1.NivelCategoria.Licao){
                u.Licao.Add(l);
            }
            foreach (Licao l in nc2.NivelCategoria.Licao)
            {
                u.Licao.Add(l);
            }
            
            re.SaveChanges();
        }
    }
}
