using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;
using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EmojiSprites", menuName = "ScriptableObjects/EmojiSprites", order = 1)]
    public class EmojiSprites : ScriptableObject
    {
        public Sprite defaultSprite;
        public Sprite happySprite;
        public Sprite sadSprite;
        public Sprite angrySprite;
        public Sprite neutralSprite;

        private Dictionary<SpriteType, Sprite> _sprites;
        private Dictionary<SpriteType, int> _spriteScore;

        private void InitializeSpriteDict()
        {
            _sprites ??= new Dictionary<SpriteType, Sprite>()
            {
                { SpriteType.Happy, happySprite },
                { SpriteType.Angry, angrySprite },
                { SpriteType.Neutral, neutralSprite },
                { SpriteType.Sad, sadSprite },
            };

            _spriteScore ??= new Dictionary<SpriteType, int>()
            {
                { SpriteType.Happy, 2 },
                { SpriteType.Neutral, 0 },
                { SpriteType.Sad, -1 },
                { SpriteType.Angry, -2 },
            };
        }

        public Sprite GetSprite(SpriteType type)
        {
            InitializeSpriteDict();

            _sprites.TryGetValue(type, out var sprite);
            sprite ??= defaultSprite;
            sprite ??= Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

            return sprite;
        }

        /// <summary>
        /// returns 3 ints the max score you can get from any of the sprites
        /// the min score you can get from any of the sprites and the score you got from the sprite
        /// </summary>
        /// <returns></returns>
        public void GetScoreFromSprite(SpriteType sprite, out int max, out int min, out int givenScore)
        {
            InitializeSpriteDict();

            max = _spriteScore.Values.Max();
            min = _spriteScore.Values.Min();
            givenScore = _spriteScore[sprite];
        }
    }
}