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

        private Caminho[,] matrizAdjacenteDeCaminhos;
        private ArvoreDeBusca<Cidade> cidades;


        public Marte(string cidadeMarteArquivo, string caminhosEntreMarteArquivo)
        {
            if (cidadeMarteArquivo.Trim() == null || caminhosEntreMarteArquivo.Trim() == null)
                throw new Exception("Nome do arquivo null");

            if (cidadeMarteArquivo.Trim().Equals("") || caminhosEntreMarteArquivo.Trim().Equals(""))
                throw new Exception("Nome do arquivo vazio");

            this.cidadeMarteArquivo = cidadeMarteArquivo;
            this.caminhosEntreMarteArquivo = caminhosEntreMarteArquivo;

            StreamReader LeitorCidade = new StreamReader(cidadeMarteArquivo);
            StreamReader LeitorCaminho = new StreamReader(caminhosEntreMarteArquivo);

            int numeroDeCidades = 0;
            cidades = new ArvoreDeBusca<Cidade>();
            while (!LeitorCidade.EndOfStream)
            {
                string linha = LeitorCidade.ReadLine();
                Cidade cidade = new Cidade(linha);
                cidades.Incluir(cidade);
                numeroDeCidades++;
            }

            
            matrizAdjacenteDeCaminhos = new Caminho[numeroDeCidades, numeroDeCidades];

            while (!LeitorCaminho.EndOfStream)
            {
                string linha = LeitorCaminho.ReadLine();
                Caminho caminho = new Caminho(linha);
                MatrizAdjacenteDeCaminhos[caminho.getIdOrigem(), caminho.getIdDestino()] = caminho;
            }
        }
        public Caminho[ ,] MatrizAdjacenteDeCaminhos
        {
            get => this.matrizAdjacenteDeCaminhos;
        }
        /*public string GetNomeCidade(int idCidade)
        {
            if (idCidade < 0)
                throw new Exception("O id da cidade nao pode ser menor que 0");
            //cidades.
        }*/
    }
}
