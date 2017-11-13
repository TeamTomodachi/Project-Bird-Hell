using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyLevelSelectPanelController : MonoBehaviour
{
	public LevelInfo Level = new LevelInfo();
	public int TotalVotes;

	public Text NameText;
	public Text VoteText;

	public RectTransform Transform2D { get; set; }
	public LobbyController Lobby { get; set; }
	private RectTransform m_parentTransform;

	void Awake()
	{
		Lobby = GetComponentInParent<LobbyController>();
	}

	private void Start()
	{
		Transform2D = transform as RectTransform;
		m_parentTransform = Lobby.transform as RectTransform;
		//NameText.text = Level.Name;
	}

	void Update()
	{
		VoteText.text = TotalVotes == 0 ? "" : TotalVotes.ToString();
	}

	public bool ContainsPosition(Vector2 position)
	{
		// Get the rectangular bounding box of your UI element
		Rect rect = Transform2D.rect;

		// Get the left, right, top, and bottom boundaries of the rect
		float leftSide      = Transform2D.anchoredPosition.x - rect.width / 2;
		float rightSide     = Transform2D.anchoredPosition.x + rect.width / 2;
		float topSide       = Transform2D.position.y + rect.height / 2;
		float bottomSide    = Transform2D.position.y - rect.height / 2;

		//Debug.Log(leftSide + ", " + rightSide + ", " + topSide + ", " + bottomSide + " | " + position);

		// Check to see if the point is in the calculated bounds
		if (position.x >= leftSide &&
			position.x <= rightSide &&
			position.y >= bottomSide &&
			position.y <= topSide)
		{
			return true;
		}
		return false;
	}
}
