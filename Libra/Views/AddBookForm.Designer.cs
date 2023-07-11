namespace Libra.Views {
    partial class AddBookForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.isbnLabel = new System.Windows.Forms.Label();
            this.isbnTextBox = new System.Windows.Forms.TextBox();
            this.getBookInfoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.addButton.Location = new System.Drawing.Point(12, 224);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 25);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "追加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cancelButton.Location = new System.Drawing.Point(172, 224);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 25);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // isbnLabel
            // 
            this.isbnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isbnLabel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.isbnLabel.Location = new System.Drawing.Point(12, 20);
            this.isbnLabel.Name = "isbnLabel";
            this.isbnLabel.Size = new System.Drawing.Size(260, 15);
            this.isbnLabel.TabIndex = 0;
            this.isbnLabel.Text = "バーコード番号（ISBN）を入力してください";
            // 
            // isbnTextBox
            // 
            this.isbnTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isbnTextBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.isbnTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.isbnTextBox.Location = new System.Drawing.Point(12, 50);
            this.isbnTextBox.MaxLength = 13;
            this.isbnTextBox.Name = "isbnTextBox";
            this.isbnTextBox.Size = new System.Drawing.Size(260, 23);
            this.isbnTextBox.TabIndex = 1;
            this.isbnTextBox.Text = "978";
            this.isbnTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IsbnTextBox_KeyDown);
            this.isbnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsbnTextBox_KeyPress);
            // 
            // getBookInfoButton
            // 
            this.getBookInfoButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.getBookInfoButton.Location = new System.Drawing.Point(12, 90);
            this.getBookInfoButton.Name = "getBookInfoButton";
            this.getBookInfoButton.Size = new System.Drawing.Size(100, 25);
            this.getBookInfoButton.TabIndex = 2;
            this.getBookInfoButton.Text = "書籍情報取得";
            this.getBookInfoButton.UseVisualStyleBackColor = true;
            this.getBookInfoButton.Click += new System.EventHandler(this.GetBookInfoButtonClickAsync);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(9, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "書籍名：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(9, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "著者名：";
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.titleLabel.Location = new System.Drawing.Point(70, 130);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(0, 15);
            this.titleLabel.TabIndex = 4;
            // 
            // authorLabel
            // 
            this.authorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorLabel.AutoSize = true;
            this.authorLabel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.authorLabel.Location = new System.Drawing.Point(70, 160);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(0, 15);
            this.authorLabel.TabIndex = 6;
            // 
            // AddBookForm
            // 
            this.AcceptButton = this.getBookInfoButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.getBookInfoButton);
            this.Controls.Add(this.isbnTextBox);
            this.Controls.Add(this.isbnLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Libra.Properties.Settings.Default, "MyLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Location = global::Libra.Properties.Settings.Default.MyLocation;
            this.MinimumSize = new System.Drawing.Size(255, 275);
            this.Name = "AddBookForm";
            this.Text = "書籍追加";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label isbnLabel;
        private System.Windows.Forms.TextBox isbnTextBox;
        private System.Windows.Forms.Button getBookInfoButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorLabel;
    }
}