namespace Libra {
    public class LibraControl {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl() {
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public void OpenAddForm() {
            var wAddBookControl = new AddBookControl();
            wAddBookControl.ShowAddBookForm();
        }
    }
}
