namespace DolbyDigitalInterleaver
{
    partial class Form1
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
            ac3TextBox = new TextBox();
            mlpTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ac3Button = new Button();
            mlpButton = new Button();
            InterleaveButton = new Button();
            SuspendLayout();
            // 
            // ac3TextBox
            // 
            ac3TextBox.Location = new Point(27, 64);
            ac3TextBox.Name = "ac3TextBox";
            ac3TextBox.Size = new Size(383, 23);
            ac3TextBox.TabIndex = 0;
            // 
            // mlpTextBox
            // 
            mlpTextBox.Location = new Point(27, 126);
            mlpTextBox.Name = "mlpTextBox";
            mlpTextBox.Size = new Size(383, 23);
            mlpTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 38);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 2;
            label1.Text = "AC3 File";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 104);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 3;
            label2.Text = "MLP/THD File";
            // 
            // ac3Button
            // 
            ac3Button.Location = new Point(435, 64);
            ac3Button.Name = "ac3Button";
            ac3Button.Size = new Size(75, 23);
            ac3Button.TabIndex = 4;
            ac3Button.Text = "Browse";
            ac3Button.UseVisualStyleBackColor = true;
            ac3Button.Click += ac3Button_Click;
            // 
            // mlpButton
            // 
            mlpButton.Location = new Point(435, 126);
            mlpButton.Name = "mlpButton";
            mlpButton.Size = new Size(75, 23);
            mlpButton.TabIndex = 5;
            mlpButton.Text = "Browse";
            mlpButton.UseVisualStyleBackColor = true;
            mlpButton.Click += mlpButton_Click;
            // 
            // InterleaveButton
            // 
            InterleaveButton.Location = new Point(24, 161);
            InterleaveButton.Name = "InterleaveButton";
            InterleaveButton.Size = new Size(75, 23);
            InterleaveButton.TabIndex = 6;
            InterleaveButton.Text = "Interleave";
            InterleaveButton.UseVisualStyleBackColor = true;
            InterleaveButton.Click += InterleaveButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 201);
            Controls.Add(InterleaveButton);
            Controls.Add(mlpButton);
            Controls.Add(ac3Button);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(mlpTextBox);
            Controls.Add(ac3TextBox);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dolby Digital Interleaver";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ac3TextBox;
        private TextBox mlpTextBox;
        private Label label1;
        private Label label2;
        private Button ac3Button;
        private Button mlpButton;
        private Button InterleaveButton;
    }
}
