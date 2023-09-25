using System;
using UnityEngine;

public enum ConnectionPointType { In, Out }

[Serializable]
public class ConnectionPoint
{
    public Rect rect;

    public ConnectionPointType type;

    public Action<ConnectionPoint> OnClickConnectionPoint;

    //public Node node;
    public Rect nodeRect;
    public string nodeID; //Reference to the connected node's ID
#if UNITY_EDITOR
    public GUIStyle style;

    public ConnectionPoint(string id, Rect node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        this.nodeRect = node;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        nodeID = id;
        rect = new Rect(0, 0, 10f, 20f);
    }

    public ConnectionPoint(ConnectionPoint other) {
        nodeRect = other.rect;
        type = other.type;
        style = other.style;
        OnClickConnectionPoint = other.OnClickConnectionPoint;
        nodeID = other.nodeID;
        rect = new Rect(0, 0, 10f, 20f);
    }
#endif

    public void updateRect(Rect nodeRect)
    {
        this.nodeRect = nodeRect;
        Draw();
    }

    public void Draw()
    {
        rect.y = nodeRect.y + (nodeRect.height * 0.5f) - rect.height * 0.5f;

        switch (type)
        {
            case ConnectionPointType.In:
                rect.x = nodeRect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Out:
                rect.x = nodeRect.x + nodeRect.width - 8f;
                break;
        }

        if (GUI.Button(rect, "", style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }

}
