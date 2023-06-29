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
            this.userNameLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.returnButton = new System.Windows.Forms.Button();
            this.borrowButton = new System.Windows.Forms.Button();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchWordTextBox = new System.Windows.Forms.TextBox();
            this.searchWordLabel = new System.Windows.Forms.Label();
            this.booksDataGridView = new System.Windows.Forms.DataGridView();
            this.bookIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.publisherDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BooksContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.貸出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返却ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.検索条件のクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.書籍追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.書籍削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.booksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.booksDataSet = new Libra.Models.BooksDataSet();
            this.deleteBookButton = new System.Windows.Forms.Button();
            this.addBookButton = new System.Windows.Forms.Button();
            this.rentalPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).BeginInit();
            this.BooksContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.booksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // rentalPanel
            // 
            this.rentalPanel.Controls.Add(this.userNameLabel);
            this.rentalPanel.Controls.Add(this.userNameTextBox);
            this.rentalPanel.Controls.Add(this.returnButton);
            this.rentalPanel.Controls.Add(this.borrowButton);
            resources.ApplyResources(this.rentalPanel, "rentalPanel");
            this.rentalPanel.Name = "rentalPanel";
            // 
            // userNameLabel
            // 
            resources.ApplyResources(this.userNameLabel, "userNameLabel");
            this.userNameLabel.Name = "userNameLabel";
            // 
            // userNameTextBox
            // 
            resources.ApplyResources(this.userNameTextBox, "userNameTextBox");
            this.userNameTextBox.Name = "userNameTextBox";
            // 
            // returnButton
            // 
            resources.ApplyResources(this.returnButton, "returnButton");
            this.returnButton.Name = "returnButton";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.Return_Click);
            // 
            // borrowButton
            // 
            resources.ApplyResources(this.borrowButton, "borrowButton");
            this.borrowButton.Name = "borrowButton";
            this.borrowButton.UseVisualStyleBackColor = true;
            this.borrowButton.Click += new System.EventHandler(this.Borrow_Click);
            // 
            // searchPanel
            // 
            resources.ApplyResources(this.searchPanel, "searchPanel");
            this.searchPanel.Controls.Add(this.clearButton);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchWordTextBox);
            this.searchPanel.Controls.Add(this.searchWordLabel);
            this.searchPanel.Name = "searchPanel";
            // 
            // clearButton
            // 
            resources.ApplyResources(this.clearButton, "clearButton");
            this.clearButton.Name = "clearButton";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.Clear_Click);
            // 
            // searchButton
            // 
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.Name = "searchButton";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.Search_Click);
            // 
            // searchWordTextBox
            // 
            resources.ApplyResources(this.searchWordTextBox, "searchWordTextBox");
            this.searchWordTextBox.Name = "searchWordTextBox";
            this.searchWordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchWordTextBoxKeyDown);
            // 
            // searchWordLabel
            // 
            resources.ApplyResources(this.searchWordLabel, "searchWordLabel");
            this.searchWordLabel.Name = "searchWordLabel";
            // 
            // booksDataGridView
            // 
            this.booksDataGridView.AllowUserToAddRows = false;
            this.booksDataGridView.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.booksDataGridView, "booksDataGridView");
            this.booksDataGridView.AutoGenerateColumns = false;
            this.booksDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.booksDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.booksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.booksDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bookIdDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.authorDataGridViewTextBoxColumn,
            this.publisherDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.booksDataGridView.ContextMenuStrip = this.BooksContextMenuStrip;
            this.booksDataGridView.DataSource = this.booksBindingSource;
            this.booksDataGridView.MultiSelect = false;
            this.booksDataGridView.Name = "booksDataGridView";
            this.booksDataGridView.ReadOnly = true;
            this.booksDataGridView.RowTemplate.Height = 21;
            this.booksDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.booksDataGridView.TabStop = false;
            this.booksDataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.BooksGridCellPainting);
            // 
            // bookIdDataGridViewTextBoxColumn
            // 
            this.bookIdDataGridViewTextBoxColumn.DataPropertyName = "BookId";
            resources.ApplyResources(this.bookIdDataGridViewTextBoxColumn, "bookIdDataGridViewTextBoxColumn");
            this.bookIdDataGridViewTextBoxColumn.Name = "bookIdDataGridViewTextBoxColumn";
            this.bookIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userNameDataGridViewTextBoxColumn
            // 
            this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            resources.ApplyResources(this.userNameDataGridViewTextBoxColumn, "userNameDataGridViewTextBoxColumn");
            this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
            this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            resources.ApplyResources(this.titleDataGridViewTextBoxColumn, "titleDataGridViewTextBoxColumn");
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // authorDataGridViewTextBoxColumn
            // 
            this.authorDataGridViewTextBoxColumn.DataPropertyName = "Author";
            resources.ApplyResources(this.authorDataGridViewTextBoxColumn, "authorDataGridViewTextBoxColumn");
            this.authorDataGridViewTextBoxColumn.Name = "authorDataGridViewTextBoxColumn";
            this.authorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // publisherDataGridViewTextBoxColumn
            // 
            this.publisherDataGridViewTextBoxColumn.DataPropertyName = "Publisher";
            resources.ApplyResources(this.publisherDataGridViewTextBoxColumn, "publisherDataGridViewTextBoxColumn");
            this.publisherDataGridViewTextBoxColumn.Name = "publisherDataGridViewTextBoxColumn";
            this.publisherDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(this.descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // BooksContextMenuStrip
            // 
            resources.ApplyResources(this.BooksContextMenuStrip, "BooksContextMenuStrip");
            this.BooksContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.貸出ToolStripMenuItem,
            this.返却ToolStripMenuItem,
            this.検索条件のクリアToolStripMenuItem,
            this.書籍追加ToolStripMenuItem,
            this.書籍削除ToolStripMenuItem});
            this.BooksContextMenuStrip.Name = "BooksContextMenuStrip";
            // 
            // 貸出ToolStripMenuItem
            // 
            this.貸出ToolStripMenuItem.Name = "貸出ToolStripMenuItem";
            resources.ApplyResources(this.貸出ToolStripMenuItem, "貸出ToolStripMenuItem");
            this.貸出ToolStripMenuItem.Click += new System.EventHandler(this.Borrow_Click);
            // 
            // 返却ToolStripMenuItem
            // 
            this.返却ToolStripMenuItem.Name = "返却ToolStripMenuItem";
            resources.ApplyResources(this.返却ToolStripMenuItem, "返却ToolStripMenuItem");
            this.返却ToolStripMenuItem.Click += new System.EventHandler(this.Return_Click);
            // 
            // 検索条件のクリアToolStripMenuItem
            // 
            this.検索条件のクリアToolStripMenuItem.Name = "検索条件のクリアToolStripMenuItem";
            resources.ApplyResources(this.検索条件のクリアToolStripMenuItem, "検索条件のクリアToolStripMenuItem");
            this.検索条件のクリアToolStripMenuItem.Click += new System.EventHandler(this.Clear_Click);
            // 
            // 書籍追加ToolStripMenuItem
            // 
            this.書籍追加ToolStripMenuItem.Name = "書籍追加ToolStripMenuItem";
            resources.ApplyResources(this.書籍追加ToolStripMenuItem, "書籍追加ToolStripMenuItem");
            this.書籍追加ToolStripMenuItem.Click += new System.EventHandler(this.AddBook_Click);
            // 
            // 書籍削除ToolStripMenuItem
            // 
            this.書籍削除ToolStripMenuItem.Name = "書籍削除ToolStripMenuItem";
            resources.ApplyResources(this.書籍削除ToolStripMenuItem, "書籍削除ToolStripMenuItem");
            this.書籍削除ToolStripMenuItem.Click += new System.EventHandler(this.DeleteBook_Click);
            // 
            // booksBindingSource
            // 
            this.booksBindingSource.DataMember = "BooksDataTable";
            this.booksBindingSource.DataSource = this.booksDataSet;
            // 
            // booksDataSet
            // 
            this.booksDataSet.DataSetName = "BooksDataSet";
            this.booksDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // deleteBookButton
            // 
            resources.ApplyResources(this.deleteBookButton, "deleteBookButton");
            this.deleteBookButton.Name = "deleteBookButton";
            this.deleteBookButton.UseVisualStyleBackColor = true;
            this.deleteBookButton.Click += new System.EventHandler(this.DeleteBook_Click);
            // 
            // addBookButton
            // 
            resources.ApplyResources(this.addBookButton, "addBookButton");
            this.addBookButton.Name = "addBookButton";
            this.addBookButton.UseVisualStyleBackColor = true;
            this.addBookButton.Click += new System.EventHandler(this.AddBook_Click);
            // 
            // LibraForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addBookButton);
            this.Controls.Add(this.deleteBookButton);
            this.Controls.Add(this.booksDataGridView);
            this.Controls.Add(this.rentalPanel);
            this.Controls.Add(this.searchPanel);
            this.KeyPreview = true;
            this.Name = "LibraForm";
            this.Load += new System.EventHandler(this.LibraForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LibraFormKeyDown);
            this.rentalPanel.ResumeLayout(false);
            this.rentalPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).EndInit();
            this.BooksContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.booksBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rentalPanel;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button borrowButton;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label searchWordLabel;
        private System.Windows.Forms.TextBox searchWordTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.DataGridView booksDataGridView;
        private System.Windows.Forms.Button deleteBookButton;
        private System.Windows.Forms.Button addBookButton;
        private System.Windows.Forms.ContextMenuStrip BooksContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 貸出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 返却ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 検索条件のクリアToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 書籍追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 書籍削除ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn bookIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn publisherDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource booksBindingSource;
        private Models.BooksDataSet booksDataSet;
    }
}

