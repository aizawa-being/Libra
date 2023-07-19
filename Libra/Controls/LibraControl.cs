namespace Libra {
    public class LibraControl : ILibraControl {

        public LibraControl() {
        }
        
        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public int OpenAddForm() {
            IAddBookControl wAddBookControl = new AddBookControl();
            return wAddBookControl.ShowAddBookForm();
        }
    }
}
