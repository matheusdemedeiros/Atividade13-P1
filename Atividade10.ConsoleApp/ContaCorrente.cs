using System;

namespace Atividade10.ConsoleApp
{
    public class ContaCorrente
    {
        #region Atrbutos

        private int _numeroDaConta;
        private decimal _saldo;
        private string _status;
        private decimal _limite;
        private Cliente _titular;
        private Movimentacao[] _movimentacoes;

        #endregion

        #region Propriedades

        public decimal Limite
        {
            set
            {
                if (value > 0)
                    _limite = value;
                else
                    _limite = 0;
            }
        }

        #endregion

        #region Construtores

        public ContaCorrente(int numeroDaConta, string status, Cliente titularDaConta, decimal limite)
        {
            _numeroDaConta = numeroDaConta;
            _status = status;
            _titular = titularDaConta;
            Limite = limite;
            _saldo = 0;
            _movimentacoes = new Movimentacao[100];
            Console.WriteLine("A conta {0} foi criada com sucesso!!\n", _numeroDaConta);
        }

        #endregion

        #region Métodos púublicos

        public void Sacar(decimal valor)
        {
            if (DecrementarSaldo(valor) == true)
            {
                Movimentacao movimentacaoAtual = new Movimentacao(valor, "débito", _numeroDaConta);

                AdicionarMovimentacao(movimentacaoAtual);

                ApresentarMensagem("\nO valor de R$: " + valor.ToString("F2") + " foi sacado com sucesso da conta " + _numeroDaConta + "!!", ConsoleColor.Green);
            }
            else
                ApresentarMensagem("\nNão foi possível efetuar o saque!\nSaldo insuficiente!!", ConsoleColor.Red);

        }

        public void Depositar(decimal valor)
        {
            IncrementarSaldo(valor);

            Movimentacao movimentacaoAtual = new Movimentacao(valor, "crédito", _numeroDaConta);

            AdicionarMovimentacao(movimentacaoAtual);

            ApresentarMensagem("\nO valor de R$: " + valor.ToString("F2") + " foi depositado com sucesso na conta " + _numeroDaConta + "!!", ConsoleColor.Green);
        }

        public void Transferir(decimal valor, ContaCorrente contaDeDestino)
        {
            if (DecrementarSaldo(valor))
            {
                contaDeDestino.IncrementarSaldo(valor);

                Movimentacao movimentacaoAtualEmitente = new Movimentacao(valor, "transferenciaEfetuada", contaDeDestino._numeroDaConta, _numeroDaConta);

                AdicionarMovimentacao(movimentacaoAtualEmitente);

                Movimentacao movimentacaoAtualDestino = new Movimentacao(valor, "transferenciaRecebida", contaDeDestino._numeroDaConta, _numeroDaConta);

                contaDeDestino.AdicionarMovimentacao(movimentacaoAtualDestino);

                ApresentarMensagem("\nTransferência de R$ " + valor.ToString("F2") + " efetuada com suceso para a conta " + contaDeDestino._numeroDaConta + "!!", ConsoleColor.Green);
            }
            else
            {
                ApresentarMensagem("\nTransferência não efetuada!! Saldo insuficiente!!", ConsoleColor.Red);
            }
        }

        public void EmitirSaldo()
        {
            ConsoleColor cor;

            if (_saldo < 0)
                cor = ConsoleColor.Red;
            else
                cor = ConsoleColor.Green;

            ApresentarMensagem(("\nEmissão de saldo\nAtual: " + _saldo.ToString("F2")), cor);
        }

        public void EmitirExtrato()
        {
            Console.WriteLine("\n=== EXTRATO DA CONTA ===\n");

            ApresentarDadosDaConta();

            if (_movimentacoes[0] == null)
            {
                ApresentarMensagem("\nEsta conta não possui movimentações!!", ConsoleColor.Yellow);
            }
            else
            {
                for (int i = 0; i < _movimentacoes.Length; i++)
                {
                    if (_movimentacoes[i] == null)
                    {
                        break;
                    }
                    else
                    {
                        switch (_movimentacoes[i].Tipo)
                        {
                            case "debito":
                                ApresentarMensagem(("\nSaque realizado da conta " + _movimentacoes[i].ContaOrigem + " no seguinte valor: " + _movimentacoes[i].Valor.ToString("F2")), ConsoleColor.Red);
                                break;
                            case "credito":
                                ApresentarMensagem(("\nDepósito realizado na conta " + _movimentacoes[i].ContaOrigem + " no seguinte valor: " + _movimentacoes[i].Valor.ToString("F2")), ConsoleColor.Green);
                                break;
                            case "transferenciaEfetuada":
                                ApresentarMensagem(("\nTransferência efetuada da conta " + _movimentacoes[i].ContaOrigem + " no valor de R$: " + _movimentacoes[i].Valor.ToString("F2") + " para a conta " + _movimentacoes[i].ContaDestino), ConsoleColor.Yellow);
                                break;
                            case "transferenciaRecebida":
                                ApresentarMensagem(("\nTransferência recebida da conta " + _movimentacoes[i].ContaOrigem + " no valor de R$ " + _movimentacoes[i].Valor.ToString("F2") + "."), ConsoleColor.Yellow);
                                break;
                        }
                    }
                }
            }
        }


        #endregion

        #region Métodos privados

        private void IncrementarSaldo(decimal valor)
        {
            _saldo += valor;
        }

        private bool DecrementarSaldo(decimal valor)
        {
            if ((_saldo + _limite) > valor)
            {
                _saldo -= valor;

                return true;
            }
            else
                return false;
        }

        private void AdicionarMovimentacao(Movimentacao movimentacaoExecutada)
        {
            for (int i = 0; i < _movimentacoes.Length; i++)
            {
                if (_movimentacoes[i] == null)
                {
                    _movimentacoes[i] = movimentacaoExecutada;

                    break;
                }
            }
        }

        private void ApresentarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;

            Console.WriteLine(mensagem);

            Console.ResetColor();
        }

        private void ApresentarDadosDaConta()
        {
            Console.WriteLine("\nNúmero da conta: {0}", _numeroDaConta);
            Console.WriteLine("\nSaldo atual da conta: R$ {0}", _saldo.ToString("F2"));
            Console.WriteLine("\nStatus da conta: {0}", _status);
            Console.WriteLine("\nLimite da conta: R$ {0}", _limite.ToString("F2"));
            Console.WriteLine();
        }

        #endregion

    }
}