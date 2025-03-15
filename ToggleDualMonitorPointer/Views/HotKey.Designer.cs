namespace ToggleDualMonitorPointer.Views
{
    partial class HotKey
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            TbBlockPointer = new TextBox();
            TbFreePointer = new TextBox();
            BtSave = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(102, 16);
            label1.TabIndex = 0;
            label1.Text = "Bloquear ponteiro";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(12, 50);
            label2.Name = "label2";
            label2.Size = new Size(91, 16);
            label2.TabIndex = 1;
            label2.Text = "Liberar ponteiro";
            // 
            // TbBlockPointer
            // 
            TbBlockPointer.Font = new Font("Microsoft YaHei", 8.25F);
            TbBlockPointer.ForeColor = SystemColors.WindowFrame;
            TbBlockPointer.Location = new Point(120, 18);
            TbBlockPointer.Name = "TbBlockPointer";
            TbBlockPointer.ReadOnly = true;
            TbBlockPointer.Size = new Size(168, 22);
            TbBlockPointer.TabIndex = 2;
            TbBlockPointer.KeyDown += TbBlockPointer_KeyDown;
            // 
            // TbFreePointer
            // 
            TbFreePointer.Font = new Font("Microsoft YaHei", 8.25F);
            TbFreePointer.ForeColor = SystemColors.WindowFrame;
            TbFreePointer.Location = new Point(120, 47);
            TbFreePointer.Name = "TbFreePointer";
            TbFreePointer.ReadOnly = true;
            TbFreePointer.Size = new Size(168, 22);
            TbFreePointer.TabIndex = 3;
            TbFreePointer.KeyDown += TbFreePointer_KeyDown;
            // 
            // BtSave
            // 
            BtSave.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtSave.ForeColor = SystemColors.ControlDarkDark;
            BtSave.Location = new Point(100, 90);
            BtSave.Name = "BtSave";
            BtSave.Size = new Size(107, 36);
            BtSave.TabIndex = 4;
            BtSave.Text = "Salvar";
            BtSave.UseVisualStyleBackColor = true;
            BtSave.Click += BtSave_Click;
            // 
            // HotKey
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 135);
            Controls.Add(BtSave);
            Controls.Add(TbFreePointer);
            Controls.Add(TbBlockPointer);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Roboto Mono Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "HotKey";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuração de atalho";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox TbBlockPointer;
        private TextBox TbFreePointer;
        private Button BtSave;
    }
}