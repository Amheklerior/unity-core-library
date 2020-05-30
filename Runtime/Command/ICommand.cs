
namespace Amheklerior.Core.Command {

    public interface ICommand {
        bool Reversible { get; }
        void Perform();
        void Undo();
    }

}
