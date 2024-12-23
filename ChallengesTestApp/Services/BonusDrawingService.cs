using System;

namespace ChallengesTestApp.Services;

public class BonusDrawingService : IBonusDrawingService
{
	private const float BonusPossibility = 0.5f; // from 0 to 1
	private static readonly Random Generator = new();

	//private readonly TimeSpan BonusCooldown = new(0, 1, 0, 0); // 1 hour timeout
	private static readonly TimeSpan BonusCooldown = new(0, 0, 0, 10); // I used this value for testing to avoid waiting for an hour

	private DateTime _bonusTime = DateTime.MinValue;

	public bool IsBonusDrawn()
	{
		var now = DateTime.Now;

		if (now - _bonusTime <= BonusCooldown ) return false;

		if (Generator.NextSingle() > BonusPossibility)
		{
			return false;
		}

		_bonusTime = now;

		return true;
	}
}