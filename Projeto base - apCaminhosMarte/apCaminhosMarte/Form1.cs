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
            {
                gps = new MarteGPS(lsbOrigem.SelectedIndex, lsbDestino.SelectedIndex, marte.MatrizAdjacenteDeCaminhos);
                List<List<Caminho>> listaCaminhos = gps.EncontrarCaminhos();
                int linha = 0;
                int coluna = 0;
                int menorTempo = int.MaxValue;
                int maiorNumCidades = 0;
                List<Caminho> menorCaminho = new List<Caminho>();
                dgvCaminhos.RowCount = listaCaminhos.Count;
                foreach (List<Caminho> caminho in listaCaminhos)
                {
                    if(caminho.Count > maiorNumCidades)
                    {
                        maiorNumCidades = caminho.Count;
                        dgvCaminhos.ColumnCount = maiorNumCidades;
                    }
                    int tempoPercursoAtual = 0;
                    foreach (Caminho cidade in caminho)
                    {
                        string nomeCidade = lsbOrigem.Items[cidade.getIdOrigem()].ToString();
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
                foreach(Caminho cidade in menorCaminho)
                {
                    dgvMelhorCaminho.Rows[0].Cells[coluna].Value = lsbOrigem.Items[cidade.getIdOrigem()].ToString();
                    coluna++;
                }

            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            pbMapa.Image = Image.FromFile("mars_political_map_by_axiaterraartunion_d4vfxdf-pre.jpg");
            Application.DoEvents();
            marte.DesenharCidades(pbMapa, 4096, 2048);
        }
    }
}
