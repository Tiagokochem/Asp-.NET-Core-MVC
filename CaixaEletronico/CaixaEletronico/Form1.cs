using Caelum.CaixaEletronico.Modelo;
using Caelum.CaixaEletronico.Modelo.Contas;
using Caelum.CaixaEletronico.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaixaEletronico
{
    public partial class Form1 : Form
    {
        Conta [] contas;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            contas = new Conta[3];

            Conta contaDoVictor = new ContaCorrente();
            contaDoVictor.Titular = new Cliente();
            contaDoVictor.Titular.Nome = "Victor";
            contaDoVictor.Numero = 1;
            contas[0] = contaDoVictor;

            Conta contaDoGuilherme = new ContaPoupanca();
            contaDoGuilherme.Titular = new Cliente();
            contaDoGuilherme.Titular.Nome = "Guilherme";
            contaDoGuilherme.Numero = 2;
            contas[1] = contaDoGuilherme;

            Conta contaDoMauricio = new ContaInvestimento();
            contaDoMauricio.Titular = new Cliente();
            contaDoMauricio.Titular.Nome = "Mauricio";
            contaDoMauricio.Numero = 3;
            contas[2] = contaDoMauricio;

            foreach (Conta conta in this.contas)
            {
                comboContas.Items.Add(conta);
                destinoDaTransferencia.Items.Add(conta);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textoValorSaque = valorOperacao.Text;

            double valorDeposito = Convert.ToDouble(textoValorSaque);

            int indiceSelecionado = comboContas.SelectedIndex;

            Conta contaSelecionada = this.contas[indiceSelecionado];
            contaSelecionada.Deposita(valorDeposito);

            this.MostraConta(contaSelecionada);
        }

        private void button2_Click(object sender, EventArgs eA)
        {
            string textoValorSaque = valorOperacao.Text;

            double valorSaque = Convert.ToDouble(textoValorSaque);
            Conta contaSelecionada = this.BuscaContaSelecionada();

            try { 
                contaSelecionada.Saca(valorSaque);
                MessageBox.Show("Dinheiro Liberado");
            }
            catch (SaldoInsuficienteException e)
            {
                MessageBox.Show("Saldo insuficiente. " + e.Message);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Não é possível sacar um valor negativo. " + e.Message);
            }

            this.MostraConta(contaSelecionada);
        }

        private void MostraConta(Conta conta)
        {
            textoTitular.Text = conta.Titular.Nome;
            textoSaldo.Text = Convert.ToString(conta.Saldo);
            textoNumero.Text = Convert.ToString(conta.Numero);
        }

        private Conta BuscaContaSelecionada()
        {
            int indiceSelecionado = comboContas.SelectedIndex;
            return this.contas[indiceSelecionado];
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string titularSelecionado = comboContas.Text;
            Conta contaSelecionada = this.BuscaContaSelecionada();
            this.MostraConta(contaSelecionada);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Conta contaSelecionada = this.BuscaContaSelecionada();
  
            int indiceDaContaDestino = destinoDaTransferencia.SelectedIndex;

            Conta contaDestino = this.contas[indiceDaContaDestino];

            string textoValor = valorOperacao.Text;
            double valorTransferencia = Convert.ToDouble(textoValor);
        
            contaSelecionada.TransferePara(contaDestino, valorTransferencia);

            this.MostraConta(contaSelecionada);

        }
    }
}