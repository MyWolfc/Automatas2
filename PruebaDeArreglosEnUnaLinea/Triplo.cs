using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDeArreglosEnUnaLinea
{
    internal class Triplo
    {
		private int intid;

		public int id
		{
			get { return intid; }
			set { intid = value; }
		}


		private string strDatoObjeto;

		public string DatoObjeto
		{
			get { return strDatoObjeto; }
			set { strDatoObjeto = value; }
		}

		private string strDatoFuente;

		public string DatoFuente
		{
			get { return strDatoFuente; }
			set { strDatoFuente = value; }
		}
		private string strOperador;

		public string Operador
		{
			get { return strOperador; }
			set { strOperador = value; }
		}
		private int intCantidadDeTemps;

		public int CantidadDeTemps
		{
			get { return intCantidadDeTemps; }
			set { intCantidadDeTemps = value; }
		}
		public Triplo()
		{

		}
		public Triplo(int i)
		{
			CantidadDeTemps = i;
		}
	}
}
