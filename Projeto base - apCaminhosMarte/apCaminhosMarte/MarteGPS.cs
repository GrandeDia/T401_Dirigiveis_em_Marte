using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class MarteGPS
    {
        private Cidade Origem, Destino;
        private Caminho[,] MatrizAdjacenteDeCaminhos;
        private bool[] PassamosAqui;
        private List<List<Caminho>> Caminhos;
        private List<Caminho> Aux;

        public MarteGPS(Cidade Origem, Cidade Destino, Caminho[,] MatrizAdjacenteDeCaminhos)
        {
            if (Origem != null && Destino != null && MatrizAdjacenteDeCaminhos != null)
                throw new Exception("Erro, um ou mais parametros nullos");

            this.Origem = Origem;
            this.Destino = Destino;
            this.MatrizAdjacenteDeCaminhos = MatrizAdjacenteDeCaminhos;
        }

        public List<List<Caminho>> EncontrarCaminhos()
        {
            Caminhos = new List<List<Caminho>>();

            EncontrarCaminhosRepeticao(Origem.GetIdCidade());

            if (this.Caminhos.Count() == 0)
                throw new Exception("Não há caminhos");

            return Caminhos;
        }

        private void EncontrarCaminhosRepeticao(int idCidadeOrigem)
        {
            for (int i = 0; i < MatrizAdjacenteDeCaminhos.GetLength(0); i++)
            {
                Caminho caminho = this.MatrizAdjacenteDeCaminhos[idCidadeOrigem, i];
                if(caminho != null || !PassamosAqui[i])
                {
                    Aux.Add(caminho);

                    if (i == this.Destino.GetIdCidade())
                        Caminhos.Add(Aux);
                    else
                    {
                        PassamosAqui[i] = true;
                        EncontrarCaminhosRepeticao(caminho.getIdDestino());
                        PassamosAqui[i] = true;
                    }
                }    
            }
        }
    }
}
