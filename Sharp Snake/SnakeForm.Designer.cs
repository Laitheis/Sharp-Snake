namespace Sharp_Snake
{
    partial class SnakeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStart = new Button();
            btnRestart = new Button();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(411, 377);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(213, 57);
            btnStart.TabIndex = 0;
            btnStart.Text = "Играть";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(62, 377);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(213, 57);
            btnRestart.TabIndex = 1;
            btnRestart.Text = "Рестарт";
            btnRestart.UseVisualStyleBackColor = true;
            // 
            // SnakeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 478);
            Controls.Add(btnRestart);
            Controls.Add(btnStart);
            Name = "SnakeForm";
            Text = "Form1";
            Load += SnakeForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnStart;
        private Button btnRestart;
    }
}
