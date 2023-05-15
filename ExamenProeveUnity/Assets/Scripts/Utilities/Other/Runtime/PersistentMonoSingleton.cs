namespace Utilities.Other.Runtime
{
    /// <summary>
    /// Uses the MonoSingleton class but also sets the object to DontDestroyOnLoad
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentMonoSingleton<T> : MonoSingleton<T> where T : PersistentMonoSingleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}