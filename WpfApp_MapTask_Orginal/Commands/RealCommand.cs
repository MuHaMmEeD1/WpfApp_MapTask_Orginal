using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp_MapTask_Orginal.Commands
{
    public class RealCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {

            add
            {

                CommandManager.RequerySuggested += value;
            }

            remove
            {

                CommandManager.RequerySuggested -= value;
            }



        }


        private readonly Predicate<object?>? _predicate;
        private readonly Action<object?>? _execute;

        public RealCommand(Action<object?>? execute, Predicate<object?>? predicate)
        {
            _predicate = predicate;
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return _predicate!.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
