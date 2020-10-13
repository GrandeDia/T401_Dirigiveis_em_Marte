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

        ArvoreDeBusca<Caminho> Caminhos;


        public Marte(string cidadeMarteArquivo, string caminhosEntreMarteArquivo)
        {
            if (cidadeMarteArquivo.Trim() == null || caminhosEntreMarteArquivo.Trim() == null)
                throw new Exception("Nome do arquivo null");

            if (cidadeMarteArquivo.Trim().Equals("") || caminhosEntreMarteArquivo.Trim().Equals(""))
                throw new Exception("Nome do arquivo vazio");

            this.cidadeMarteArquivo = cidadeMarteArquivo;
            this.caminhosEntreMarteArquivo = caminhosEntreMarteArquivo;

        }
    }
}
