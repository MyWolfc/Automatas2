using System;
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


namespace PruebaDeArreglosEnUnaLinea
{
    public partial class Form1 : Form
    {
        string[,] Cadena;
        Linea[] CadenaTK;
        Linea[] auxCadenaTK;
        Linea[] CodigoLimpio;
        string RutaEpic = "Server=localhost;Database=pruebalex;Uid=root;Pwd=Juan@20";
        int Estado = 0;
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

        }

        private void btnPasarALexico_Click(object sender, EventArgs e)
        {

            string CODIGO = txtCodigo.Text;
            CODIGO = CODIGO.ToLower();
            //Aqui contamos los saltos de linea
            string[] SaltosLinea = CODIGO.Split('\n');

            int PalabrasReservadasXlinea = 0;
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
            //CodigoLimpio = new Linea[SaltosLinea.Length];
            for (int i = 0; i < SaltosLinea.Length; i++)
            {
                //string[] CONTADORPRXLINEA = SaltosLinea[i].Split(' ');
                //Creamos un objeto temporal para llenar el arreglo el obejto
                Linea MiLinea = new Linea();
                MiLinea.NumeroDeLinea = i;
                MiLinea.ContenidoDeLinea = SaltosLinea[i];
                CadenaTK[i] = MiLinea;
                //CodigoLimpio[i] = MiLinea;
            }
            auxCadenaTK = CadenaTK;
            
            string Comando = txtCodigo.Text;
            Comando = Comando.ToLower();
            string[] Token;
            string[] Palabras = Comando.Split(' ');
            string LP = "";
            string PA = "";
            int ContadorPalabras = 0;

           

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
                    cmd.CommandText = "Select * From pruebam";
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
                                            if (k==0)
                                            {
                                                AUXTOK = row["TOKEN"].ToString();
                                            }
                                            else
                                            {
                                                AUXTOK =  AUXTOK + " " + row["TOKEN"].ToString();
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

                    for (int q = 0; q < CadenaTK.Length; q++)
                    {

                        //MessageBox.Show("Token es: " + CadenaTK[q].ContenidoDeLinea);
                        if (q == 0)
                        {
                            txtLexico.Text = CadenaTK[q].ContenidoDeLinea;
                        }
                        else
                        {
                            txtLexico.Text = txtLexico.Text + Environment.NewLine + CadenaTK[q].ContenidoDeLinea;
                        }
                    }
                    // GlobalArregllo = Token;
                    MessageBox.Show("Final de lexico");

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
                    //Verificar si la conexionEPICA Esta abierta de ser asi se cierra
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

                for (int j = 1; j < auxiliarVar.Length; j++)
                {
                    if (j == 1)
                    {
                        if (auxiliarVar[j] == "TD01" || auxiliarVar[j] == "TD02" || auxiliarVar[j] == "TD03" || auxiliarVar[j] == "TD04")
                        {
                            auxCadenaTK[i].EsVariable = true;
                            break;
                        }
                    }
                }

            }

        }

        private void btnSintactico_Click(object sender, EventArgs e)
        {
            string PruebaEpica = txtLexico.Text;
            bool BANDERAEPICA = true;
            string Error = "Error en";
            string Try = " ";
            string TokensYes = "";
            string[] TokensValidos = new string[37];
            //MessageBox.Show("XD: " + PruebaEpica);
            /*for (int i = 1; i < GlobalArregllo.Length; i++)
            {
                Try = Try + GlobalArregllo[i] + " ";
            }*/
            //MessageBox.Show("Test de arreglo:\r" + Try );
            MySqlConnection Conexion = new MySqlConnection(RutaEpic);

            MySqlCommand cmd;

            Conexion.Open();
            try
            {
                cmd = Conexion.CreateCommand();
                cmd.CommandText = "select TOKEN from pruebam WHERE TOKEN != ''";
                MySqlDataAdapter miAdaptador = new MySqlDataAdapter(cmd);
                DataSet misDatos = new DataSet();
                miAdaptador.Fill(misDatos);



                foreach (DataRow row in misDatos.Tables[0].Rows)
                {
                    //Console.WriteLine(row["EmpID"] + ", " + row["EmpName"] + ", " + row["EmpMobile"]);
                    //MessageBox.Show("Token" + row["TOKEN"]);
                    TokensYes = TokensYes + row["TOKEN"].ToString() + " ";
                }
                TokensValidos = TokensYes.Split(' ');
                MessageBox.Show("token num36: " + TokensValidos[35].ToString() + "\n 36: " + TokensValidos[36].ToString());

                for (int i = 0; i < TokensValidos.Length; i++)
                {
                    // MessageBox.Show("Tokens desde la base de datos: " + TokensValidos[i]);
                }
                //Esta bandera nos ayuda a cachar las excepcioness del if en caso que se use un else if o un else y no exista un IF antes de estos
                bool banderitaINT = false;
                bool banderitaFLT = false;
                bool BanderaIF = false;
                bool banderaWHILE = false;
                bool banderallaveopen = false;
                bool banderallaveclose = false;
                string TraeTODO = "";
                string[] GlobalArregllo;
                //GlobalArregllo = PruebaEpica.Split(' ','\n');
                for (int i = 0; i < CadenaTK.Length; i++)
                {
                    
                        TraeTODO =  TraeTODO+ "" + CadenaTK[i].ContenidoDeLinea ;
                    
                    
                }
                char[] Delimitadores = { ' ',';', '\n','\r' };
                GlobalArregllo = TraeTODO.Split(Delimitadores);
                for (
                    int i = 1; i < GlobalArregllo.Length; i++)
                {
                    if (TokensValidos[8] == GlobalArregllo[i])
                    {
                        banderaWHILE = true;
                        break;
                    }
                }
                for (int i = 1; i < GlobalArregllo.Length; i++)
                {
                    banderitaINT = false;
                    banderitaFLT = false;
                    //Validacion de la palabra inc = COM1 que el programa comience con inc
                    if (i == 1)
                    {
                        //Validacion de la palabra inc = COM1 que el programa comience con inc
                        if (TokensValidos[0] == GlobalArregllo[i])
                        {
                            //MessageBox.Show("Com1 es valido");
                            BANDERAEPICA = true;

                        }
                        else
                        {
                            BANDERAEPICA = false;
                            Error = Error + "El programa tiene que empezar con inc";
                            break;

                        }
                    }
                    //Validacion de la palabra int = ENT2 LISTO FUNCIONAL
                    else if (TokensValidos[1] == GlobalArregllo[i])
                    {
                        //aqui comprobamos que despues de un INT venga ID
                        if (GlobalArregllo[i + 1] == TokensValidos[33])
                        {

                            if (GlobalArregllo[i + 2] != TokensValidos[30])
                            {
                                //TODO bien aqui saltamos del ciclo las posiciones comparadas de los tokens
                                i = i + 2;
                            }
                            else if (GlobalArregllo[i + 2] == TokensValidos[30])
                            {
                                if ((GlobalArregllo[i + 3] == TokensValidos[33]) || (GlobalArregllo[i + 3] == TokensValidos[16]))
                                {
                                    //aqui saltamos del ciclo las posiciones comparadas de los tokens

                                    //Todo chill porque se asigno un varible con un ID o con constante numerica entera

                                    //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
                                    if (i + 4 < GlobalArregllo.Length - 1)
                                    {
                                        //Aqui verificamos  que despues esto tengo un OPA 
                                        if ((VerificarOPA(TokensValidos, GlobalArregllo[i + 4])))
                                        {
                                            //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
                                            if (i + 5 < GlobalArregllo.Length)
                                            {
                                                //aqui hacemo referencia que puede ser un ID o CNE
                                                if ((GlobalArregllo[i + 5] == TokensValidos[16]) || (GlobalArregllo[i + 5] == TokensValidos[33]))
                                                {
                                                    //aqui saltamos del ciclo las posiciones comparadas de los tokens
                                                    banderitaINT = true;
                                                    i = i + 4;
                                                }
                                                //si es diferente a un ID o CNE en este punto se debe disparara un error
                                                else if ((GlobalArregllo[i + 5] != TokensValidos[16]) || (GlobalArregllo[i + 5] != TokensValidos[33]))
                                                {
                                                    //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
                                                    Error = Error + "  despues del una OPA(+,-,/,*) no se esta refiriendo a un ID o CNE";
                                                    BANDERAEPICA = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
                                                    Error = Error + "  despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE";
                                                    BANDERAEPICA = false;
                                                    break;
                                                }
                                            }


                                        }
                                    }

                                    if (banderitaINT)
                                    {

                                    }
                                    else
                                    {
                                        i = i + 2;
                                    }


                                }
                                else
                                {
                                    //capturamos el error que despues del igual tiene que tener un ID o una constante numerica entera
                                    Error = Error + "  despues del igual tiene que tener un ID o una constante numerica entera";
                                    BANDERAEPICA = false;
                                    break;
                                }
                            }

                            i = i + 1;
                        }
                        else
                        {
                            //capturamos error despues de un tipo de dato debe ir un ID
                            Error = Error + " despues de un tipo de dato (INT) debe ir un ID";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra imp = PRN3 LISTO FUNCIONAL
                    else if (TokensValidos[2] == GlobalArregllo[i])
                    {

                        if ((GlobalArregllo[i + 1] == TokensValidos[15]) || (GlobalArregllo[i + 1] == TokensValidos[16]) || (GlobalArregllo[i + 1] == TokensValidos[17]) || (GlobalArregllo[i + 1] == TokensValidos[33]))
                        {
                            //Comprobamos que la siguiente posicion del token sea && si no no se mostrara error
                            if (GlobalArregllo[i + 2] == TokensValidos[23])
                            {
                                //Comprobamos que la siguiente posicion del token sea && si no no se mostrara error
                                if ((GlobalArregllo[i + 3] == TokensValidos[15]) || (GlobalArregllo[i + 3] == TokensValidos[16]) || (GlobalArregllo[i + 3] == TokensValidos[17]) || (GlobalArregllo[i + 3] == TokensValidos[33]))
                                {

                                }
                                else
                                {
                                    // Capturamos del error despues de IMP tiene que tener un ID o CN o una cadena
                                    Error = Error + " despues de IMP con un argunmento y un & tiene que tener un ID o CN o una cadena";
                                    BANDERAEPICA = false;
                                    break;
                                }
                                //saltamos del ciclo las posiciones comparadas de los tokens
                                i = i + 2;
                            }
                            //saltamos del ciclo las posiciones comparadas de los tokens
                            i = i + 1;

                        }
                        else
                        {
                            // Capturamos del error despues de IMP tiene que tener un ID o CN o una cadena
                            Error = Error + " despues de IMP tiene que tener un ID o CN o una cadena";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra flt = FLT5 LISTO FUNCIONAL
                    else if (TokensValidos[4] == GlobalArregllo[i])
                    {
                        //aqui comprobamos que despues de un FLT venga ID
                        if (GlobalArregllo[i + 1] == TokensValidos[33])
                        {

                            if (GlobalArregllo[i + 2] != TokensValidos[30])
                            {
                                //TODO bien aqui saltamos del ciclo las posiciones comparadas de los tokens
                                i = i + 2;
                            }
                            else if (GlobalArregllo[i + 2] == TokensValidos[30])
                            {
                                if ((GlobalArregllo[i + 3] == TokensValidos[33]) || (GlobalArregllo[i + 3] == TokensValidos[17]))
                                {


                                    //Todo chill porque se asigno un varible con un ID o con constante numerica real

                                    //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
                                    if (i + 4 < GlobalArregllo.Length - 1)
                                    {
                                        //Aqui verificamos  que despues esto tengo un OPA 
                                        if ((VerificarOPA(TokensValidos, GlobalArregllo[i + 4])))
                                        {
                                            //ESTE IF IMPORTATE YA QUE NOS EVITA EL ERROR DE INDEX DEL ARREGLO
                                            if (i + 5 < GlobalArregllo.Length)
                                            {
                                                //aqui hacemo referencia que puede ser un ID o CNR
                                                if ((GlobalArregllo[i + 5] == TokensValidos[17]) || (GlobalArregllo[i + 5] == TokensValidos[33]))
                                                {
                                                    //aqui saltamos del ciclo las posiciones comparadas de los tokens
                                                    banderitaFLT = true;
                                                    i = i + 4;
                                                }
                                                //si es diferente a un ID o CNE en este punto se debe disparara un error
                                                else if ((GlobalArregllo[i + 5] != TokensValidos[17]) || (GlobalArregllo[i + 5] != TokensValidos[33]))
                                                {
                                                    //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
                                                    Error = Error + "  despues del una OPA(+,-,/,*) no se esta refiriendo a un ID o CNR";
                                                    BANDERAEPICA = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    //capturamos el error que despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNE
                                                    Error = Error + "  despues del una OPA(+,-,/,*) no esta correctamente o la referencia no es a hacia un ID o CNR";
                                                    BANDERAEPICA = false;
                                                    break;
                                                }
                                            }


                                        }
                                    }
                                    if (banderitaFLT)
                                    {

                                    }
                                    else
                                    {
                                        i = i + 2;
                                    }

                                }
                                else
                                {
                                    //capturamos el error que despues del igual tiene que tener un ID o una constante numerica entera
                                    Error = Error + "  despues del igual tiene que tener un ID o una constante numerica real";
                                    BANDERAEPICA = false;
                                    break;
                                }
                            }


                        }
                        else
                        {
                            //capturamos error despues de un tipo de dato debe ir un ID
                            Error = Error + " despues de un tipo de dato (INT) debe ir un ID";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra fori = fri6 LISTO FUNCIONAL
                    else if (TokensValidos[5] == GlobalArregllo[i])
                    {
                        //en el caaso del ciclo for se tiene que ser muy especifico dado que esperamos en total 11 argumentos 
                        //con este if evitamos el error por index
                        if (i + 11 < GlobalArregllo.Length)
                        {
                            //la base es esta int X = 1 X < 10 X + 1
                            // que tenga una declaracion de una variable CNE, despues la variable con la comprobacion una operacion relacional y un CNE
                            // y por ultimo una operacion aritmetica con un ID el operador y una CNE
                            if ((TokensValidos[1] == GlobalArregllo[i + 1])
                           && (TokensValidos[33] == GlobalArregllo[i + 2])
                           && (TokensValidos[30] == GlobalArregllo[i + 3])
                           && TokensValidos[16] == GlobalArregllo[i + 4]
                           && TokensValidos[33] == GlobalArregllo[i + 5]
                           && VerificarOPL(TokensValidos, GlobalArregllo[i + 6])
                           && (TokensValidos[16] == GlobalArregllo[i + 7])
                           && (TokensValidos[33] == GlobalArregllo[i + 8])
                           && VerificarOPA(TokensValidos, GlobalArregllo[i + 9])
                           && TokensValidos[16] == GlobalArregllo[i + 10])
                            {
                                i = i + 10;
                            }
                            else
                            {
                                //capturamos error despues de la escritura como esta compuesto el fori
                                Error = Error + " despues de la escritura como esta compuesto el fori";
                                BANDERAEPICA = false;
                                break;

                            }
                        }
                        else
                        {
                            //capturamos error Faltaa de argumentos en el for ejemplo fori int X = 1 X < 10 X + 1
                            Error = Error + " Faltaa de argumentos en el for ejemplo fori int X = 1 X < 10 X + 1";
                            BANDERAEPICA = false;
                            break;
                        }

                    }
                    //Validacion de la palabra nul = NUL7 LISTO FUNCIONAL
                    else if (TokensValidos[6] == GlobalArregllo[i])
                    {
                        //Aqui comprobamos que despues del nul venga una ID
                        if (TokensValidos[33] == GlobalArregllo[i + 1])
                        {

                        }
                        else
                        {
                            //capturamos error despues de la palabra reservada NUL debe de seguir un identificador valido
                            Error = Error + " despues de la palabra reservada NUL debe de seguir un identificador valido";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra nuev = NEW8 LISTO FUNCIONAL problema aqui we
                    else if (TokensValidos[7] == GlobalArregllo[i])
                    {
                        //Aqui comprobamos que despues del new venga una ID
                        if (TokensValidos[33] == GlobalArregllo[i + 1])
                        {

                        }
                        else
                        {
                            //capturamos error despues de la palabra reservada NEW debe de seguir un identificador valido
                            Error = Error + " despues de la palabra reservada NEW debe de seguir un identificador valido";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra whi = whi9 LISTO FUNCIONAL
                    else if (TokensValidos[8] == GlobalArregllo[i])
                    {
                        //Comparamos que se realice una comparacion relacion y son id OPR y una CNE
                        if ((TokensValidos[33] == GlobalArregllo[i + 1]) && VerificarOPR(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
                        {
                            i = i + 3;
                        }
                        else
                        {
                            //capturamos error despues de la palabra reservada whi tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
                            Error = Error + " despues de la palabra reservada whi tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra Do = DW10 LISTO FUNCIONAL
                    else if (TokensValidos[9] == GlobalArregllo[i])
                    {
                        if (banderaWHILE)
                        {

                        }
                        else
                        {
                            //Atrapamos la excepcion
                            Error = Error + " No puede utlizar un DO si no tienes definido un while";
                            BANDERAEPICA = false;
                            break;

                        }
                    }
                    //Validacion de la palabra si = IF11LISTO FUNCIONAL
                    else if (TokensValidos[10] == GlobalArregllo[i])
                    {
                        BanderaIF = true;
                        //Comprabos que no se salga del arreglo para evitar error de index del arreglo
                        if (i + 3 < GlobalArregllo.Length)
                        {
                            //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
                            if (TokensValidos[33] == GlobalArregllo[i + 1] && VerificarOPL(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
                            {
                                //verificamos si viene otro operador relacional para repetir el if de arriba con diferentee
                                if (!VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
                                {
                                    if (TokensValidos[11] == GlobalArregllo[i + 4] || TokensValidos[12] == GlobalArregllo[i + 4])
                                    {
                                        //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
                                        Error = Error + " capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova";
                                        BANDERAEPICA = false;
                                        break;
                                    }
                                    i = i + 3;
                                }
                                else
                                {
                                    //Comprabos que no se salga del arreglo para evitar error de index del arreglo
                                    if (i + 7 < GlobalArregllo.Length)
                                    {
                                        //Verificamos quee operador reelacional como va
                                        if (VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
                                        {
                                            //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
                                            if (TokensValidos[33] == GlobalArregllo[i + 5] && VerificarOPL(TokensValidos, GlobalArregllo[i + 6]) && TokensValidos[16] == GlobalArregllo[i + 7])
                                            {

                                                if (TokensValidos[11] == GlobalArregllo[i + 8] || TokensValidos[12] == GlobalArregllo[i + 8])
                                                {
                                                    //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
                                                    Error = Error + " capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova";
                                                    BANDERAEPICA = false;
                                                    break;
                                                }
                                                i = i + 7;
                                            }
                                            else
                                            {
                                                //capturamos error despues de la palabra reservada SI tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
                                                Error = Error + " despues del operador logico && ó || ó ! tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
                                                BANDERAEPICA = false;
                                                break;
                                            }
                                        }


                                    }
                                    else
                                    {

                                        //capturamos error de falta de argumentos
                                        Error = Error + " error de falta de argumentos despues Operador logico minimo una operacion relacional";
                                        BANDERAEPICA = false;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                //capturamos error despues de la palabra reservada SI tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
                                Error = Error + " despues de la palabra reservada SI tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
                                BANDERAEPICA = false;
                                break;
                            }
                        }
                        else
                        {
                            //capturamos error de falta de argumentos
                            Error = Error + " error de falta de argumentos minimo una operacion relacional";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra sino = EL12 OJO esta solo va ser valido si se declaro un if antes LISTO FUNCIONAL
                    else if (TokensValidos[11] == GlobalArregllo[i])
                    {
                        if (BanderaIF)
                        {
                            //seguimos con el codigo
                            //Comprabos que no se salga del arreglo para evitar error de index del arreglo
                            if (i + 3 < GlobalArregllo.Length)
                            {
                                //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
                                if (TokensValidos[33] == GlobalArregllo[i + 1] && VerificarOPL(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
                                {
                                    //verificamos si viene otro operador relacional para repetir el if de arriba con diferentee
                                    if (!VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
                                    {
                                        if (TokensValidos[10] == GlobalArregllo[i + 4] || TokensValidos[12] == GlobalArregllo[i + 4])
                                        {
                                            //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
                                            Error = Error + " capturamos error despues de la palabra reservada SINO y toda su sintaxis no puede ir un si o sinova";
                                            BANDERAEPICA = false;
                                            break;
                                        }
                                        i = i + 3;
                                    }
                                    else
                                    {
                                        //Comprabos que no se salga del arreglo para evitar error de index del arreglo
                                        if (i + 7 < GlobalArregllo.Length)
                                        {
                                            //Verificamos quee operador reelacional como va
                                            if (VerificarOPR(TokensValidos, GlobalArregllo[i + 4]))
                                            {
                                                //Aqui estamos especificando que se haga una comparacion logica: un ID, un operador logico y una CNE
                                                if (TokensValidos[33] == GlobalArregllo[i + 5] && VerificarOPL(TokensValidos, GlobalArregllo[i + 6]) && TokensValidos[16] == GlobalArregllo[i + 7])
                                                {
                                                    if (TokensValidos[10] == GlobalArregllo[i + 8] || TokensValidos[12] == GlobalArregllo[i + 8])
                                                    {
                                                        //capturamos error despues de la palabra reservada SI y toda su sintaxis no puede ir un sino sinova
                                                        Error = Error + " capturamos error despues de la palabra reservada SINO y toda su sintaxis no puede ir un si o sinova";
                                                        BANDERAEPICA = false;
                                                        break;
                                                    }
                                                    i = i + 7;
                                                }
                                                else
                                                {
                                                    //capturamos error despues de la palabra reservada SINO tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
                                                    Error = Error + " despues del operador logico && ó || ó ! tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
                                                    BANDERAEPICA = false;
                                                    break;
                                                }
                                            }


                                        }
                                        else
                                        {

                                            //capturamos error de falta de argumentos
                                            Error = Error + " error de falta de argumentos despues Operador logico minimo una operacion relacional";
                                            BANDERAEPICA = false;
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    //capturamos error despues de la palabra reservada SINO tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
                                    Error = Error + " despues de la palabra reservada SINO tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
                                    BANDERAEPICA = false;
                                    break;
                                }
                            }
                            else
                            {
                                //capturamos error de falta de argumentos
                                Error = Error + " error de falta de argumentos minimo una operacion relacional";
                                BANDERAEPICA = false;
                                break;
                            }
                        }
                        else
                        {
                            //Atrapamos la excepcion
                            Error = Error + " No puede utlizar un SINOVA sin antes utilizar un SI para su desicion";
                            BANDERAEPICA = false;
                            break;
                        }

                    }
                    //Validacion de la palabra Sinova = IF13 OJO esta solo va ser valido si se declaro un if antes LISTO FUNCIONAL
                    else if (TokensValidos[12] == GlobalArregllo[i])
                    {
                        if (BanderaIF)
                        {
                            //seguimos con el codigo
                        }
                        else
                        {
                            //Atrapamos la excepcion
                            Error = Error + " No puede utlizar un SINO sin antes utilizar un SI para su desicion";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra str = SR14 LISTO FUNCIONAL
                    else if (TokensValidos[13] == GlobalArregllo[i])
                    {
                        //Comprobamos que siga un ID 
                        if (GlobalArregllo[i + 1] == TokensValidos[33])
                        {
                            //despues puede venir un igual o solo se puede declara pero no usuarse
                            if (GlobalArregllo[i + 2] != TokensValidos[30])
                            {
                                //TODO bien aqui saltamos del ciclo las posiciones comparadas de los tokens
                                i = i + 2;
                            }
                            //
                            else if (GlobalArregllo[i + 2] == TokensValidos[30])
                            {
                                if (GlobalArregllo[i + 3] == TokensValidos[15])
                                {
                                    i = i + 3;
                                }
                                else
                                {
                                    //capturamos el error que despues del igual tiene que ser una cadena
                                    Error = Error + "  despues del igual tiene que ser una cadena";
                                    BANDERAEPICA = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //capturamos error despues de un tipo de dato debe ir un ID
                            Error = Error + " despues de un tipo de dato (STR) debe ir un ID";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de la palabra exc = EX15L ISTO FUNCIONAL
                    else if (TokensValidos[14] == GlobalArregllo[i])
                    {
                        if (i + 5 < GlobalArregllo.Length)
                        {
                            if (TokensValidos[33] == GlobalArregllo[i + 1] && VerificarOPL(TokensValidos, GlobalArregllo[i + 2]) && TokensValidos[16] == GlobalArregllo[i + 3])
                            {
                                if (TokensValidos[20] == GlobalArregllo[i + 4] && TokensValidos[15] == GlobalArregllo[i + 5])
                                {
                                    i = i + 5;
                                }
                                else
                                {
                                    //capturamos el error despues de operador aritmetica / o la cadena no esta definida
                                    Error = Error + " despues de operador aritmetica / o la cadena no esta definida";
                                    BANDERAEPICA = false;
                                    break;
                                }
                            }
                            else
                            {
                                //capturamos error despues de la palabra reservada exc tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10
                                Error = Error + " despues de la palabra reservada exc tiene que venir con una compracion entre un ID y una CNE por ejemplo X < 10";
                                BANDERAEPICA = false;
                                break;
                            }

                        }
                        else
                        {
                            //capturamos error de falta de argumentos
                            Error = Error + " error de falta de argumentos minimo una operacion relacional y una cadena que suelte la excepcion";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion del comentario // = CO22
                    else if (TokensValidos[21] == GlobalArregllo[i])
                    {
                        //
                        if (TokensValidos[15] == GlobalArregllo[i + 1])
                        {
                            i = i + 1;
                        }
                        else
                        {
                            Error = Error + " Despues de la querrer comentar // tiene que ir una cadena ejem: «Este_es_un_comentario»";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Validacion de cadena sola que quiere decir que un delimitador de una cadena
                    else if (TokensValidos[15] == GlobalArregllo[i])
                    {
                        //aqui no pasa nada solo se sigue el ciclo normal
                    }
                    //Validacion de un identificador idn = id34
                    else if (TokensValidos[33] == GlobalArregllo[i])
                    {
                        if (VerificarOPA(TokensValidos, GlobalArregllo[i + 1]))
                        {
                            if ((TokensValidos[33] == GlobalArregllo[i + 2]) || (TokensValidos[16] == GlobalArregllo[i]) || (TokensValidos[17] == GlobalArregllo[i + 2]))
                            {
                                i = i + 2;
                            }
                            else
                            {
                                //Despues de un OPA tiene que ir un CN 0 un ID
                                Error = Error + " Despues de un Operacion artimetica (+-/*) tiene que ir un CN 0 un ID";
                                BANDERAEPICA = false;
                                break;
                            }
                        }
                        else
                        {
                            //cachamos el error de un ID solo
                            Error = Error + " Despues de un id solo puede ir un Operacion artimetica (+-/*) y constante numerica pero no puede ir solo";
                            BANDERAEPICA = false;
                            break;
                        }
                    }
                    //Quiebre en que caso que tenga un token de error
                    else if (TokensValidos[34] == GlobalArregllo[i])
                    {

                        Error = Error + " se decteto un error de TOKEN (LITERAL EL TOKEN DICE ERROR)";
                        BANDERAEPICA = false;
                        break;
                    }
                    //vaalidacion de abrir llave "{"
                    /*else if (TokensValidos[36] == GlobalArregllo[i])
                    {
                        if (TokensValidos[36] != GlobalArregllo[i+1])
                        {

                        }
                        else
                        {
                            BANDERAEPICA = false;
                            Error = Error + "No puedes colocar dos inicio de llaves {{";
                            break;
                        }
                    }*/
                    //vaalidacion de cerrar llave "}"
                    else if (TokensValidos[37] == GlobalArregllo[i])
                    {
              
                    }
                    //Validacion de la palabra fin = END4 que el program termine con fin
                    if (i == GlobalArregllo.Length - 1)
                    {
                        //Validacion de la palabra fin = END4 que el program termine con fin
                        if (TokensValidos[3] == GlobalArregllo[i])
                        {
                            //MessageBox.Show("End4 es valido");
                            BANDERAEPICA = true;
                        }
                        else
                        {
                            BANDERAEPICA = false;
                            Error = Error + "El programa tiene que finalizar con fin";
                            break;
                        }
                    }



                }
                if (BANDERAEPICA)
                {
                    MessageBox.Show("Analizador sintactico: correctamente pasado");
                }
                else
                {
                    MessageBox.Show("Analizador sintactico: Inconrrectamente en " + Error);
                }
                if (Conexion.State == ConnectionState.Open)
                {
                    //Cerramos la conexion
                    Conexion.Close();
                }
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
                //Verificar si la conexionEPICA Esta abierta de ser asi se cierra
                if (Conexion.State == ConnectionState.Open)
                {
                    //Cerramos la conexion
                    Conexion.Close();
                }
            }

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
            int ContadorDeVariables=0;
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
                    
                    ValoresVar =CodigoLimpio[i].ContenidoDeLinea.Split(' ');
                    V.TipoDeDato = ValoresVar[0];
                    V.Identidicador = ValoresVar[1];
                    if (ValoresVar.Length <= 4)
                    {
                        V.Valor = ValoresVar[3];
                    }
                    else
                    {
                        for (int j  = 3; j < ValoresVar.Length; j++)
                        {
                            V.Valor =  V.Valor + " " + ValoresVar[j];
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

        private void btnCargarVariables_Click(object sender, EventArgs e)
        {
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
        }
    }
}
    

