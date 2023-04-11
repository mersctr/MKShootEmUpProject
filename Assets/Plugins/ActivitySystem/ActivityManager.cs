using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class ActivityManager : MonoBehaviour
{
    [Inject] private DiContainer _container = null;

    private List<Activity> _stack = new List<Activity>();

    public void StartCreatedActivity(Activity activity)
    {
        if (_stack.Contains(activity))
            return;

        Activity current = GetTop();
        if (current != null)
            current.ActPause();

        _stack.Add(activity);
        activity.ActStart();
        activity.ActResume();
    }

    public void FinishCreatedActivity(Activity activity)
    {
        if (_stack.Contains(activity) == false)
            return;

        bool isTop = activity == GetTop();

        if (isTop)
            activity.ActPause();

        activity.ActFinish();
        _stack.Remove(activity);

        if (isTop && HasActivity())
        {
            Activity current = GetTop();
            current.ActResume(); // Activity form stack was already created, we only start this one.
        }
    }

    public Activity Create(string resourcePath)
    {
        Activity activity = PreCreate(resourcePath);
        StartCreatedActivity(activity);
        return activity;
    }

    public T Create<T>(string resourcePath) where T : MonoBehaviour
    {
        Activity activity = PreCreate(resourcePath);
        StartCreatedActivity(activity);
        return activity.GetComponent<T>();
    }

    public Activity PreCreate(string resourcePath)
    {
        GameObject activityPrefab = Resources.Load<GameObject>(resourcePath);
        Assert.IsNotNull(activityPrefab, "Activity not found: " + resourcePath);

        // Disable so callbacks will not get triggered.
        bool isActiveBackup = activityPrefab.activeSelf;
        activityPrefab.SetActive(false);

        GameObject activityGameObject = Instantiate(activityPrefab, transform);
        activityGameObject.name = activityPrefab.name;
        SetAsLastChild(activityGameObject.transform);

        // Restore
        activityPrefab.SetActive(isActiveBackup);

        Activity activity = activityGameObject.GetComponent<Activity>();
        Assert.IsNotNull(activity, "Activity script not attached: " + resourcePath);

        // DI
        DiContainer diContainer = _container.CreateSubContainer();
        diContainer.Bind<Activity>().FromInstance(activity);
        diContainer.InjectGameObject(activity.gameObject);

        return activity;
    }

    public void UpdateFadedActivityTranslucency(Activity activity)
    {
        // When activity finishes fade in, we can make activities below disabled.
        if (activity == GetTop() && activity.IsTranslucent == false)
        {
            foreach (var item in _stack)
                if (item != activity)
                    item.ActHide();
        }
    }

    public bool HasActivity()
    {
        return _stack.Count > 0;
    }

    public Activity GetTop()
    {
        if (_stack.Count == 0)
            return null;

        int index = _stack.Count - 1;
        Activity activity = _stack[index];
        return activity;
    }

    public bool Contains(Activity activity)
    {
        return _stack.Contains(activity);
    }

    private void Update()
    {
        if (GetBackButtonDown())
        {
            Activity current = GetTop();

            if (current != null)
                current.Back();
        }
    }

    private bool GetBackButtonDown()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
    
    public void SetAsLastChild(Transform activityTransform)
    {
        activityTransform.transform.SetParent(transform);
        activityTransform.transform.SetAsLastSibling();
    }
}
