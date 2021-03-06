﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Microsoft.Toolkit.Mvvm.Input.Wpf
{
    public class AsyncRelayCommand<T> : RelayCommand<T>
    {
        public event EventHandler Started;

        public event EventHandler Ended;

        public bool IsExecuting
        {
            get => isExecuting;
        }
        private bool isExecuting = false;

        public AsyncRelayCommand(Action<T> execute, Func<T, bool> canExecute) : base(execute, canExecute) { }

        public AsyncRelayCommand(Action<T> execute) : base(execute) { }

        public override bool CanExecute(object parameter)
        {
            return ((base.CanExecute(parameter)) && (!this.isExecuting));
        }

        public override void Execute(object parameter)
        {
            isExecuting = true;
            try
            {
                if (Started != null)
                {
                    Started(this, EventArgs.Empty);
                }

                Task task = Task.Factory.StartNew(
                    () =>
                    {
                        execute((T)parameter);
                    }
                );

                task.ContinueWith(
                    t =>
                    {
                        OnRunWorkerCompleted(EventArgs.Empty);
                    },
                    TaskScheduler.FromCurrentSynchronizationContext()
                );
            }
            catch (Exception ex)
            {
                OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            isExecuting = false;
            if (Ended != null)
            {
                Ended(this, e);
            }
        }
    }
}
