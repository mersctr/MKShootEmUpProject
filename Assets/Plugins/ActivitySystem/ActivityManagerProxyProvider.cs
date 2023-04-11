using Zenject;

public class ActivityManagerProxyProvider : IInitializable, ILateDisposable
{
    [Inject] private ActivityManagerProxy _activityManagerProxy;
    [Inject] private ActivityManager _activityManager;

    public void Initialize()
    {
        _activityManagerProxy.Add(_activityManager);
    }

    public void LateDispose()
    {
        _activityManagerProxy.Remove(_activityManager);
    }
}