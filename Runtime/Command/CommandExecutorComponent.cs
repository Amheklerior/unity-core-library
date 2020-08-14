using Amheklerior.Core.Common;

namespace Amheklerior.Core.Command {

    public class CommandExecutorComponent : RichMonoBehaviour, ICommandExecutor {
        
        #region ICommandExecutor interface forwarding 

        public int Count => _executor.Count;
        public bool CanUndo => _executor.CanUndo;
        public ICommand LastExcecuted => _executor.LastExcecuted;
        public void Execute(ICommand command) => _executor.Execute(command);
        public void Undo() => _executor.Undo();
        public void Clear() => _executor.Clear();

        #endregion

        #region Internals

        private ICommandExecutor _executor;

        private void Awake() {
            name = GlobalCommandExecutor.EXECUTOR_NAME;
            _executor = GetExecutorInstance();
        }

        protected virtual ICommandExecutor GetExecutorInstance() {
#if UNITY_EDITOR
            _description = _description ?? $"CommandExecutor: {name}";
            return _debugMode ? new DebuggableCommandExecutor() : new CommandExecutor();
#else
            return new CommandExecutor();
#endif
        }

        #endregion

    }
}