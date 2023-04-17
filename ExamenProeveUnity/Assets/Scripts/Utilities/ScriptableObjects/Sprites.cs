using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sprites", menuName = "ScriptableObjects/ItemSprites", order = 1)]
    public class Sprites : ScriptableObject
    {
        public Sprite defaultSprite;
        public Sprite happySprite;
        public Sprite sadSprite;
        public Sprite angrySprite;
        public Sprite neutralSprite;

        private Dictionary<SpriteType, Sprite> _sprites;

        private void InitializeSpriteDict()
        {
            _sprites ??= new Dictionary<SpriteType, Sprite>()
            {
                { SpriteType.Happy, happySprite },
                { SpriteType.Angry, angrySprite },
                { SpriteType.Neutral, neutralSprite },
                { SpriteType.Sad, sadSprite },
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
    }
}