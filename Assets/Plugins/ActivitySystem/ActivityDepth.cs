using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ActivityDepth : MonoBehaviour
{
    private List<int> _depthList = new List<int>() { 100 };

    public int Request()
    {
        var maxValue = _depthList.Last();
        maxValue++;

        _depthList.Add(maxValue);
        return maxValue;
    }

    public void Release(int depth)
    {
        _depthList.Remove(depth);
    }
}
