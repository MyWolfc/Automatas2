﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PruebaDeArreglosEnUnaLinea
{
    public partial class Form1 : Form
    {
        string[,] Cadena;
        Linea[] CadenaTK;
        Linea[] auxCadenaTK;
        Linea[] ParaSinctatico;
        List<Linea> ParaSinctatico2;
        Linea[] CodigoLimpio;
        List<Triplo> ListaTriploMain = new List<Triplo>();
        List<Triplo> ListaTriploTrue = new List<Triplo>();
        List<Triplo> ListaTriploFalse = new List<Triplo>();
        List<Triplo> ListaTriploLoop = new List<Triplo>();
        string RutaEpic = "Server=localhost;Database=pruebalex;Uid=root;Pwd=Juan@20";
        int Estado = 0;
        Variable[] ArregloVarEpico;
        List<Variable> ListaDeVariables = new List<Variable>();
        public Form1()
        {
            InitializeComponent();
            dtgVariables.ReadOnly = true;
            dtgVariables.AllowUserToAddRows = false;
            dtgVariables.AllowUserToDeleteRows = false;
            dtgVariables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgVariables.Columns.Add("Columna Tipo", "Tipo de dato");
            dtgVariables.Columns.Add("Columna Variable", "Nombre Variable");
            dtgVariables.Columns.Add("Columna Valor", "Valor");
            dtgVariables.Columns.Add("Columna Token", "Token");

            dtgTriplos.ReadOnly = true;
            dtgTriplos.AllowUserToAddRows = false;
            dtgTriplos.AllowUserToDeleteRows = false;
            dtgTriplos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgTriplos.Columns.Add("Columna Id", "id");
            dtgTriplos.Columns.Add("Columna Dato Objeto", "Dato Obejto");
            dtgTriplos.Columns.Add("Columna Dato Fuente", "Dato Fuente");
            dtgTriplos.Columns.Add("Columna Operador", "Operador");

            dtgLadoVerdadero.ReadOnly = true;
            dtgLadoVerdadero.AllowUserToAddRows = false;
            dtgLadoVerdadero.AllowUserToDeleteRows = false;
            dtgLadoVerdadero.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgLadoVerdadero.Columns.Add("Columna Id", "id");
            dtgLadoVerdadero.Columns.Add("Columna Dato Objeto", "Dato Obejto");
            dtgLadoVerdadero.Columns.Add("Columna Dato Fuente", "Dato Fuente");
            dtgLadoVerdadero.Columns.Add("Columna Operador", "Operador");

            dtgLadoFalso.ReadOnly = true;
            dtgLadoFalso.AllowUserToAddRows = false;
            dtgLadoFalso.AllowUserToDeleteRows = false;
            dtgLadoFalso.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgLadoFalso.Columns.Add("Columna Id", "id");
            dtgLadoFalso.Columns.Add("Columna Dato Objeto", "Dato Obejto");
            dtgLadoFalso.Columns.Add("Columna Dato Fuente", "Dato Fuente");
            dtgLadoFalso.Columns.Add("Columna Operador", "Operador");

            dtgLoop.ReadOnly = true;
            dtgLoop.AllowUserToAddRows = false;
            dtgLoop.AllowUserToDeleteRows = false;
            dtgLoop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgLoop.Columns.Add("Columna Id", "id");
            dtgLoop.Columns.Add("Columna Dato Objeto", "Dato Obejto");
            dtgLoop.Columns.Add("Columna Dato Fuente", "Dato Fuente");
            dtgLoop.Columns.Add("Columna Operador", "Operador");

        }
        private void btnPasarALexico_Click(object sender, EventArgs e)
        {

            string CODIGO = txtCodigo.Text;
            CODIGO = CODIGO.ToLower();
            //Aqui contamos los saltos de linea
            string[] SaltosLinea = CODIGO.Split('\n');

            int MAYORLINEAPALABRARESERVADAXLINEA = 0;
            //COMO UTILIZAR ARREGLOS BIDIMENSIONALES
            /*for (int i = 0; i < SaltosLinea.Length; i++)
            {
                string[] CONTADORPRXLINEA = SaltosLinea[i].Split(' ');
                int auxCONTADOREPICO = 0;
                for (int j = 0; j < CONTADORPRXLINEA.Length; j++)
                {

                    if (CONTADORPRXLINEA[j] == " ")
                    {

                    }
                    else
                    {
                        auxCONTADOREPICO++;
                        PalabrasReservadasXlinea++;
                        if (auxCONTADOREPICO >= MAYORLINEAPALABRARESERVADAXLINEA)
                        {
                            MAYORLINEAPALABRARESERVADAXLINEA = auxCONTADOREPICO;
                        }
                    }

                }


            }
            */

            Cadena = new string[SaltosLinea.Length, MAYORLINEAPALABRARESERVADAXLINEA];

            //Creamos un arreglo de objetos linea como limite los saltos de linea
            CadenaTK = new Linea[SaltosLinea.Length];
            ParaSinctatico = new Linea[SaltosLinea.Length];
            //CodigoLimpio = new Linea[SaltosLinea.Length];
            for (int i = 0; i < SaltosLinea.Length; i++)
            {
                //string[] CONTADORPRXLINEA = SaltosLinea[i].Split(' ');
                //Creamos un objeto temporal para llenar el arreglo el obejto
                Linea MiLinea = new Linea();
                MiLinea.NumeroDeLinea = i;
                MiLinea.ContenidoDeLinea = SaltosLinea[i];
                CadenaTK[i] = MiLinea;
                ParaSinctatico[i] = MiLinea;
                //CodigoLimpio[i] = MiLinea;
            }
            auxCadenaTK = CadenaTK;
            string Comando = txtCodigo.Text;
            Comando = Comando.ToLower();
            //string[] Token;
            string[] Palabras = Comando.Split(' ');
            //string LP = "";
            //string PA = "";
            //int ContadorPalabras = 0;
            //Token = new string[PalabrasF.Length];
            //MessageBox.Show("Codigo \n" + Comando+ " \nNum de Palabras: " + PalabrasF.Length + " \nLista de palabras:\n" + LP);

            if (Comando != "")
            {
                MySqlConnection Conexion = new MySqlConnection(RutaEpic);
                MySqlCommand cmd;

                Conexion.Open();
                try
                {
                    cmd = Conexion.CreateCommand();
                    cmd.CommandText = "Select * From tablafinatk";
                    MySqlDataAdapter miAdaptador = new MySqlDataAdapter(cmd);
                    DataSet misDatos = new DataSet();
                    miAdaptador.Fill(misDatos);
                    string AUXTOK = "";
                    for (int x = 0; x < CadenaTK.Length; x++)
                    {
                        string[] PalabrasF = CadenaTK[x].ContenidoDeLinea.Split(' ');
                        for (int i = 0; i < PalabrasF.Length; i++)
                        {
                            bool banderita = false;
                            string AuxP = PalabrasF[i];
                            AuxP = AuxP.Replace("\r", "");
                            char[] ArregloEpic = new char[AuxP.Length + 1];

                            for (int m = 0; m < AuxP.Length + 1; m++)
                            {
                                if (m == AuxP.Length)
                                {
                                    ArregloEpic[m] = ';';

                                }
                                else
                                {
                                    ArregloEpic[m] = AuxP[m];

                                }
                            }
                            //for (int j = 0; j < ArregloEpic.Length; j++)
                            //{
                            //    MessageBox.Show(ArregloEpic[i].ToString());
                            //}
                            int k = 0;
                            do
                            {
                                foreach (DataRow row in misDatos.Tables[0].Rows)
                                {
                                    foreach (DataColumn col in misDatos.Tables[0].Columns)
                                    {
                                        //MessageBox.Show(row["ESTADOS"].ToString());

                                        if (banderita)
                                        {
                                            banderita = false;
                                            break;
                                        }
                                        else if ((row["ESTADOS"].ToString() == Estado.ToString()))
                                        {
                                            if ((Convert.ToChar(ArregloEpic[k]).ToString() == col.ColumnName.ToString()))
                                            {
                                                //Ayuda a sabeer en que estaado estamos
                                                //MessageBox.Show("Si encontro" + row[col].ToString());
                                                Estado = int.Parse(row[col].ToString());
                                                if (Estado == 94)
                                                {
                                                    k++;
                                                    banderita = true;
                                                    break;

                                                }
                                                banderita = true;
                                                k++;
                                                break;

                                            }
                                            if (Estado == 94)
                                            {
                                                break;
                                            }
                                        }
                                        if (Estado == 94)
                                        {
                                            break;
                                        }

                                    }
                                    if (Estado == 94)
                                    {
                                        break;
                                    }
                                }
                                if (Estado == 94)
                                {
                                    break;
                                }

                            } while (k < ArregloEpic.Length);
                            //MessageBox.Show("Estado: " + Estado);

                            foreach (DataRow row in misDatos.Tables[0].Rows)
                            {
                                foreach (DataColumn col in misDatos.Tables[0].Columns)
                                {
                                    //MessageBox.Show(row["ESTADOS"].ToString());

                                    if ((row["ESTADOS"].ToString() == Estado.ToString()))
                                    {
                                        if ((Convert.ToChar(ArregloEpic[ArregloEpic.Length - 1]).ToString() == col.ColumnName.ToString()))
                                        {
                                            //nos ayuda a saber el token de la palabra 
                                            //MessageBox.Show("Si encontro" + row["TOKEN"].ToString());
                                            if (k == 0)
                                            {
                                                AUXTOK = row["TOKEN"].ToString();
                                            }
                                            else
                                            {
                                                AUXTOK = AUXTOK + " " + row["TOKEN"].ToString();
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            //
                            Estado = 0;

                        }
                        CadenaTK[x].ContenidoDeLinea = AUXTOK;
                        ParaSinctatico[x].ContenidoDeLinea = AUXTOK;
                        AUXTOK = "";
                        /*
                                                             _              _        _                  
                          __ _  ___ ___  _ __ ___   ___   __| | __ _ _ __  | |_ ___ | | _____ _ __  ___ 
                         / _` |/ __/ _ \| '_ ` _ \ / _ \ / _` |/ _` | '__| | __/ _ \| |/ / _ \ '_ \/ __|
                        | (_| | (_| (_) | | | | | | (_) | (_| | (_| | |    | || (_) |   <  __/ | | \__ \
                         \__,_|\___\___/|_| |_| |_|\___/ \__,_|\__,_|_|     \__\___/|_|\_\___|_| |_|___/
                          */
                        //Token = AUXTOK.Split('\n');

                        //MessageBox.Show(AUXTOK);

                        //for (int x = 0; x < Token.Length; x++)
                        //{
                        //    MessageBox.Show("token " + Token[x]);
                        //}
                        //txtCodigoLexico.Text = AUXTOK;
                    }
                    txtLexico.Text = "";

                    // GlobalArregllo = Token;
                    //MessageBox.Show("Final de lexico");
                }
                catch (Exception X)
                {
                    MessageBox.Show(X.Message);
                    if (Conexion.State == ConnectionState.Open)
                    {
                        //Cerramos la conexion
                        Conexion.Close();
                    }
                }
                finally
                {
                    //Verificar si la conexionEPICA esta abierta de ser asi se cierra
                    if (Conexion.State == ConnectionState.Open)
                    {
                        //Cerramos la conexion
                        Conexion.Close();
                    }
                }
                if (Conexion.State == ConnectionState.Open)
                {
                    //Cerramos la conexion
                    Conexion.Close();
                }
            }
            else
            {
                MessageBox.Show("Error no puede estar vacio el codigo");
            }
            for (int i = 0; i < auxCadenaTK.Length; i++)
            {

                string[] auxiliarVar;
                auxiliarVar = auxCadenaTK[i].ContenidoDeLinea.Split(' ');
                if (!(i == 0))
                {
                    for (int j = 1; j < auxiliarVar.Length; j++)
                    {
                        if (j == 1)
                        {
                            //Identificamos si la palabra reservada es una variable
                            if (auxiliarVar[j] == "TD01" || auxiliarVar[j] == "TD02" || auxiliarVar[j] == "TD03" || auxiliarVar[j] == "TD04" )
                            {
                                auxCadenaTK[i].EsVariable = true;
                                //break; //porque?
                            }
                            //identificamos si es un ciclo
                            if (auxiliarVar[j] == "CI01" || auxiliarVar[j] == "CL02")
                            {
                                auxCadenaTK[i].EsCiclo = true;
                                auxCadenaTK[i + 1].EsCiclo = true;
                                auxCadenaTK[i].ParteDeCiclo = true;
                                auxCadenaTK[i + 1].ParteDeCiclo = true;
                            }

                            //si es un if es lado verdadeero
                            if (auxiliarVar[j] == "PR04" || auxiliarVar[j] == "PR05" )
                            {
                                auxCadenaTK[i].EsDesicion = true;
                                auxCadenaTK[i + 1].EsDesicion = true;
                                auxCadenaTK[i].LadoDesicion = "Verdadero";
                                auxCadenaTK[i + 1].LadoDesicion = "Verdadero";
                            }
                            //si es un else es lado falso (mayor mente)
                            else if(auxiliarVar[j] == "PR06")
                            {
                                auxCadenaTK[i].EsDesicion = true;
                                auxCadenaTK[i + 1].EsDesicion = true;
                                auxCadenaTK[i].LadoDesicion = "Falso";
                                auxCadenaTK[i + 1].LadoDesicion = "Falso";

                            }
                            //aqui buscamos separar el lado True y False para hacer los triplos individuales.
                            if ((auxCadenaTK[i - 1].LadoDesicion == "Verdadero" && !(auxiliarVar[j] == "LL02")))
                            {
                                auxCadenaTK[i].LadoDesicion = "Verdadero";
                            }
                            else if ((auxCadenaTK[i - 1].LadoDesicion == "Falso" && !(auxiliarVar[j] == "LL02")))
                            {
                                auxCadenaTK[i].LadoDesicion = "Falso";
                            }
                            //en este punto queremos saber hasta donde abarca la desicion delimitando
                            if ((auxCadenaTK[i - 1].EsDesicion && !(auxiliarVar[j] == "LL02")))
                            {
                                auxCadenaTK[i].EsDesicion = true;
                                auxCadenaTK[i].EsVariable = false;
                            }
                            // auqui veremos hasta donde lleva el ciclo
                            if ((auxCadenaTK[i - 1].EsCiclo && !(auxiliarVar[j] == "LL02")))
                            {
                                auxCadenaTK[i].EsCiclo = true;
                                auxCadenaTK[i].ParteDeCiclo = true;

                            }


                        }
                    }
                }
                
            }
            
            Linea[] auxlinea = new Linea[CadenaTK.Length];
            //Array.Copy(auxlinea, CadenaTK, CadenaTK.Length);
            //ParaSinctatico = new Linea[auxlinea.Length];
            //Array.Copy(ParaSinctatico,auxlinea,auxlinea.Length);
            //ParaSinctatico2 = new List<Linea>(auxCadenaTK);
            //btnSintactico_Click(sender,e);
            int u_u = 0;
            btnCargarVariables_Click(sender, e);
            ComprobarVariable(ListaDeVariables, auxCadenaTK, CodigoLimpio);
            int o_o = 0;
        }
        private void btnSintactico_Click(object sender, EventArgs e)
        {
            //string PruebaEpica = txtLexico.Text;
            //bool BANDERAEPICA = true;
            //string Error = "Error en";
            ////string Try = " ";
            //string TokensYes = "";
            //string[] TokensValidos = new string[37];
            ////MessageBox.Show("XD: " + PruebaEpica);
            ///*for (int i = 1; i < GlobalArregllo.Length; i++)
            //{
            //    Try = Try + GlobalArregllo[i] + " ";
            //}*/
            ////MessageBox.Show("Test de arreglo:\r" + Try );
            //MySqlConnection Conexion = new MySqlConnection(RutaEpic);

            //MySqlCommand cmd;

            //Conexion.Open();
            //try
            //{
            //    cmd = Conexion.CreateCommand();
            //    cmd.CommandText = "select TOKEN from tablafinatk WHERE TOKEN != ''";
            //    MySqlDataAdapter miAdaptador = new MySqlDataAdapter(cmd);
            //    DataSet misDatos = new DataSet();
            //    miAdaptador.Fill(misDatos);
            //    foreach (DataRow row in misDatos.Tables[0].Rows)
            //    {
            //        //Console.WriteLine(row["EmpID"] + ", " + row["EmpName"] + ", " + row["EmpMobile"]);
            //        //MessageBox.Show("Token" + row["TOKEN"]);
            //        TokensYes = TokensYes + row["TOKEN"].ToString() + " ";
            //    }
            //    TokensValidos = TokensYes.Split(' ');
            //    //MessageBox.Show("token num36: " + TokensValidos[35].ToString() + "\n 36: " + TokensValidos[36].ToString());

            //    for (int i = 0; i < TokensValidos.Length; i++)
            //    {
            //        // MessageBox.Show("Tokens desde la base de datos: " + TokensValidos[i]);
            //    }
            //    //Esta bandera nos ayuda a cachar las excepcioness del if en caso que se use un else if o un else y no exista un IF antes de estos
            //    bool banderitaINT = false;
            //    bool banderitaFLT = false;
            //    bool BanderaIF = false;
            //    bool banderaWHILE = false;
            //    string TraeTODO = "";
            //    string[] GlobalArregllo;
            //    //GlobalArregllo = PruebaEpica.Split(' ','\n');
            //    for (int i = 0; i < CadenaTK.Length; i++)
            //    {
            //        TraeTODO = TraeTODO + "" + CadenaTK[i].ContenidoDeLinea;

            //    }
            //    char[] Delimitadores = { ' ', ';', '\n', '\r' };
            //    GlobalArregllo = TraeTODO.Split(Delimitadores);
            //    for (
            //        int i = 1; i < GlobalArregllo.Length; i++)
            //    {
            //        if (TokensValidos[8] == GlobalArregllo[i])
            //        {
            //            banderaWHILE = true;
            //            break;
            //        }
            //    }
            //    for (int i = 1; i < GlobalArregllo.Length; i++)
            //    {
            //        banderitaINT = false;
            //        banderitaFLT = false;
            //        //Validacion de la palabra inc = COM1 que el programa comience con inc
            //        if (i == 1)
            //        {
            //            //Validacion de la palabra inc = COM1 que el programa comience con inc
            //            if (TokensValidos[0] == GlobalArregllo[i])
            //            {
            //                //MessageBox.Show("Com1 es valido");
            //                BANDERAEPICA = true;

            //            }
            //            else
            //            {
            //                BANDERAEPICA = false;
            //                Error = Error + "El programa tiene que empezar con inc";
            //                break;

            //            }
            //        }
            //        //Validacion de la palabra int = ENT2 LISTO FUNCIONAL
            //        else if (TokensValidos[1] == GlobalArregllo[i])
            //        {
            //            //aqui comprobamos que despues de un INT venga ID
            //            if (GlobalArregllo[i + 1] == TokensValidos[33])
            //            {

            //                if (GlobalArregllo[i + 2] != TokensValidos[30])
            //                {
            //                    //TODO bien aqui saltamos del ciclo las posiciones comparadas de los tokens
            //                    i = i + 2;
            //                }
            //                else if (GlobalArregllo[i + 2] == TokensValidos[30])
            //                {
            //                    if ((GlobalArregllo[i + 3] == TokensValidos[33]) || (GlobalArregllo[i + 3] == TokensValidos[16]))
            //                    {
            //                        //aqui saltamos del ciclo las posiciones comparadas de los tokens

            //                        //Todo chill porque se asigno un varible con un ID o con constante numerica entera

            //                        //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
            //                        if (i + 4 < GlobalArregllo.Length - 1)
            //                        {
            //                            //Aqui verificamos  que despues esto tengo un OPA 
            //                            if ((VerificarOPA(TokensValidos, GlobalArregllo[i + 4])))
            //                            {
            //                                //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
            //                                if (i + 5 < GlobalArregllo.Length)
            //                                {
            //                                    //aqui hacemo referencia que puede ser un ID o CNE
            //                                    if ((GlobalArregllo[i + 5] == TokensValidos[16]) || (GlobalArregllo[i + 5] == TokensValidos[33]))
            //                                    {
            //                                        //aqui saltamos del ciclo las posiciones comparadas de los tokens
            //                                        banderitaINT = true;
            //                                        i = i + 4;
            //                                    }
            //                                    //si es diferente a un ID o CNE en este punto se debe disparara un error
            //                                    else if ((GlobalArregllo[i + 5] != TokensValidos[16]) || (GlobalArregllo[i + 5] != TokensValidos[33]))
            //                                    {
            //                                        //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
            //                                        Error = Error + "  despues del una OPA(+,-,/,*) no se esta refiriendo a un ID o CNE";
            //                                        BANDERAEPICA = false;
            //                                        break;
            //                                    }
            //                                    else
            //                                    {
            //                                        //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
            //                                        Error = Error + "  despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE";
            //                                        BANDERAEPICA = false;
            //                                        break;
            //                                    }
            //                                }


            //                            }
            //                        }
            //                        if (banderitaINT)
            //                        {

            //                        }
            //                        else
            //                        {
            //                            i = i + 2;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        //capturamos el error que despues del igual tiene que tener un ID o una constante numerica entera
            //                        Error = Error + "  despues del igual tiene que tener un ID o una constante numerica entera";
            //                        BANDERAEPICA = false;
            //                        break;
            //                    }
            //                }

            //                i = i + 1;
            //            }
            //            else
            //            {
            //                //capturamos error despues de un tipo de dato debe ir un ID
            //                Error = Error + " despues de un tipo de dato (INT) debe ir un ID";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra imp = PRN3 LISTO FUNCIONAL
            //        else if (TokensValidos[2] == GlobalArregllo[i])
            //        {

            //            if ((GlobalArregllo[i + 1] == TokensValidos[15]) || (GlobalArregllo[i + 1] == TokensValidos[16]) || (GlobalArregllo[i + 1] == TokensValidos[17]) || (GlobalArregllo[i + 1] == TokensValidos[33]))
            //            {
            //                //Comprobamos que la siguiente posicion del token sea && si no no se mostrara error
            //                if (GlobalArregllo[i + 2] == TokensValidos[23])
            //                {
            //                    //Comprobamos que la siguiente posicion del token sea && si no no se mostrara error
            //                    if ((GlobalArregllo[i + 3] == TokensValidos[15]) || (GlobalArregllo[i + 3] == TokensValidos[16]) || (GlobalArregllo[i + 3] == TokensValidos[17]) || (GlobalArregllo[i + 3] == TokensValidos[33]))
            //                    {

            //                    }
            //                    else
            //                    {
            //                        // Capturamos del error despues de IMP tiene que tener un ID o CN o una cadena
            //                        Error = Error + " despues de IMP con un argunmento y un & tiene que tener un ID o CN o una cadena";
            //                        BANDERAEPICA = false;
            //                        break;
            //                    }
            //                    //saltamos del ciclo las posiciones comparadas de los tokens
            //                    i = i + 2;
            //                }
            //                //saltamos del ciclo las posiciones comparadas de los tokens
            //                i = i + 1;

            //            }
            //            else
            //            {
            //                // Capturamos del error despues de IMP tiene que tener un ID o CN o una cadena
            //                Error = Error + " despues de IMP tiene que tener un ID o CN o una cadena";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra flt = FLT5 LISTO FUNCIONAL
            //        else if (TokensValidos[4] == GlobalArregllo[i])
            //        {
            //            //aqui comprobamos que despues de un FLT venga ID
            //            if (GlobalArregllo[i + 1] == TokensValidos[33])
            //            {

            //                if (GlobalArregllo[i + 2] != TokensValidos[30])
            //                {
            //                    //TODO bien aqui saltamos del ciclo las posiciones comparadas de los tokens
            //                    i = i + 2;
            //                }
            //                else if (GlobalArregllo[i + 2] == TokensValidos[30])
            //                {
            //                    if ((GlobalArregllo[i + 3] == TokensValidos[33]) || (GlobalArregllo[i + 3] == TokensValidos[17]))
            //                    {


            //                        //Todo chill porque se asigno un varible con un ID o con constante numerica real

            //                        //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
            //                        if (i + 4 < GlobalArregllo.Length - 1)
            //                        {
            //                            //Aqui verificamos  que despues esto tengo un OPA 
            //                            if ((VerificarOPA(TokensValidos, GlobalArregllo[i + 4])))
            //                            {
            //                                //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
            //                                if (i + 5 < GlobalArregllo.Length)
            //                                {
            //                                    //aqui hacemo referencia que puede ser un ID o CNR
            //                                    if ((GlobalArregllo[i + 5] == TokensValidos[17]) || (GlobalArregllo[i + 5] == TokensValidos[33]))
            //                                    {
            //                                        //aqui saltamos del ciclo las posiciones comparadas de los tokens
            //                                        banderitaFLT = true;
            //                                        i = i + 4;
            //                                    }
            //                                    //si es diferente a un ID o CNE en este punto se debe disparara un error
            //                                    else if ((GlobalArregllo[i + 5] != TokensValidos[17]) || (GlobalArregllo[i + 5] != TokensValidos[33]))
            //                                    {
            //                                        //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
            //                                        Error = Error + "  despues del una OPA(+,-,/,*) no se esta refiriendo a un ID o CNR";
            //                                        BANDERAEPICA = false;
            //                                        break;
            //                                    }
            //                                    else
            //                                    {
            //                                        //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
            //                                        Error = Error + "  despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNR";
            //                                        BANDERAEPICA = false;
            //                                        break;
            //                                    }
            //                                }


            //                            }
            //                        }
            //                        if (banderitaFLT)
            //                        {

            //                        }
            //                        else
            //                        {
            //                            i = i + 2;
            //                        }

            //                    }
            //                    else
            //                    {
            //                        //capturamos el error que despues del igual tiene que tener un ID o una constante numerica entera
            //                        Error = Error + "  despues del igual tiene que tener un ID o una constante numerica real";
            //                        BANDERAEPICA = false;
            //                        break;
            //                    }
            //                }


            //            }
            //            else
            //            {
            //                //capturamos error despues de un tipo de dato debe ir un ID
            //                Error = Error + " despues de un tipo de dato (INT) debe ir un ID";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra fori = fri6 LISTO FUNCIONAL
            //        else if (TokensValidos[5] == GlobalArregllo[i])
            //        {
            //            //en el caaso del ciclo for se tiene que ser muy especifico dado que esperamos en total 11 argumentos 
            //            //con este if evitamos el error por index
            //            if (i + 11 < GlobalArregllo.Length)
            //            {
            //                //la base es esta int X = 1 X < 10 X + 1
            //                // que tenga una declaracion de una variable CNE, despues la variable con la comprobacion una operacion relacional y un CNE
            //                // y por ultimo una operacion aritmetica con un ID el operador y una CNE
            //                if ((TokensValidos[1] == GlobalArregllo[i + 1])
            //               && (TokensValidos[33] == GlobalArregllo[i + 2])
            //               && (TokensValidos[30] == GlobalArregllo[i + 3])
            //               && TokensValidos[16] == GlobalArregllo[i + 4]
            //               && TokensValidos[33] == GlobalArregllo[i + 5]
            //               && VerificarOPL(TokensValidos, GlobalArregllo[i + 6])
            //               && (TokensValidos[16] == GlobalArregllo[i + 7])
            //               && (TokensValidos[33] == GlobalArregllo[i + 8])
            //               && VerificarOPA(TokensValidos, GlobalArregllo[i + 9])
            //               && TokensValidos[16] == GlobalArregllo[i + 10])
            //                {
            //                    i = i + 10;
            //                }
            //                else
            //                {
            //                    //capturamos error despues de la escritura como esta compuesto el fori
            //                    Error = Error + " despues de la escritura como esta compuesto el fori";
            //                    BANDERAEPICA = false;
            //                    break;

            //                }
            //            }
            //            else
            //            {
            //                //capturamos error Faltaa de argumentos en el for ejemplo fori int X = 1 X < 10 X + 1
            //                Error = Error + " Faltaa de argumentos en el for ejemplo fori int X = 1 X < 10 X + 1";
            //                BANDERAEPICA = false;
            //                break;
            //            }

            //        }
            //        //Validacion de la palabra nul = NUL7 LISTO FUNCIONAL
            //        else if (TokensValidos[6] == GlobalArregllo[i])
            //        {
            //            //Aqui comprobamos que despues del nul venga una ID
            //            if (TokensValidos[33] == GlobalArregllo[i + 1])
            //            {

            //            }
            //            else
            //            {
            //                //capturamos error despues de la palabra reservada NUL debe de seguir un identificador valido
            //                Error = Error + " despues de la palabra reservada NUL debe de seguir un identificador valido";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra nuev = NEW8 LISTO FUNCIONAL problema aqui we
            //        else if (TokensValidos[7] == GlobalArregllo[i])
            //        {
            //            //Aqui comprobamos que despues del new venga una ID
            //            if (TokensValidos[33] == GlobalArregllo[i + 1])
            //            {

            //            }
            //            else
            //            {
            //                //capturamos error despues de la palabra reservada NEW debe de seguir un identificador valido
            //                Error = Error + " despues de la palabra reservada NEW debe de seguir un identificador valido";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra whi = whi9 LISTO FUNCIONAL
            //        else if (TokensValidos[8] == GlobalArregllo[i])
            //        {
            //            //Comparamos que se realice una comparacion relacion y son id OPR y una CNE
            //            if ((TokensValidos[33] == GlobalArregllo[i + 1]) && VerificarOPR(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
            //            {
            //                i = i + 3;
            //            }
            //            else
            //            {
            //                //capturamos error despues de la palabra reservada whi tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
            //                Error = Error + " despues de la palabra reservada whi tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra Do = DW10 LISTO FUNCIONAL
            //        else if (TokensValidos[9] == GlobalArregllo[i])
            //        {
            //            if (banderaWHILE)
            //            {

            //            }
            //            else
            //            {
            //                //Atrapamos la excepcion
            //                Error = Error + " No puede utlizar un DO si no tienes definido un while";
            //                BANDERAEPICA = false;
            //                break;

            //            }
            //        }
            //        //Validacion de la palabra si = IF11LISTO FUNCIONAL
            //        else if (TokensValidos[10] == GlobalArregllo[i])
            //        {
            //            BanderaIF = true;
            //            //Comprabos que no se salga del arreglo para evitar error de index del arreglo
            //            if (i + 3 < GlobalArregllo.Length)
            //            {
            //                //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
            //                if (TokensValidos[33] == GlobalArregllo[i + 1] && VerificarOPL(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
            //                {
            //                    //verificamos si viene otro operador relacional para repetir el if de arriba con diferentee
            //                    if (!VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
            //                    {
            //                        if (TokensValidos[11] == GlobalArregllo[i + 4] || TokensValidos[12] == GlobalArregllo[i + 4])
            //                        {
            //                            //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
            //                            Error = Error + " capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova";
            //                            BANDERAEPICA = false;
            //                            break;
            //                        }
            //                        i = i + 3;
            //                    }
            //                    else
            //                    {
            //                        //Comprabos que no se salga del arreglo para evitar error de index del arreglo
            //                        if (i + 7 < GlobalArregllo.Length)
            //                        {
            //                            //Verificamos quee operador reelacional como va
            //                            if (VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
            //                            {
            //                                //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
            //                                if (TokensValidos[33] == GlobalArregllo[i + 5] && VerificarOPL(TokensValidos, GlobalArregllo[i + 6]) && TokensValidos[16] == GlobalArregllo[i + 7])
            //                                {

            //                                    if (TokensValidos[11] == GlobalArregllo[i + 8] || TokensValidos[12] == GlobalArregllo[i + 8])
            //                                    {
            //                                        //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
            //                                        Error = Error + " capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova";
            //                                        BANDERAEPICA = false;
            //                                        break;
            //                                    }
            //                                    i = i + 7;
            //                                }
            //                                else
            //                                {
            //                                    //capturamos error despues de la palabra reservada SI tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
            //                                    Error = Error + " despues del operador logico && ó || ó ! tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
            //                                    BANDERAEPICA = false;
            //                                    break;
            //                                }
            //                            }


            //                        }
            //                        else
            //                        {

            //                            //capturamos error de falta de argumentos
            //                            Error = Error + " error de falta de argumentos despues Operador logico minimo una operacion relacional";
            //                            BANDERAEPICA = false;
            //                            break;
            //                        }
            //                    }

            //                }
            //                else
            //                {
            //                    //capturamos error despues de la palabra reservada SI tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
            //                    Error = Error + " despues de la palabra reservada SI tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
            //                    BANDERAEPICA = false;
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                //capturamos error de falta de argumentos
            //                Error = Error + " error de falta de argumentos minimo una operacion relacional";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra sino = EL12 OJO esta solo va ser valido si se declaro un if antes LISTO FUNCIONAL
            //        else if (TokensValidos[11] == GlobalArregllo[i])
            //        {
            //            if (BanderaIF)
            //            {
            //                //seguimos con el codigo
            //                //Comprabos que no se salga del arreglo para evitar error de index del arreglo
            //                if (i + 3 < GlobalArregllo.Length)
            //                {
            //                    //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
            //                    if (TokensValidos[33] == GlobalArregllo[i + 1] && VerificarOPL(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
            //                    {
            //                        //verificamos si viene otro operador relacional para repetir el if de arriba con diferentee
            //                        if (!VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
            //                        {
            //                            if (TokensValidos[10] == GlobalArregllo[i + 4] || TokensValidos[12] == GlobalArregllo[i + 4])
            //                            {
            //                                //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
            //                                Error = Error + " capturamos error despues de la palabra reservada SINO y toda su sintaxis no puede ir un si o sinova";
            //                                BANDERAEPICA = false;
            //                                break;
            //                            }
            //                            i = i + 3;
            //                        }
            //                        else
            //                        {
            //                            //Comprabos que no se salga del arreglo para evitar error de index del arreglo
            //                            if (i + 7 < GlobalArregllo.Length)
            //                            {
            //                                //Verificamos quee operador reelacional como va
            //                                if (VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
            //                                {
            //                                    //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
            //                                    if (TokensValidos[33] == GlobalArregllo[i + 5] && VerificarOPL(TokensValidos, GlobalArregllo[i + 6]) && TokensValidos[16] == GlobalArregllo[i + 7])
            //                                    {
            //                                        if (TokensValidos[10] == GlobalArregllo[i + 8] || TokensValidos[12] == GlobalArregllo[i + 8])
            //                                        {
            //                                            //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
            //                                            Error = Error + " capturamos error despues de la palabra reservada SINO y toda su sintaxis no puede ir un si o sinova";
            //                                            BANDERAEPICA = false;
            //                                            break;
            //                                        }
            //                                        i = i + 7;
            //                                    }
            //                                    else
            //                                    {
            //                                        //capturamos error despues de la palabra reservada SINO tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
            //                                        Error = Error + " despues del operador logico && ó || ó ! tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
            //                                        BANDERAEPICA = false;
            //                                        break;
            //                                    }
            //                                }


            //                            }
            //                            else
            //                            {

            //                                //capturamos error de falta de argumentos
            //                                Error = Error + " error de falta de argumentos despues Operador logico minimo una operacion relacional";
            //                                BANDERAEPICA = false;
            //                                break;
            //                            }
            //                        }

            //                    }
            //                    else
            //                    {
            //                        //capturamos error despues de la palabra reservada SINO tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
            //                        Error = Error + " despues de la palabra reservada SINO tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
            //                        BANDERAEPICA = false;
            //                        break;
            //                    }
            //                }
            //                else
            //                {
            //                    //capturamos error de falta de argumentos
            //                    Error = Error + " error de falta de argumentos minimo una operacion relacional";
            //                    BANDERAEPICA = false;
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                //Atrapamos la excepcion
            //                Error = Error + " No puede utlizar un SINOVA sin antes utilizar un SI para su desicion";
            //                BANDERAEPICA = false;
            //                break;
            //            }

            //        }
            //        //Validacion de la palabra Sinova = IF13 OJO esta solo va ser valido si se declaro un if antes LISTO FUNCIONAL
            //        else if (TokensValidos[12] == GlobalArregllo[i])
            //        {
            //            if (BanderaIF)
            //            {
            //                //seguimos con el codigo.
            //            }
            //            else
            //            {
            //                //Atrapamos la excepcion.
            //                Error = Error + " No puede utlizar un SINO sin antes utilizar un SI para su desicion";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra str = SR14 LISTO FUNCIONAL
            //        else if (TokensValidos[13] == GlobalArregllo[i])
            //        {
            //            //Comprobamos que siga un ID 
            //            if (GlobalArregllo[i + 1] == TokensValidos[33])
            //            {
            //                //despues puede venir un igual o solo se puede declara pero no usuarse
            //                if (GlobalArregllo[i + 2] != TokensValidos[30])
            //                {
            //                    //TODO bien aqui saltamos del ciclo las posiciones comparadas de los tokens
            //                    i = i + 2;
            //                }
            //                //
            //                else if (GlobalArregllo[i + 2] == TokensValidos[30])
            //                {
            //                    if (GlobalArregllo[i + 3] == TokensValidos[15])
            //                    {
            //                        i = i + 3;
            //                    }
            //                    else
            //                    {
            //                        //capturamos el error que despues del igual tiene que ser una cadena
            //                        Error = Error + "  despues del igual tiene que ser una cadena";
            //                        BANDERAEPICA = false;
            //                        break;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                //capturamos error despues de un tipo de dato debe ir un ID
            //                Error = Error + " despues de un tipo de dato (STR) debe ir un ID";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de la palabra exc = EX15L ISTO FUNCIONAL
            //        else if (TokensValidos[14] == GlobalArregllo[i])
            //        {
            //            if (i + 5 < GlobalArregllo.Length)
            //            {
            //                if (TokensValidos[33] == GlobalArregllo[i + 1] && VerificarOPL(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
            //                {
            //                    if (TokensValidos[20] == GlobalArregllo[i + 4] && TokensValidos[15] == GlobalArregllo[i + 5])
            //                    {
            //                        i = i + 5;
            //                    }
            //                    else
            //                    {
            //                        //capturamos el error despues de operador aritmetica / o la cadena no esta definida
            //                        Error = Error + " despues de operador aritmetica / o la cadena no esta definida";
            //                        BANDERAEPICA = false;
            //                        break;
            //                    }
            //                }
            //                else
            //                {
            //                    //capturamos error despues de la palabra reservada exc tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
            //                    Error = Error + " despues de la palabra reservada exc tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
            //                    BANDERAEPICA = false;
            //                    break;
            //                }

            //            }
            //            else
            //            {
            //                //capturamos error de falta de argumentos
            //                Error = Error + " error de falta de argumentos minimo una operacion relacional y una cadena que suelte la excepcion";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion del comentario // = CO22
            //        else if (TokensValidos[21] == GlobalArregllo[i])
            //        {
            //            //
            //            if (TokensValidos[15] == GlobalArregllo[i + 1])
            //            {
            //                i = i + 1;
            //            }
            //            else
            //            {
            //                Error = Error + " Despues de la querrer comentar // tiene que ir una cadena ejem: «Este_es_un_comentario»";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Validacion de cadena sola que quiere decir que un delimitador de una cadena
            //        else if (TokensValidos[15] == GlobalArregllo[i])
            //        {
            //            //aqui no pasa nada solo se sigue el ciclo normal
            //        }
            //        //Validacion de un identificador idn = id34
            //        else if (TokensValidos[33] == GlobalArregllo[i])
            //        {
            //            if (VerificarOPA(TokensValidos, GlobalArregllo[i + 1]))
            //            {
            //                if ((TokensValidos[33] == GlobalArregllo[i + 2]) || (TokensValidos[16] == GlobalArregllo[i]) || (TokensValidos[17] == GlobalArregllo[i + 2]))
            //                {
            //                    i = i + 2;
            //                }
            //                else
            //                {
            //                    //Despues de un OPA tiene que ir un CN 0 un ID
            //                    Error = Error + " Despues de un Operacion artimetica (+-/*) tiene que ir un CN 0 un ID";
            //                    BANDERAEPICA = false;
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                //cachamos el error de un ID solo
            //                Error = Error + " Despues de un id solo puede ir un Operacion artimetica (+-/*) y constante numerica pero no puede ir solo";
            //                BANDERAEPICA = false;
            //                break;
            //            }
            //        }
            //        //Quiebre en que caso que tenga un token de error
            //        else if (TokensValidos[34] == GlobalArregllo[i])
            //        {

            //            Error = Error + " se decteto un error de TOKEN (LITERAL EL TOKEN DICE ERROR)";
            //            BANDERAEPICA = false;
            //            break;
            //        }
            //        //vaalidacion de abrir llave "{"
            //        /*else if (TokensValidos[36] == GlobalArregllo[i])
            //        {
            //            if (TokensValidos[36] != GlobalArregllo[i+1])
            //            {

            //            }
            //            else
            //            {
            //                BANDERAEPICA = false;
            //                Error = Error + "No puedes colocar dos inicio de llaves {{";
            //                break;
            //            }
            //        }*/
            //        //vaalidacion de cerrar llave "}"
            //        else if (TokensValidos[37] == GlobalArregllo[i])
            //        {

            //        }
            //        //Validacion de la palabra fin = END4 que el program termine con fin
            //        if (i == GlobalArregllo.Length - 1)
            //        {
            //            //Validacion de la palabra fin = END4 que el program termine con fin
            //            if (TokensValidos[3] == GlobalArregllo[i])
            //            {
            //                //MessageBox.Show("End4 es valido");
            //                BANDERAEPICA = true;
            //            }
            //            else
            //            {
            //                BANDERAEPICA = false;
            //                Error = Error + "El programa tiene que finalizar con fin";
            //                break;
            //            }
            //        }
            //    }
            //    if (BANDERAEPICA)
            //    {
            //        MessageBox.Show("Analizador sintactico: correctamente pasado");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Analizador sintactico: Inconrrectamente en " + Error);
            //    }
            //    if (Conexion.State == ConnectionState.Open)
            //    {
            //        //Cerramos la conexion
            //        Conexion.Close();
            //    }
            //}
            //catch (Exception X)
            //{
            //    MessageBox.Show(X.Message);
            //    if (Conexion.State == ConnectionState.Open)
            //    {
            //        //Cerramos la conexion
            //        Conexion.Close();
            //    }

            //}
            //finally
            //{
            //    //Verificar si la conexionEPICA Esta abierta de ser asi se cierra
            //    if (Conexion.State == ConnectionState.Open)
            //    {
            //        //Cerramos la conexion
            //        Conexion.Close();
            //    }
            //}
            MessageBox.Show("Analizador sintactico: correctamente pasado");

        }
        private bool VerificarOPA(string[] TokensValidos, string OPA)
        {
            string opa = OPA;
            bool Banderita = false;
            if ((TokensValidos[18] == opa) || (TokensValidos[19] == opa) || (TokensValidos[20] == opa) || (TokensValidos[22] == opa))
            {
                Banderita = true;
            }
            return Banderita;
        }
        private bool VerificarOPR(string[] TokensValidos, string OPA)
        {
            string opa = OPA;
            bool Banderita = false;
            if ((TokensValidos[23] == opa) || (TokensValidos[24] == opa) || (TokensValidos[25] == opa))
            {
                Banderita = true;
            }
            return Banderita;
        }
        private bool VerificarOPL(string[] TokensValidos, string OPA)
        {
            string opa = OPA;
            bool Banderita = false;
            if ((TokensValidos[26] == opa) || (TokensValidos[27] == opa) || (TokensValidos[28] == opa) || (TokensValidos[29] == opa) || (TokensValidos[31] == opa) || (TokensValidos[32] == opa))
            {
                Banderita = true;
            }
            return Banderita;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //NOTA LOGRASTE CAPTURAR LA LINEA DONDE HAY UNA VARIABLE
            //PERO AHORA TIENES QUE ENCONTRAR LA MANERA DE TRAER ESE VALOR EN CONDIGO

            //Aqui tomaremos el valor directamente del TXTCodigo

            //Aqui contamos los saltos de linea

            string CODIGO = txtCodigo.Text;
            CODIGO = CODIGO.ToLower();
            string[] SaltosLinea = CODIGO.Split('\n');

            //Todo epico aqui
            CodigoLimpio = new Linea[SaltosLinea.Length];
            for (int i = 0; i < SaltosLinea.Length; i++)
            {
                //string[] CONTADORPRXLINEA = SaltosLinea[i].Split(' ');
                //Creamos un objeto temporal para llenar el arreglo el obejto
                Linea MiLinea = new Linea();
                MiLinea.NumeroDeLinea = i;
                MiLinea.ContenidoDeLinea = SaltosLinea[i];
                //CadenaTK[i] = MiLinea;
                CodigoLimpio[i] = MiLinea;
            }
            //Todo epico aqui importante aqui captamos cual es la linea que declara una variable
            string var = "";
            int ContadorDeVariables = 0;
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                if (CadenaTK[i].EsVariable)
                {
                    CodigoLimpio[i].EsVariable = true;
                    ContadorDeVariables++;
                }
            }
            //Aqui simplemente cocactenamos
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                if (CodigoLimpio[i].EsVariable)
                {
                    var = var + " " + CodigoLimpio[i].ContenidoDeLinea;
                }
            }
            int ContDeVar = 0;
            Variable[] ArregloDeVariables = new Variable[ContadorDeVariables];
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {

                string[] ValoresVar;
                Variable[] VA = new Variable[ContadorDeVariables];
                Variable V = new Variable();

                if (CodigoLimpio[i].EsVariable)
                {

                    ValoresVar = CodigoLimpio[i].ContenidoDeLinea.Split(' ');
                    V.TipoDeDato = ValoresVar[0];
                    V.Identidicador = ValoresVar[1];
                    if (ValoresVar.Length <= 4)
                    {
                        V.Valor = ValoresVar[3];
                    }
                    else
                    {
                        for (int j = 3; j < ValoresVar.Length; j++)
                        {
                            V.Valor = V.Valor + " " + ValoresVar[j];
                        }
                    }

                    ArregloDeVariables[ContDeVar] = V;

                    ContDeVar++;
                    // MessageBox.Show("Variables en objeto\n Tipo de dato: " + V.TipoDeDato + "\n Identificador: " + V.Identidicador + "\n Valor: " + V.Valor);

                }
            }
            /*for (int i = 0; i < ArregloDeVariables.Length; i++)
            {
                MessageBox.Show("Variables en objeto\n Tipo de dato: " + ArregloDeVariables[i].TipoDeDato + "\n Identificador: " + ArregloDeVariables[i].Identidicador + "\n Valor: " + ArregloDeVariables[i].Valor);
            }*/
            dtgVariables.Rows.Clear();
            foreach (Variable v in ArregloDeVariables)
            {
                dtgVariables.Rows.Add(v.TipoDeDato, v.Identidicador, v.Valor);
            }
            void GenerarTXTCODIGO()
            {
                MessageBox.Show("GUARDAR CODIGO TXT");
                SaveFileDialog salvar = new SaveFileDialog();
                salvar.Filter = "Archivo txt | *.txt";
                if (salvar.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter escribir = new StreamWriter(salvar.FileName);
                    escribir.WriteLine(txtCodigo.Text);
                    escribir.Close();

                }
            }
            void GenerarTXTLexico()
            {
                MessageBox.Show("GUARDAR TXT LEXICO");
                SaveFileDialog guardar = new SaveFileDialog();
                guardar.Filter = "Archivo txt | *.txt";
                if (guardar.ShowDialog() == DialogResult.OK)
                {

                    StreamWriter escribir = new StreamWriter(guardar.FileName);
                    MessageBox.Show($"{txtLexico.Text}");
                    escribir.WriteLine(txtLexico.Text);
                    escribir.Close();
                }

            }
            void GenerarTXTVar()
            {
                MessageBox.Show("GUARDAR TXT VARIABLES");
                SaveFileDialog guardar = new SaveFileDialog();
                guardar.Filter = "Archivo txt | *.txt";
                if (guardar.ShowDialog() == DialogResult.OK)
                {

                    StreamWriter Crear = new StreamWriter(guardar.FileName);
                    string VariablesEpicas = "";
                    for (int i = 0; i < CodigoLimpio.Length; i++)
                    {
                        if (CodigoLimpio[i].EsVariable)
                        {
                            VariablesEpicas = VariablesEpicas + " " + CodigoLimpio[i].ContenidoDeLinea + "\n";
                        }
                    }

                    Crear.WriteLine(VariablesEpicas);
                    Crear.Close();
                }


            }
            GenerarTXTCODIGO();
            GenerarTXTLexico();
            GenerarTXTVar();
            MessageBox.Show("se ha generado correctamente el archivo lexico y tambien el archivo de variables");
            //MessageBox.Show("Test");


        }
        private void btnSalir_Click(object sender, EventArgs e)
        {

            //File.Delete(@" C:\Users\Mein_\Documents\CodigosEpicos\miarchivolexico.txt");
            //File.Delete(@" C:\Users\Mein_\Documents\CodigosEpicos\miarchivovariables.txt");
            Application.Exit();
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();
            Abrir.Filter = "txt files (*.txt)|*.txt";
            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                txtCodigo.Text = File.ReadAllText(Abrir.FileName);
            }
            Abrir.Dispose();
        }
        //Boton para cagar las variables
        private void btnCargarVariables_Click(object sender, EventArgs e)
        {
            string CODIGO = txtCodigo.Text;
            CODIGO = CODIGO.ToLower();
            string[] SaltosLinea = CODIGO.Split('\n');
            string[] Numeros = new string[1000];
            int contadorConstNum = 0;
            int contadorConstReal = 0;
            int contadorCadenas = 0;
            for (int i = 0; i < Numeros.Length; i++)
            {
                string AuxNum = i.ToString();
                Numeros[i] = AuxNum;
            }
            /* Para mi yo del futuro espero que se nos ocurra algo porque estoy en 0 pero tengo una idea: creamos una arreglo
            variable[] par verificar si existe algún numero del arreglo numeros, pero antes de esto preguntar si esa constante 
            ya existe el programa y reservarla y que la pase y en el metodo comprobrar variables hacer la misma funcionalidad 
            con los ID, PD: si funciono :D
            */
            //Todo epico aqui
            CodigoLimpio = new Linea[SaltosLinea.Length];
            for (int i = 0; i < SaltosLinea.Length; i++)
            {
                //string[] CONTADORPRXLINEA = SaltosLinea[i].Split(' ');
                //Creamos un objeto temporal para llenar el arreglo el obejto
                Linea MiLinea = new Linea();
                MiLinea.NumeroDeLinea = i;
                MiLinea.ContenidoDeLinea = SaltosLinea[i];
                //CadenaTK[i] = MiLinea;
                CodigoLimpio[i] = MiLinea;
            }
            //en este for contamos las constantes numericas enteras
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                string[] auxiliarVar;
                auxiliarVar = CodigoLimpio[i].ContenidoDeLinea.Split(' ');
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    auxiliarVar[j] = auxiliarVar[j].Trim();
                }
                //Este for es para atrapar la cantidad de constantes numericas enteras
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    string aver = auxiliarVar[j];
                    int n = 0;
                    bool result = Int32.TryParse(auxiliarVar[j], out n);
                    if (result)
                    {
                        contadorConstNum++;
                    }
                }
                //Este for es para atrapar la cantidad de constante numericas reales
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    string aver = auxiliarVar[j];
                    float n = 0;
                    bool result = float.TryParse(auxiliarVar[j], out n);
                    if (result)
                    {
                        contadorConstReal++;
                    }
                }
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    string aver = auxiliarVar[j];
                    if (auxiliarVar[j][0] == '«')
                    {
                        contadorCadenas++;
                    }
                }
                //este for es para atrapar la cantidad de variables y marcarlas en el objeto linea
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    if (j == 0)
                    {
                        if (auxiliarVar[j] == "int" || auxiliarVar[j] == "flt" || auxiliarVar[j] == "str" || auxiliarVar[j] == "nul")
                        {

                            CodigoLimpio[i].EsVariable = true;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                string[] auxiliarVar;
                string auxcontenido = CodigoLimpio[i].ContenidoDeLinea;
                auxcontenido = auxcontenido.Replace('\r',' ');
                auxiliarVar = auxcontenido.Split(' ');
                if (i==0)
                {

                }
                else
                {
                    if (auxiliarVar[0] == "si" || auxiliarVar[0] == "sino" || auxiliarVar[0] == "sinova")
                    {
                        CodigoLimpio[i].EsDesicion = true;
                    }
                   
                    if ((CodigoLimpio[i-1].EsDesicion && !(auxiliarVar[0] == "}")))
                    {
                        
                            CodigoLimpio[i].EsDesicion = true;
                    }
                    
                }
            }

            //Todo epico aqui importante aqui captamos cual es la linea que declara una variable
            string var = "";
            int ContadorDeVariables = 0;
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                if (CodigoLimpio[i].EsVariable)
                {

                    ContadorDeVariables++;
                    if (ContadorDeVariables < 10)
                    {
                        CodigoLimpio[i].TokenVar = "ID0" + (ContadorDeVariables);
                        CodigoLimpio[i].EsVariable = true;
                        //Aqui simplemente cocactenamos
                        CodigoLimpio[i].ContenidoDeLinea = CodigoLimpio[i].ContenidoDeLinea;
                        var = var + " " + CodigoLimpio[i].ContenidoDeLinea;
                    }
                    else
                    {
                        CodigoLimpio[i].TokenVar = "ID" + (ContadorDeVariables);
                        CodigoLimpio[i].EsVariable = true;
                        //Aqui simplemente cocactenamos
                        CodigoLimpio[i].ContenidoDeLinea = CodigoLimpio[i].ContenidoDeLinea;
                        var = var + " " + CodigoLimpio[i].ContenidoDeLinea;
                    }

                }
            }

            //Usamos un contador de variables para crear el arreglo
            Variable[] ArregloDeVariables = new Variable[ContadorDeVariables];
            ArregloDeVariables = ArregloIdentificador(ArregloDeVariables);
            //Este es el arrreglo de constantes numericas enteras
            Variable[] ArregloDeConstantesEnteras = new Variable[contadorConstNum];
            //el metodo nos regresa el arreglo con las constantes numericas ya listas
            ArregloDeConstantesEnteras = ArregloDeNumEnteros(ArregloDeConstantesEnteras);
            //Aqui creamos el arreglo de constante reales
            Variable[] ArregloDeConstantesReales = new Variable[contadorConstReal];
            ArregloDeConstantesReales = ArregloDeNumEnteros(ArregloDeConstantesReales, ArregloDeConstantesEnteras);//puede que funcione? no funciona como quiero D: .

            Variable[] ArregloDeCadenas = new Variable[contadorCadenas];
            ArregloDeCadenas = ArregloDeCadenasTotal(ArregloDeCadenas);

            //Eliminamos los espacios vacios del arreglo
            int ArregloConstReales = EliminarNulls(ArregloDeConstantesReales);
            int ArregloConstEnteras = EliminarNulls(ArregloDeConstantesEnteras);
            int cantidadArregloDeCadenas = EliminarNulls(ArregloDeCadenas); 

            int maxArreglo = 0;
             maxArreglo = ArregloDeVariables.Length + ArregloConstEnteras + ArregloConstReales + cantidadArregloDeCadenas;
            //List<Variable> ListaFinalDeVar = new List<Variable>();
            //inicializamos el arreglo global para poder usarlo 
            ListaDeVariables.Clear();
            ArregloVarEpico = new Variable[maxArreglo];
            for (int i = 0; i < ArregloDeVariables.Length; i++)
            {

                ArregloVarEpico[i] = ArregloDeVariables[i];
                ListaDeVariables.Add(ArregloDeVariables[i]);
            }
            for (int i = 0; i < ArregloDeConstantesEnteras.Length; i++)
            {
                if (!(ArregloDeConstantesEnteras[i] == null))
                {
                    ListaDeVariables.Add(ArregloDeConstantesEnteras[i]);

                }
            }
            for (int i = 0; i < ArregloDeConstantesReales.Length; i++)
            {
                if (!(ArregloDeConstantesReales[i] == null))
                {
                    ListaDeVariables.Add(ArregloDeConstantesReales[i]);

                }
            }
            for (int i = 0; i < ArregloDeCadenas.Length; i++)
            {
                if (!(ArregloDeCadenas[i] == null))
                {
                    ListaDeVariables.Add(ArregloDeCadenas[i]);
                }
            }
            //if (!(ArregloConstEnteras==0))
            //{
            //    int calmao = 0;
            //    for (int i = ArregloDeVariables.Length; i < ArregloDeVariables.Length + ArregloDeConstantesEnteras.Length; i++)
            //    {
            //        ArregloVarEpico[i] = ArregloDeConstantesEnteras[calmao];
            //        calmao++;
            //    }
            //}
            //if (!(ArregloConstReales == 0))
            //{
            //    int DondeTerminoCNE = ArregloDeVariables.Length + ArregloDeConstantesEnteras.Length;
            //    int calmao2 = 0;
            //    //ni idea dee porque le habia puesto mas 1
            //    if (DondeTerminoCNE == ArregloVarEpico.Length)
            //    {
            //        for (int i = DondeTerminoCNE; i < ArregloVarEpico.Length; i++)
            //        {
            //            ArregloVarEpico[i] = ArregloDeConstantesReales[calmao2];
            //            calmao2++;
            //        }
            //    }
            //    else
            //    {
            //        for (int i = DondeTerminoCNE- 1; i < ArregloVarEpico.Length; i++)
            //        {
            //            ArregloVarEpico[i] = ArregloDeConstantesReales[calmao2];
            //            calmao2++;
            //        }
            //    }
            //}



            //ArregloVarEpico = ArregloDeVariables + ArregloDeConstantesEnteras;
            dtgVariables.Rows.Clear();
            foreach (Variable v in ListaDeVariables)
            {
                if (!(v == null))
                {
                    dtgVariables.Rows.Add(v.TipoDeDato, v.Identidicador, v.Valor, v.Token);

                }
            }
        }
        private void ComprobarVariable(List<Variable> ArregloDelMetodo, Linea[] ArregloDeTokens, Linea[] ArregloCodigoLimpio)
        {

            //comprobar variables
            for (int i = 0; i < ArregloCodigoLimpio.Length; i++)
            {
                string[] auxLim = new string[ArregloCodigoLimpio[i].ContenidoDeLinea.Length];
                string[] auxTok = new string[ArregloDeTokens[i].ContenidoDeLinea.Length + 1];

                auxLim = ArregloCodigoLimpio[i].ContenidoDeLinea.Split(' ');
                for (int j = 0; j < auxLim.Length; j++)
                {
                    auxLim[j] = auxLim[j].Trim();
                }


                auxTok = ArregloDeTokens[i].ContenidoDeLinea.Split(' ', '\r');
                auxTok = auxTok.Skip(1).ToArray();
                for (int j = 0; j < ArregloDelMetodo.Count; j++)
                {
                    for (int k = 0; k < auxLim.Length; k++)
                    {
                        if (!(ArregloDelMetodo[j] == null))
                        {

                            string VariableDelMal = auxLim[k];
                            if (ArregloDelMetodo[j].Identidicador == VariableDelMal)
                            {
                                auxTok[k] = ArregloDelMetodo[j].Token;
                            }
                        }
                    }
                }
                string EpicoFinalToken = "";
                for (int j = 0; j < auxTok.Length; j++)
                {
                    EpicoFinalToken = EpicoFinalToken + " " + auxTok[j];
                }
                ArregloDeTokens[i].ContenidoDeLinea = EpicoFinalToken;
            }
            for (int q = 0; q < ArregloDeTokens.Length; q++)
            {
                //MessageBox.Show("Token es: " + CadenaTK[q].ContenidoDeLinea);
                if (q == 0)
                {
                    txtLexico.Text = ArregloDeTokens[q].ContenidoDeLinea;
                }
                else
                {

                    txtLexico.Text = txtLexico.Text + Environment.NewLine + ArregloDeTokens[q].ContenidoDeLinea;
                }
            }
        }
        private bool ExisteAqui(Variable[] ArregloAVerificar, string Comparando)
        {
            bool banderita = false;
            for (int i = 0; i < ArregloAVerificar.Length; i++)
            {
                if (ArregloAVerificar[i] == null)
                {
                    banderita = false;
                    break;
                }
                if (ArregloAVerificar[i].Identidicador == Comparando)
                {
                    banderita = true;
                    break;
                }
            }
            return banderita;
        }
        private int EliminarNulls(Variable[] ArregloNoNulls)
        {
            int LonigutudReal = 0;
            for (int i = 0; i < ArregloNoNulls.Length; i++)
            {
                if (!(ArregloNoNulls[i] == null))
                {
                    LonigutudReal++;
                }
            }
            return LonigutudReal;
        }
        private Variable[] ArregloDeNumEnteros(Variable[] ADCNE)
        {
            int contaCN = 0;
            int cnparaArrreglo = 0;
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                //Variable[] ConstE = new Variable[contadorConstNum];

                string[] auxiliarVar;
                auxiliarVar = CodigoLimpio[i].ContenidoDeLinea.Split(' ');
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    auxiliarVar[j] = auxiliarVar[j].Trim();
                    //MessageBox.Show("Codigo limpio: " + auxiliarVar[j] );
                }
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    Variable Num = new Variable();
                    string aver = auxiliarVar[j];
                    int n = 0;
                    bool result = Int32.TryParse(auxiliarVar[j], out n);
                    if (result)
                    {
                        if (!ExisteAqui(ADCNE, aver))
                        {
                            contaCN++;
                            if (contaCN < 10)
                            {
                                Num.Identidicador = aver;
                                Num.Token = "CE0" + contaCN;
                                Num.Valor = aver;
                                Num.TipoDeDato = "Num. Entero";
                            }
                            else
                            {
                                Num.Identidicador = aver;
                                Num.Token = "CE" + contaCN;
                                Num.Valor = aver;
                                Num.TipoDeDato = "Num. Entero";
                            }
                            ADCNE[cnparaArrreglo] = Num;
                            cnparaArrreglo++;
                        }
                    }

                }
            }
            return ADCNE;
        }
        private Variable[] ArregloDeCadenasTotal(Variable[] ADCNE)
        {
            int contaCN = 0;
            int cnparaArrreglo = 0;
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                //Variable[] ConstE = new Variable[contadorConstNum];

                string[] auxiliarVar;
                auxiliarVar = CodigoLimpio[i].ContenidoDeLinea.Split(' ');
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    auxiliarVar[j] = auxiliarVar[j].Trim();
                    //MessageBox.Show("Codigo limpio: " + auxiliarVar[j] );
                }
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    Variable Num = new Variable();
                    string aver = auxiliarVar[j];
                    int n = 0;
                    if (auxiliarVar[j][0]== '«')
                    {
                        if (!ExisteAqui(ADCNE, aver))
                        {
                            contaCN++;
                            if (contaCN < 10)
                            {
                                
                                Num.Identidicador = aver;
                                Num.Token = "CA0" + contaCN;
                                aver = aver.Replace('«', ' ');
                                aver = aver.Replace('»', ' ');
                                Num.Valor = aver;
                                Num.TipoDeDato = "Cadena";
                            }
                            else
                            {
                                Num.Identidicador = aver;
                                Num.Token = "CE" + contaCN;
                                aver = aver.Replace('«', ' ');
                                aver = aver.Replace('»', ' ');
                                Num.Valor = aver;
                                Num.TipoDeDato = "Cadena";
                            }
                            ADCNE[cnparaArrreglo] = Num;
                            cnparaArrreglo++;
                        }
                    }

                }
            }
            return ADCNE;
        }
        private Variable[] ArregloDeNumEnteros(Variable[] ArregloDeConstantesReales, Variable[] ArregloDeConstantesEnteras)
        {
            int contaCN = 0;
            int cnparaArrreglo = 0;
            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                //Variable[] ConstE = new Variable[contadorConstNum];

                string[] auxiliarVar;
                auxiliarVar = CodigoLimpio[i].ContenidoDeLinea.Split(' ');
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    auxiliarVar[j] = auxiliarVar[j].Trim();
                    //MessageBox.Show("Codigo limpio: " + auxiliarVar[j] );
                }
                for (int j = 0; j < auxiliarVar.Length; j++)
                {
                    Variable Num = new Variable();
                    string aver = auxiliarVar[j];
                    float n = 0;
                    bool result = float.TryParse(auxiliarVar[j], out n);
                    if (result)
                    {
                        if (!(ExisteAqui(ArregloDeConstantesEnteras, aver)))
                        {
                            if (!(ExisteAqui(ArregloDeConstantesReales, aver)))
                            {
                                contaCN++;
                                if (contaCN < 10)
                                {
                                    Num.Identidicador = aver;
                                    Num.Token = "CR0" + contaCN;
                                    Num.Valor = aver;
                                    Num.TipoDeDato = "Num. Real";
                                }
                                else
                                {
                                    Num.Identidicador = aver;
                                    Num.Token = "CR" + contaCN;
                                    Num.Valor = aver;
                                    Num.TipoDeDato = "Num. Real";
                                }
                                int X = 0;
                                ArregloDeConstantesReales[cnparaArrreglo] = Num;
                                cnparaArrreglo++;
                            }
                        }
                    }
                }
            }
            return ArregloDeConstantesReales;
        }
        private Variable[] ArregloIdentificador(Variable[] ArregloDeVariables)
        {
            int ContDeVar = 0;

            for (int i = 0; i < CodigoLimpio.Length; i++)
            {
                string[] ValoresVar;
                Variable[] VA = new Variable[ArregloDeVariables.Length];
                Variable V = new Variable();
                if (CodigoLimpio[i].EsVariable)
                {

                    ValoresVar = CodigoLimpio[i].ContenidoDeLinea.Split(' ');
                    V.Token = CodigoLimpio[i].TokenVar;
                    V.TipoDeDato = ValoresVar[0];
                    V.Identidicador = ValoresVar[1];
                    if (ValoresVar.Length <= 4)
                    {
                        V.Valor = ValoresVar[3];
                    }
                    else
                    {
                        for (int j = 3; j < ValoresVar.Length; j++)
                        {
                            V.Valor = V.Valor + " " + ValoresVar[j];
                        }
                    }
                    ArregloDeVariables[ContDeVar] = V;
                    ContDeVar++;
                    //MessageBox.Show("Variables en objeto\n Tipo de dato: " + V.TipoDeDato + "\n Identificador: " + V.Identidicador + "\n Valor: " + V.Valor +"\n Token: " + V.Token);
                }
            }
            return ArregloDeVariables;
        }
        private void btnSinctatcio2_Click(object sender, EventArgs e)
        {
            //tenemos que usar el arreglo auxCadenaTK ahora como lo haremos ? XD
            //primero recoremos el arreglo de tokens
            for (int m = 0; m < auxCadenaTK.Length; m++)
            {
                if (auxCadenaTK[m].EsVariable)
                {
                    string cadenainfija = auxCadenaTK[m].ContenidoDeLinea;

                    string[] CambioOPA = cadenainfija.Split(' ');
                    string aux = "";
                    for (int i = 0; i < CambioOPA.Length; i++)
                    {
                        CambioOPA[i] = CambioOPA[i].Trim();
                        aux = aux + CambioOPA[i];
                    }
                    string aux2 = OPA_TK(CambioOPA);
                    cadenainfija = cadenainfija.Replace(" ", "");
                    aux2 = aux2.Replace(" ", "");
                    string PostFijaFinal = InfixToPostfix(aux2);
                    List<string> ListaDePostFija = new List<string>();

                    string AuxP = PostFijaFinal;
                    AuxP = AuxP.Replace("+", "OA01");
                    AuxP = AuxP.Replace("-", "OA02");
                    AuxP = AuxP.Replace("/", "OA03");
                    AuxP = AuxP.Replace("*", "OA04");
                    AuxP = AuxP.Replace("=", "PR09");

                    AuxP = AuxP.Replace("<", "OR01");
                    AuxP = AuxP.Replace(">", "OR02");
                    AuxP = AuxP.Replace("<=", "OR03");
                    AuxP = AuxP.Replace(">=", "OR04");
                    AuxP = AuxP.Replace("==", "OR05");
                    
                    AuxP = AuxP.Replace("&", "OL01");
                    AuxP = AuxP.Replace("||", "OL02");
                    AuxP = AuxP.Replace("!", "OL03");
                    //AuxP = AuxP.Replace("(", "PAR1");
                    //AuxP = AuxP.Replace(")", "PAR2");

                    char[] ArregloEpic = new char[AuxP.Length];

                    for (int pm = 0; pm < AuxP.Length; pm++)
                    {
                        ArregloEpic[pm] = AuxP[pm];
                    }

                    //for (int i = 0; i < ArregloEpic.Length; i += 4)
                    //{
                    //    if (i + 3 < ArregloEpic.Length)
                    //    {
                    //        ListaDePostFija.Add(new string(ArregloEpic, i, 4));
                    //    }
                    //    else
                    //    {
                    //        ListaDePostFija.Add(new string(ArregloEpic, i, ArregloEpic.Length - i));
                    //    }
                    //}
                    string OwO = "";
                    for (int i = 0; i < ArregloEpic.Length; i++)
                    {

                        OwO = OwO + ArregloEpic[i];
                        if (OwO.Length == 4)
                        {
                            ListaDePostFija.Add(OwO);

                            OwO = "";
                        }
                    }
                    AuxP = "";
                    for (int i = 0; i < ListaDePostFija.Count; i++)
                    {
                        if (i == 0)
                        {
                            AuxP = ListaDePostFija[i];
                        }
                        else
                        {
                            AuxP = AuxP + " " + ListaDePostFija[i];
                        }
                    }
                    int T_T = 0;
                    auxCadenaTK[m].ContenidoDeLinea = AuxP;
                }
                if (auxCadenaTK[m].EsDesicion)
                {
                    string cadenainfija = auxCadenaTK[m].ContenidoDeLinea;

                    string[] CambioOPA = cadenainfija.Split(' ');
                    string aux = "";
                    for (int i = 0; i < CambioOPA.Length; i++)
                    {
                        CambioOPA[i] = CambioOPA[i].Trim();
                        aux = aux + CambioOPA[i];
                    }
                    string aux2 = OPA_TK(CambioOPA);
                    cadenainfija = cadenainfija.Replace(" ", "");
                    aux2 = aux2.Replace(" ", "");
                    string PostFijaFinal = InfixToPostfix(aux2);
                    List<string> ListaDePostFija = new List<string>();

                    string AuxP = PostFijaFinal;
                    AuxP = AuxP.Replace("+", "OA01");
                    AuxP = AuxP.Replace("-", "OA02");
                    AuxP = AuxP.Replace("/", "OA03");
                    AuxP = AuxP.Replace("*", "OA04");
                    AuxP = AuxP.Replace("=", "PR09");

                    AuxP = AuxP.Replace("<", "OR01");
                    AuxP = AuxP.Replace(">", "OR02");
                    AuxP = AuxP.Replace("<=", "OR03");
                    AuxP = AuxP.Replace(">=", "OR04");
                    AuxP = AuxP.Replace("==", "OR05");

                    AuxP = AuxP.Replace("&", "OL01");
                    AuxP = AuxP.Replace("||", "OL02");
                    AuxP = AuxP.Replace("!", "OL03");
                    //AuxP = AuxP.Replace("(", "PAR1");
                    //AuxP = AuxP.Replace(")", "PAR2");

                    char[] ArregloEpic = new char[AuxP.Length];
                    for (int pm = 0; pm < AuxP.Length; pm++)
                    {
                        ArregloEpic[pm] = AuxP[pm];
                    }
                    //for (int i = 0; i < ArregloEpic.Length; i += 4)
                    //{
                    //    if (i + 3 < ArregloEpic.Length)
                    //    {
                    //        ListaDePostFija.Add(new string(ArregloEpic, i, 4));
                    //    }
                    //    else
                    //    {
                    //        ListaDePostFija.Add(new string(ArregloEpic, i, ArregloEpic.Length - i));
                    //    }
                    //}
                    string OwO = "";
                    for (int i = 0; i < ArregloEpic.Length; i++)
                    {

                        OwO = OwO + ArregloEpic[i];
                        if (OwO.Length == 4)
                        {
                            ListaDePostFija.Add(OwO);

                            OwO = "";
                        }
                    }
                    AuxP = "";
                    for (int i = 0; i < ListaDePostFija.Count; i++)
                    {
                        if (i == 0)
                        {
                            AuxP = ListaDePostFija[i];
                        }
                        else
                        {
                            AuxP = AuxP + " " + ListaDePostFija[i];
                        }
                    }
                    int T_T = 0;
                    auxCadenaTK[m].ContenidoDeLinea = AuxP;
                }
                if (auxCadenaTK[m].EsCiclo)
                {
                    string cadenainfija = auxCadenaTK[m].ContenidoDeLinea;

                    string[] CambioOPA = cadenainfija.Split(' ');
                    string aux = "";
                    for (int i = 0; i < CambioOPA.Length; i++)
                    {
                        CambioOPA[i] = CambioOPA[i].Trim();
                        aux = aux + CambioOPA[i];
                    }
                    string aux2 = OPA_TK(CambioOPA);
                    cadenainfija = cadenainfija.Replace(" ", "");
                    aux2 = aux2.Replace(" ", "");
                    string PostFijaFinal = InfixToPostfix(aux2);
                    List<string> ListaDePostFija = new List<string>();

                    string AuxP = PostFijaFinal;
                    AuxP = AuxP.Replace("+", "OA01");
                    AuxP = AuxP.Replace("-", "OA02");
                    AuxP = AuxP.Replace("/", "OA03");
                    AuxP = AuxP.Replace("*", "OA04");
                    AuxP = AuxP.Replace("=", "PR09");

                    AuxP = AuxP.Replace("<", "OR01");
                    AuxP = AuxP.Replace(">", "OR02");
                    AuxP = AuxP.Replace("<=", "OR03");
                    AuxP = AuxP.Replace(">=", "OR04");
                    AuxP = AuxP.Replace("==", "OR05");

                    AuxP = AuxP.Replace("&", "OL01");
                    AuxP = AuxP.Replace("||", "OL02");
                    AuxP = AuxP.Replace("!", "OL03");
                    //AuxP = AuxP.Replace("(", "PAR1");
                    //AuxP = AuxP.Replace(")", "PAR2");

                    char[] ArregloEpic = new char[AuxP.Length];
                    for (int pm = 0; pm < AuxP.Length; pm++)
                    {
                        ArregloEpic[pm] = AuxP[pm];
                    }
                    //for (int i = 0; i < ArregloEpic.Length; i += 4)
                    //{
                    //    if (i + 3 < ArregloEpic.Length)
                    //    {
                    //        ListaDePostFija.Add(new string(ArregloEpic, i, 4));
                    //    }
                    //    else
                    //    {
                    //        ListaDePostFija.Add(new string(ArregloEpic, i, ArregloEpic.Length - i));
                    //    }
                    //}
                    string OwO = "";
                    for (int i = 0; i < ArregloEpic.Length; i++)
                    {

                        OwO = OwO + ArregloEpic[i];
                        if (OwO.Length == 4)
                        {
                            ListaDePostFija.Add(OwO);

                            OwO = "";
                        }
                    }
                    AuxP = "";
                    for (int i = 0; i < ListaDePostFija.Count; i++)
                    {
                        if (i == 0)
                        {
                            AuxP = ListaDePostFija[i];
                        }
                        else
                        {
                            AuxP = AuxP + " " + ListaDePostFija[i];
                        }
                    }
                    int T_T = 0;
                    auxCadenaTK[m].ContenidoDeLinea = AuxP;
                }


                string UwU = "";

            }

            for (int q = 0; q < auxCadenaTK.Length; q++)
            {
                //MessageBox.Show("Token es: " + CadenaTK[q].ContenidoDeLinea);
                if (q == 0)
                {
                    txtCodigoIntermedio.Text = auxCadenaTK[q].ContenidoDeLinea;
                }
                else
                {

                    txtCodigoIntermedio.Text = txtCodigoIntermedio.Text + Environment.NewLine + auxCadenaTK[q].ContenidoDeLinea;
                }
            }

            //Ahora si viene lo chido los poderosismos "triplos".
            //Estos triplos son para asignacion y operaciones ARIMETICAS.
            int contadorTemps = 1;
            int idTriplos = 0;
            List<Triplo> TriplosEpicos = new List<Triplo>();
            for (int i = 0; i < auxCadenaTK.Length; i++)
            {
                if (auxCadenaTK[i].EsVariable)
                {
                    string cadenainfija = auxCadenaTK[i].ContenidoDeLinea;
                    string[] CambioOPA = cadenainfija.Split(' ');
                    CambioOPA = CambioOPA.Skip(1).ToArray();
                    List<string> ListaEpicaAux = new List<string>();
                    for (int p = 0; p < CambioOPA.Length; p++)
                    {
                        ListaEpicaAux.Add(CambioOPA[p]);
                    }
                    for (int p = 0; p < ListaEpicaAux.Count; p++)
                    {

                        if (EsOperadorTK(ListaEpicaAux[p]))
                        {
                            string temp = "TE0" + contadorTemps;
                            Stack<string> PilaEpica = new Stack<string>();
                            //Como chetto le digo los pasos en esta parte??
                            //la idea es agarrar los 2 numeros y el operador
                            //Aqui pondremos si la operacion no se hace con Temporales haga una nueva
                            //si no que continue:
                            if (ListaEpicaAux[p] == "PR09")
                            {
                                Triplo T = new Triplo(contadorTemps);
                                //
                                PilaEpica.Push(ListaEpicaAux[p]);
                                PilaEpica.Push(ListaEpicaAux[p - 1]);
                                PilaEpica.Push(ListaEpicaAux[p - 2]);

                                T.DatoObjeto = PilaEpica.Pop();
                                T.DatoFuente = PilaEpica.Pop();
                                T.Operador = PilaEpica.Pop();
                                //
                                ListaEpicaAux.RemoveAt(p);
                                ListaEpicaAux.RemoveAt(p - 1);
                                ListaEpicaAux[p - 2] = T.DatoObjeto;
                                idTriplos++;
                                T.id = idTriplos;
                                TriplosEpicos.Add(T);
                                //contadorTemps++;
                                p = 0;
                            }
                            else
                            {
                                if (temp == ListaEpicaAux[p - 2])
                                {
                                    if (ListaEpicaAux[p] == "PR09")
                                    {
                                        Triplo T = new Triplo(contadorTemps);
                                        //
                                        PilaEpica.Push(ListaEpicaAux[p]);
                                        PilaEpica.Push(ListaEpicaAux[p - 1]);
                                        PilaEpica.Push(ListaEpicaAux[p - 2]);

                                        T.DatoFuente = PilaEpica.Pop();
                                        T.DatoObjeto = PilaEpica.Pop();
                                        T.Operador = PilaEpica.Pop();
                                        //
                                        //ListaEpicaAux.RemoveAt(i);
                                        //ListaEpicaAux.RemoveAt(i - 1);
                                        //ListaEpicaAux[i - 2] = T.DatoObjeto;
                                        idTriplos++;
                                        T.id = idTriplos;
                                        TriplosEpicos.Add(T);
                                    }
                                    else
                                    {
                                        Triplo T = new Triplo(contadorTemps);
                                        Triplo T1 = new Triplo(contadorTemps);

                                        PilaEpica.Push(ListaEpicaAux[p]);
                                        PilaEpica.Push(ListaEpicaAux[p - 1]);
                                        PilaEpica.Push(ListaEpicaAux[p - 2]);
                                        T.Operador = "PR09";
                                        T.DatoObjeto = temp;
                                        T.DatoFuente = PilaEpica.Pop();

                                        T1.DatoObjeto = temp;
                                        T1.DatoFuente = PilaEpica.Pop();
                                        T1.Operador = PilaEpica.Pop();

                                        ListaEpicaAux.RemoveAt(p);
                                        ListaEpicaAux.RemoveAt(p - 1);
                                        ListaEpicaAux[p - 2] = T1.DatoObjeto;
                                        idTriplos++;
                                        T.id = idTriplos;
                                        TriplosEpicos.Add(T);
                                        idTriplos++;
                                        T1.id = idTriplos;
                                        TriplosEpicos.Add(T1);
                                        p = 0;
                                    }
                                }
                                else
                                {
                                    Triplo T = new Triplo(contadorTemps);
                                    Triplo T1 = new Triplo(contadorTemps);
                                    T.Operador = "PR09";
                                    T.DatoObjeto = temp;
                                    PilaEpica.Push(ListaEpicaAux[p]);
                                    PilaEpica.Push(ListaEpicaAux[p - 1]);
                                    PilaEpica.Push(ListaEpicaAux[p - 2]);

                                    T.DatoFuente = PilaEpica.Pop();

                                    T1.DatoObjeto = temp;
                                    T1.DatoFuente = PilaEpica.Pop();
                                    T1.Operador = PilaEpica.Pop();
                                    idTriplos++;
                                    T.id = idTriplos;
                                    
                                    ListaEpicaAux.RemoveAt(p);
                                    ListaEpicaAux.RemoveAt(p - 1);
                                    ListaEpicaAux[p - 2] = T1.DatoObjeto;
                                    TriplosEpicos.Add(T);
                                    idTriplos++;
                                    T1.id = idTriplos;
                                    TriplosEpicos.Add(T1);
                                    contadorTemps++;
                                    p = 0;
                                }
                            }
                        }
                    }
                }
            }


            //Triplos de decisión.
            //estas usando banderas en la linea, que tienes, problema que haras si dos banderas se prenden?! en caso lo que nunca pero nunca es que
            //se prendan lass banderas de ciclo y de desicion.
            List<Triplo> EstadoVerdadero = new List<Triplo>();
            List<Triplo> Estado_Falso = new List<Triplo>();
            int idVerdadero = 0;
            int idFalso = 0;
            for (int i = 0; i < auxCadenaTK.Length; i++)
            {
                if (auxCadenaTK[i].EsDesicion)
                {
                    if (!(auxCadenaTK[i].ContenidoDeLinea == "LL01"))
                    {
                        string cadenainfija = auxCadenaTK[i].ContenidoDeLinea;
                        string[] CambioOPA = cadenainfija.Split(' ');
                        //CambioOPA = CambioOPA.Skip(1).ToArray();
                        List<string> ListaEpicaAux = new List<string>();
                        for (int p = 0; p < CambioOPA.Length; p++)
                        {
                            ListaEpicaAux.Add(CambioOPA[p]);
                        }
                        for (int p = 0; p < ListaEpicaAux.Count; p++)
                        {
                            if (EsOperadorTK(ListaEpicaAux[p]))
                            {
                                Stack<string> PilaEpica = new Stack<string>();
                                if (EsOperadorTKL(ListaEpicaAux[p]))
                                {

                                    string temp = "TE0" + contadorTemps;
                                    Triplo T = new Triplo(contadorTemps);
                                    contadorTemps++;

                                    Triplo T1 = new Triplo(contadorTemps);
                                    string temp1 = "TE0" + contadorTemps;
                                   

                                    PilaEpica.Push(ListaEpicaAux[p]);
                                    PilaEpica.Push(ListaEpicaAux[p - 1]);
                                    PilaEpica.Push(ListaEpicaAux[p - 2]);

                                    T.Operador = "PR09";
                                    T.DatoObjeto = temp;
                                    T.DatoFuente = PilaEpica.Pop();
                                    idTriplos++;
                                    T.id = idTriplos;
                                    TriplosEpicos.Add(T);

                                    T1.Operador = "PR09";
                                    T1.DatoObjeto = temp1;
                                    T1.DatoFuente = PilaEpica.Pop();
                                    idTriplos++;
                                    T1.id = idTriplos;
                                    TriplosEpicos.Add(T1);

                                    Triplo Comparacion = new Triplo();
                                    Comparacion.Operador = PilaEpica.Pop();
                                    Comparacion.DatoFuente = T1.DatoObjeto;
                                    Comparacion.DatoObjeto = T.DatoObjeto;
                                    idTriplos++;
                                    Comparacion.id = idTriplos;
                                    TriplosEpicos.Add(Comparacion);

                                    Triplo EstadoTrue = new Triplo();
                                    idTriplos++;
                                    EstadoTrue.id = idTriplos;
                                    EstadoTrue.DatoObjeto = "TR1";
                                    EstadoTrue.DatoFuente = "TRUE";
                                    int positivo = idTriplos + 2;
                                    EstadoTrue.Operador = positivo.ToString();
                                    TriplosEpicos.Add(EstadoTrue);
                                    Triplo EstadoFalso = new Triplo();
                                    idTriplos++;
                                    EstadoFalso.id = idTriplos;
                                    EstadoFalso.DatoObjeto = "TR1";
                                    EstadoFalso.DatoFuente = "FALSE";
                                    int negativo = idTriplos + 2;
                                    EstadoFalso.Operador = negativo.ToString();
                                    TriplosEpicos.Add(EstadoFalso);

                                    Triplo ET = new Triplo();
                                    idTriplos++;
                                    ET.id = idTriplos;
                                    ET.DatoObjeto = "ET";
                                    ET.DatoFuente = "TRTRUE";
                                    ET.Operador = "FIN";
                                    //int True_= idTriplos + 2;
                                    //ET.Operador = True_.ToString();
                                    TriplosEpicos.Add(ET);
                                    Triplo EF = new Triplo();
                                    idTriplos++;
                                    EF.id = idTriplos;
                                    EF.DatoObjeto = "EF";
                                    EF.DatoFuente = "TRFALSE";
                                    EF.Operador = "FIN";
                                    //int False_ = idTriplos + 2;
                                    //EF.Operador = False_.ToString();
                                    TriplosEpicos.Add(EF);

                                    //Triplo FIN = new Triplo();
                                    //idTriplos++;
                                    //FIN.id = idTriplos;
                                    //FIN.DatoObjeto = "FIN";
                                    //TriplosEpicos.Add(FIN);

                                }
                                else
                                {

                                    if (auxCadenaTK[i].LadoDesicion == "Verdadero")
                                    {
                                        if (ListaEpicaAux[p] == "PR09")
                                        {

                                            Triplo T = new Triplo(contadorTemps);
                                            //
                                            PilaEpica.Push(ListaEpicaAux[p]);
                                            PilaEpica.Push(ListaEpicaAux[p - 1]);
                                            PilaEpica.Push(ListaEpicaAux[p - 2]);

                                            T.DatoObjeto = PilaEpica.Pop();
                                            T.DatoFuente = PilaEpica.Pop();
                                            T.Operador = PilaEpica.Pop();
                                            //
                                            //ListaEpicaAux.RemoveAt(i);
                                            //ListaEpicaAux.RemoveAt(i - 1);
                                            //ListaEpicaAux[i - 2] = T.DatoObjeto;
                                            idVerdadero++;
                                            T.id = idVerdadero;
                                            EstadoVerdadero.Add(T);
                                            //if (i == auxCadenaTK.Length) 
                                            //{
                                            //    Triplo FIN = new Triplo();
                                            //    idTriplos++;
                                            //    FIN.id = idTriplos;
                                            //    FIN.DatoObjeto = "FIN";
                                            //    TriplosEpicos.Add(FIN);
                                            //}
                                        }
                                        else
                                        {
                                            contadorTemps++;
                                            string temp = "TE0" + contadorTemps;

                                            Triplo T = new Triplo(contadorTemps);
                                            Triplo T1 = new Triplo(contadorTemps);

                                            PilaEpica.Push(ListaEpicaAux[p]);
                                            PilaEpica.Push(ListaEpicaAux[p - 1]);
                                            PilaEpica.Push(ListaEpicaAux[p - 2]);
                                            T.Operador = "PR09";
                                            T.DatoObjeto = temp;
                                            T.DatoFuente = PilaEpica.Pop();

                                            T1.DatoObjeto = temp;
                                            T1.DatoFuente = PilaEpica.Pop();
                                            T1.Operador = PilaEpica.Pop();

                                            ListaEpicaAux.RemoveAt(p);
                                            ListaEpicaAux.RemoveAt(p - 1);
                                            ListaEpicaAux[p - 2] = T1.DatoObjeto;
                                            idVerdadero++;
                                            T.id = idVerdadero;
                                            EstadoVerdadero.Add(T);
                                            idVerdadero++;
                                            T1.id = idVerdadero;
                                            EstadoVerdadero.Add(T1);
                                            p = 0;
                                            //if (i == auxCadenaTK.Length )
                                            //{
                                            //    Triplo FIN = new Triplo();
                                            //    idTriplos++;
                                            //    FIN.id = idTriplos;
                                            //    FIN.DatoObjeto = "FIN";
                                            //    TriplosEpicos.Add(FIN);
                                            //}

                                        }

                                    }
                                    else if (auxCadenaTK[i].LadoDesicion == "Falso")
                                    {
                                        if (ListaEpicaAux[p] == "PR09")
                                        {
                                            Triplo T = new Triplo(contadorTemps);
                                            //
                                            PilaEpica.Push(ListaEpicaAux[p]);
                                            PilaEpica.Push(ListaEpicaAux[p - 1]);
                                            PilaEpica.Push(ListaEpicaAux[p - 2]);

                                            T.DatoObjeto = PilaEpica.Pop();
                                            T.DatoFuente = PilaEpica.Pop();
                                            T.Operador = PilaEpica.Pop();
                                            //
                                            //ListaEpicaAux.RemoveAt(i);
                                            //ListaEpicaAux.RemoveAt(i - 1);
                                            //ListaEpicaAux[i - 2] = T.DatoObjeto;
                                            idFalso++;
                                            T.id = idFalso;
                                            Estado_Falso.Add(T);
                                            //if (i == auxCadenaTK.Length) 
                                            //{
                                            //    Triplo FIN = new Triplo();
                                            //    idTriplos++;
                                            //    FIN.id = idTriplos;
                                            //    FIN.DatoObjeto = "FIN";
                                            //    TriplosEpicos.Add(FIN);
                                            //}
                                        }
                                        else
                                        {
                                            contadorTemps++;
                                            string temp = "TE0" + contadorTemps;
                                            Triplo T = new Triplo(contadorTemps);
                                            Triplo T1 = new Triplo(contadorTemps);

                                            PilaEpica.Push(ListaEpicaAux[p]);
                                            PilaEpica.Push(ListaEpicaAux[p - 1]);
                                            PilaEpica.Push(ListaEpicaAux[p - 2]);
                                            T.Operador = "PR09";
                                            T.DatoObjeto = temp;
                                            T.DatoFuente = PilaEpica.Pop();

                                            T1.DatoObjeto = temp;
                                            T1.DatoFuente = PilaEpica.Pop();
                                            T1.Operador = PilaEpica.Pop();

                                            ListaEpicaAux.RemoveAt(p);
                                            ListaEpicaAux.RemoveAt(p - 1);
                                            ListaEpicaAux[p - 2] = T1.DatoObjeto;
                                            idFalso++;
                                            T.id = idFalso;
                                            Estado_Falso.Add(T);
                                            idFalso++;
                                            T1.id = idFalso;
                                            Estado_Falso.Add(T1);
                                            p = 0;
                                            //if (i == auxCadenaTK.Length )
                                            //{
                                            //    Triplo FIN = new Triplo();
                                            //    idTriplos++;
                                            //    FIN.id = idTriplos;
                                            //    FIN.DatoObjeto = "FIN";
                                            //    TriplosEpicos.Add(FIN);
                                            //}

                                        }
                                    }
                                    //if (ListaEpicaAux[p] == "PR09")
                                    //{
                                    //    Triplo T = new Triplo(contadorTemps);
                                    //    //
                                    //    PilaEpica.Push(ListaEpicaAux[p]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 1]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 2]);

                                    //    T.DatoFuente = PilaEpica.Pop();
                                    //    T.DatoObjeto = PilaEpica.Pop();
                                    //    T.Operador = PilaEpica.Pop();
                                    //    //
                                    //    //ListaEpicaAux.RemoveAt(i);
                                    //    //ListaEpicaAux.RemoveAt(i - 1);
                                    //    //ListaEpicaAux[i - 2] = T.DatoObjeto;
                                    //    idTriplos++;
                                    //    T.id = idTriplos;
                                    //    TriplosEpicos.Add(T);
                                    //    //if (i == auxCadenaTK.Length) 
                                    //    //{
                                    //    //    Triplo FIN = new Triplo();
                                    //    //    idTriplos++;
                                    //    //    FIN.id = idTriplos;
                                    //    //    FIN.DatoObjeto = "FIN";
                                    //    //    TriplosEpicos.Add(FIN);
                                    //    //}
                                    //}
                                    //else
                                    //{
                                    //    Triplo T = new Triplo(contadorTemps);
                                    //    Triplo T1 = new Triplo(contadorTemps);

                                    //    PilaEpica.Push(ListaEpicaAux[p]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 1]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 2]);
                                    //    T.Operador = "PR09";
                                    //    T.DatoObjeto = temp;
                                    //    T.DatoFuente = PilaEpica.Pop();

                                    //    T1.DatoObjeto = temp;
                                    //    T1.DatoFuente = PilaEpica.Pop();
                                    //    T1.Operador = PilaEpica.Pop();

                                    //    ListaEpicaAux.RemoveAt(p);
                                    //    ListaEpicaAux.RemoveAt(p - 1);
                                    //    ListaEpicaAux[p - 2] = T1.DatoObjeto;
                                    //    idTriplos++;
                                    //    T.id = idTriplos;
                                    //    TriplosEpicos.Add(T);
                                    //    idTriplos++;
                                    //    T1.id = idTriplos;
                                    //    TriplosEpicos.Add(T1);
                                    //    p = 0;
                                    //    //if (i == auxCadenaTK.Length )
                                    //    //{
                                    //    //    Triplo FIN = new Triplo();
                                    //    //    idTriplos++;
                                    //    //    FIN.id = idTriplos;
                                    //    //    FIN.DatoObjeto = "FIN";
                                    //    //    TriplosEpicos.Add(FIN);
                                    //    //}

                                    //}
                                }
                            }
                        }
                    }
                }
            }
            //Triplos de ciclo.
            List<Triplo> LoopEpico = new List<Triplo>();
            int idLoop = 0;
            for (int i = 0; i < auxCadenaTK.Length; i++)
            {
                if (auxCadenaTK[i].EsCiclo)
                {
                    if (!(auxCadenaTK[i].ContenidoDeLinea == "LL01"))
                    {
                        string cadenainfija = auxCadenaTK[i].ContenidoDeLinea;
                        string[] CambioOPA = cadenainfija.Split(' ');
                        //CambioOPA = CambioOPA.Skip(1).ToArray();
                        List<string> ListaEpicaAux = new List<string>();
                        for (int p = 0; p < CambioOPA.Length; p++)
                        {
                            ListaEpicaAux.Add(CambioOPA[p]);
                        }
                        for (int p = 0; p < ListaEpicaAux.Count; p++)
                        {
                            if (EsOperadorTK(ListaEpicaAux[p]))
                            {
                                Stack<string> PilaEpica = new Stack<string>();
                                if (EsOperadorTKL(ListaEpicaAux[p]))
                                {

                                    string temp = "TE0" + contadorTemps;
                                    Triplo T = new Triplo(contadorTemps);
                                    contadorTemps++;

                                    Triplo T1 = new Triplo(contadorTemps);
                                    string temp1 = "TE0" + contadorTemps;


                                    //
                                    PilaEpica.Push(ListaEpicaAux[p]);
                                    PilaEpica.Push(ListaEpicaAux[p - 1]);
                                    PilaEpica.Push(ListaEpicaAux[p - 2]);
                                    //
                                    T.Operador = "PR09";
                                    T.DatoObjeto = temp;

                                    T.DatoFuente = PilaEpica.Pop();
                                    idTriplos++;
                                    T.id = idTriplos;
                                    TriplosEpicos.Add(T);
                                    //
                                    T1.Operador = "PR09";
                                    T1.DatoObjeto = temp1;
                                    T1.DatoFuente = PilaEpica.Pop();
                                    idTriplos++;
                                    T1.id = idTriplos;
                                    TriplosEpicos.Add(T1);
                                    //
                                    Triplo Comparacion = new Triplo();
                                    Comparacion.Operador = PilaEpica.Pop();
                                    Comparacion.DatoFuente = T1.DatoObjeto;
                                    Comparacion.DatoObjeto = T.DatoObjeto;
                                    idTriplos++;
                                    Comparacion.id = idTriplos;
                                    TriplosEpicos.Add(Comparacion);
                                    //
                                    Triplo EstadoTrue = new Triplo();
                                    idTriplos++;
                                    EstadoTrue.id = idTriplos;
                                    EstadoTrue.DatoObjeto = "TR1";
                                    EstadoTrue.DatoFuente = "TRUE";
                                    int positivo = idTriplos + 2;
                                    EstadoTrue.Operador =positivo.ToString();
                                    TriplosEpicos.Add(EstadoTrue);
                                    //
                                    Triplo EstadoFalso = new Triplo();
                                    idTriplos++;
                                    EstadoFalso.id = idTriplos;
                                    EstadoFalso.DatoObjeto = "TR1";
                                    EstadoFalso.DatoFuente = "FALSE";
                                    int negativo = idTriplos + 1;
                                    EstadoFalso.Operador = "FIN";
                                    TriplosEpicos.Add(EstadoFalso);
                                    //
                                    Triplo Loop = new Triplo();
                                    idTriplos++;
                                    Loop.id = idTriplos;
                                    Loop.DatoObjeto = "LP";
                                    Loop.DatoFuente = "LOOP";
                                    TriplosEpicos.Add(Loop);
                                    //
                                    Triplo Salto = new Triplo();
                                    idTriplos++;
                                    Salto.id = idTriplos;
                                    Salto.DatoObjeto = "JMP";
                                    Salto.DatoFuente = "CMP";
                                    Salto.Operador = Comparacion.id.ToString();
                                    TriplosEpicos.Add(Salto);
                                    //
                                }
                                else
                                {
                                    if (auxCadenaTK[i].ParteDeCiclo)
                                    {
                                        if (ListaEpicaAux[p] == "PR09")
                                        {
                                            //contadorTemps++;
                                            string temp = "TE0" + contadorTemps;

                                            Triplo T = new Triplo(contadorTemps);
                                            //
                                            PilaEpica.Push(ListaEpicaAux[p]);
                                            PilaEpica.Push(ListaEpicaAux[p - 1]);
                                            PilaEpica.Push(ListaEpicaAux[p - 2]);

                                            T.DatoObjeto = PilaEpica.Pop();
                                            T.DatoFuente = PilaEpica.Pop();
                                            T.Operador = PilaEpica.Pop();
                                            //
                                            //ListaEpicaAux.RemoveAt(i);
                                            //ListaEpicaAux.RemoveAt(i - 1);
                                            //ListaEpicaAux[i - 2] = T.DatoObjeto;
                                            idLoop++;
                                            T.id = idLoop;
                                            LoopEpico.Add(T);
                                            //contadorTemps++;
                                            //if (i == auxCadenaTK.Length) 
                                            //{
                                            //    Triplo FIN = new Triplo();
                                            //    idTriplos++;
                                            //    FIN.id = idTriplos;
                                            //    FIN.DatoObjeto = "FIN";
                                            //    TriplosEpicos.Add(FIN);
                                            //}
                                        }
                                        else
                                        {
                                            contadorTemps++;
                                            string temp = "TE0" + contadorTemps;

                                            // contadorTemps++;
                                            Triplo T = new Triplo(contadorTemps);

                                            // contadorTemps++;
                                            Triplo T1 = new Triplo(contadorTemps);

                                            PilaEpica.Push(ListaEpicaAux[p]);
                                            PilaEpica.Push(ListaEpicaAux[p - 1]);
                                            PilaEpica.Push(ListaEpicaAux[p - 2]);
                                            T.Operador = "PR09";
                                            T.DatoObjeto = temp;
                                            T.DatoFuente = PilaEpica.Pop();

                                            T1.DatoObjeto = temp;
                                            T1.DatoFuente = PilaEpica.Pop();
                                            T1.Operador = PilaEpica.Pop();
                                            ListaEpicaAux.RemoveAt(p);
                                            ListaEpicaAux.RemoveAt(p - 1);
                                            ListaEpicaAux[p - 2] = T1.DatoObjeto;
                                            idLoop++;
                                            T.id = idLoop;
                                            LoopEpico.Add(T);

                                            idLoop++;
                                            T1.id = idLoop;
                                            LoopEpico.Add(T1);
                                            p = 0;
                                            //if (i == auxCadenaTK.Length )
                                            //{
                                            //    Triplo FIN = new Triplo();
                                            //    idTriplos++;
                                            //    FIN.id = idTriplos;
                                            //    FIN.DatoObjeto = "FIN";
                                            //    TriplosEpicos.Add(FIN);
                                            //}

                                        }

                                    }
                                    //if (ListaEpicaAux[p] == "PR09")
                                    //{
                                    //    Triplo T = new Triplo(contadorTemps);
                                    //    //
                                    //    PilaEpica.Push(ListaEpicaAux[p]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 1]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 2]);

                                    //    T.DatoFuente = PilaEpica.Pop();
                                    //    T.DatoObjeto = PilaEpica.Pop();
                                    //    T.Operador = PilaEpica.Pop();
                                    //    //
                                    //    //ListaEpicaAux.RemoveAt(i);
                                    //    //ListaEpicaAux.RemoveAt(i - 1);
                                    //    //ListaEpicaAux[i - 2] = T.DatoObjeto;
                                    //    idTriplos++;
                                    //    T.id = idTriplos;
                                    //    TriplosEpicos.Add(T);
                                    //    //if (i == auxCadenaTK.Length) 
                                    //    //{
                                    //    //    Triplo FIN = new Triplo();
                                    //    //    idTriplos++;
                                    //    //    FIN.id = idTriplos;
                                    //    //    FIN.DatoObjeto = "FIN";
                                    //    //    TriplosEpicos.Add(FIN);
                                    //    //}
                                    //}
                                    //else
                                    //{
                                    //    Triplo T = new Triplo(contadorTemps);
                                    //    Triplo T1 = new Triplo(contadorTemps);

                                    //    PilaEpica.Push(ListaEpicaAux[p]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 1]);
                                    //    PilaEpica.Push(ListaEpicaAux[p - 2]);
                                    //    T.Operador = "PR09";
                                    //    T.DatoObjeto = temp;
                                    //    T.DatoFuente = PilaEpica.Pop();

                                    //    T1.DatoObjeto = temp;
                                    //    T1.DatoFuente = PilaEpica.Pop();
                                    //    T1.Operador = PilaEpica.Pop();

                                    //    ListaEpicaAux.RemoveAt(p);
                                    //    ListaEpicaAux.RemoveAt(p - 1);
                                    //    ListaEpicaAux[p - 2] = T1.DatoObjeto;
                                    //    idTriplos++;
                                    //    T.id = idTriplos;
                                    //    TriplosEpicos.Add(T);
                                    //    idTriplos++;
                                    //    T1.id = idTriplos;
                                    //    TriplosEpicos.Add(T1);
                                    //    p = 0;
                                    //    //if (i == auxCadenaTK.Length )
                                    //    //{
                                    //    //    Triplo FIN = new Triplo();
                                    //    //    idTriplos++;
                                    //    //    FIN.id = idTriplos;
                                    //    //    FIN.DatoObjeto = "FIN";
                                    //    //    TriplosEpicos.Add(FIN);
                                    //    //}

                                    //}
                                }
                            }
                        }

                    }
                }
            }



            Triplo FIN = new Triplo();
            idTriplos++;
            FIN.id = idTriplos;
            FIN.DatoObjeto = "FIN";
            TriplosEpicos.Add(FIN);

            ListaTriploMain = TriplosEpicos;
            ListaTriploTrue = EstadoVerdadero;
            ListaTriploFalse = Estado_Falso;
            ListaTriploLoop = LoopEpico;

            dtgTriplos.Rows.Clear();
            foreach (Triplo triplo in TriplosEpicos)
            {
                dtgTriplos.Rows.Add(triplo.id, triplo.DatoObjeto, triplo.DatoFuente, triplo.Operador);
            }
            dtgLadoVerdadero.Rows.Clear();
            foreach (Triplo triplo in EstadoVerdadero)
            {
                dtgLadoVerdadero.Rows.Add(triplo.id, triplo.DatoObjeto, triplo.DatoFuente, triplo.Operador);
            }
            dtgLadoFalso.Rows.Clear();
            foreach (Triplo triplo in Estado_Falso)
            {
                dtgLadoFalso.Rows.Add(triplo.id, triplo.DatoObjeto, triplo.DatoFuente, triplo.Operador);
            }
            dtgLoop.Rows.Clear();
            foreach (Triplo triplo in LoopEpico)
            {
                dtgLoop.Rows.Add(triplo.id, triplo.DatoObjeto, triplo.DatoFuente, triplo.Operador);
            }
        }
        public static string InfixToPostfix(string infix)
        {
            // Eliminar espacios en blanco de la cadena de entrada
            infix = infix.Replace(" ", "");

            // Definir la precedencia de los operadores
            Dictionary<char, int> precedence = new Dictionary<char, int>()
            {
                 {'+', 1},
                 {'-', 1},
                 {'*', 2},
                 {'/', 2},
                 {'^', 3},
                 {'>', 4},
                 {'<', 4},
                 {'!', 4},
                 {'&', 5},
                 {'|', 6}
            };

            // Crear una pila para los operadores
            Stack<char> operators = new Stack<char>();

            // Crear una cadena para la salida postfija
            StringBuilder postfix = new StringBuilder();

            // Iterar por cada carácter de la cadena de entrada
            for (int i = 0; i < infix.Length; i++)
            {
                char c = infix[i];

                // Si el carácter es un operando, agregarlo a la salida
                if (Char.IsLetterOrDigit(c))
                {
                    postfix.Append(c);
                }
                // Si el carácter es un paréntesis izquierdo, agregarlo a la pila de operadores
                else if (c == '(')
                {
                    operators.Push(c);
                }
                // Si el carácter es un paréntesis derecho, desapilar los operadores hasta encontrar el paréntesis izquierdo correspondiente
                else if (c == ')')
                {
                    while (operators.Peek() != '(')
                    {
                        postfix.Append(operators.Pop());
                    }
                    operators.Pop(); // desapilar el paréntesis izquierdo
                }
                // Si el carácter es un operador, desapilar los operadores con mayor o igual precedencia y agregarlos a la salida
                // Luego agregar el operador a la pila de operadores
                else if (precedence.ContainsKey(c))
                {
                    while (operators.Count > 0 && operators.Peek() != '(' && precedence[operators.Peek()] >= precedence[c])
                    {
                        postfix.Append(operators.Pop());
                    }
                    operators.Push(c);
                }
            }

            // Desapilar todos los operadores restantes y agregarlos a la salida
            while (operators.Count > 0)
            {
                postfix.Append(operators.Pop());
            }
            //Agregar el igual al final del string
            if (infix.Contains("="))
            {
                postfix.Append("=");
            }
            //Retornar la cadena de salida postfija
            return postfix.ToString();
        }
        private string OPA_TK(string[] OPA)
        {
            string resultado = "";
            for (int i = 0; i < OPA.Length; i++)
            {
                if (OPA[i] == "OA01")
                {
                    OPA[i] = "+";
                }
                else if (OPA[i] == "OA02")
                {
                    OPA[i] = "-";
                }
                else if (OPA[i] == "OA03")
                {
                    OPA[i] = "/";
                }
                else if (OPA[i] == "OA04")
                {
                    OPA[i] = "*";
                }
                else if (OPA[i] == "PR09")
                {
                    OPA[i] = "=";
                }
                else if (OPA[i] == "PAR1")
                {
                    OPA[i] = "(";
                }
                else if (OPA[i] == "PAR2")
                {
                    OPA[i] = ")";
                }
                else if (OPA[i]== "OL01")
                {
                    OPA[i] = "&";
                }
                else if (OPA[i] == "OL02")
                {
                    OPA[i] = "|";
                }
                else if (OPA[i] == "OL03")
                {
                    OPA[i] = "!";
                }
                else if (OPA[i] == "OR01")
                {
                    OPA[i] = "<";
                }
                else if (OPA[i] == "OR02")
                {
                    OPA[i] = ">";
                }
                else if (OPA[i] == "OR06")
                {
                    OPA[i] = "!";
                }
                resultado = resultado + OPA[i];
            }
            return resultado;
        }
        private bool EsOperadorTK(string Operador)
        {
            if (Operador == "OA01" || Operador == "OA02" || Operador == "OA03" || Operador == "OA04" || Operador == "PR09"||Operador=="OL01"|| 
                Operador == "OL02"|| Operador == "OL03" || Operador == "OR01"|| Operador == "OR02"|| Operador == "OR06")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool EsOperadorTKL(string Operador)
        {
            if ( Operador == "OL01" || Operador == "OL02" || Operador == "OL03" || Operador == "OR01" || Operador == "OR02" || Operador == "OR06")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnEnsamblador_Click(object sender, EventArgs e)
        {
            //al chile no se como chettos empezar ayuda D:
            //Estos foreach son para cambiar el valor de token al original
            //estoy pensado en cambiar el valor AX;
            if (ListaTriploMain.Count > 0)
            {
                foreach (Triplo t in ListaTriploMain)
                {
                    foreach (Variable v in ListaDeVariables)
                    {
                       
                        if (t.DatoFuente == v.Token)
                        {
                            if (v.Identidicador[0] == '«')
                            {
                                string aux = v.Identidicador;
                                aux = aux.Replace('«', ' ');
                                aux = aux.Replace('»', ' ');
                                t.DatoFuente = aux;
                            }
                            else
                            {
                                t.DatoFuente = v.Identidicador;
                            }
                        }
                        if (t.DatoObjeto == v.Token)
                        {
                            t.DatoObjeto = v.Identidicador;
                        }
                        if (t.DatoFuente == "TE01")
                        {
                            t.DatoFuente = "AX";
                        }
                        if (t.DatoFuente == "TE02")
                        {
                            t.DatoFuente = "BX";
                        }
                        if (t.DatoFuente == "TE03")
                        {
                            t.DatoFuente = "CX";
                        }
                        if (t.DatoFuente == "TE04")
                        {
                            t.DatoFuente = "DX";
                        }
                        if (t.DatoObjeto == "TE01")
                        {
                            t.DatoObjeto = "AX";
                        }
                        if (t.DatoObjeto == "TE02")
                        {
                            t.DatoObjeto = "BX";
                        }
                        if (t.DatoObjeto == "TE03")
                        {
                            t.DatoObjeto = "CX";
                        }
                        if (t.DatoObjeto == "TE04")
                        {
                            t.DatoObjeto = "DX";
                        }
                    }
                }
            }
            if (ListaTriploTrue.Count > 0)
            {
                foreach (Triplo t in ListaTriploTrue)
                {
                    foreach (Variable v in ListaDeVariables)
                    {
                        if (t.DatoFuente == v.Token)
                        {
                            t.DatoFuente = v.Identidicador;
                        }
                        if (t.DatoObjeto == v.Token)
                        {
                            t.DatoObjeto = v.Identidicador;
                        }
                        if (t.DatoFuente == "TE01")
                        {
                            t.DatoFuente = "AX";
                        }
                        if (t.DatoFuente == "TE02")
                        {
                            t.DatoFuente = "BX";
                        }
                        if (t.DatoFuente == "TE03")
                        {
                            t.DatoFuente = "CX";
                        }
                        if (t.DatoFuente == "TE04")
                        {
                            t.DatoFuente = "DX";
                        }
                        if (t.DatoObjeto == "TE01")
                        {
                            t.DatoObjeto = "AX";
                        }
                        if (t.DatoObjeto == "TE02")
                        {
                            t.DatoObjeto = "BX";
                        }
                        if (t.DatoObjeto == "TE03")
                        {
                            t.DatoObjeto = "CX";
                        }
                        if (t.DatoObjeto == "TE04")
                        {
                            t.DatoObjeto = "DX";
                        }
                    }
                }
            }
            if (ListaTriploFalse.Count > 0)
            {
                foreach (Triplo t in ListaTriploFalse)
                {
                    foreach (Variable v in ListaDeVariables)
                    {
                        if (t.DatoFuente == v.Token)
                        {
                            t.DatoFuente = v.Identidicador;
                        }
                        if (t.DatoObjeto == v.Token)
                        {
                            t.DatoObjeto = v.Identidicador;
                        }
                        if (t.DatoFuente == "TE01")
                        {
                            t.DatoFuente = "AX";
                        }
                        if (t.DatoFuente == "TE02")
                        {
                            t.DatoFuente = "BX";
                        }
                        if (t.DatoFuente == "TE03")
                        {
                            t.DatoFuente = "CX";
                        }
                        if (t.DatoFuente == "TE04")
                        {
                            t.DatoFuente = "DX";
                        }
                        if (t.DatoObjeto == "TE01")
                        {
                            t.DatoObjeto = "AX";
                        }
                        if (t.DatoObjeto == "TE02")
                        {
                            t.DatoObjeto = "BX";
                        }
                        if (t.DatoObjeto == "TE03")
                        {
                            t.DatoObjeto = "CX";
                        }
                        if (t.DatoObjeto == "TE04")
                        {
                            t.DatoObjeto = "DX";
                        }
                    }
                }
            }
            if (ListaTriploLoop.Count > 0)
            {
                foreach (Triplo t in ListaTriploLoop)
                {
                    foreach (Variable v in ListaDeVariables)
                    {
                        if (t.DatoFuente == v.Token)
                        {
                            t.DatoFuente = v.Identidicador;
                        }
                        if (t.DatoObjeto == v.Token)
                        {
                            t.DatoObjeto = v.Identidicador;
                        }
                        if (t.DatoFuente == "TE01")
                        {
                            t.DatoFuente = "AX";
                        }
                        if (t.DatoFuente == "TE02")
                        {
                            t.DatoFuente = "BX";
                        }
                        if (t.DatoFuente == "TE03")
                        {
                            t.DatoFuente = "CX";
                        }
                        if (t.DatoFuente == "TE04")
                        {
                            t.DatoFuente = "DX";
                        }
                        if (t.DatoObjeto == "TE01")
                        {
                            t.DatoObjeto = "AX";
                        }
                        if (t.DatoObjeto == "TE02")
                        {
                            t.DatoObjeto = "BX";
                        }
                        if (t.DatoObjeto == "TE03")
                        {
                            t.DatoObjeto = "CX";
                        }
                        if (t.DatoObjeto == "TE04")
                        {
                            t.DatoObjeto = "DX";
                        }
                    }
                }
            }
            string OwO = "";


            string ensamblador = ConvertidorEnsamblador(OwO);
            MessageBox.Show(ensamblador);
            int UWU = 0;
            MessageBox.Show("GUARDAR PROGRAMA OBJETO");
            SaveFileDialog salvar = new SaveFileDialog();
            salvar.Filter = "Archivo txt | *.txt";
            if (salvar.ShowDialog() == DialogResult.OK)
            {
                StreamWriter escribir = new StreamWriter(salvar.FileName);
                escribir.WriteLine(ensamblador);
                escribir.Close();

            }
        }
        private string ConvertidorEnsamblador(string OwO)
        {
            int contadorloop = 0;
            if (ListaTriploMain.Count > 0)
            {
                OwO = OwO + "code segment \r\n    assume cs:code, ds:code, ss:code \r\norg 100h";


                //Para asignacion de variables
                foreach (Triplo T in ListaTriploMain)
                {
                    if (!(T.DatoObjeto == "FIN"))
                    {
                        if (T.DatoFuente.Length > 4 && T.DatoObjeto != "ET" && T.DatoObjeto != "EF" && T.DatoObjeto != "TR1")
                        {
                            OwO = OwO + "\n " + T.DatoObjeto + " db 0dh,0ah, '" + T.DatoFuente + "$' ";
                        }
                        else if ((T.DatoObjeto != "AX") && (T.DatoFuente == "AX" || T.DatoFuente == "BX"))
                        {
                            OwO = OwO + "\n " + T.DatoObjeto + " dw ?" + " ";

                        }
                        else if ((T.DatoObjeto != "AX" && T.DatoObjeto != "BX" && T.DatoObjeto != "TR1" 
                            && T.DatoObjeto !="LP" && T.DatoObjeto !="LOP" && T.DatoObjeto != "JMP" && T.DatoObjeto != "ET" && T.DatoObjeto != "EF")
                            &&
                            T.DatoFuente != "TE01")
                        {
                            OwO = OwO + "\n " + T.DatoObjeto + " dw " + T.DatoFuente + " ";
                        }
                    }
                }
                OwO = OwO + "\r\nmain proc \r\n    mov ax,cs\r\n    mov ds,ax";
                //Para operaciones aritmeticas
                //    foreach (Triplo T in ListaTriploMain)
                //    {
                //        if (!(T.DatoObjeto == "FIN"))
                //        {
                //            if (T.DatoFuente.Length > 4 && T.DatoObjeto != "ET" && T.DatoObjeto != "EF" && T.DatoObjeto != "TR1")
                //            {

                //            }
                //            else if (T.Operador == "PR09")
                //            {
                //                OwO = OwO + "\n MOV " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //            }
                //            else if (T.Operador == "OA01")
                //            {
                //                OwO = OwO + "\n ADD " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //            }
                //            else if (T.Operador == "OA02")
                //            {
                //                OwO = OwO + "\n SUB " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //            }
                //            else if (T.Operador == "OA04")
                //            {
                //                OwO = OwO + "\n MUL " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //            }
                //            //para el loop while
                //            if (ListaTriploLoop.Count > 0)
                //            {
                //                //contadorloop++;
                //                //if (contadorloop == 1)
                //                //{

                //                //}
                //                if (T.Operador == "OR01")
                //                {
                //                    OwO = OwO + "\n LOP: ";

                //                    OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //                    OwO = OwO + "\n JG " + "FIN ";
                //                }
                //                else if (T.Operador == "OR02")
                //                {
                //                    OwO = OwO + "\n LOP: ";
                //                    OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //                    OwO = OwO + "\n JL " + "FIN ";
                //                }
                //            }
                //            //comparacion 
                //            if (ListaTriploFalse.Count > 0)
                //            {
                //                if (T.Operador == "OR01")
                //                {
                //                    OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //                    OwO = OwO + "\n JG " + "TRFALSE ";
                //                }
                //                else if (T.Operador == "OR02")
                //                {
                //                    OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                //                    OwO = OwO + "\n JL " + "TRFALSE ";
                //                }
                //            }

                //        }

                //    }
                //    if (ListaTriploFalse.Count > 0)
                //    {

                //    }
                //    else if(ListaTriploLoop.Count > 0)
                //    {

                //    }
                //    else
                //    {
                //        OwO = OwO + "\n MOV AH, 4CH ";
                //        OwO = OwO + "\n INT 21H ";
                //        OwO = OwO + "\r\nmain endp\r\ncode ends \r\nend main ";
                //    }
                //}
               
                for(int i = 0; i < ListaTriploMain.Count; i++)
                {
                    Triplo T = ListaTriploMain[i];
                   
                    if (!(T.DatoObjeto == "FIN"))
                    {
                        if (ListaTriploMain.Count > i + 2)
                        {
                            Triplo T2 = ListaTriploMain[i + 2];

                            if (T2.Operador == "OR01" || T2.Operador == "OR02")
                            {
                                OwO = OwO + "\n LOP: ";
                            }
                        }
                        if (T.DatoFuente.Length > 4 && T.DatoObjeto != "ET" && T.DatoObjeto != "EF" && T.DatoObjeto != "TR1")
                        {

                        }
                        else if (T.Operador == "PR09")
                        {
                            OwO = OwO + "\n MOV " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA01")
                        {
                            OwO = OwO + "\n ADD " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA02")
                        {
                            OwO = OwO + "\n SUB " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA04")
                        {
                            OwO = OwO + "\n MUL " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        //para el loop while
                        if (ListaTriploLoop.Count > 0)
                        {
                            //contadorloop++;
                            //if (contadorloop == 1)
                            //{

                            //}
                            if (T.Operador == "OR01")
                            {
                                //

                                OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                                OwO = OwO + "\n JG " + "FIN ";
                            }
                            else if (T.Operador == "OR02")
                            {
                                //OwO = OwO + "\n LOP: ";
                                OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                                OwO = OwO + "\n JL " + "FIN ";
                            }
                        }
                        //comparacion 
                        if (ListaTriploFalse.Count > 0)
                        {
                            if (T.Operador == "OR01")
                            {
                                OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                                OwO = OwO + "\n JG " + "TRFALSE ";
                            }
                            else if (T.Operador == "OR02")
                            {
                                OwO = OwO + "\n CMP " + T.DatoObjeto + "," + T.DatoFuente + " ";
                                OwO = OwO + "\n JL " + "TRFALSE ";
                            }
                        }

                    }

                }
                if (ListaTriploFalse.Count > 0)
                {

                }
                else if (ListaTriploLoop.Count > 0)
                {

                }
                else
                {
                    OwO = OwO + "\n MOV AH, 4CH ";
                    OwO = OwO + "\n INT 21H ";
                    OwO = OwO + "\r\nmain endp\r\ncode ends \r\nend main ";
                }
            }

            if (ListaTriploTrue.Count > 0)
            {
                foreach (Triplo T in ListaTriploTrue)
                {
                    if (!(T.DatoObjeto == "FIN"))
                    {
                        if (T.DatoObjeto == "PR02")
                        {
                            OwO = OwO + "\nlea DX, " + T.DatoFuente;
                            OwO = OwO + "\nmov ah,09\r\n int 21h";
                        }
                        else if (T.Operador == "PR09")
                        {
                            OwO = OwO + "\n MOV " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA01")
                        {
                            OwO = OwO + "\n ADD " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA02")
                        {
                            OwO = OwO + "\n SUB " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA04")
                        {
                            OwO = OwO + "\n MUL " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        
                    }
                }
                OwO = OwO + "\n MOV AH, 4CH ";
                OwO = OwO + "\n INT 21H ";

            }

            if (ListaTriploFalse.Count > 0)
            {
                OwO = OwO + "\n TRFALSE: ";

                foreach (Triplo T in ListaTriploFalse)
                {
                    if (!(T.DatoObjeto == "FIN"))
                    {
                        if (T.DatoObjeto == "PR02")
                        {
                            OwO = OwO + "\nlea DX, " + T.DatoFuente;
                            OwO = OwO + "\nmov ah,09\r\n int 21h";
                        }
                        else if (T.Operador == "PR09")
                        {
                            OwO = OwO + "\n MOV " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA01")
                        {
                            OwO = OwO + "\n ADD " + T.DatoObjeto + "," + T.DatoFuente+" ";
                        }
                        else if (T.Operador == "OA02")
                        {
                            OwO = OwO + "\n SUB " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA04")
                        {
                            OwO = OwO + "\n MUL " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (true)
                        {

                        }
                    }
                }
                OwO = OwO + "\n MOV AH, 4CH ";
                OwO = OwO + "\n INT 21H ";
                OwO = OwO + "\r\nmain endp\r\ncode ends \r\nend main ";

            }

            if (ListaTriploLoop.Count > 0)
            {
                foreach (Triplo T in ListaTriploLoop)
                {
                    if (!(T.DatoObjeto == "FIN"))
                    {
                        if (T.DatoObjeto == "PR02")
                        {
                            OwO = OwO + "\nlea DX, " + T.DatoFuente;
                            OwO = OwO + "\nmov ah,09\r\n int 21h";
                        }
                        else if (T.Operador == "PR09")
                        {
                            OwO = OwO + "\n MOV " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA01")
                        {
                            OwO = OwO + "\n ADD " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA02")
                        {
                            OwO = OwO + "\n SUB " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                        else if (T.Operador == "OA04")
                        {
                            OwO = OwO + "\n MUL " + T.DatoObjeto + "," + T.DatoFuente + " ";
                        }
                    }
                }
                OwO = OwO + "\n JMP LOP ";

                OwO = OwO + "\n FIN: ";
                OwO = OwO + "\n MOV AH, 4CH ";
                OwO = OwO + "\n INT 21H ";
                OwO = OwO + "\r\nmain endp\r\ncode ends \r\nend main ";


            }

            return OwO;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}