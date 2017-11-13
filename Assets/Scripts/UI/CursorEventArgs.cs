using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CursorEventHandler(object sender, CursorEventArgs e);
public class CursorEventArgs : EventArgs
{
	public Vector3 Position;

	public CursorEventArgs(Vector3 position)
	{
		Position = position;
	}
}