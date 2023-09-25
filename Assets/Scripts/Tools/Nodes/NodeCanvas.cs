using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[System.Serializable]
public class NodeCanvas : ScriptableObject {

    public Node root;

    [SerializeField]
    private List<Node> _nodes = new List<Node>();
    public List<Node> nodes { get { return _nodes; } }

    [SerializeField]
    private List<Connection> _connections = new List<Connection>();
    public List<Connection> connections { get { return _connections; } }

    [SerializeField]
    private string _canvasName = "";
    public string canvasName { get { return _canvasName; } }

    [SerializeField]
    private Dictionary<string, Node> _nodeTable = new Dictionary<string, Node>();
    public Dictionary<string, Node> nodeTable { get { return _nodeTable; } }

    public void SetCanvas(string name, Node rootNode, List<Node> nodes, List<Connection> conn, Dictionary<string, Node> nt)
    {
        _canvasName = string.Copy(name);
        _nodes = new List<Node>();
        _nodeTable.Clear();
        foreach(Node other in nodes) {
            Node newNode = null;
            if (other is DialogueNode) {
                newNode = new DialogueNode((DialogueNode)other);
			}
            _nodes.Add(newNode);
            _nodeTable.Add(newNode.id, newNode);
            if (newNode.isRoot) root = newNode;
		}

        foreach(Node other in nodes) {
			if (!string.IsNullOrEmpty(other.prevNodeID))
				other.previousNode = _nodeTable[other.prevNodeID];
			if (!string.IsNullOrEmpty(other.nextNodeID))
				other.nextNode = _nodeTable[other.nextNodeID];
		}

		_connections = new List<Connection>();
        foreach(Connection other in conn) {
            _connections.Add(new Connection(other));
		}
        //RefreshNodePair();
    }
    /*
    private void OnEnable()
    {
        if ((_nodeTable == null || _nodeTable.Count == 0) && nodes != null)
        {
            _nodeTable = new Dictionary<string, Node>();
            foreach (Node n in nodes)
            {
                _nodeTable.Add(n.id, n);
            }
            RefreshNodePair();
        }
    }

    //[UnityEditor.Callbacks.DidReloadScripts]
    public void RefreshNodePair()
    {
        root = null;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!_nodeTable.ContainsKey(nodes[i].id)) _nodeTable.Add(nodes[i].id, nodes[i]);
        }

        if (connections != null) connections.Clear();
        else _connections = new List<Connection>();

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
                root = nodes[i];
            }
        }
    }
    */

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
}