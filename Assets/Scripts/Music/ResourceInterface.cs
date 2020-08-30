using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceInterface
{

    public static Object Load(string path)
    {
        return Resources.Load(path);
    }

    public static GameObject InstantiateGO(GameObject gameObject, Transform parent = null, bool inWorldSpace = true)
    {
        GameObject go = GameObject.Instantiate(gameObject, parent, inWorldSpace) as GameObject;
        if (go)
        {
            int pos = go.name.IndexOf("(Clone)");
            go.name = go.name.Remove(pos, go.name.Length - pos);
        }
        return go;
    }

    public static GameObject InstantiateGO(string path, Transform parent = null, bool inWorldSpace = true)
    {
        GameObject go = GameObject.Instantiate(Resources.Load(path), parent, inWorldSpace) as GameObject;
        if (go)
        {
            int pos = go.name.IndexOf("(Clone)");
            go.name = go.name.Remove(pos, go.name.Length - pos);
        }
        return go;
    }

    public static Sprite LoadSprite(string path)
    {
        Texture2D texture = (Texture2D)Load(path);
        if (!texture)
        {
            Debug.LogWarning("LoadSprite null");
            return null;
        }
        Rect rect = new Rect(0f, 0f, texture.width, texture.height);
        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }

    public static Sprite LoadSprite(Texture2D texture)
    {
        Rect rect = new Rect(0f, 0f, texture.width, texture.height);
        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }

    public static Texture2D LoadTexture(string path)
    {
        Texture2D texture = (Texture2D)Load(path);
        if (!texture)
        {
            Debug.LogError(path);
        }
        return texture;
    }

    public static AudioClip LoadAudio(string path)
    {
        AudioClip audio = (AudioClip)Load(path);
        if (!audio)
        {
            Debug.LogError(path);
        }
        return audio;
    }
}

