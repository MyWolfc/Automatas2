using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDeArreglosEnUnaLinea
{
    internal class Linea
    {
		private int intNumeroLinea;

		public int NumeroDeLinea
		{
			get { return intNumeroLinea; }
			set { intNumeroLinea = value; }
		}

		private string strContenidoDeLinea;

		public string ContenidoDeLinea
		{
			get { return strContenidoDeLinea; }
			set { strContenidoDeLinea = value; }
		}

		private bool blnEsVariable;

		public bool EsVariable
		{
			get { return blnEsVariable; }
			set { blnEsVariable = value; }
		}
		private string intTokenVar;

		private bool blnEsDesicion;

		public bool EsDesicion
		{
			get { return blnEsDesicion; }
			set { blnEsDesicion = value; }
		}
		private string strLadoDesicion;

		public string LadoDesicion
		{
			get { return strLadoDesicion; }
			set { strLadoDesicion = value; }
		}

		private bool blnEsCiclo;

		public bool EsCiclo
		{
			get { return blnEsCiclo; }
			set { blnEsCiclo = value; }
		}
		public string TokenVar
		{
			get { return intTokenVar; }
			set { intTokenVar = value; }
		}

		private string[] strArregloTokens;

		public string[] ArregloTokens
		{
			get { return strArregloTokens; }
			set { strArregloTokens = value; }
		}


	}
}
