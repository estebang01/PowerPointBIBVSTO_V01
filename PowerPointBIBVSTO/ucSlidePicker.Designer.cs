namespace PowerPointBIBVSTO
{
    partial class ucSlidePicker
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpThumbs = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpThumbs
            // 
            this.flpThumbs.AutoScroll = true;
            this.flpThumbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThumbs.Location = new System.Drawing.Point(0, 0);
            this.flpThumbs.Name = "flpThumbs";
            this.flpThumbs.Size = new System.Drawing.Size(150, 150);
            this.flpThumbs.TabIndex = 0;
            // 
            // ucSlidePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpThumbs);
            this.Name = "ucSlidePicker";
            this.Size = new System.Drawing.Size(150, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpThumbs;
    }
}
