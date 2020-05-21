using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace StockbookTickerList
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = string.Empty;
            
            List<Acao> listaAcao = new List<Acao>();
            Acao a = new Acao();
            url = "https://www.fundamentus.com.br/detalhes.php";
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            web.CacheOnly = false;
            web.CachePath = null;
            web.UsingCache = false;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(url);

            int i = 0;
            
            foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//table[@id='test1']/tbody/tr/td"))
            {
                
                if (i == 0)
                {
                    a = new Acao();
                    a.papel = row.InnerText;
                    i++;
                }
                else if (i == 1)
                {
                    a.nomeComercial = row.InnerText;
                    i++;
                }
                else if (i == 2)
                {
                    a.razaoSocial = row.InnerText;
                    listaAcao.Add(a);
                    i = 0;
                }

                Console.WriteLine(row.InnerText);
            }

            Console.ReadLine();

            string path = @"C:\Users\Yuri\Desktop\Stockbook\SQL\v2\listaAcoes.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (var item in listaAcao)
                    {
                        //sw.WriteLine("INSERT INTO tb_empresa (nome_comercial, razao_social) VALUES('" + item.nomeComercial + "', '" + item.razaoSocial + "')");
                        //sw.WriteLine("INSERT INTO tb_acao (ticker, id_empresa) VALUES('" + item.papel + "', (SELECT id_empresa FROM tb_empresa WHERE razao_social = '" + item.razaoSocial + "'))");
                    }
                }
            }
        }
    }

    public class Acao
    {
        public string papel { get; set; }
        public string nomeComercial { get; set; }
        public string razaoSocial { get; set; }
    }
}
