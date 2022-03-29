using System;

namespace Atividade10.ConsoleApp
{
    public class Movimentacao
    {

        #region Atributos

        private decimal _valor;
        private string _tipo;
        private int _contaDestino;
        private int _contaOrigem;

        #endregion

        #region Propriedades

        public string Tipo
        {
            get { return _tipo; }
        }

        public decimal Valor
        {
            get { return _valor; }
        }

        public int ContaDestino
        {
            get { return _contaDestino; }
        }

        public int ContaOrigem
        {
            get { return _contaOrigem; }
        }

        #endregion

        #region Construtores

        public Movimentacao(decimal valor, string tipo, int numeroContaOrigem)
        {
            _valor = valor;
            _tipo = tipo;
            _contaOrigem = numeroContaOrigem;
        }

        public Movimentacao(decimal valor, string tipo, int contaDestino, int contaOrigem)
        {
            this._valor = valor;
            this._tipo = tipo;
            this._contaDestino = contaDestino;
            this._contaOrigem = contaOrigem;
        }

        #endregion

    }
}
