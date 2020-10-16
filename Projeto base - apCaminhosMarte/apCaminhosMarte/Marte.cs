using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace apCaminhosMarte
{
    class Marte
    {
        private string cidadeMarteArquivo, 
            caminhosEntreMarteArquivo;

        private Caminho[,] MatrizAdjacenteDeCaminhos;
        private ArvoreDeBusca<Cidade> Cidades;


        public Marte(string cidadeMarteArquivo, string caminhosEntreMarteArquivo)
        {
            if (cidadeMarteArquivo.Trim() == null || caminhosEntreMarteArquivo.Trim() == null)
                throw new Exception("Nome do arquivo null");

            if (cidadeMarteArquivo.Trim().Equals("") || caminhosEntreMarteArquivo.Trim().Equals(""))
                throw new Exception("Nome do arquivo vazio");

            this.cidadeMarteArquivo = cidadeMarteArquivo;
            this.caminhosEntreMarteArquivo = caminhosEntreMarteArquivo;

            StreamReader LeitorCidade = new StreamReader(cidadeMarteArquivo);
            StreamReader LeitorCaminho = new StreamReader(cidadeMarteArquivo);

            int numeroDeCidades = 0;
            while (!LeitorCidade.EndOfStream)
            {
                string linha = LeitorCidade.ReadLine();
                Cidade cidade = new Cidade(linha);
                Cidades.Incluir(cidade);
                numeroDeCidades++;
            }

            
            MatrizAdjacenteDeCaminhos = new Caminho[numeroDeCidades, numeroDeCidades];

            while (!LeitorCaminho.EndOfStream)
            {
                string linha = LeitorCaminho.ReadLine();
                Caminho caminho = new Caminho(linha);
                MatrizAdjacenteDeCaminhos[caminho.getIdDestino(), caminho.getIdDestino()] = caminho;
            }
        }
    }
}
