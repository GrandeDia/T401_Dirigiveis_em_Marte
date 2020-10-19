using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    class ArvoreCidades : ArvoreDeBusca<Cidade>
    {
        public void DesenharCidades(PictureBox mapa, int width, int height)
        {
            int pWidth = width / mapa.Width;
            int pHeight = height / mapa.Height;
            this.DesenharCidadesRec(base.Raiz, mapa);
        }

        private void DesenharCidadesRec(NoArvore<Cidade> atual, PictureBox mapa)
        {

        }
    }
}
