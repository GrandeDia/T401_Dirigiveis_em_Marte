using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace apCaminhosMarte
{
    class ArvoreDeBusca<Dado>  // : IComparable<NoArvore<Dado>> 
                                where Dado : IComparable<Dado>, new()
    {
        protected NoArvore<Dado> raiz,
                       atual,       // indica o n� que est� sendo visitado no momento
                       antecessor;  // indica o n� ancestral daquele que est� sendo visitado no momento
        int quantosNos;

        public Panel painelArvore;

        public Panel OndeExibir
        {
            get { return painelArvore; }
            set { painelArvore = value; }
        }

        public ArvoreDeBusca()
        {
            raiz = null;
            atual = null;
            antecessor = null;
            quantosNos = 0;
        }

        public NoArvore<Dado> Raiz
        {
            get => raiz; set => raiz = value; 
        }

        public String InOrdem  // propriedade que gera a string do percurso in-ordem da �rvore
        {
            get { return FazInOrdem(raiz); }
        }

        public String PreOrdem  // propriedade que gera a string do percurso pre-ordem da �rvore
        {
            get { return FazPreOrdem(raiz); }
        }

        public String PosOrdem  // propriedade que gera a string do percurso pos-ordem da �rvore
        {
            get { return FazPosOrdem(raiz); }
        }

        private String FazInOrdem(NoArvore<Dado> r)
        {
            if (r == null)
                return "";  // retorna cadeia vazia

            return FazInOrdem(r.Esq) + " " + 
                   r.Info.ToString() + " " +
                   FazInOrdem(r.Dir);
        }

        private String FazPreOrdem(NoArvore<Dado> r)
        {
            if (r == null)
                return "";  // retorna cadeia vazia

            return r.Info.ToString() + " " + 
                   FazPreOrdem(r.Esq) + " " +
                   FazPreOrdem(r.Dir);
        }

        private String FazPosOrdem(NoArvore<Dado> r)
        {
            if (r == null)
                return "";  // retorna cadeia vazia

            return FazPosOrdem(r.Esq) + " " +
                   FazPosOrdem(r.Dir) + " " + 
                   r.Info.ToString();
        }

        // Exerc�cios iniciais

        private bool Eq(NoArvore<Dado> atualA, 
                        NoArvore<Dado> atualB)
        {
            if (atualA == null && atualB == null)
                return true;
            
            if ((atualA == null) != (atualB == null)) // apenas um dos n�s �
                return false; // uma � nulo e outra n�o �
            
            // os dois n�s n�o s�o nulos
            if (atualA.Info.CompareTo(atualB.Info) != 0)
                return false; // Infos diferentes
            
            return Eq(atualA.Esq, atualB.Esq) && 
                   Eq(atualA.Dir, atualB.Dir);
        }

        public bool EquivaleA(ArvoreDeBusca<Dado> outraArvore)
        {
            /* . ambas s�o vazias
            ou
            .. Info(A) = Info(B) e
            ... Esq(A) eq Esq(B) e Dir(A) eq Dir(B)
            */
            return Eq(this.raiz, outraArvore.raiz);
        }

        public int QtosNos(NoArvore<Dado> noAtual)
        {
            if (noAtual == null)  // �rvore vazia ou descendente de folha
                return 0;

            return 1 +                 // conta o n� atual
                QtosNos(noAtual.Esq) + // conta n�s da sub�rvore esquerda
                QtosNos(noAtual.Dir);  // conta n�s da sub�rvore direita
        }

        public int QtasFolhas(NoArvore<Dado> noAtual)
        {
            if (noAtual == null)
                return 0;
            if (noAtual.Esq == null && noAtual.Dir == null) // noAtual � folha
                return 1;
            // noAtual n�o � folha, portanto procuramos as folhas de cada ramo e as contamos
            return QtasFolhas(noAtual.Esq) + // conta folhas da sub�rvore esquerda
            QtasFolhas(noAtual.Dir); // conta folhas da sub�rvore direita
        }

        public bool EstritamenteBinaria(NoArvore<Dado> noAtual)
        {
            if (noAtual == null)
                return true;
            // noAtual n�o � nulo
            if (noAtual.Esq == null && noAtual.Dir == null)
                return true;
            // um dos descendentes � nulo e o outro n�o �
            if (noAtual.Esq == null && noAtual.Dir != null)
                return false;
            if (noAtual.Esq != null && noAtual.Dir == null)
                return false;
            // se chegamos aqui, nenhum dos descendentes � nulo, dai testamos a
            // "estrita binariedade" das duas sub�rvores descendentes do n� atual
            return EstritamenteBinaria(noAtual.Esq) && EstritamenteBinaria(noAtual.Dir);
        }

        public int Altura(NoArvore<Dado> noAtual)
        {
            int alturaEsquerda,
                alturaDireita;
            if (noAtual == null)
                return 0;
            alturaEsquerda = Altura(noAtual.Esq);
            alturaDireita = Altura(noAtual.Dir);
            if (alturaEsquerda >= alturaDireita)
                return 1 + alturaEsquerda;
            return 1 + alturaDireita;
        }

        public string EntreParenteses(NoArvore<Dado> noAtual)
        {
            string saida = "(";
            if (noAtual != null)
                saida += noAtual.Info + ":" +
                EntreParenteses(noAtual.Esq) +
                "," +
                EntreParenteses(noAtual.Dir);
            saida += ")";
            return saida;
        }

        public void Trocar(NoArvore<Dado> noAtual)
        {
            if (noAtual != null)
            {
                NoArvore<Dado> auxiliar = noAtual.Esq;
                noAtual.Esq = noAtual.Dir;
                noAtual.Dir = auxiliar;
                Trocar(noAtual.Esq);
                Trocar(noAtual.Dir);
            }
        }

        public string PercursoPorNiveis(NoArvore<Dado> noAtual)
        {
            string saida = "";
            //Filalista<NoArvore<Dado>> umaFila = new FilaLista<NoArvore<Dado>>();
            var umaFila = new Queue<NoArvore<Dado>>();
            while (noAtual != null)
            {
                if (noAtual.Esq != null)
                    umaFila.Enqueue(noAtual.Esq);
                if (noAtual.Dir != null)
                    umaFila.Enqueue(noAtual.Dir);
                saida += " " + noAtual.Info;
                if (umaFila.Count == 0)
                    noAtual = null;
                else
                    noAtual = umaFila.Dequeue();
            }
            return saida;
        }

        int[] quantosNoNivel = new int[1000]; // GLOBAL NA CLASSE
        public int largura(NoArvore<Dado> noAtual)
        {
            for (int i = 0; i < 1000; i++)
                quantosNoNivel[i] = 0;
            ContarNosNosNiveis(noAtual, 0);
            // acha o n�vel com o maior contador de n�s
            int indiceMaior = 0;
            for (int i = 0; i < 1000; i++)
                if (quantosNoNivel[i] > quantosNoNivel[indiceMaior])
                    indiceMaior = i;
            return quantosNoNivel[indiceMaior];
        }
        public void ContarNosNosNiveis(NoArvore<Dado> noAtual, int qualNivel)
        {
            if (noAtual != null)
            {
                ++quantosNoNivel[qualNivel];
                ContarNosNosNiveis(noAtual.Esq, qualNivel + 1);
                ContarNosNosNiveis(noAtual.Dir, qualNivel + 1);
            }
        }

        bool achou = false;
        private string EscreverAntecessores(NoArvore<Dado> atual, Dado procurado)
        {
            string saida = "";
            if (atual != null)
            {
                if (!achou)
                    EscreverAntecessores(atual.Esq, procurado);
                if (!achou)
                    EscreverAntecessores(atual.Dir, procurado);
                if (atual.Info.CompareTo(procurado) == 0)
                    achou = true;
                if (achou)
                    saida +=" "+atual.Info;
            }
            return saida;
        }
        public string PreparaEscritaDosAntecessores(Dado procurado)
        {
            achou = false;
            return EscreverAntecessores(Raiz, procurado);
        }

        public void DesenharArvore(bool primeiraVez, NoArvore<Cidade> raiz,
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
                Thread.Sleep(500);
                DesenharArvore(false, raiz.Esq, xf, yf, Math.PI / 2 + incremento,
                                       incremento * 0.60, comprimento * 0.8, g);
                DesenharArvore(false, raiz.Dir, xf, yf, Math.PI / 2 - incremento,
                                        incremento * 0.60, comprimento * 0.8, g);
                Thread.Sleep(500);
                SolidBrush preenchimento = new SolidBrush(Color.Blue);
                g.FillEllipse(preenchimento, xf - 25, yf - 15, 42, 30);
                g.DrawString(Convert.ToString(raiz.Info.ToString()), new Font("Comic Sans", 10),
                    new SolidBrush(Color.Yellow), xf - 23, yf - 7);

            }
        }

        private bool Existe(NoArvore<Dado> local, Dado procurado)
        {
            antecessor = null;
            while (local != null)
            {
                if (local.Info.CompareTo(procurado) == 0)
                {
                    atual = local;
                    return true;
                }
                else
                {
                    antecessor = local;
                    if (procurado.CompareTo(local.Info) < 0)
                       local = local.Esq;     // Desloca apontador para o ramo � esquerda
                    else
                      local = local.Dir;     // Desloca apontador para o ramo � direita
                }
            }
            return false;       // Se local == null, a chave n�o existe
        }

        public bool Existe(Dado procurado)
        {
            return Existe(raiz, procurado);
        }

        public bool ExisteRec(NoArvore<Dado> local, Dado procurado)
        {
            if (local == null)
                return false;
            else
              if (local.Info.CompareTo(procurado) == 0)
            {
                atual = local;
                return true;
            }
            else
            {
                antecessor = local;
                if (procurado.CompareTo(local.Info) < 0)
                    return ExisteRec(local.Esq, procurado);	 // Desloca apontador na 
                else                                         // pr�xima inst�ncia do 
                    return ExisteRec(local.Dir, procurado);	 // m�todo
            }
        }

        public void Incluir(Dado dadoLido)
        {
            if (dadoLido == null)
                throw new Exception("O dado passado nao pode ser nulo");
            Incluir(ref raiz, dadoLido);
        }


        private void Incluir(ref NoArvore<Dado> atual, Dado dadoLido)
        {
            if (atual == null)
            {
                atual = new NoArvore<Dado>(dadoLido);
            }
            else
              if (dadoLido.CompareTo(atual.Info) == 0)
                throw new Exception("J� existe esse registro!");
            else
              if (dadoLido.CompareTo(atual.Info) > 0)
              {
                 NoArvore<Dado> apDireito = atual.Dir;
                 Incluir(ref apDireito, dadoLido);
                 atual.Dir = apDireito;
              }
              else
                {
                   NoArvore<Dado> apEsquerdo = atual.Esq;
                   Incluir(ref apEsquerdo, dadoLido);
                   atual.Esq = apEsquerdo;
                }
        }

            

        public void Inserir(Dado novosDados)
        {
            bool achou = false, fim = false;
            NoArvore<Dado> novoNo = new NoArvore<Dado>(novosDados);
            if (raiz == null)         // �rvore vazia
                raiz = novoNo;
            else                      // �rvore n�o-vazia
            {
                antecessor = null;
                atual = raiz;
                while (!achou && !fim)
                {
                    antecessor = atual;
                    if (novosDados.CompareTo(atual.Info) < 0)
                    {
                        atual = atual.Esq;
                        if (atual == null)
                        {
                            antecessor.Esq = novoNo;
                            fim = true;
                        }
                    }
                    else
                        if (novosDados.CompareTo(atual.Info) == 0)
                            achou = true;  // pode-se disparar uma exce��o neste caso
                        else
                        {
                            atual = atual.Dir;
                            if (atual == null)
                            {
                                antecessor.Dir = novoNo;
                                fim = true;
                            }
                        }
                }
            }
        }


        public bool Excluir(Dado procurado)
        {
            return Excluir(ref raiz);


            bool Excluir(ref NoArvore<Dado> atual)
            {
                NoArvore<Dado> atualAnt;
                if (atual == null)
                    return false;
                else
                  if (atual.Info.CompareTo(procurado) > 0)
                {
                    var temp = atual.Esq;
                    bool result = Excluir(ref temp);
                    atual.Esq = temp;
                    return result;
                }
                else
                    if (atual.Info.CompareTo(procurado) < 0)
                    {
                       var temp = atual.Dir;
                       bool result = Excluir(ref temp);
                       atual.Esq = temp;
                       return result;
                }
                else
                    {
                        atualAnt = atual;   // n� a retirar 
                        if (atual.Dir == null)
                            atual = atual.Esq;
                        else
                          if (atual.Esq == null)
                            atual = atual.Dir;
                        else
                        {   // pai de 2 filhos 
                        var temp = atual.Esq;
                            Rearranjar(ref temp, ref atualAnt);
                        atual.Esq = temp;
                            atualAnt = null;  // libera o n� exclu�do
                        }
                        return true;
                    }
            }

            void Rearranjar(ref NoArvore<Dado> aux, ref NoArvore<Dado> atualAnt)
            {
                if (aux.Dir != null)
                {
                    NoArvore<Dado> temp = aux.Dir;
                    Rearranjar(ref temp, ref atualAnt);  // Procura Maior
                    aux.Dir = temp;
                }
                else
                {                           // Guarda os dados do n� a excluir
                    atualAnt.Info = aux.Info;   // troca conte�do!
                    atualAnt = aux;             // funciona com a passagem por refer�ncia
                    aux = aux.Esq;
                }
            }

        }

        public bool ApagarNo(Dado chaveARemover)
        {
            atual = raiz;
            antecessor = null;
            bool ehFilhoEsquerdo = true;
            while (atual.Info.CompareTo(chaveARemover) != 0)  // enqto n�o acha a chave a remover
            {
                antecessor = atual;
                if (atual.Info.CompareTo(chaveARemover) > 0)
                {
                    ehFilhoEsquerdo = true;
                    atual = atual.Esq;
                }
                else
                {
                    ehFilhoEsquerdo = false;
                    atual = atual.Dir;
                }

                if (atual == null)  // neste caso, a chave a remover n�o existe e n�o pode
                    return false;   // ser exclu�da, dai retornamos falso indicando isso
            }  // fim do while

            // se fluxo de execu��o vem para este ponto, a chave a remover foi encontrada
            // e o ponteiro atual indica o n� que cont�m essa chave

            if ((atual.Esq == null) && (atual.Dir == null))  // � folha, n� com 0 filhos
            {
                if (atual == raiz)
                    raiz = null;   // exclui a raiz e a �rvore fica vazia
                else
                    if (ehFilhoEsquerdo)        // se for filho esquerdo, o antecessor deixar� 
                        antecessor.Esq = null;  // de ter um descendente esquerdo
                    else                               // se for filho direito, o antecessor deixar� de 
                        antecessor.Dir = null;  // apontar para esse filho

                atual = antecessor;  // feito para atual apontar um n� v�lido ao sairmos do m�todo
            }
            else   // verificar� as duas outras possibilidades, exclus�o de n� com 1 ou 2 filhos
                if (atual.Dir == null)   // neste caso, s� tem o filho esquerdo
                {
                    if (atual == raiz)
                        raiz = atual.Esq;
                    else
                        if (ehFilhoEsquerdo)
                            antecessor.Esq = atual.Esq;
                        else
                            antecessor.Dir = atual.Esq;
                    atual = antecessor;
                }
                else
                    if (atual.Esq == null)  // neste caso, s� tem o filho direito
                    {
                        if (atual == raiz)
                            raiz = atual.Dir;
                        else
                            if (ehFilhoEsquerdo)
                                antecessor.Esq = atual.Dir;
                            else
                                antecessor.Dir = atual.Dir;
                        atual = antecessor;
                    }
                    else // tem os dois descendentes
                    {
                        NoArvore<Dado> menorDosMaiores = ProcuraMenorDosMaioresDescendentes(atual);
                        atual.Info = menorDosMaiores.Info;
                        menorDosMaiores = null; // para liberar o n� trocado da mem�ria
                    }
            return true;
        }

        public NoArvore<Dado> ProcuraMenorDosMaioresDescendentes(NoArvore<Dado> noAExcluir)
        {
            NoArvore<Dado> paiDoSucessor = noAExcluir;
            NoArvore<Dado> sucessor = noAExcluir;
            NoArvore<Dado> atual = noAExcluir.Dir;   // vai ao ramo direito do n� a ser exclu�do, pois este ramo cont�m
            // os descendentes que s�o maiores que o n� a ser exclu�do 
            while (atual != null)
            {
                if (atual.Esq != null)
                   paiDoSucessor = atual;
                sucessor = atual;
                atual = atual.Esq;
            }

            if (sucessor != noAExcluir.Dir)
            {
                paiDoSucessor.Esq = sucessor.Dir;
                sucessor.Dir = noAExcluir.Dir;
            }
            return sucessor;
        }

        public int alturaArvore(NoArvore<Dado> atual, ref bool balanceada)
        {
            int alturaDireita, alturaEsquerda, result;
            if (atual != null && balanceada)
            {
              alturaEsquerda = 1 + alturaArvore(atual.Esq, ref balanceada);
              alturaDireita = 1 + alturaArvore(atual.Dir, ref balanceada);
              result = Math.Max(alturaEsquerda, alturaDireita);
              
              //if (alturaDireita > alturaEsquerda)
              //    result = alturaDireita;
              //else
              //  result = alturaEsquerda;
              
              if (Math.Abs(alturaDireita - alturaEsquerda) > 1)
                 balanceada = false;
            }
            else
                result = 0;
            return result;
        }
       
    }   
}