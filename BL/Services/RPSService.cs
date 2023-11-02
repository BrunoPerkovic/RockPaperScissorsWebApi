using Microsoft.EntityFrameworkCore;
using RockPaperScissorsAPI.BL.Config;
using RockPaperScissorsAPI.BL.Interfaces;
using RockPaperScissorsAPI.BL.Models;
using RockPaperScissorsAPI.BL.Utils;

namespace RockPaperScissorsAPI.BL.Services;

public class RpsService : IRpsService
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<RpsService> _logger;

    private List<RpsModel> CreatePlayers()
    {
        var human = new RpsHuman();
        var computer = new RpsComputer();

        return new List<RpsModel> { human, computer };
    }

    private bool PlayerWin(RpsAction humanAction, RpsAction computerAction)
    {
        switch (humanAction)
        {
            case RpsAction.Rock when computerAction == RpsAction.Scissors:
            case RpsAction.Paper when computerAction == RpsAction.Rock:
            case RpsAction.Scissors when computerAction == RpsAction.Paper: return true;
            default: return false;
        }
    }

    private bool PlayerLose(RpsAction humanAction, RpsAction computerAction)
    {
        switch (humanAction)
        {
            case RpsAction.Scissors when computerAction == RpsAction.Rock:
            case RpsAction.Paper when computerAction == RpsAction.Scissors:
            case RpsAction.Rock when computerAction == RpsAction.Paper: return true;
            default: return false;
        }
    }

    private bool PlayerDraw(RpsAction humanAction, RpsAction computerAction)
    {
        switch (humanAction)
        {
            case RpsAction.Rock when computerAction == RpsAction.Rock:
            case RpsAction.Paper when computerAction == RpsAction.Paper:
            case RpsAction.Scissors when computerAction == RpsAction.Scissors: return true;
            default: return false;
        }
    }

    private void PlayedActions(RpsAction humanAction, RpsAction computerAction)
    {
        Console.WriteLine($"Player played {humanAction.ToString()}");
        Console.WriteLine($"Computer played {computerAction.ToString()}");
    }

    public RpsService(AppDbContext appDbContext, ILogger<RpsService> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public void Rock()
    {
        var players = CreatePlayers();
        var human = players[0] as RpsHuman;
        var computer = players[1] as RpsComputer;

        var humanAction = human.ChooseRock();
        var computerAction = computer.GetRandomAction();

        if (PlayerWin(humanAction, computerAction))
        {
            human.Wins++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Human wins");
            _appDbContext.SaveChanges();
        }

        if (PlayerDraw(humanAction, computerAction))
        {
            human.Draw++;
        }
        else
        {
            human.Losses++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Loss");
            _appDbContext.SaveChanges();
        }

        _appDbContext.GameResults.Add(new GameResult
        {
            PlayerId = human.Id,
            Wins = human.Wins,
            Losses = human.Losses,
            Draws = human.Draw,
        });
        _appDbContext.SaveChanges();
    }

    public void Paper()
    {
        var players = CreatePlayers();
        var human = players[0] as RpsHuman;
        var computer = players[1] as RpsComputer;

        var humanAction = human.ChoosePaper();
        var computerAction = computer.GetRandomAction();

        if (PlayerWin(humanAction, computerAction))
        {
            human.Wins++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Human wins");
        }
        else if (human.PlayerAction == computerAction)
        {
            human.Draw++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Draw");
        }
        else
        {
            human.Losses++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Loss");
        }

        _appDbContext.GameResults.Add(new GameResult
        {
            PlayerId = human.Id,
            Wins = human.Wins,
            Losses = human.Losses,
            Draws = human.Draw,
        });
        _appDbContext.SaveChanges();
    }

    public void Scissors()
    {
        var players = CreatePlayers();
        var human = players[0] as RpsHuman;
        var computer = players[1] as RpsComputer;

        var humanAction = human.ChooseScissors();
        var computerAction = computer.GetRandomAction();

        if (PlayerWin(humanAction, computerAction))
        {
            human.Wins++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Human wins");
        }
        else if (human.PlayerAction == computerAction)
        {
            human.Draw++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Draw");
        }
        else
        {
            human.Losses++;
            _logger.LogInformation($"Computer played {computerAction.ToString()}");
            _logger.LogInformation("Loss");
        }

        _appDbContext.GameResults.Add(new GameResult
        {
            PlayerId = human.Id,
            Wins = human.Wins,
            Losses = human.Losses,
            Draws = human.Draw,
        });
        _appDbContext.SaveChanges();
    }


    /*public async Task<GameResult> GetPlayerStats(int id)
    {
        var gameResult = await _appDbContext.GameResults
            .Where(gr => gr.PlayerId == id)
            .FirstOrDefaultAsync();

        var playerStats = new GameResult
        {
            PlayerId = gameResult.PlayerId,
            Wins = gameResult.Wins,
            Losses = gameResult.Losses,
            Draws = gameResult.Draws,
        };

        _logger.LogInformation(
            $"Player stats: Wins: {playerStats.Wins} Losses:{playerStats.Losses} Draws:{playerStats.Draws}");

        return playerStats;
    }
    */

    public async Task<List<GameResult>> GetAllPlayerStats()
    {
        var gameResults = await _appDbContext.GameResults.ToListAsync();

        var playerStats = new List<GameResult>();

        foreach (var gameResult in gameResults)
        {
            var result = new GameResult
            {
                PlayerId = gameResult.PlayerId,
                Wins = gameResult.Wins,
                Losses = gameResult.Losses,
                Draws = gameResult.Draws,
            };
            playerStats.Add(result);
            _logger.LogInformation(
                $"Player id: {result.PlayerId} stats: Wins: {result.Wins} Losses:{result.Losses} Draws:{result.Draws}");
        }

        _logger.LogInformation(
            $"Player stats: Wins: {playerStats[0].Wins} Losses:{playerStats[0].Losses} Draws:{playerStats[0].Draws}");
        return playerStats;
    }


    public void PlayGame(int playerId, RpsAction humanAction)
    {
        var gameResult = _appDbContext.GameResults.FirstOrDefault(gr => gr.Id == playerId);

        if (gameResult == null)
        {
            gameResult = new GameResult { Id = playerId };
            _appDbContext.GameResults.Add(gameResult);
        }

        var players = CreatePlayers();
        var computer = players[1] as RpsComputer;
        var computerAction = computer.GetRandomAction();

        PlayedActions(humanAction, computerAction);

        if (PlayerWin(humanAction, computerAction))
        {
            Console.WriteLine("Human wins");
            gameResult.Wins++;
        }

        if (PlayerLose(humanAction, computerAction))
        {
            Console.WriteLine("Human loses");
            gameResult.Losses++;
        }

        if (PlayerDraw(humanAction, computerAction))
        {
            Console.WriteLine("Draw");
            gameResult.Draws++;
        }

        _appDbContext.SaveChanges();
    }

    public async void ResetStats(int playerId)
    {
        var gameResult = await _appDbContext.GameResults.Where(gr => gr.PlayerId == playerId)
            .ToListAsync();

        foreach (var game in gameResult)
        {
            Console.WriteLine($"Wins before reset: {game.Wins}");
            Console.WriteLine($"Losses before reset: {game.Losses}");
            Console.WriteLine($"Draws before reset: {game.Draws}");
            game.Wins = 0;
            game.Losses = 0;
            game.Draws = 0;
            Console.WriteLine($"Wins after reset: {game.Wins}");
            Console.WriteLine($"Losses after reset: {game.Losses}");
            Console.WriteLine($"Draws after reset: {game.Draws}");
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task<PlayerStats> GetPlayerStats(int playerId)
    {
        var gameResult = await _appDbContext.GameResults.Where(gr => gr.PlayerId == playerId)
            .ToListAsync();

        if (gameResult == null)
        {
            throw new Exception($"No game result found for player id {playerId}");
        }

        var playerWins = gameResult.Sum(gr => gr.Wins);
        var playerLosses = gameResult.Sum(gr => gr.Losses);
        var playerDraws = gameResult.Sum(gr => gr.Draws);

        Console.WriteLine(
            $"Player with id {playerId} has {playerWins} wins, {playerLosses} losses and {playerDraws} draws");

        return new PlayerStats
        {
            Id = playerId,
            Wins = playerWins,
            Losses = playerLosses,
            Draws = playerDraws
        };
    }
}