using System.ComponentModel.DataAnnotations;
using RockPaperScissorsAPI.BL.Utils;

namespace RockPaperScissorsAPI.BL.Models;

public class RpsHuman : RpsModel
{
    private RpsAction PlayAction(RpsAction action)
    {
        PlayerAction = action;

        return action;
    }

    [Key] public int Id { get; set; }
    public string PlayerName { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draw { get; set; }

    public RpsAction ChooseRock()
    {
        return PlayAction(RpsAction.Rock);
    }

    public RpsAction ChoosePaper()
    {
        return PlayAction(RpsAction.Paper);
    }

    public RpsAction ChooseScissors()
    {
        return PlayAction(RpsAction.Scissors);
    }
}