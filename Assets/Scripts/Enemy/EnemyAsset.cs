using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animation;

        [Header("Игровые параметры")]
        public float moveSpeed = 1;
        public int hp = 15;
        public int armor = 0;
        public int score = 0;
        public float radius = 0.19f;
        public int damage = 1;
        public int gold = 1;
        public int mana = 1;
        public Enemy.ArmorType armorType;
    }
}