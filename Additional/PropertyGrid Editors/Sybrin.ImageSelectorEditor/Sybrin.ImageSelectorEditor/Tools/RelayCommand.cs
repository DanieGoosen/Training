using System;
using System.Windows.Input;

namespace Sybrin.ImageSelectorEditor {
    public class RelayCommand : ICommand {
        private readonly Func<object, bool> canExecute;
        private readonly Action<object> execute;
        /// <summary>
        ///     Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="executeAction">The action to execute.</param>
        public RelayCommand(Action<object> executeAction)
            : this(executeAction, null) {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="executeAction">The action to execute.</param>
        /// <param name="canExecuteFunction">The function that evaluates if the command can execute or not.</param>
        public RelayCommand(Action<object> executeAction, Func<object, bool> canExecuteFunction) {
            execute = executeAction;
            canExecute = canExecuteFunction;
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged() {
            if (CanExecuteChanged != null) {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        public virtual bool CanExecute(object parameter) {
            if (canExecute == null) {
                return true;
            }

            return canExecute(parameter);
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public virtual void Execute(object parameter) {
            execute(parameter);
        }
    }
}
