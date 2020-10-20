using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.DesenharCidadesRec(base.Raiz, mapa, pWidth, pHeight);
        }

        private void DesenharCidadesRec(NoArvore<Cidade> atual, PictureBox mapa, int pWidth, int pHeight)
        {
            if(atual != null)
            {
                int x = atual.Info.GetCoordenadaX/pWidth;
                int y = atual.Info.GetCoordenadaY/pHeight;
                SolidBrush pincelNome = new SolidBrush(Color.Black);
                SolidBrush pincelPonto = new SolidBrush(Color.Red);
                Graphics grafico = mapa.CreateGraphics();
                grafico.FillEllipse(pincelPonto, (x-4), (y-4), 8, 8);
                grafico.DrawString(atual.Info.GetNomeCidade, new Font("Arial", 10, FontStyle.Bold), pincelNome, new PointF(x, (y + 4)));
                DesenharCidadesRec(atual.Esq, mapa, pWidth, pHeight);
                DesenharCidadesRec(atual.Dir, mapa, pWidth, pHeight);
            }
        }

        public Cidade BuscarCidade(int idCidade)
        {
            Cidade procurada = new Cidade(idCidade);
            return this.RealizarBuscaDaCidade(base.raiz ,procurada);
        }

        private Cidade RealizarBuscaDaCidade(NoArvore<Cidade> local, Cidade procurado)
        {
            base.antecessor = null;
            while (local != null)
            {
                if (local.Info.CompareTo(procurado) == 0)
                {
                    base.atual = local;
                    return (Cidade)local.Info;
                }
                else
                {
                    antecessor = local;
                    if (procurado.CompareTo(local.Info) < 0)
                        local = local.Esq;     // Desloca apontador para o ramo à esquerda
                    else
                        local = local.Dir;     // Desloca apontador para o ramo à direita
                }
            }
            throw new Exception("nao existe nenhuma cidade com o codigo fornecido");       // Se local == null, a chave não existe
        }
        public void DesenharArvore(Graphics g)
        {
            DesenharArvoreRec(true, base.raiz, 660, 5, (Math.PI / 180) * 90, 1.4, 400, g);
        }
        private void DesenharArvoreRec(bool primeiraVez, NoArvore<Cidade> raiz,
                                    int x, int y, double angulo, double incremento,
                                    double comprimento, Graphics g)
        {
            int xf, yf;
            if (raiz != null)
            {
                Pen caneta = new Pen(Color.Red);
                xf = (int)Math.Round(x + Math.Cos(angulo) * comprimento);
                yf = (int)Math.Round(y + Math.Sin(angulo) * comprimento);
                if (primeiraVez)
                    yf = 25;
                g.DrawLine(caneta, x, y, xf, yf);
                DesenharArvoreRec(false, raiz.Esq, xf, yf, Math.PI / 2 + incremento,
                incremento * 0.6, comprimento * 0.7, g);
                DesenharArvoreRec(false, raiz.Dir, xf, yf, Math.PI / 2 - incremento,
                incremento * 0.6, comprimento * 0.7, g);
                SolidBrush preenchimento = new SolidBrush(Color.LightGreen);
                g.FillEllipse(preenchimento, xf - 25, yf - 15, 100, 30);
                g.DrawString(Convert.ToString(raiz.Info.ToString()), new Font("Comic Sans", 10, FontStyle.Bold),
                new SolidBrush(Color.Black), xf - 23, yf - 7);
            }
        }
    }
}
