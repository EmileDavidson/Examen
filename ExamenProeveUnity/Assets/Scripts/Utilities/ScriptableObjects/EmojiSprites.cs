using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EmojiSprites", menuName = "ScriptableObjects/EmojiSprites", order = 1)]
    public class EmojiSprites : ScriptableObject
    {
        [Header("Sprites")]
        public Sprite defaultSprite;
        public Sprite happySprite;
        public Sprite sadSprite;
        public Sprite angrySprite;
        public Sprite neutralSprite;

        private Dictionary<EmojiType, Sprite> _sprites;
        private Dictionary<EmojiType, int> _spriteScore;

        /// <summary>
        /// Initializes the sprite dictionaries if they are null.
        /// </summary>
        private void InitializeSpriteDict()
        {
            _sprites ??= new Dictionary<EmojiType, Sprite>()
            {
                { EmojiType.Happy, happySprite },
                { EmojiType.Angry, angrySprite },
                { EmojiType.Neutral, neutralSprite },
                { EmojiType.Sad, sadSprite },
            };

            _spriteScore ??= new Dictionary<EmojiType, int>()
            {
                { EmojiType.Happy, 2 },
                { EmojiType.Neutral, 0 },
                { EmojiType.Sad, -1 },
                { EmojiType.Angry, -2 },
            };
        }

        /// <summary>
        /// Gets sprite from the emoji type. If the sprite is null it will return the default sprite.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Sprite GetSprite(EmojiType type)
        {
            InitializeSpriteDict();

            _sprites.TryGetValue(type, out var sprite);
            sprite ??= GetDefaultSprite();

            return sprite;
        }

        /// <summary>
        /// returns 3 ints the max score you can get from any of the sprites
        /// the min score you can get from any of the sprites and the score you got from the sprite
        /// </summary>
        /// <returns></returns>
        public void GetScoreFromSprite(EmojiType emoji, out int max, out int min, out int givenScore)
        {
            InitializeSpriteDict();

            max = _spriteScore.Values.Max();
            min = _spriteScore.Values.Min();
            givenScore = _spriteScore[emoji];
        }

        /// <summary>
        /// Get previous emoji get the previous emoji towards being angry if you are angry you stay angry
        /// </summary>
        /// <param name="emoji"></param>
        /// <returns></returns>
        public EmojiType GetPrevious(EmojiType emoji)
        {
            InitializeSpriteDict();

            int index = _spriteScore.Keys.ToList().IndexOf(emoji);
            if (index == -1) return emoji;
            if (index >= _spriteScore.Keys.Count - 1) return EmojiType.Angry;
            return _spriteScore.Keys.ToList()[index + 1];
        }

        /// <summary>
        /// Runs GetPrevious multiple times to get the emoji that is the given amount of times previous
        /// </summary>
        /// <param name="emoji"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public EmojiType GetPrevious(EmojiType emoji, int times)
        {
            var setEmoji = emoji;
            for (int i = 0; i <= times; i++)
            {
                setEmoji = GetPrevious(setEmoji);
            }
            
            return setEmoji;
        }

        /// <summary>
        /// Get next emoji get the next emoji towards being happy if you are happy you stay happy
        /// </summary>
        /// <param name="emoji"></param>
        /// <returns></returns>
        public EmojiType GetNext(EmojiType emoji)
        {
            InitializeSpriteDict();

            int index = _spriteScore.Keys.ToList().IndexOf(emoji);
            if (index == -1) return emoji;
            if (index <= 0) return EmojiType.Happy;
            return _spriteScore.Keys.ToList()[index - 1];
        }

        /// <summary>
        /// Creates a default sprite if 
        /// </summary>
        /// <returns></returns>
        private Sprite GetDefaultSprite()
        {
            if(defaultSprite != null) return defaultSprite;
            defaultSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
            return defaultSprite;
        }
    }
}