using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class DialogueNode : Node
{

	public Sprite actorSprite;
	public string actorName; //Name of the actor shown
	public string actorSays;
	public Alignment alignment;
	public AudioClip voice;

	public static int width = 300;
	public static int height = 175;

	public DialogueNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, Action<Node> OnClickChangeRoot) 
		: base(position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnClickChangeRoot) {

	}

	public DialogueNode(string id, Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, Action<Node> OnClickChangeRoot) 
		: base(id, position, width, height, nodeStyle, selectedStyle, inPointStyle, outPointStyle,OnClickInPoint, OnClickOutPoint,OnClickRemoveNode,OnClickChangeRoot) 
		 { }

	public DialogueNode(DialogueNode other) : base(other) {
		actorSprite = other.actorSprite;
		actorName = other.actorName;
		actorSays = other.actorSays;
		alignment = other.alignment;
		voice = other.voice;
	}

	public void Set(string speech, string name, Sprite face, Alignment align, AudioClip ac) {
		actorSays = speech;
		actorName = name;
		actorSprite = face;
		alignment = align;
		voice = ac;
	}

	public override void Draw() {
		base.Draw();

		actorSprite = (Sprite)EditorGUI.ObjectField(new Rect(rect.position.x + 140f, rect.position.y + 15, 140f, 15), "", actorSprite, typeof(Sprite), false);
		alignment = (Alignment)EditorGUI.EnumPopup(new Rect(rect.position.x + 15, rect.position.y + 15, 110f, 15), alignment);
		GUI.Label(new Rect(rect.position.x + 15, rect.position.y + 40, 270, 20), "Actor Name: ");
		actorName = GUI.TextField(new Rect(rect.position.x + 90, rect.position.y + 40, 195, 20), actorName);
		actorSays = GUI.TextArea(new Rect(rect.position.x + 15, rect.position.y + 65, 270, 70), actorSays);
		GUI.Label(new Rect(rect.position.x + 15, rect.position.y + 140f, 50f, 20f), "Voice: ");
		voice = (AudioClip)EditorGUI.ObjectField(new Rect(rect.position.x + 70f, rect.position.y + 140f, 150f, 20f), "", voice, typeof(AudioClip), false);
	}
}