using System;
using System.Diagnostics;
using System.Windows.Input;

namespace TestApplication.ViewModels.Common.Base
{
    /// <summary>
    /// コマンドの処理内容をデリゲートで定義できる <see cref="ICommand"/> インタフェースの実装を提供します。
    /// </summary>
    /// <typeparam name="T">
    /// コマンドで使用されるパラメータのデータ型。
    /// パラメータが利用されない場合は <see cref="object"/> 型を指定してください。
    /// </typeparam>
    [DebuggerStepThrough]
    public class DelegateCommand<T> : ICommand, ICanExecuteChangedRaisable
    {
        #region Fields

        /// <summary>
        /// コマンドが呼び出されたときに実行する処理。
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// コマンドを実行できるかどうかを判断する処理。
        /// </summary>
        private readonly Predicate<T> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// <see cref="Execute"/> の処理を指定して、<see cref="DelegateCommand{T}"/> インスタンスを新しく作成します。
        /// </summary>
        /// <param name="execute">コマンドが呼び出されたときに実行する処理。</param>
        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// <see cref="Execute"/>、<see cref="CanExecute"/> それぞれの処理を指定して、<see cref="DelegateCommand{T}"/> インスタンスを生成します。
        /// </summary>
        /// <param name="execute">コマンドが呼び出されたときに実行する処理。</param>
        /// <param name="canExecute">コマンドを実行できるかどうかを判断する処理。</param>
        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// コマンドを実行するかどうかに影響するような変更があった場合に発生します。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 現在の状態でこのコマンドを実行できるかどうかを判断します。
        /// </summary>
        /// <param name="parameter">
        /// コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを <c>null</c> 参照 (Visual Basic では <c>Nothing</c>) に設定できます。
        /// </param>
        /// <returns>
        /// このコマンドを実行できる場合は <c>true</c>。それ以外の場合は <c>false</c>。
        /// </returns>
        public bool CanExecute(object parameter)
        {
            var result = true;
            var action = _canExecute;
            if (action != null)
            {
                var p = SafeCast(parameter);
                result = action(p);
            }
            return result;
        }

        /// <summary>
        /// コマンドに紐づいた処理を実行します。
        /// </summary>
        /// <param name="parameter">
        /// コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを <c>null</c> 参照 (Visual Basic では <c>Nothing</c>) に設定できます。
        /// </param>
        public void Execute(object parameter)
        {
            var p = SafeCast(parameter);
            _execute(p);
        }

        #endregion

        #region Methods

        /// <summary>
        /// <see cref="CanExecute"/> から返却される値が変更される可能性がある場合に、このメソッドを呼び出してください。
        /// </summary>
        void ICanExecuteChangedRaisable.RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        /// <summary>
        /// <see cref="CanExecute"/> から返却される値が変更される可能性がある場合に、このメソッドを呼び出してください。
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var h = this.CanExecuteChanged;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 値を安全に型変換します。
        /// </summary>
        /// <param name="value">型変換する元の値。</param>
        /// <returns>
        /// 型変換された値。型変換できない場合は、型変換先の型のデフォルト値。
        /// </returns>
        protected T SafeCast(object value)
        {
            return (value is T) ? (T)value : default(T);
        }

        #endregion
    }

    /// <summary>
    /// コマンドを実行するかどうかに影響するような変更があった際の通知を行う機能を定義します。
    /// </summary>
    public interface ICanExecuteChangedRaisable : ICommand
    {
        /// <summary>
        /// <see cref="ICommand.CanExecute"/> から返却される値が変更される可能性がある場合に、このメソッドを呼び出してください。
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    /// <summary>
    /// <see cref="ICanExecuteChangedRaisable"/> に対する <see cref="ICommand"/> を拡張する一連のメソッド群を提供します。
    /// </summary>
    [DebuggerStepThrough]
    public static class CommandCanExecuteChangedRaisableExtension
    {
        /// <summary>
        /// <see cref="ICommand.CanExecute"/> から返却される値が変更される可能性がある場合に、このメソッドを呼び出してください。
        /// </summary>
        /// <param name="source">コマンドインスタンス。</param>
        public static void RaiseCanExecuteChanged(this ICommand source)
        {
            var o = source as ICanExecuteChangedRaisable;
            if (o != null)
            {
                o.RaiseCanExecuteChanged();
            }
        }
    }
}
