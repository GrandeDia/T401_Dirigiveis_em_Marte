﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class MarteGPS
    {
        private int idOrigem, idDestino;
        private Caminho[,] matrizAdjacenteDeCaminhos;
        private bool[] passamosAqui;
        private List<List<Caminho>> caminhos;
        private List<Caminho> aux;

        public MarteGPS(int idOrigem, int idDestino, Caminho[,] matrizAdjacenteDeCaminhos)
        {
            if (idOrigem < 0 && idDestino < 0 && matrizAdjacenteDeCaminhos != null)
                throw new Exception("Erro, um ou mais parametros nullos");

            this.idOrigem = idOrigem;
            this.idDestino = idDestino;
            this.matrizAdjacenteDeCaminhos = matrizAdjacenteDeCaminhos;
            this.passamosAqui = new bool[matrizAdjacenteDeCaminhos.GetLength(0)];
            this.caminhos = new List<List<Caminho>>();
            this.aux = new List<Caminho>();
        }

        public List<List<Caminho>> EncontrarCaminhos()
        {
            EncontrarCaminhosRepeticao(idOrigem);

            if (this.caminhos.Count() == 0)
                throw new Exception("Não há caminhos");

            return caminhos;
        }

        private void EncontrarCaminhosRepeticao(int idCidadeAtual)
        {
            for (int i = 0; i < matrizAdjacenteDeCaminhos.GetLength(0); i++)
            {
                Caminho caminho = this.matrizAdjacenteDeCaminhos[idCidadeAtual, i];
                if(caminho != null && !passamosAqui[i])
                {
                    aux.Add(caminho);

                    if (i == this.idDestino)
                        caminhos.Add(aux.Select(item => (Caminho) item.Clone()).ToList());
                    else
                    {
                        passamosAqui[caminho.GetIdDestino] = true;
                        EncontrarCaminhosRepeticao(caminho.GetIdDestino);
                        passamosAqui[caminho.GetIdDestino] = false;
                    }
                    aux.RemoveAt(aux.Count - 1);
                }    
            }
        }
    }
}
