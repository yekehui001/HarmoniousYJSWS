using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HarmoniousYJSWS
{
    class DelegateCommand : ICommand
    {
        public DelegateCommand(Action<object> action)
        {
            this.Action = action;
        }
        public Action<object> Action { get; set; }
        public event EventHandler CanExecuteChanged;
        private bool isExecuting { get; set; }
        public bool CanExecute(object parameter)
        {
            return Action != null && isExecuting == false;
        }

        public void Execute(object parameter)
        {
            isExecuting = true;
            Action?.Invoke(parameter);
            isExecuting = false;
        }
    }
}
