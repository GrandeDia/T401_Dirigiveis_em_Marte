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
        int idCidade, coordenadaX, coordenadaY;

        string nomeCidade;

        public Cidade()
        {

        }

        public Cidade(string linha)
        {
            if (linha == null || linha == "")
                throw new Exception("A linha passada nao pode ser nula ou vazia");
            idCidade = int.Parse(linha.Substring(0, 3).Trim());
            nomeCidade = linha.Substring(3, 16).Trim();
            coordenadaX = int.Parse(linha.Substring(19, 5).Trim());
            coordenadaY = int.Parse(linha.Substring(24, 4).Trim());
        }

        public Cidade(int idCidade)
        {
            if (idCidade < 0)
                throw new Exception("id invalido");
            this.idCidade = idCidade;
            this.coordenadaX = 0;
            this.coordenadaY = 0;
            this.nomeCidade = "Cidade De Busca";
        }

        private Cidade(int IdCidade, int coordenadaX, int coordenadaY, string nomeCidade)
        {
            this.idCidade = IdCidade;
            this.nomeCidade = nomeCidade;
            this.coordenadaX = coordenadaX;
            this.coordenadaY = coordenadaY;
        }

        public Cidade(Cidade cidade)
        {
            if (cidade == null)
                throw new Exception("Erro: Classe nulla");
            else
            {
                this.idCidade = cidade.idCidade;
                this.nomeCidade = cidade.nomeCidade;
                this.coordenadaX = cidade.coordenadaX;
                this.coordenadaY = cidade.coordenadaY;
            }
        }

        public int GetIdCidade
        {
            get => idCidade;
        }

        //verificar depois
        public bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            if (!(obj.GetType() == this.GetType()))
                return false;
            Cidade cidade = (Cidade)obj;
            if (this.idCidade != cidade.idCidade)
                return false;
            if (!this.nomeCidade.Equals(cidade.nomeCidade))
                return false;
            if (this.coordenadaX != cidade.coordenadaX)
                return false;
            if (this.coordenadaY != cidade.coordenadaY)
                return false;
            return true;
        }

        public override string ToString()
        {
            string ret = "";
            ret = this.idCidade + " - " + this.nomeCidade;
            return ret;
        }

        public override int GetHashCode()
        {
            int ret = 19;

            ret = ret * 7 + idCidade.GetHashCode();
            ret = ret * 7 + nomeCidade.GetHashCode();
            ret = ret * 7 + coordenadaX.GetHashCode();
            ret = ret * 7 + coordenadaY.GetHashCode();

            if (ret < 0)
                ret = -ret;

            return ret;
        }

        public Object Clone()
        {
            Cidade exemplar = null;
            try
            {
                exemplar = new Cidade(this.idCidade, this.coordenadaX, this.coordenadaY, this.nomeCidade);
            }
            catch
            {}

            return exemplar;
        }

        public int CompareTo(Cidade cidade)
        {
            if (this.idCidade < cidade.idCidade)
                return -1;
            if (this.idCidade == cidade.idCidade)
                return 0;
            return 1;
        }

        public int GetCoordenadaX
        {
            get => this.coordenadaX;
        }

        public int GetCoordenadaY
        {
            get => this.coordenadaY;
        }

        public string GetNomeCidade
        {
            get => this.nomeCidade;
        }
    }
}
