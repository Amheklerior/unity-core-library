using System.Collections.Generic;
using System.Diagnostics;

namespace Amheklerior.Core.Command {
    
    internal class CommandStack : Stack<ICommand> { }

    /**
     * Maintains a stack of the last excecuted commands. 
     * The stack is cleared as soon as a not reversable command is performed.
     * there is no limit on how many command execution are traced.
     * 
     * You have to check for CanUndo before performing Undo
     */
    public class CommandExecutor : ICommandExecutor {
        
        private readonly CommandStack _cmdStack = new CommandStack();
        
        public bool CanUndo => _cmdStack.Count > 0;

        public int Count => _cmdStack.Count;

        public virtual ICommand LastExcecuted => _cmdStack.Peek();

        public virtual void Execute(ICommand cmd) {
            cmd.Perform();

            if (cmd.Reversible) _cmdStack.Push(cmd);
            else _cmdStack.Clear();
        }

        public virtual void Undo() => _cmdStack.Pop().Undo();

    }


    #region Debuggable version

    public class DebuggableCommandExecutor : CommandExecutor {

        public override ICommand LastExcecuted {
            get {
                var cmd = base.LastExcecuted;
                Debug.Write($"The last executed command is: {cmd}");
                return cmd;
            }
        }

        public override void Execute(ICommand cmd) {
            base.Execute(cmd);
            Debug.Write($"The {cmd} command has been executed. The stack now has {Count} commands.");
        }

        public override void Undo() {
            var cmd = base.LastExcecuted;
            base.Undo();
            Debug.Write($"The execution of the {cmd} command has been reverted. The stack now has {Count} commands.");
        }

    }

    #endregion

}