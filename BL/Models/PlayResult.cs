using RockPaperScissorsAPI.BL.Utils;

namespace RockPaperScissorsAPI.BL.Models;

public class PlayResult
{
    public RpsAction PlayerAction { get; set; }
    public RpsAction ComputerAction { get; set; }
    public string Result { get; set; }
}