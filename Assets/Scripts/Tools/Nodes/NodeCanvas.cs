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
}