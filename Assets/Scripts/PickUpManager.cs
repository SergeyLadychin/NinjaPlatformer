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

    public static int GetCount(string pickUp)
    {
        if (instance == null)
            return 0;

        int count;
        if (instance.pickUps.TryGetValue(pickUp, out count))
        {
            return count;
        }
        return 0;
    }

    public static void Add(string pickUp, int count)
    {
        if (instance.pickUps.ContainsKey(pickUp))
        {
            instance.pickUps[pickUp] += count;
        }
        else
        {
            instance.pickUps[pickUp] = count;
        }
    }

    public static void Remove(string pickUp, int count)
    {
        instance.pickUps[pickUp] -= count;
    }

    public static void Save()
    {
        instance.savedPickUps = new Dictionary<string, int>(instance.pickUps);
    }

    public static void Restore()
    {
        instance.pickUps = new Dictionary<string, int>(instance.savedPickUps);
    }

    public static void ResetCounts()
    {
        instance.savedPickUps.Clear();
        instance.pickUps.Clear();
    }
}
