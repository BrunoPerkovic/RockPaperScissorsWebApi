﻿using System.ComponentModel.DataAnnotations;

namespace RockPaperScissorsAPI.BL.Models;

public class PlayerStats
{
    [Key]
    public int Id { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }
}