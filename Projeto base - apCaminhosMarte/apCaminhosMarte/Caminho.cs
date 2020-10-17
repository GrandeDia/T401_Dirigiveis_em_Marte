using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace apCaminhosMarte
{
    class Caminho : IComparable<Caminho>
    {
        /*
         * 19164 - Bruno Arnone franchi
         * 19188 - Mateus Stolze Vazquez 
         */
        private int idCidadeOrigem, idCidadeDestino, distancia, tempo, custo;


        public Caminho()
        {
        }
        public Caminho(string caminhoLinha)
        {
            if (caminhoLinha == null || caminhoLinha == "")
                throw new Exception("O caminho não pode ser nulo");
            idCidadeOrigem = int.Parse(caminhoLinha.Substring(0, 3).Trim());
            idCidadeDestino = int.Parse(caminhoLinha.Substring(3, 3).Trim());
            distancia = int.Parse(caminhoLinha.Substring(6, 5));
            tempo = int.Parse(caminhoLinha.Substring(11, 4));
            custo = int.Parse(caminhoLinha.Substring(15, 5));
        }

        /*public Caminho(int idCidadeOrigem, int idCidadeDestino, int distancia, int tempo, int custo)
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
        }*/

        public Caminho(Caminho caminho)
        {
            caminho.idCidadeDestino = this.idCidadeDestino;
            caminho.idCidadeOrigem = this.idCidadeOrigem;
            caminho.distancia = this.tempo;
        }

        public int getIdOrigem()
        {
            return idCidadeOrigem;
        }

        public int getIdDestino()
        {
            return idCidadeDestino;
        }
        //concertar depois
        public int CompareTo(Caminho obj)
        {
            return 1;
        }
        public int Tempo
        {
            get => this.tempo;
        }
    }
}
