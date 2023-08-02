using System;
using System.Runtime.Serialization;

namespace Libra {
    [Serializable()]
    public class BookOperationException : Exception {
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BookOperationException() : base() {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vMessage"></param>
        public BookOperationException(string vMessage) : base(vMessage) {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vInnerException"></param>
        public BookOperationException(string vMessage, Exception vInnerException) : base(vMessage, vInnerException) {
        }

        /// <summary>
        /// 逆シリアル化コンストラクタ
        /// </summary>
        /// <param name="vInfo"></param>
        /// <param name="vContext"></param>
        protected BookOperationException(SerializationInfo vInfo, StreamingContext vContext)
            : base(vInfo, vContext) {
        }
        
        /// <summary>
        /// エラー種別
        /// </summary>
        public ErrorTypeEnum ErrorType { get; }

        /// <summary>
        /// 書籍名
        /// </summary>
        public string BookTitle { get; }

        /// <summary>
        /// 書籍操作エラー発生。
        /// エラーコードと操作中の書籍名を指定してください。
        /// </summary>
        /// <param name="vErrorType"></param>
        /// <param name="vBookTitle"></param>
        public BookOperationException(ErrorTypeEnum vErrorType, string vBookTitle) {
            this.ErrorType = vErrorType;
            this.BookTitle = vBookTitle;
        }
    }
}
