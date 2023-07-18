namespace Libra {
    /// <summary>
    /// 書籍追加用コントローラです。
    /// </summary>
    public class AddBookControl : IAddBookControl {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AddBookControl() {
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public void ShowAddBookForm() {
            using (var wAddBookForm = new AddBookForm()) {
                wAddBookForm.ShowDialog();
            }
        }
    }
}
