using System;
using UnityEngine;

namespace Amheklerior.Core.Command {

    public static class GlobalCommandExecutor {

        public static void Execute(ICommand command) => Executor.Execute(command);

        public static void Execute(Action perform) => Executor.Execute(new Command(perform));

        public static void Execute(Action perform, Action undo) => Executor.Execute(new Command(perform, undo));

        public static bool CanUndo() => Executor.CanUndo;

        public static void Undo() => Executor.Undo();

        public static void Clear() => Executor.Clear();

        #region Internals 

        internal static string EXECUTOR_NAME = "Command Executor";

        private static CommandExecutorComponent _cmdExecutor;

        private static CommandExecutorComponent Executor => _cmdExecutor ?? (_cmdExecutor = GetOrCreateExecutor());

        private static CommandExecutorComponent GetOrCreateExecutor() {
            return GameObject.Find(EXECUTOR_NAME).GetComponent<CommandExecutorComponent>() ??
                new GameObject(EXECUTOR_NAME).AddComponent<CommandExecutorComponent>();
        }

        #endregion

    }
}