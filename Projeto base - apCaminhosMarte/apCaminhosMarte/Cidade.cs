using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace apCaminhosMarte
{
    class Cidade : IComparable<Cidade>
    {
        int IdCidade, CoordenadaX, CoordenadaY,
            TamanhoId = 3,
            TamanhoX = 5,
            TamanhoY = 5,
            TamanhoNome = 15;

        string NomeCidade;

        public Cidade()
        {

        }

        public Cidade(string linha)
        {
            IdCidade = int.Parse(linha.Substring(0, TamanhoId).Trim());
            NomeCidade = linha.Substring(TamanhoId, TamanhoNome).Trim();
            CoordenadaX = int.Parse(linha.Substring(TamanhoId + TamanhoNome, CoordenadaX).Trim());
            CoordenadaY = int.Parse(linha.Substring(TamanhoId + TamanhoNome + TamanhoX, TamanhoY).Trim());
        }

        /*public Cidade(int IdCidade, int CoordenadaX, int CoordenadaY, string NomeCidade)
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

        }*/

        public Cidade(Cidade cidade)
        {
            if (cidade == null)
                throw new Exception("Erro: Classe nulla");
            else
            {
                cidade.IdCidade = this.IdCidade;
                cidade.NomeCidade = this.NomeCidade;
                cidade.CoordenadaX = this.CoordenadaX;
                cidade.CoordenadaY = this.CoordenadaY;
            }
        }

        public int GetIdCidade()
        {
            return IdCidade;
        }

        //verificar depois
        public bool Equals(Cidade cidade)
        {
            if (this.IdCidade != cidade.IdCidade)
                return false;
            if (this.TamanhoId != cidade.TamanhoId)
                return false;
            if (!this.NomeCidade.Equals(cidade.NomeCidade))
                return false;
            if (this.TamanhoNome != cidade.TamanhoNome)
                return false;
            if (this.CoordenadaX != cidade.CoordenadaX)
                return false;
            if (this.TamanhoX != cidade.TamanhoX)
                return false;
            if (this.CoordenadaY != cidade.CoordenadaY)
                return false;
            if (this.TamanhoY != cidade.TamanhoY)
                return false;

            return true;
        }

        public override string ToString()
        {
            string ret = "";
            ret += "O Id da cidade é " + IdCidade.ToString() + "\n";
            ret += "O tamanho do Id da cidade é " + TamanhoId.ToString() + "\n";

            ret += "O nome da cidade é " + NomeCidade + "\n";
            ret += "O tamanho do nome da cidade é " + TamanhoNome.ToString() + "\n";

            ret += "A coordenada X é " + CoordenadaX.ToString() + "\n";
            ret += "O tamanho da coordenada X é " + TamanhoX.ToString() + "\n";

            ret += "A coordenada Y é " + CoordenadaY.ToString() + "\n";
            ret += "O Id da cidade é " + TamanhoY.ToString() + "\n";

            return ret;
        }

        public override int GetHashCode()
        {
            int ret = 19;

            ret = ret * 7 + IdCidade.GetHashCode();
            ret = ret * 7 + TamanhoId.GetHashCode();
            ret = ret * 7 + NomeCidade.GetHashCode();
            ret = ret * 7 + TamanhoNome.GetHashCode();
            ret = ret * 7 + CoordenadaX.GetHashCode();
            ret = ret * 7 + TamanhoX.GetHashCode();
            ret = ret * 7 + CoordenadaY.GetHashCode();
            ret = ret * 7 + TamanhoY.GetHashCode();

            if (ret < 0)
                ret = -ret;

            return ret;
        }

        public Object Clone()
        {
            Cidade exemplar = null;
            try
            {
                exemplar = new Cidade(this);
            }
            catch
            {}

            return exemplar;
        }

        public int CompareTo(Cidade cidade)
        {
            if(this.IdCidade < cidade.IdCidade)
                return -1;

            if (this.IdCidade > cidade.IdCidade)
                return 1;

            return 1;
        }

    }
}
