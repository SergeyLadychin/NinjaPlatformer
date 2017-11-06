using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    private static PickUpManager instance;

    private Dictionary<string, int> savedPickUps;
    private Dictionary<string, int> pickUps;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Init();

        DontDestroyOnLoad(instance);
    }

    public static PickUpManager GetInstance()
    {
        return instance;
    }

    public void Init()
    {
        pickUps = new Dictionary<string, int>();
        savedPickUps = new Dictionary<string, int>();
    }

    public int GetCount(string pickUp)
    {
        int count;
        if (pickUps.TryGetValue(pickUp, out count))
        {
            return count;
        }
        return 0;
    }

    public void Add(string pickUp, int count)
    {
        if (pickUps.ContainsKey(pickUp))
        {
            pickUps[pickUp] += count;
        }
        else
        {
            pickUps[pickUp] = count;
        }
    }

    public void Remove(string pickUp, int count)
    {
        pickUps[pickUp] -= count;
    }

    public void Save()
    {
        savedPickUps = new Dictionary<string, int>(pickUps);
    }

    public void Restore()
    {
        pickUps = new Dictionary<string, int>(savedPickUps);
    }

    public void ResetCounts()
    {
        savedPickUps.Clear();
        pickUps.Clear();
    }
}
