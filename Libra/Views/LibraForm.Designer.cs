namespace Libra {
    partial class LibraForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraForm));
            this.rentalPanel = new System.Windows.Forms.Panel();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.BorrowButton = new System.Windows.Forms.Button();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.ClearButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchWordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.booksDataGridView = new System.Windows.Forms.DataGridView();
            this.BooksContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.貸出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返却ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.検索条件のクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.書籍追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.書籍削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteBookButton = new System.Windows.Forms.Button();
            this.AddBookButton = new System.Windows.Forms.Button();
            this.rentalPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).BeginInit();
            this.BooksContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // rentalPanel
            // 
            this.rentalPanel.Controls.Add(this.UserNameLabel);
            this.rentalPanel.Controls.Add(this.UserNameTextBox);
            this.rentalPanel.Controls.Add(this.ReturnButton);
            this.rentalPanel.Controls.Add(this.BorrowButton);
            resources.ApplyResources(this.rentalPanel, "rentalPanel");
            this.rentalPanel.Name = "rentalPanel";
            // 
            // UserNameLabel
            // 
            resources.ApplyResources(this.UserNameLabel, "UserNameLabel");
            this.UserNameLabel.Name = "UserNameLabel";
            // 
            // UserNameTextBox
            // 
            resources.ApplyResources(this.UserNameTextBox, "UserNameTextBox");
            this.UserNameTextBox.Name = "UserNameTextBox";
            // 
            // ReturnButton
            // 
            resources.ApplyResources(this.ReturnButton, "ReturnButton");
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.UseVisualStyleBackColor = true;
            // 
            // BorrowButton
            // 
            resources.ApplyResources(this.BorrowButton, "BorrowButton");
            this.BorrowButton.Name = "BorrowButton";
            this.BorrowButton.UseVisualStyleBackColor = true;
            // 
            // searchPanel
            // 
            resources.ApplyResources(this.searchPanel, "searchPanel");
            this.searchPanel.Controls.Add(this.ClearButton);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchWordTextBox);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Name = "searchPanel";
            // 
            // ClearButton
            // 
            resources.ApplyResources(this.ClearButton, "ClearButton");
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.Name = "searchButton";
            this.searchButton.UseVisualStyleBackColor = true;
            // 
            // searchWordTextBox
            // 
            resources.ApplyResources(this.searchWordTextBox, "searchWordTextBox");
            this.searchWordTextBox.Name = "searchWordTextBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // booksDataGridView
            // 
            this.booksDataGridView.AllowUserToAddRows = false;
            this.booksDataGridView.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.booksDataGridView, "booksDataGridView");
            this.booksDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.booksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.booksDataGridView.ContextMenuStrip = this.BooksContextMenuStrip;
            this.booksDataGridView.MultiSelect = false;
            this.booksDataGridView.Name = "booksDataGridView";
            this.booksDataGridView.ReadOnly = true;
            this.booksDataGridView.RowTemplate.Height = 21;
            this.booksDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // BooksContextMenuStrip
            // 
            this.BooksContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.貸出ToolStripMenuItem,
            this.返却ToolStripMenuItem,
            this.検索条件のクリアToolStripMenuItem,
            this.書籍追加ToolStripMenuItem,
            this.書籍削除ToolStripMenuItem});
            this.BooksContextMenuStrip.Name = "BooksContextMenuStrip";
            resources.ApplyResources(this.BooksContextMenuStrip, "BooksContextMenuStrip");
            // 
            // 貸出ToolStripMenuItem
            // 
            this.貸出ToolStripMenuItem.Name = "貸出ToolStripMenuItem";
            resources.ApplyResources(this.貸出ToolStripMenuItem, "貸出ToolStripMenuItem");
            // 
            // 返却ToolStripMenuItem
            // 
            this.返却ToolStripMenuItem.Name = "返却ToolStripMenuItem";
            resources.ApplyResources(this.返却ToolStripMenuItem, "返却ToolStripMenuItem");
            // 
            // 検索条件のクリアToolStripMenuItem
            // 
            this.検索条件のクリアToolStripMenuItem.Name = "検索条件のクリアToolStripMenuItem";
            resources.ApplyResources(this.検索条件のクリアToolStripMenuItem, "検索条件のクリアToolStripMenuItem");
            // 
            // 書籍追加ToolStripMenuItem
            // 
            this.書籍追加ToolStripMenuItem.Name = "書籍追加ToolStripMenuItem";
            resources.ApplyResources(this.書籍追加ToolStripMenuItem, "書籍追加ToolStripMenuItem");
            // 
            // 書籍削除ToolStripMenuItem
            // 
            this.書籍削除ToolStripMenuItem.Name = "書籍削除ToolStripMenuItem";
            resources.ApplyResources(this.書籍削除ToolStripMenuItem, "書籍削除ToolStripMenuItem");
            // 
            // DeleteBookButton
            // 
            resources.ApplyResources(this.DeleteBookButton, "DeleteBookButton");
            this.DeleteBookButton.Name = "DeleteBookButton";
            this.DeleteBookButton.UseVisualStyleBackColor = true;
            // 
            // AddBookButton
            // 
            resources.ApplyResources(this.AddBookButton, "AddBookButton");
            this.AddBookButton.Name = "AddBookButton";
            this.AddBookButton.UseVisualStyleBackColor = true;
            // 
            // LibraForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddBookButton);
            this.Controls.Add(this.DeleteBookButton);
            this.Controls.Add(this.booksDataGridView);
            this.Controls.Add(this.rentalPanel);
            this.Controls.Add(this.searchPanel);
            this.Name = "LibraForm";
            this.rentalPanel.ResumeLayout(false);
            this.rentalPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).EndInit();
            this.BooksContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rentalPanel;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button BorrowButton;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchWordTextBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.DataGridView booksDataGridView;
        private System.Windows.Forms.Button DeleteBookButton;
        private System.Windows.Forms.Button AddBookButton;
        private System.Windows.Forms.ContextMenuStrip BooksContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 貸出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 返却ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 検索条件のクリアToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 書籍追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 書籍削除ToolStripMenuItem;
    }
}

