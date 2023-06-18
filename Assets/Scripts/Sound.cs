namespace TowerDefence
{
    public enum Sound
    {
        BGM = 0,
        Arrrow = 1,
        ArrowHit = 2, 
        EnemyDie = 3,
        EnemyWin = 4,
        PlayerDie = 5,
        PlayerWin = 6
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}