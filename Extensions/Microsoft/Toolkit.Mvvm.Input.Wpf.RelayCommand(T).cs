using System;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Microsoft.Toolkit.Mvvm.Input.Wpf
{

    public class RelayCommand<T> : ICommand
    {
        protected Action<T> execute;

        private Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute)
        {
            this.execute = execute;
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanExecute(T parameter)
        {
            return this.canExecute?.Invoke(parameter) != false;
        }

        public virtual bool CanExecute(object parameter)
        {
            if (execute != null)
            {
                T tparm = (T)parameter;
                return canExecute(tparm);
            }
            if (execute != null)
            {
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(T parameter)
        {
            if (execute != null)
            {
                execute(parameter);
            }
        }

        public virtual void Execute(object parameter)
        {
            Execute((T)parameter!);
        }
    }
}
