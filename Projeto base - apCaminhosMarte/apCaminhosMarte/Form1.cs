using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    public partial class Form1 : Form
    {
        Marte marte;
        MarteGPS gps;
        List<List<Caminho>> listaCaminhos = new List<List<Caminho>>();
        List<Caminho> menorCaminho = new List<Caminho>();
        public Form1()
        {
            InitializeComponent();
            marte = new Marte("CidadesMarte.txt", "CaminhosEntreCidadesMarte.txt");
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if(lsbOrigem.SelectedIndex == -1 || lsbDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione a cidade de origem e a cidade de destino!");
            }
            else
                if(lsbOrigem.SelectedIndex == lsbDestino.SelectedIndex)
                {
                    MessageBox.Show("Selecione duas cidades diferentes!");
                }
                else
                {
                    try
                    {
                        dgvCaminhos.Rows.Clear();
                        dgvMelhorCaminho.Rows.Clear();
                        gps = new MarteGPS(lsbOrigem.SelectedIndex, lsbDestino.SelectedIndex, marte.MatrizAdjacenteDeCaminhos);
                        listaCaminhos = gps.EncontrarCaminhos();
                        int linha = 0;
                        int coluna = 0;
                        int menorTempo = int.MaxValue;
                        int maiorNumCidades = 0;
                        menorCaminho = new List<Caminho>();
                        dgvCaminhos.RowCount = listaCaminhos.Count;
                        foreach (List<Caminho> caminho in listaCaminhos)
                        {
                            if (caminho.Count > maiorNumCidades)
                            {
                                maiorNumCidades = caminho.Count;
                                dgvCaminhos.ColumnCount = maiorNumCidades;
                            }
                            int tempoPercursoAtual = 0;
                            foreach (Caminho cidade in caminho)
                            {
                                string nomeCidade = lsbOrigem.Items[cidade.GetIdOrigem].ToString();
                                dgvCaminhos.Rows[linha].Cells[coluna].Value = nomeCidade;
                                coluna++;
                                tempoPercursoAtual += cidade.Tempo;
                            }
                            if (tempoPercursoAtual < menorTempo)
                            {
                                menorTempo = tempoPercursoAtual;
                                menorCaminho = caminho;
                            }
                            coluna = 0;
                            linha++;
                        }
                        dgvMelhorCaminho.ColumnCount = menorCaminho.Count;
                        dgvMelhorCaminho.RowCount = 1;
                        foreach (Caminho cidade in menorCaminho)
                        {
                            dgvMelhorCaminho.Rows[0].Cells[coluna].Value = lsbOrigem.Items[cidade.GetIdOrigem].ToString();
                            coluna++;
                        }
                    }
                    catch(Exception erro)
                    {
                        MessageBox.Show("Não há caminho entre as cidades!");
                    }
                }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            pbMapa.Image = Image.FromFile("mars_political_map_by_axiaterraartunion_d4vfxdf-pre.jpg");
            Application.DoEvents();
            marte.DesenharCidades(pbMapa, 4096, 2048);
        }

        private void dgvCaminhos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pbMapa.Image = Image.FromFile("mars_political_map_by_axiaterraartunion_d4vfxdf-pre.jpg");
            Application.DoEvents();
            this.DesenharCaminho(listaCaminhos[dgvCaminhos.CurrentCell.RowIndex]);
        }

        private void DesenharCaminho(List<Caminho> lista)
        {
            Graphics grafico = pbMapa.CreateGraphics();
            Pen caneta = new Pen(Color.Red, 2);
            int pWidth = 4096 / pbMapa.Width;
            int pHeight = 2048 / pbMapa.Height;
            foreach(Caminho caminho in lista)
            {
                Cidade origem = marte.GetCidade(caminho.GetIdOrigem);
                Cidade destino = marte.GetCidade(caminho.GetIdDestino);
                int xOrigem = origem.GetCoordenadaX;
                int yOrigem = origem.GetCoordenadaY;
                int xDestino = destino.GetCoordenadaX;
                int yDestino = destino.GetCoordenadaY;
                grafico.DrawLine(caneta, new Point(xOrigem/pWidth, yOrigem/pHeight), new Point(xDestino/pWidth, yDestino/pHeight));
            }
            Application.DoEvents();
            marte.DesenharCidades(pbMapa, 4096, 2048);
        }

        private void dgvMelhorCaminho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pbMapa.Image = Image.FromFile("mars_political_map_by_axiaterraartunion_d4vfxdf-pre.jpg");
            Application.DoEvents();
            this.DesenharCaminho(menorCaminho);
        }

        private void tpArvore_Paint(object sender, PaintEventArgs e)
        {
            marte.DesenharArvore(e.Graphics);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tpRotas"])
            {
                pbMapa.Image = Image.FromFile("mars_political_map_by_axiaterraartunion_d4vfxdf-pre.jpg");
                Application.DoEvents();
                marte.DesenharCidades(pbMapa, 4096, 2048);
                Application.DoEvents();
            }
        }
    }
}
