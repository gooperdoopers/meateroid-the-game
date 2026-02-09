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
	private void fillMatrix(){
		
	}

	private int scanMatrix(){
		return 0;
	}

	public override void _Ready(){
		for (int y = 0; y < matrixSize; y++){
			Array<int> xPlane = new Array<int>();
			for (int x = 0; x < matrixSize; x++){
				xPlane.Add(0);
			}
			shipMatrix.Add(xPlane);
		}

		GD.Print(shipMatrix[5][2]);
		shipMatrix[matrixSize/2][matrixSize/2] = 2;
		shipMatrix[matrixSize/2][matrixSize/2+1] = 2;
		shipMatrix[matrixSize/2+1][matrixSize/2] = 2;
		shipMatrix[matrixSize/2+1][matrixSize/2+1] = 2;
	}

	public override void _Input(InputEvent @event){

		if(@event is InputEventKey eventKey)
		{
			if (eventKey.Pressed && eventKey.Keycode == Key.B){
            	isBuilding = !isBuilding;
        	}
		}

		else if(@event is InputEventMouseButton mouseButton){
			if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left && isBuilding == true){
				
			}
		}
	}

	public override void _Process(double delta){
		if(isBuilding == true){
			Vector2 mousePosition = (GetLocalMousePosition() + new Vector2(gridScale/2,gridScale/2))/gridScale;
			int mouseGridX = (int)Mathf.Round(mousePosition.X);
			int mouseGridY = (int)Mathf.Round(mousePosition.Y);
			float calculateModuleX = (float)mouseGridX * gridScale - gridScale/2;
			float calculateModuleY = (float)mouseGridY * gridScale - gridScale/2;
			module.Position = new Vector2(calculateModuleX,calculateModuleY);
		}
	}
}
