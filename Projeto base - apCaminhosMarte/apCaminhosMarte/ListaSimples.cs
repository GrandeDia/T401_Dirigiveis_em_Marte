using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    public class ListaSimples<Dado>
    {
        private NoLista<Dado> primeiro, ultimo;
        private int qtd = 0;

        public ListaSimples ()
        {}

        public void InsiraNoFim (Dado dado)
        {
            if (dado == null)
                throw new Exception("Dado ausente");

            if (GetQtd() == 0)
            {
                primeiro = new NoLista<Dado>(dado, primeiro);
                ultimo = primeiro;
                qtd++;
                return;
            }
            else
            {
                NoLista<Dado> aux = new NoLista<Dado>(dado, null);
                ultimo.Prox = aux;
                ultimo = aux;
                qtd++;
                return;
            }
        }

        public void RemovaDoFim ()
        {
            if (GetQtd() == 0)
                throw new Exception("Nada a remover");


            if (GetQtd() == 1)
            {
                primeiro = null;
                ultimo = null;
                qtd--;
                return;
            }

            NoLista<Dado> aux = primeiro;
            while (aux != null)
            {
                if (aux.Prox.Equals(ultimo))
                {
                    aux.Prox = null;
                    ultimo = aux;
                    qtd--;
                    break;
                }
                else
                    aux = aux.Prox;
            }
        }

        public NoLista<Dado> Primeiro
        {
            get => primeiro;
        }

        public NoLista<Dado> Ultimo
        {
            get => ultimo;
        }

        public Dado GetUltimo ()
        {
            if (GetQtd() == 0)
                throw new Exception("Nada a retornar");

            return ultimo.Info;
        }

        public int GetQtd ()
        {
            return qtd;
        }
    }
}
