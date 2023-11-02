using RockPaperScissorsAPI.BL.Models;
using RockPaperScissorsAPI.BL.Utils;

namespace RockPaperScissorsAPI.BL.Interfaces;

public interface IRpsService
{
    void Rock();
    void Paper();
    void Scissors();
    /*Task<GameResult> GetPlayerStats(int id);*/
    Task<List<GameResult>> GetAllPlayerStats();
    Task<PlayerStats> GetPlayerStats(int id);
    void PlayGame(int playerId, RpsAction playerAction);
    void ResetStats(int playerId);
}