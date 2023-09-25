using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Connection {

    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;
    public Action<Connection> OnClickRemoveConnection;

    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        this.OnClickRemoveConnection = OnClickRemoveConnection;
    }

    public Connection(Connection c)
    {
        inPoint = new ConnectionPoint(c.inPoint);
        outPoint = new ConnectionPoint(c.outPoint);
        OnClickRemoveConnection = c.OnClickRemoveConnection;
    }

    public void Draw()
    {
        if (inPoint != null && outPoint != null)
        {

            Handles.DrawBezier(
                inPoint.rect.center, //Start position
                outPoint.rect.center, //End position
                inPoint.rect.center + Vector2.left * 50f, //Start tangent
                outPoint.rect.center - Vector2.left * 50f, //End tangent
                Color.white,
                null,
                2f
                );

            if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8,
                Handles.RectangleHandleCap))
            {

                if (OnClickRemoveConnection != null)
                {
                    OnClickRemoveConnection(this);
                }
            }

        }
    }
}
