using System;

public static class GameEvents
{
    public static event Action EnemyKilledByBullet;
    public static event Action<int> PlayerDamaged;
    public static event Action GameWon;
    public static event Action GameLost;

    public static void RaiseEnemyKilledByBullet() => EnemyKilledByBullet?.Invoke();
    public static void RaisePlayerDamaged(int amount) => PlayerDamaged?.Invoke(amount);
    public static void RaiseGameWon() => GameWon?.Invoke();
    public static void RaiseGameLost() => GameLost?.Invoke();
}
