using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDeArreglosEnUnaLinea
{
    internal class Variable
    {
		private string strTipoDato;

		public string TipoDeDato
		{
			get { return strTipoDato; }
			set { strTipoDato = value; }
		}
		private string strIdentificador;

		public string Identidicador
		{
			get { return strIdentificador; }
			set { strIdentificador = value; }
		}
		private string strValor;

		public string Valor
		{
			get { return strValor; }
			set { strValor = value; }
		}
		private string strToken;

		public string Token
		{
			get { return strToken; }
			set { strToken = value; }
		}


	}
}
