﻿using System;
using System.Windows.Input;

namespace PropertyManagement.Domain
{
    public class CommandImplementation : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public CommandImplementation(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (x => true);
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
