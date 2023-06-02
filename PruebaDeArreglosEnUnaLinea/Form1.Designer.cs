namespace PruebaDeArreglosEnUnaLinea
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtLexico = new System.Windows.Forms.TextBox();
            this.btnPasarALexico = new System.Windows.Forms.Button();
            this.btnSintactico = new System.Windows.Forms.Button();
            this.dtgVariables = new System.Windows.Forms.DataGridView();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblLexico = new System.Windows.Forms.Label();
            this.lblVariables = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnCargarVariables = new System.Windows.Forms.Button();
            this.btnSinctatcio2 = new System.Windows.Forms.Button();
            this.txtCodigoIntermedio = new System.Windows.Forms.TextBox();
            this.lblCodigoIntermedio = new System.Windows.Forms.Label();
            this.dtgTriplos = new System.Windows.Forms.DataGridView();
            this.lblTriplos = new System.Windows.Forms.Label();
            this.btnEnsamblador = new System.Windows.Forms.Button();
            this.dtgLadoVerdadero = new System.Windows.Forms.DataGridView();
            this.dtgLadoFalso = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgLoop = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTriplos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLadoVerdadero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLadoFalso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLoop)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtCodigo.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtCodigo.Location = new System.Drawing.Point(12, 26);
            this.txtCodigo.Multiline = true;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(335, 362);
            this.txtCodigo.TabIndex = 2;
            // 
            // txtLexico
            // 
            this.txtLexico.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtLexico.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLexico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtLexico.Location = new System.Drawing.Point(460, 26);
            this.txtLexico.Multiline = true;
            this.txtLexico.Name = "txtLexico";
            this.txtLexico.Size = new System.Drawing.Size(410, 362);
            this.txtLexico.TabIndex = 3;
            // 
            // btnPasarALexico
            // 
            this.btnPasarALexico.Location = new System.Drawing.Point(358, 179);
            this.btnPasarALexico.Name = "btnPasarALexico";
            this.btnPasarALexico.Size = new System.Drawing.Size(91, 41);
            this.btnPasarALexico.TabIndex = 4;
            this.btnPasarALexico.Text = "Boton Lexico";
            this.btnPasarALexico.UseVisualStyleBackColor = true;
            this.btnPasarALexico.Click += new System.EventHandler(this.btnPasarALexico_Click);
            // 
            // btnSintactico
            // 
            this.btnSintactico.Location = new System.Drawing.Point(876, 162);
            this.btnSintactico.Name = "btnSintactico";
            this.btnSintactico.Size = new System.Drawing.Size(78, 56);
            this.btnSintactico.TabIndex = 8;
            this.btnSintactico.Text = "Boton Sinctático";
            this.btnSintactico.UseVisualStyleBackColor = true;
            this.btnSintactico.Click += new System.EventHandler(this.btnSintactico_Click);
            // 
            // dtgVariables
            // 
            this.dtgVariables.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgVariables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgVariables.GridColor = System.Drawing.Color.Lime;
            this.dtgVariables.Location = new System.Drawing.Point(12, 412);
            this.dtgVariables.Name = "dtgVariables";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dtgVariables.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgVariables.Size = new System.Drawing.Size(335, 258);
            this.dtgVariables.TabIndex = 9;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.Location = new System.Drawing.Point(12, 3);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(63, 20);
            this.lblCodigo.TabIndex = 10;
            this.lblCodigo.Text = "codigo";
            // 
            // lblLexico
            // 
            this.lblLexico.AutoSize = true;
            this.lblLexico.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLexico.Location = new System.Drawing.Point(456, 3);
            this.lblLexico.Name = "lblLexico";
            this.lblLexico.Size = new System.Drawing.Size(63, 20);
            this.lblLexico.TabIndex = 11;
            this.lblLexico.Text = "Lexico";
            // 
            // lblVariables
            // 
            this.lblVariables.AutoSize = true;
            this.lblVariables.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVariables.Location = new System.Drawing.Point(8, 389);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(90, 20);
            this.lblVariables.TabIndex = 12;
            this.lblVariables.Text = "Variables";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(429, 490);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(429, 548);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 14;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(429, 519);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.TabIndex = 15;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnCargarVariables
            // 
            this.btnCargarVariables.Location = new System.Drawing.Point(347, 516);
            this.btnCargarVariables.Name = "btnCargarVariables";
            this.btnCargarVariables.Size = new System.Drawing.Size(75, 36);
            this.btnCargarVariables.TabIndex = 16;
            this.btnCargarVariables.Text = "Cargar Variables";
            this.btnCargarVariables.UseVisualStyleBackColor = true;
            this.btnCargarVariables.Click += new System.EventHandler(this.btnCargarVariables_Click);
            // 
            // btnSinctatcio2
            // 
            this.btnSinctatcio2.Location = new System.Drawing.Point(876, 224);
            this.btnSinctatcio2.Name = "btnSinctatcio2";
            this.btnSinctatcio2.Size = new System.Drawing.Size(75, 56);
            this.btnSinctatcio2.TabIndex = 17;
            this.btnSinctatcio2.Text = "Codigo Intermedio";
            this.btnSinctatcio2.UseVisualStyleBackColor = true;
            this.btnSinctatcio2.Click += new System.EventHandler(this.btnSinctatcio2_Click);
            // 
            // txtCodigoIntermedio
            // 
            this.txtCodigoIntermedio.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtCodigoIntermedio.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoIntermedio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtCodigoIntermedio.Location = new System.Drawing.Point(957, 26);
            this.txtCodigoIntermedio.Multiline = true;
            this.txtCodigoIntermedio.Name = "txtCodigoIntermedio";
            this.txtCodigoIntermedio.Size = new System.Drawing.Size(401, 362);
            this.txtCodigoIntermedio.TabIndex = 18;
            // 
            // lblCodigoIntermedio
            // 
            this.lblCodigoIntermedio.AutoSize = true;
            this.lblCodigoIntermedio.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoIntermedio.Location = new System.Drawing.Point(953, 3);
            this.lblCodigoIntermedio.Name = "lblCodigoIntermedio";
            this.lblCodigoIntermedio.Size = new System.Drawing.Size(153, 20);
            this.lblCodigoIntermedio.TabIndex = 19;
            this.lblCodigoIntermedio.Text = "CodigoIntermedio";
            // 
            // dtgTriplos
            // 
            this.dtgTriplos.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgTriplos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgTriplos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgTriplos.GridColor = System.Drawing.Color.Lime;
            this.dtgTriplos.Location = new System.Drawing.Point(506, 412);
            this.dtgTriplos.Name = "dtgTriplos";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dtgTriplos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgTriplos.Size = new System.Drawing.Size(364, 266);
            this.dtgTriplos.TabIndex = 20;
            // 
            // lblTriplos
            // 
            this.lblTriplos.AutoSize = true;
            this.lblTriplos.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTriplos.Location = new System.Drawing.Point(502, 389);
            this.lblTriplos.Name = "lblTriplos";
            this.lblTriplos.Size = new System.Drawing.Size(72, 20);
            this.lblTriplos.TabIndex = 21;
            this.lblTriplos.Text = "Triplos";
            // 
            // btnEnsamblador
            // 
            this.btnEnsamblador.Location = new System.Drawing.Point(876, 286);
            this.btnEnsamblador.Name = "btnEnsamblador";
            this.btnEnsamblador.Size = new System.Drawing.Size(75, 40);
            this.btnEnsamblador.TabIndex = 22;
            this.btnEnsamblador.Text = "Codigo en ensamblador";
            this.btnEnsamblador.UseVisualStyleBackColor = true;
            this.btnEnsamblador.Click += new System.EventHandler(this.btnEnsamblador_Click);
            // 
            // dtgLadoVerdadero
            // 
            this.dtgLadoVerdadero.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLadoVerdadero.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgLadoVerdadero.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLadoVerdadero.GridColor = System.Drawing.Color.Lime;
            this.dtgLadoVerdadero.Location = new System.Drawing.Point(885, 411);
            this.dtgLadoVerdadero.Name = "dtgLadoVerdadero";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dtgLadoVerdadero.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgLadoVerdadero.Size = new System.Drawing.Size(221, 118);
            this.dtgLadoVerdadero.TabIndex = 23;
            // 
            // dtgLadoFalso
            // 
            this.dtgLadoFalso.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLadoFalso.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgLadoFalso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLadoFalso.GridColor = System.Drawing.Color.Lime;
            this.dtgLadoFalso.Location = new System.Drawing.Point(885, 554);
            this.dtgLadoFalso.Name = "dtgLadoFalso";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dtgLadoFalso.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dtgLadoFalso.Size = new System.Drawing.Size(221, 124);
            this.dtgLadoFalso.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(881, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "TRTRUE";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(882, 532);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "TRFALSE";
            // 
            // dtgLoop
            // 
            this.dtgLoop.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLoop.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dtgLoop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLoop.GridColor = System.Drawing.Color.Lime;
            this.dtgLoop.Location = new System.Drawing.Point(1112, 411);
            this.dtgLoop.Name = "dtgLoop";
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dtgLoop.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dtgLoop.Size = new System.Drawing.Size(221, 141);
            this.dtgLoop.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Code", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1114, 391);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "LOOP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1370, 726);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtgLoop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgLadoFalso);
            this.Controls.Add(this.dtgLadoVerdadero);
            this.Controls.Add(this.btnEnsamblador);
            this.Controls.Add(this.lblTriplos);
            this.Controls.Add(this.dtgTriplos);
            this.Controls.Add(this.lblCodigoIntermedio);
            this.Controls.Add(this.txtCodigoIntermedio);
            this.Controls.Add(this.btnSinctatcio2);
            this.Controls.Add(this.btnCargarVariables);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblVariables);
            this.Controls.Add(this.lblLexico);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.dtgVariables);
            this.Controls.Add(this.btnSintactico);
            this.Controls.Add(this.btnPasarALexico);
            this.Controls.Add(this.txtLexico);
            this.Controls.Add(this.txtCodigo);
            this.Name = "Form1";
            this.Text = "Analizador Increible";
            ((System.ComponentModel.ISupportInitialize)(this.dtgVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTriplos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLadoVerdadero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLadoFalso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLoop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtLexico;
        private System.Windows.Forms.Button btnPasarALexico;
        private System.Windows.Forms.Button btnSintactico;
        private System.Windows.Forms.DataGridView dtgVariables;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblLexico;
        private System.Windows.Forms.Label lblVariables;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCargarVariables;
        private System.Windows.Forms.Button btnSinctatcio2;
        private System.Windows.Forms.TextBox txtCodigoIntermedio;
        private System.Windows.Forms.Label lblCodigoIntermedio;
        private System.Windows.Forms.DataGridView dtgTriplos;
        private System.Windows.Forms.Label lblTriplos;
        private System.Windows.Forms.Button btnEnsamblador;
        private System.Windows.Forms.DataGridView dtgLadoVerdadero;
        private System.Windows.Forms.DataGridView dtgLadoFalso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtgLoop;
        private System.Windows.Forms.Label label3;
    }
}

