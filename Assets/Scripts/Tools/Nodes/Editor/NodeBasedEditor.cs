using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEditor.UIElements;

public class NodeBasedEditor : EditorWindow
{
    private List<Node> nodes = new List<Node>();
    private List<Connection> connections = new List<Connection>();
    private Dictionary<string, Node> _nodeTable = new Dictionary<string, Node>();
    public Dictionary<string, Node> nodeTable { get { return _nodeTable; } }

    private string canvasName;

    private Node rootNode;
    private NodeCanvas canvasToLoad;

    private GUIStyle nodeStyle;
    private GUIStyle selectedNodeStyle;
    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;

    private ConnectionPoint selectedInPoint;
    private ConnectionPoint selectedOutPoint;

    private Vector2 offset = Vector2.zero;
    private Vector2 drag = Vector2.zero;

    public delegate void reloadScripts();
    public static event reloadScripts OnReloadScripts;

    [MenuItem("Window/Node Editor")]
    private static void OpenWindow()
    {
        NodeBasedEditor window = GetWindow<NodeBasedEditor>();
        window.titleContent = new GUIContent("Node Editor");
        window.autoRepaintOnSceneChange = true;
    }

    private void OnEnable()
    {
        OnReloadScripts += ClearCanvas;

        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);
        

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);

        if (_nodeTable == null) _nodeTable = new Dictionary<string, Node>();
    }

    private void OnDisable()
    {
        OnReloadScripts -= ClearCanvas;
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    public static void OnScriptsReloaded()
    {
        OnReloadScripts?.Invoke();

    }

	private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        DrawNodes();
        DrawConnections();

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        DrawConnectionLine(Event.current);

        GUI.color = Color.white;

        GUI.Box(new Rect(15, 25, 250, 30), "");
        GUI.Label(new Rect(20, 30, 100, 20), "Canvas Name: ");
        canvasName = GUI.TextField(new Rect(120, 30, 140, 20), canvasName);

        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Button("Clear Canvas", EditorStyles.toolbarButton)) ClearCanvas();
        if (GUILayout.Button("Save Canvas", EditorStyles.toolbarButton)) SaveCanvas();
        if (GUILayout.Button("Load Canvas", EditorStyles.toolbarButton)) LoadCanvas();

        canvasToLoad = (NodeCanvas)EditorGUILayout.ObjectField(canvasToLoad, typeof(NodeCanvas), false);

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void DrawNodes() {
		for (int i = 0; i < nodes.Count; i++) {
			nodes[i].Draw();
		}
		GUI.changed = true;
	}

	private void DrawConnections() {
		for (int i = 0; i < connections.Count; i++) {
			connections[i].Draw();
		}
		GUI.changed = true;
	}

    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:

                if (e.button == 0)
                {
                    ClearConnectionSelection();
                }

                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0 || e.button == 2)
                {
                    OnDrag(e.delta);
                }
                break;
        }
	}

	private void ProcessNodeEvents(Event e) {
		for (int i = nodes.Count - 1; i >= 0; i--) {
			if (nodes[i].ProcessEvents(e)) {
				GUI.changed = true;
			}
		}
	}

	private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    /// <summary>
    /// Here we add the types of nodes we want to use in the node editor
    /// </summary>
    /// <param name="mousePosition"></param>
    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add dialogue node"), false, () => OnClickAddDialogueNode(mousePosition));
        //genericMenu.AddItem(new GUIContent("Add camera node"), false, () => OnClickAddCameraNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Drag(delta);
            }
        }
        GUI.changed = true;
    }

    private void OnClickAddDialogueNode(Vector2 mousePosition)
    {
        //Instead of dialogue node, we just make NodeType a dialogue
        DialogueNode newNode = new DialogueNode(mousePosition, DialogueNode.width, DialogueNode.height, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickChangeRoot);
        if (rootNode == null)
        {
            rootNode = newNode;
            rootNode.isRoot = true;
        }
        _nodeTable.Add(newNode.id, newNode);
        nodes.Add(newNode);
       
    }

    private void OnClickAddCameraNode(Vector2 mousePosition)
    {
        Node newNode = new Node(mousePosition, 235, 80, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickChangeRoot);
        if (rootNode == null)
        {
            rootNode = newNode;
            rootNode.isRoot = true;
        }
        _nodeTable.Add(newNode.id, newNode);
        nodes.Add(newNode);
    }

    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedInPoint != null && selectedOutPoint != null)
        {
            if (selectedOutPoint.nodeID != selectedInPoint.nodeID)
            {
                CreateConnection(selectedInPoint.nodeID, selectedOutPoint.nodeID);
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedOutPoint != null && selectedInPoint != null)
        {
            if (selectedOutPoint.nodeID != selectedInPoint.nodeID) {
                CreateConnection(selectedInPoint.nodeID, selectedOutPoint.nodeID);
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    void OnClickChangeRoot(Node node)
    {
        if (rootNode != null) rootNode.isRoot = false;
        node.isRoot = true;
        rootNode = node;
        Repaint();
    }

    private void OnClickRemoveNode(Node node) {
		List<Connection> connectionsToRemove = new List<Connection>();

		for (int i = 0; i < connections.Count; i++) {
			if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint) {
				connectionsToRemove.Add(connections[i]);
			}
		}

		for (int i = 0; i < connectionsToRemove.Count; i++) {
			connections.Remove(connectionsToRemove[i]);
		}

		if (node.isRoot) rootNode = null;
		_nodeTable.Remove(node.id);

		//Remove the node connection from whatever side it was on
		if (node.previousNode != null) node.previousNode.nextNode = null;
        if (node.nextNode != null) node.nextNode.previousNode = null;
        nodes.Remove(node);
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        Node leftNode;
        Node rightNode;
        _nodeTable.TryGetValue(connection.inPoint.nodeID, out rightNode);
        _nodeTable.TryGetValue(connection.outPoint.nodeID, out leftNode);

        leftNode.nextNodeID = "";
        leftNode.nextNode = null;
        rightNode.prevNodeID = "";
        rightNode.previousNode = null;

        connections.Remove(connection);
    }

    private void CreateConnection(string inNodeID, string outNodeID)
    {
        if (!_nodeTable.ContainsKey(outNodeID) || !_nodeTable.ContainsKey(inNodeID)) return;
        
        //Left node is OutPoint, right node is InPoint
        Node outNode = _nodeTable[outNodeID];
        Node inNode = _nodeTable[inNodeID];

        _nodeTable[outNodeID].nextNodeID = selectedInPoint.nodeID;
        _nodeTable[outNodeID].nextNode = inNode;

        _nodeTable[inNodeID].prevNodeID = selectedOutPoint.nodeID;
        _nodeTable[inNodeID].previousNode = outNode;
        connections.Add(new Connection(inNode.inPoint, outNode.outPoint, OnClickRemoveConnection));

    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    void ClearCanvas()
    {
        AssetDatabase.ReleaseCachedFileHandles();
        AssetDatabase.Refresh();
        base.DiscardChanges();
        canvasToLoad = null;
        canvasName = "";
        nodes.Clear();
        rootNode = null;
        connections.Clear();
        _nodeTable.Clear();
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    void SaveCanvas()
    {
        if (string.IsNullOrEmpty(canvasName))
        {
            Debug.LogError("Canvas requires a name before it can be saved!");
            return;
        }
        if (nodes.Count == 0 || rootNode == null) {
            Debug.LogError("Can't save empty canvases");
            return;
		}

        NodeCanvas canvas = CreateInstance<NodeCanvas>();
        if (!Directory.Exists(Application.dataPath + "/Game Data/Dialogue")){
            Directory.CreateDirectory(Application.dataPath + "/Game Data/Dialogue");
        }

        canvas.SetCanvas(canvasName, rootNode, nodes, connections, nodeTable);
        AssetDatabase.CreateAsset(canvas, "Assets/Game Data/Dialogue/" + canvasName + ".asset");
        AssetDatabase.ReleaseCachedFileHandles();
        AssetDatabase.Refresh();
        Repaint();
    }

    void LoadCanvas()
    {
        base.DiscardChanges();
        NodeCanvas canvas = canvasToLoad;
        ClearCanvas();
        canvasName = canvas.canvasName;
        if (canvas == null) {
            Debug.LogError("Cannot load empty canvas!");
            return;
		}

		foreach (Node i in canvas.nodes) {
            Node copy = null;

            if (i is DialogueNode) {
                copy = new DialogueNode(i.id, new Vector2(i.rect.x, i.rect.y), i.rect.width, i.rect.height, i.defaultNodeStyle, i.selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickChangeRoot);
                DialogueNode dn = i as DialogueNode;
                ((DialogueNode)copy).Set(dn.actorSays, dn.actorName, dn.actorSprite, dn.alignment, dn.voice);
            }
            //if (i is CameraNode) {
            //copy.cam.targetToFollow = i.cam.targetToFollow;
            //copy.cam.followTime = i.cam.followTime;
            //}    

            if (copy == null) continue;

			copy.nextNodeID = i.nextNodeID;
			copy.prevNodeID = i.prevNodeID;
			nodes.Add(copy);
			if (i.isRoot == true) {
				copy.isRoot = true;
				rootNode = nodes[nodes.Count - 1];
			}
			_nodeTable.Add(copy.id, copy);
		}

		foreach (Connection c in canvas.connections) {
			Node rightNode;
			Node leftNode;
			_nodeTable.TryGetValue(c.inPoint.nodeID, out rightNode);
			_nodeTable.TryGetValue(c.outPoint.nodeID, out leftNode);
            if (rightNode == null || leftNode == null) continue;
			leftNode.nextNode = rightNode;
			rightNode.previousNode = leftNode;

			Connection copy = new Connection(rightNode.inPoint, leftNode.outPoint, OnClickRemoveConnection);
			connections.Add(copy);
		}


        Repaint();
		GUI.changed = true;
    }

    void RefreshPairs()
    {

        rootNode = null;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!_nodeTable.ContainsKey(nodes[i].id)) _nodeTable.Add(nodes[i].id, nodes[i]);
        }

        connections.Clear();

        for (int i = 0; i < nodes.Count; i++)
        {
            Node rightNode;
            Node leftNode;
            if (!string.IsNullOrEmpty(nodes[i].nextNodeID))
            {
                _nodeTable.TryGetValue(nodes[i].nextNodeID, out rightNode);
                nodes[i].nextNode = rightNode;

                Connection copy = new Connection(nodes[i].nextNode.inPoint, nodes[i].outPoint, OnClickRemoveConnection);
                if (!connections.Contains(copy)) connections.Add(copy);
            }
            if (!string.IsNullOrEmpty(nodes[i].prevNodeID))
            {
                _nodeTable.TryGetValue(nodes[i].prevNodeID, out leftNode);
                nodes[i].previousNode = leftNode;
            }

            if (nodes[i].isRoot == true)
            {
                rootNode = nodes[i];
            }
        }

        Repaint();
        //GUI.changed = true;

    }
}
