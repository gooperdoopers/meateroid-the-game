using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class gridScript : Node2D
{
	public Array<Array<int>> shipMatrix = new Array<Array<int>>();
	private int matrixSize = 100;//must be divisible by 2
	private float gridScale = 64f;
	public Boolean isBuilding = true;
	public Array<int> currentGridPosition;
	[Export] public Sprite2D module;
	public override void _Ready()
	{
		for (int y = 0; y < matrixSize; y++){
			Array<int> xPlane = new Array<int>();
			for (int x = 0; x < matrixSize; x++){
				xPlane.Add(0);
			}
			shipMatrix.Add(xPlane);
		}

		GD.Print(shipMatrix[5][2]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(isBuilding == true){
			Vector2 mousePosition = GetGlobalMousePosition()/gridScale;
			int mouseGridX = (int)Mathf.Round(mousePosition.X);
			int mouseGridY = (int)Mathf.Round(mousePosition.Y);
			module.Position = new Vector2((float)mouseGridX*gridScale,(float)mouseGridY*gridScale);
		}
	}
}
