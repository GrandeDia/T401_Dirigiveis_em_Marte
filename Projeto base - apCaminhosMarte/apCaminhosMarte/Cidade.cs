using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace apCaminhosMarte
{
    class Cidade
    {
        int IdCidade, CoordenadaX, CoordenadaY,
            TamanhoId = 3,
            TamanhoX = 5,
            TamanhoY = 5,
            TamanhoCidade = 15;

        string NomeCidade;

        public Cidade(string linha)
        {
            IdCidade = int.Parse(linha.Substring(0,TamanhoId).Trim());
            NomeCidade = linha.Substring(TamanhoId, TamanhoCidade).Trim();
            CoordenadaX = int.Parse(linha.Substring(TamanhoId + TamanhoCidade, CoordenadaX).Trim());
            CoordenadaY  = int.Parse(linha.Substring(TamanhoId + TamanhoCidade + TamanhoX, TamanhoY).Trim());
        }

        public Cidade(int IdCidade, int CoordenadaX, int CoordenadaY, string NomeCidade)
        {
            if (IdCidade <= 0)
                throw new Exception("O Id da cidade não pode ser menor ou igual a 0");

            if (NomeCidade.Trim().Equals(""))
                throw new Exception("O nome da cidade não pode ser menor que 0");

            if (CoordenadaX <= 0)
                throw new Exception("A cordenada de X não pode ser menor que 0");

            if (CoordenadaY <= 0)
                throw new Exception("A cordenada de Y não pode ser menor que 0");

            this.IdCidade = IdCidade;
            this.NomeCidade = NomeCidade;
            this.CoordenadaX = CoordenadaX;
            this.CoordenadaY = CoordenadaY;
            
        }


    }
}
