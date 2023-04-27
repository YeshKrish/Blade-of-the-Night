using UnityEngine;

namespace BaldeOfNight
{
    public abstract class Controller : ScriptableObject
    {
        public Character character { get; set; }

        public abstract void Init();
        public abstract void OnCharacterUpdate();
        public abstract void OnCharacterFixedUpdate();
    }

}
