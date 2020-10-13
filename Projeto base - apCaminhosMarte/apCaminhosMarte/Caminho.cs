using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace apCaminhosMarte
{
    class Caminho
    {
        private int idCidadeOrigem,
                    idCidadeDestino,
                    distancia,
                    tempo,
                    custo,
            tamanhoCidadeO = 3,
            tamanhoCidadeD = 3,
            tamanhoDistancia = 5,
            tamanhoTempo = 4,
            tamanhoCusto = 5;

        public Caminho(string caminhoLinha)
        {
            idCidadeDestino = int.Parse(caminhoLinha.Substring(0, tamanhoCidadeO).Trim());
            idCidadeDestino = int.Parse(caminhoLinha.Substring(tamanhoCidadeO, tamanhoCidadeD).Trim());
            distancia = int.Parse(caminhoLinha.Substring(tamanhoCidadeO + tamanhoCidadeD, tamanhoDistancia));
            tempo = int.Parse(caminhoLinha.Substring(tamanhoCidadeO + tamanhoCidadeD + tamanhoDistancia, tamanhoTempo));
            custo = int.Parse(caminhoLinha.Substring(tamanhoCidadeO + tamanhoCidadeD + tamanhoDistancia + tamanhoTempo, tamanhoCusto));
        }

        public Caminho(int idCidadeOrigem, int idCidadeDestino, int distancia, int tempo, int custo)
        {
            if (idCidadeOrigem <= 0)
                throw new Exception("O Id da cidade origem não pode ser menor ou igual a 0");

            if (idCidadeDestino <= 0)
                throw new Exception("O Id da cidade destino não pode ser menor ou igual a 0");

            if (distancia <= 0)
                throw new Exception("A distancia não pode ser menor ou igual a 0");

            if (tempo <= 0)
                throw new Exception("O tempo não pode ser menor ou igual a 0");

            if (custo <= 0)
                throw new Exception("O custo não pode ser menor ou igual a 0");

            this.idCidadeOrigem = idCidadeOrigem;
            this.idCidadeDestino = idCidadeDestino;
            this.distancia = distancia;
            this.tempo = tempo;
            this.custo = custo;
        }
    }
}
