using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using RocketEnglishv2.DAO;

namespace RocketEnglishv2.Models
{
    public class DBexpander
    {

        public void processFile(string filename)
        {
            RocketEnglish re = new RocketEnglish();

            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    switch (fields[0])
                    {
                        case "Categoria":               addCategory(re,fields);
                                                        break;

                        case "TipoDesbloqueavel":       addUnlockableType(re, fields);
                                                        break;

                        case "Desbloqueavel":           addUnlockable(re,fields);
                                                        break;

                        case "Dialogo":                 addUnlocakbleDiaglog(re, fields);
                                                        break;

                        case "Licao":                   addLesson(re,fields);
                                                        break;

                        case "NivelCategoria":          addCategoryLevel(re,fields);
                                                        break;
                        
                        case "PerguntaEscolhaMultipla": addMultiple(re,fields);
                                                        break;

                        case "PerguntaPalavra":         addFill(re,fields);
                                                        break;
                    }
                }
            }
        }

        //Categoria;nome;
        public void addCategory(RocketEnglish re, string[] fields)
        {
            Categoria c = new Categoria();
            c.nome = fields[1];

            re.Categoria.Add(c);

            re.SaveChanges();
        }

        //TipoDesbloqueavel;nome;
        public void addUnlockableType(RocketEnglish re, string[] fields)
        {
            TipoDesbloqueavel td = new TipoDesbloqueavel();
            td.nome = fields[1];

            re.TipoDesbloqueavel.Add(td);

            re.SaveChanges();
        }

        //Desbloqueavel;imagem;nome;nomeTipo
        public void addUnlockable(RocketEnglish re, string[] fields)
        {

            string aux = fields[3];
            int unTypeId = re.TipoDesbloqueavel.Single(b => b.nome == aux).id;
            
            Desbloqueavel d = new Desbloqueavel();
            d.imagem = fields[1];
            d.nome = fields[2];
            d.tipo = unTypeId;

            re.Desbloqueavel.Add(d);

            re.SaveChanges();
        }

        //Dialogo;dialogo;nomeDesbloqueavel
        public void addUnlocakbleDiaglog(RocketEnglish re, string[] fields)
        {
            string aux = fields[2];
            int dId = re.Desbloqueavel.Single(b => b.nome == aux).id;

            Dialogo d = new Dialogo();
            d.dialogo1 = fields[1];
            d.idDesbloqueavel = dId;

            re.Dialogo.Add(d);

            re.SaveChanges();
        }

        //NivelCategoria;nivel;nomeCategoria;nomeDesbloqueável
        public void addCategoryLevel(RocketEnglish re, string[] fields)
        {
            string aux = fields[2];
            int catId = re.Categoria.Single(b => b.nome == aux).id;
            aux = fields[3];
            int dblId = re.Desbloqueavel.Single(b => b.nome == aux).id;

            NivelCategoria cl = new NivelCategoria();
            cl.nivel = Convert.ToInt32(fields[1]);
            cl.idCategoria = catId;
            cl.idDesbloqueavel = dblId;

            re.NivelCategoria.Add(cl);

            re.SaveChanges();
        }

        //Licao;nivel;nome;descricao;nomeCategoria
        public void addLesson(RocketEnglish re, string[] fields)
        {
            int nivel = Convert.ToInt32(fields[1]);
            string aux = fields[4];
            int cId = re.Categoria.Single(b => b.nome == aux).id;

            Licao l = new Licao();
            l.nivel = nivel;
            l.nome = fields[2];
            l.descricao = fields[3];
            l.idCategoria = cId;

            re.Licao.Add(l);

            re.SaveChanges();
        }

        //PerguntaEscolhaMultipla;resposta;errada1;errada2;errada3;pergunta;nomeLicao;dica;imagem;video
        public void addMultiple(RocketEnglish re, string[] fields)
        {
            string aux = fields[6];
            int lId = re.Licao.Single(b => b.nome == aux).id;

            PerguntaEscolhaMultipla pem = new PerguntaEscolhaMultipla();
            pem.resposta = fields[1];
            pem.errada1 = fields[2];
            pem.errada2 = fields[3];
            pem.errada3 = fields[4];
            pem.pergunta = fields[5];
            pem.idLicao = lId;
            pem.dica = fields[7];
            pem.imagem = fields[8];
            pem.video = fields[9];

            re.PerguntaEscolhaMultipla.Add(pem);

            re.SaveChanges();
        }

        //PerguntaPalavra;pergunta;resposta;nomeLicao;dica;imagem;video
        public void addFill(RocketEnglish re, string[] fields)
        {
            string aux = fields[3];
            int lId = re.Licao.Single(b => b.nome == aux).id;

            PerguntaPalavra pp = new PerguntaPalavra();
            pp.pergunta = fields[1];
            pp.resposta = fields[2];
            pp.idLicao = lId;
            pp.dica = fields[4];
            pp.imagem = fields[5];
            pp.video = fields[6];

            re.PerguntaPalavra.Add(pp);

            re.SaveChanges();
        }

    }
}