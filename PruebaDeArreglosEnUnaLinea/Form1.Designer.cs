﻿namespace PruebaDeArreglosEnUnaLinea
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
            ((System.ComponentModel.ISupportInitialize)(this.dtgVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtCodigo.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.txtLexico.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLexico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtLexico.Location = new System.Drawing.Point(460, 26);
            this.txtLexico.Multiline = true;
            this.txtLexico.Name = "txtLexico";
            this.txtLexico.Size = new System.Drawing.Size(327, 362);
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
            this.btnSintactico.Location = new System.Drawing.Point(798, 164);
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
            this.dtgVariables.Location = new System.Drawing.Point(12, 426);
            this.dtgVariables.Name = "dtgVariables";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dtgVariables.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgVariables.Size = new System.Drawing.Size(437, 150);
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
            this.lblVariables.Location = new System.Drawing.Point(8, 403);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(90, 20);
            this.lblVariables.TabIndex = 12;
            this.lblVariables.Text = "Variables";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(852, 443);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(638, 491);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 14;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(852, 472);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.TabIndex = 15;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnCargarVariables
            // 
            this.btnCargarVariables.Location = new System.Drawing.Point(460, 484);
            this.btnCargarVariables.Name = "btnCargarVariables";
            this.btnCargarVariables.Size = new System.Drawing.Size(75, 36);
            this.btnCargarVariables.TabIndex = 16;
            this.btnCargarVariables.Text = "Cargar Variables";
            this.btnCargarVariables.UseVisualStyleBackColor = true;
            this.btnCargarVariables.Click += new System.EventHandler(this.btnCargarVariables_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1042, 613);
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
    }
}
