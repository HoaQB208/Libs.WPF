using System.Windows.Input;

#pragma warning disable CS8767 // Nullability
namespace Libs.WPF.MVVM
{
    public class Command : ICommand
    {
        private readonly Action _execute;
        private readonly Func<object, bool> _canExecute;
        private readonly TimeSpan _throttleTime = TimeSpan.FromSeconds(0.5);
        private DateTime _lastExecutionTime = DateTime.MinValue;

        public Command(Action execute, Func<object, bool> canExecute = null!)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)

        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (DateTime.Now - _lastExecutionTime > _throttleTime)
            {
                _lastExecutionTime = DateTime.Now;
                _execute();
            }
        }
    }


    public class Command<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly TimeSpan _throttleTime = TimeSpan.FromSeconds(0.3);
        private DateTime _lastExecutionTime = DateTime.MinValue;

        public Command(Action<T> execute, Func<T, bool> canExecute = null!)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (DateTime.Now - _lastExecutionTime > _throttleTime)
            {
                _lastExecutionTime = DateTime.Now;
                _execute((T)parameter);
            }
        }
    }
}
#pragma warning restore CS8767 // Nullability