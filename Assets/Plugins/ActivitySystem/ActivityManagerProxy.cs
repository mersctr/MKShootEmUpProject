using System.Collections.Generic;

public class ActivityManagerProxy
{
    public ActivityManager ActivityManager => _activityManagers[0];
    private List<ActivityManager> _activityManagers = new List<ActivityManager>(2);

    public void Add(ActivityManager activityManager)
    {
        _activityManagers.Add(activityManager);
    }
    
    public void Remove(ActivityManager activityManager)
    {
        _activityManagers.Remove(activityManager);
    }
}