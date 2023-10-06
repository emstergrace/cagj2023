using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ResponseNode : Node
{
	public static readonly int width = 300;
	public static readonly int height = 175;
	public int currentBuffer = 110;
	public readonly int bufferSize = 110;

	public List<Response> responses = new List<Response>();

	public ResponseNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, Action<Node> OnClickChangeRoot)
		: base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickChangeRoot) {

		responses.Add(new Response());
		responses[0].outputPoint = outPoint;
	}

	public ResponseNode(string id, Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, Action<Node> OnClickChangeRoot)
	: base(id, position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickChangeRoot) {
		responses.Add(new Response());
		responses[0].outputPoint = outPoint;
	}

	public ResponseNode(ResponseNode other) : base(other) {
		responses = new List<Response>();
		foreach(Response r in other.responses) {
			responses.Add(new Response(r.dialogue, r.outputNodeID));
		}
	}

	public void Set(Response[] listResponses) {

	}

	public override void Draw() {
		if (isRoot) GUI.color = Color.green;
		else GUI.color = Color.cyan;

		inPoint.updateRect(rect);
		outPoint.updateRect(new Rect(rect.x, rect.y, rect.width, 110f));
		GUI.Box(rect, "", style);

		GUI.color = Color.white;

		GUI.Label(new Rect(rect.position.x + 15, rect.position.y + 10, 65, 15), "Response: ");
		responses[0].dialogue = GUI.TextArea(new Rect(rect.position.x + 85, rect.position.y + 10, 180, 120), responses[0].dialogue);

		for (int i = 1; i < responses.Count; i++) {
			GUI.Label(new Rect(rect.position.x + 15, rect.position.y + 30 + i * bufferSize, 65, 15), "Response: ");
			responses[i].dialogue = GUI.TextArea(new Rect(rect.position.x + 85, rect.position.y + 30 + i * bufferSize, 180, 100), responses[i].dialogue);
			responses[i].outputPoint.updateRect(new Rect(rect.x, rect.y, rect.width, rect.position.y + 60 + i * bufferSize));
		}

		if (GUI.Button(new Rect(rect.xMax - 30, rect.yMin + currentBuffer - bufferSize / 2, 18f, 16f), "+")) {
			currentBuffer += bufferSize;
			rect.height += bufferSize;

			Response newResp = new Response();
			//newResp.outputPoint = new ConnectionPoint(id, new Rect(rect.x, rect.y, rect.width, rect.position.y + 60 + (responses.Count - 1) * bufferSize), ConnectionPointType.Out, selectedNodeStyle, )
			responses.Add(new Response());

		}
	}

	public override void Drag(Vector2 delta) {
		base.Drag(delta);
		outPoint.updateRect(new Rect(rect.x, rect.y, rect.width, 50f));

	}

}

[Serializable]
public class Response
{
	public string dialogue;
	public string outputNodeID;
	public Node outputNode;
	public ConnectionPoint outputPoint;

	public Response(string d, string o) {
		dialogue = d;
		outputNodeID = o;
	}
	public Response() { }
}