
namespace VisionCommon.Commands
{
    public interface ICommand
    {
        string CommandId { get; }
        string CommandName { get; }
    }
}
