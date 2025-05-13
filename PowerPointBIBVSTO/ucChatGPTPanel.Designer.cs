namespace PowerPointBIBVSTO
{
    partial class ucChatGPTPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador

        private void InitializeComponent()
        {
            this.LayoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.LayoutTablaRow0 = new System.Windows.Forms.TableLayoutPanel();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.btnValidar = new System.Windows.Forms.Button();
            this.LayoutTablaRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPregunta1 = new System.Windows.Forms.TextBox();
            this.btnEnviar1 = new System.Windows.Forms.Button();
            this.chatPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LayoutPrincipal.SuspendLayout();
            this.LayoutTablaRow0.SuspendLayout();
            this.LayoutTablaRow2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPrincipal
            // 
            this.LayoutPrincipal.ColumnCount = 1;
            this.LayoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPrincipal.Controls.Add(this.LayoutTablaRow2, 0, 2);
            this.LayoutPrincipal.Controls.Add(this.LayoutTablaRow0, 0, 0);
            this.LayoutPrincipal.Controls.Add(this.chatPanel, 0, 1);
            this.LayoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPrincipal.Location = new System.Drawing.Point(0, 0);
            this.LayoutPrincipal.Name = "LayoutPrincipal";
            this.LayoutPrincipal.RowCount = 3;
            this.LayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.LayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.LayoutPrincipal.Size = new System.Drawing.Size(276, 564);
            this.LayoutPrincipal.TabIndex = 0;
            this.LayoutPrincipal.Paint += new System.Windows.Forms.PaintEventHandler(this.LayoutPrincipal_Paint);
            // 
            // LayoutTablaRow0
            // 
            this.LayoutTablaRow0.ColumnCount = 2;
            this.LayoutTablaRow0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutTablaRow0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LayoutTablaRow0.Controls.Add(this.txtApiKey, 0, 0);
            this.LayoutTablaRow0.Controls.Add(this.btnValidar, 1, 0);
            this.LayoutTablaRow0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutTablaRow0.Location = new System.Drawing.Point(3, 3);
            this.LayoutTablaRow0.MaximumSize = new System.Drawing.Size(0, 40);
            this.LayoutTablaRow0.Name = "LayoutTablaRow0";
            this.LayoutTablaRow0.RowCount = 1;
            this.LayoutTablaRow0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutTablaRow0.Size = new System.Drawing.Size(270, 30);
            this.LayoutTablaRow0.TabIndex = 3;
            // 
            // txtApiKey
            // 
            this.txtApiKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtApiKey.Location = new System.Drawing.Point(3, 3);
            this.txtApiKey.Multiline = true;
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtApiKey.Size = new System.Drawing.Size(183, 24);
            this.txtApiKey.TabIndex = 0;
            // 
            // btnValidar
            // 
            this.btnValidar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnValidar.Location = new System.Drawing.Point(192, 3);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(75, 24);
            this.btnValidar.TabIndex = 1;
            this.btnValidar.Text = "Validar";
            this.btnValidar.UseVisualStyleBackColor = true;
            // 
            // LayoutTablaRow2
            // 
            this.LayoutTablaRow2.ColumnCount = 2;
            this.LayoutTablaRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutTablaRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LayoutTablaRow2.Controls.Add(this.txtPregunta1, 0, 0);
            this.LayoutTablaRow2.Controls.Add(this.btnEnviar1, 1, 0);
            this.LayoutTablaRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutTablaRow2.Location = new System.Drawing.Point(3, 531);
            this.LayoutTablaRow2.MaximumSize = new System.Drawing.Size(0, 40);
            this.LayoutTablaRow2.MinimumSize = new System.Drawing.Size(20, 0);
            this.LayoutTablaRow2.Name = "LayoutTablaRow2";
            this.LayoutTablaRow2.RowCount = 1;
            this.LayoutTablaRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LayoutTablaRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LayoutTablaRow2.Size = new System.Drawing.Size(270, 30);
            this.LayoutTablaRow2.TabIndex = 4;
            // 
            // txtPregunta1
            // 
            this.txtPregunta1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPregunta1.Location = new System.Drawing.Point(3, 3);
            this.txtPregunta1.Multiline = true;
            this.txtPregunta1.Name = "txtPregunta1";
            this.txtPregunta1.Size = new System.Drawing.Size(183, 24);
            this.txtPregunta1.TabIndex = 0;
            // 
            // btnEnviar1
            // 
            this.btnEnviar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEnviar1.Location = new System.Drawing.Point(192, 3);
            this.btnEnviar1.Name = "btnEnviar1";
            this.btnEnviar1.Size = new System.Drawing.Size(75, 24);
            this.btnEnviar1.TabIndex = 1;
            this.btnEnviar1.Text = "Chatear";
            this.btnEnviar1.UseVisualStyleBackColor = true;
            // 
            // chatPanel
            // 
            this.chatPanel.AutoScroll = true;
            this.chatPanel.BackColor = System.Drawing.Color.OldLace;
            this.chatPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chatPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.chatPanel.Location = new System.Drawing.Point(3, 39);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(270, 486);
            this.chatPanel.TabIndex = 5;
            this.chatPanel.WrapContents = false;
            // 
            // ucChatGPTPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LayoutPrincipal);
            this.Name = "ucChatGPTPanel";
            this.Size = new System.Drawing.Size(276, 564);
            this.LayoutPrincipal.ResumeLayout(false);
            this.LayoutTablaRow0.ResumeLayout(false);
            this.LayoutTablaRow0.PerformLayout();
            this.LayoutTablaRow2.ResumeLayout(false);
            this.LayoutTablaRow2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LayoutPrincipal;
        private System.Windows.Forms.TableLayoutPanel LayoutTablaRow0;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.TableLayoutPanel LayoutTablaRow2;
        private System.Windows.Forms.TextBox txtPregunta1;
        private System.Windows.Forms.Button btnEnviar1;
        private System.Windows.Forms.FlowLayoutPanel chatPanel;
    }
}
