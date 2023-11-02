using RockPaperScissorsAPI.BL.Utils;

namespace RockPaperScissorsAPI.BL.Models;

public class RpsComputer : RpsModel
{
    private readonly Random _random = new();
    
    public RpsAction GetRandomAction()
    {
        var actions = Enum.GetValues<RpsAction>();
        var index = _random.Next(actions.Length);
        return actions[index];
    }
}