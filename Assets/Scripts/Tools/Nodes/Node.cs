using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Node
{

    #region structs

    public enum Alignment
	{
        LeftAligned,
        RightAligned
	}

    [Serializable]
    public struct Camera
    {
        //public GameObject targetToFollow;
        public string targetToFollow;
        public float followTime;
    }
    #endregion

    public string id; //Internal ID

    public Rect rect;
    public bool isDragged;
    public bool isSelected;
    public bool isRoot;

    //Traversal goes from left to right! We can change it if we need to but this is default.
    [SerializeField]
    public string prevNodeID;
    public Node previousNode;

    [SerializeField]
    public string nextNodeID;
    public Node nextNode;

    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;
#if UNITY_EDITOR
    public GUIStyle style;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;
#endif

    public Action<Node> OnRemoveNode;
    public Action<Node> OnNewRoot;



#if UNITY_EDITOR
    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, Action<Node> OnClickChangeRoot)
    {
        AssignInternalID();
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(id, rect, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(id, rect, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
        OnNewRoot = OnClickChangeRoot;
        isRoot = false;
    }

    public Node(string id, Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, Action<Node> OnClickChangeRoot)
    {
        this.id = id;
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(id, rect, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(id, rect, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
        OnNewRoot = OnClickChangeRoot;
        isRoot = false;
    }

    public Node(Node other) {
        id = other.id;
        rect = other.rect;
        style = other.style;
        inPoint = new ConnectionPoint(id, rect, ConnectionPointType.In, other.inPoint.style, other.inPoint.OnClickConnectionPoint);
        outPoint = new ConnectionPoint(id, rect, ConnectionPointType.Out, other.outPoint.style, other.outPoint.OnClickConnectionPoint);
        defaultNodeStyle = other.style;
        selectedNodeStyle = other.selectedNodeStyle;
        OnRemoveNode = other.OnRemoveNode;
        OnNewRoot = other.OnRemoveNode;
        isRoot = other.isRoot;
        prevNodeID = other.prevNodeID;
        nextNodeID = other.nextNodeID;
	}

#endif

    protected void AssignInternalID()
    {
        if (string.IsNullOrEmpty(id))
            id = Guid.NewGuid().ToString();
    }

    public virtual void Drag(Vector2 delta)
    {
        rect.position += delta;
        inPoint.updateRect(rect);
        outPoint.updateRect(rect);
    }

    public virtual void Draw()
    {

        if (isRoot) GUI.color = Color.green;
        else GUI.color = Color.cyan;

        inPoint.updateRect(rect);
        outPoint.updateRect(rect);
        GUI.Box(rect, "", style);

        GUI.color = Color.white;
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.KeyUp:
                if (e.keyCode == KeyCode.Delete)
                {
                    if (isSelected)
                    {
                        OnClickRemoveNode();
                        GUI.changed = true;
                    }
                }
                break;
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    protected void ProcessContextMenu()
    {
#if UNITY_EDITOR
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Make root"), false, OnClickChangeRoot);
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
#endif
	}

	protected void OnClickRemoveNode() {
		OnRemoveNode?.Invoke(this);
	}

	protected void OnClickChangeRoot() {
		OnNewRoot?.Invoke(this);
	}

}