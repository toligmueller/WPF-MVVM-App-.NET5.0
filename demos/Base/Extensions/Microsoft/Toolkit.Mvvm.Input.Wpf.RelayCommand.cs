using System;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Microsoft.Toolkit.Mvvm.Input.Wpf
{
    public class RelayCommand : ICommand
    {
        protected Action execute;

        private Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                bool result = canExecute.Invoke();
                return result;
            }
        }

        public virtual void Execute(object parameter)
        {
            execute.Invoke();
        }
    }
}
