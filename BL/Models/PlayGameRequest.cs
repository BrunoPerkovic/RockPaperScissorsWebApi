using RockPaperScissorsAPI.BL.Utils;

namespace RockPaperScissorsAPI.BL.Models;

public class PlayGameRequest
{
    public int PlayerId { get; set; }
    public RpsAction PlayerAction { get; set; }
}
