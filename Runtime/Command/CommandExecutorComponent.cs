using Amheklerior.Core.Common;

namespace Amheklerior.Core.Command {

    public class CommandExecutorComponent : RichMonoBehaviour {

        private ICommandExecutor _executor;

        private void Awake() => _executor = GetExecutorInstance();
        
        public void Execute(ICommand command) => _executor.Execute(command);

        public void Undo() {
            if (!_executor.CanUndo) return;
            _executor.Undo();
        }

        protected virtual ICommandExecutor GetExecutorInstance() {
#if UNITY_EDITOR
            _description = _description ?? $"CommandExecutor: {name}";
            return _debugMode ? new DebuggableCommandExecutor() : new CommandExecutor();
#else
            return new CommandExecutor();
#endif
        }

    }
}