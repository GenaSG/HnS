using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataList<T> : ScriptableObject, IList<T>
{
    private IList<T> items = new List<T>();

    public T this[int index] { get => items[index]; set => items[index] = value; }

    public int Count => items.Count;

    public bool IsReadOnly => items.IsReadOnly;

    public void Add(T item) => items.Add(item);

    public void AddRange(IEnumerable<T> collection) => items.AddRange(collection);

    public void Clear() => items.Clear();

    public bool Contains(T item) => items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

    public int IndexOf(T item) => items.IndexOf(item);

    public void Insert(int index, T item) => items.Insert(index, item);

    public void Remove(T item) => items.Remove(item);

    public void RemoveAt(int index) => items.RemoveAt(index);

    public void RemoveAll(System.Predicate<T> match) => items.RemoveAll(match);

    IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

    bool ICollection<T>.Remove(T item) => items.Remove(item);

    public void Init(IList<T> list)
    {
        items = list;
    }

}

# if UNITY_EDITOR

[CustomEditor(typeof(GameObjectsDataList))]
class DecalMeshHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Debug"))
        {
            if (target is GameObjectsDataList)
            {
                Debug.Log("Items count = " + ((GameObjectsDataList)target).Count);
                for (int i = 0; i < ((GameObjectsDataList)target).Count; i++)
                {
                    Debug.Log(((GameObjectsDataList)target)[i]);
                }
            }
        }

    }
}
#endif

public static class IListExtensions
{
    public static void AddRange<T>(this IList<T> source, IEnumerable<T> newList)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (newList == null)
        {
            throw new ArgumentNullException(nameof(newList));
        }

        if (source is List<T> concreteList)
        {
            concreteList.AddRange(newList);
            return;
        }

        foreach (var element in newList)
        {
            source.Add(element);
        }
    }

    public static int RemoveAll<T>(this IList<T> source, Predicate<T> predicate)
    {
        if (source == null || predicate == null)
        {
            return -1;
        }

        if (source is List<T> concreteList)
        {
            return concreteList.RemoveAll(predicate);
        }

        int result = 0;

        for (int i = source.Count - 1; i >= 0; i--)
        {
            if (!predicate(source[i]))
            {
                continue;
            }

            ++result;
            source.RemoveAt(i);
        }

        return result;
    }
}
