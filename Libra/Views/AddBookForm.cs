using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// 書籍追加画面
    /// </summary>
    public partial class AddBookForm : Form {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AddBookForm() {
            InitializeComponent();
        }

        /// <summary>
        /// 書籍情報取得ボタン押下のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetBookInfoButtonClick(object sender, EventArgs e) {

        }

        /// <summary>
        /// 追加ボタン押下のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButtonClick(object sender, EventArgs e) {

        }

        /// <summary>
        /// キャンセルボタン押下のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButtonClick(object sender, EventArgs e) {
            
        }

        /// <summary>
        /// ISBNコード入力欄のキー押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsbnTextBox_KeyDown(object sender, KeyEventArgs e) {
            // クリップボード内に半角数字以外が含まれている場合、ペースト不可。
            if (e.KeyData == (Keys.Control | Keys.V)) {
                string wClipboardText = Clipboard.GetText();
                if (!Regex.IsMatch(wClipboardText, @"^[0-9]+$")) {
                    e.SuppressKeyPress = true;
                }
            }
        }

        /// <summary>
        /// ISBNコード入力欄のキー押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsbnTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            // バックスペースは利用可
            if (e.KeyChar == '\b') {
                return;
            }

            // 半角数字以外の押下時はイベントをキャンセルする
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        /// <summary>
        /// フォームロード時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBookForm_Load(object sender, EventArgs e) {
            // 右クリックメニューは利用不可
            this.isbnTextBox.ContextMenu = new ContextMenu();
        }
    }
}
