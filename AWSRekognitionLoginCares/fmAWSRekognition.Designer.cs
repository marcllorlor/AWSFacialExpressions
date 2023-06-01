namespace AWSRekognitionLoginCares
{
    partial class fmAWSRekognition
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
            this.pbImatgeCamara = new System.Windows.Forms.PictureBox();
            this.btFerFoto = new System.Windows.Forms.Button();
            this.lbParaulaTriada = new System.Windows.Forms.Label();
            this.lbEmocioTriada = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImatgeCamara)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImatgeCamara
            // 
            this.pbImatgeCamara.Image = global::AWSRekognitionLoginCares.Properties.Resources.camera_off;
            this.pbImatgeCamara.Location = new System.Drawing.Point(55, 32);
            this.pbImatgeCamara.Name = "pbImatgeCamara";
            this.pbImatgeCamara.Size = new System.Drawing.Size(633, 477);
            this.pbImatgeCamara.TabIndex = 0;
            this.pbImatgeCamara.TabStop = false;
            // 
            // btFerFoto
            // 
            this.btFerFoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btFerFoto.Location = new System.Drawing.Point(1074, 32);
            this.btFerFoto.Name = "btFerFoto";
            this.btFerFoto.Size = new System.Drawing.Size(155, 67);
            this.btFerFoto.TabIndex = 1;
            this.btFerFoto.Text = "Fer Foto";
            this.btFerFoto.UseVisualStyleBackColor = false;
            this.btFerFoto.Click += new System.EventHandler(this.btFerFoto_Click);
            // 
            // lbParaulaTriada
            // 
            this.lbParaulaTriada.AutoSize = true;
            this.lbParaulaTriada.Location = new System.Drawing.Point(22, 929);
            this.lbParaulaTriada.Name = "lbParaulaTriada";
            this.lbParaulaTriada.Size = new System.Drawing.Size(132, 16);
            this.lbParaulaTriada.TabIndex = 3;
            this.lbParaulaTriada.Text = "La paraula triada és: ";
            // 
            // lbEmocioTriada
            // 
            this.lbEmocioTriada.AutoSize = true;
            this.lbEmocioTriada.Location = new System.Drawing.Point(429, 929);
            this.lbEmocioTriada.Name = "lbEmocioTriada";
            this.lbEmocioTriada.Size = new System.Drawing.Size(132, 16);
            this.lbEmocioTriada.TabIndex = 4;
            this.lbEmocioTriada.Text = "La paraula triada és: ";
            // 
            // fmAWSRekognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.lbEmocioTriada);
            this.Controls.Add(this.lbParaulaTriada);
            this.Controls.Add(this.btFerFoto);
            this.Controls.Add(this.pbImatgeCamara);
            this.KeyPreview = true;
            this.Name = "fmAWSRekognition";
            this.Text = "AWSRekognition";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmAWSRekognition_FormClosing);
            this.Load += new System.EventHandler(this.fmAWSRekognition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImatgeCamara)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImatgeCamara;
        private System.Windows.Forms.Button btFerFoto;
        private System.Windows.Forms.Label lbParaulaTriada;
        private System.Windows.Forms.Label lbEmocioTriada;
    }
}

