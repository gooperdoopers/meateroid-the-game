using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class gridScript : Node2D
{
	//indexPos is [y][x] value is 0 = empty, 1 = power, 2 = full, 3 = power full
	public Array<Array<int>> shipMatrix = new Array<Array<int>>();
	public Array<Array<int>> powerMatrix = new Array<Array<int>>();
	public Array<Array<int>> adjacentMatrix = new Array<Array<int>>();
	public Array<int> mouseGridPosition = new Array<int>();
	private int matrixSize = 10;//must be divisible by 2
	private float gridScale = 64f;
	private int worldToGridOffset = 9;
	public Boolean isBuilding = true;
	public Array<int> currentGridPosition;
	[Export] public Sprite2D module;
	[Export] public Node2D shipCore;
	private void quadrantFill(Array<Array<int>> table, int amount, Array<int> indexPos) {
		table[indexPos[1]+1][indexPos[0]] += amount;
		table[indexPos[1]-1][indexPos[0]] += amount;
		table[indexPos[1]][indexPos[0]+1] += amount;
		table[indexPos[1]][indexPos[0]-1] += amount;
	}
	private void fillMatrix(Array<Array<int>> blockMatrix, Array<int> blockPosition, string fillType) {
		for (int blockCount = 0; blockCount < blockMatrix.Count; blockCount++){
			Array<int> currentBlock = blockMatrix[blockCount];
			int blockYOffset = blockPosition[1]+matrixSize/2;
			int blockXOffset = blockPosition[0]+matrixSize/2;
			if(fillType == "full"){
				Array<int> indexPos = new Array<int>{currentBlock[0]+blockXOffset,currentBlock[1]+blockYOffset};
				shipMatrix[indexPos[1]][indexPos[0]] = 1;
				quadrantFill(powerMatrix,1,indexPos);
				quadrantFill(adjacentMatrix,1,indexPos);
			}
			else{
				shipMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset] = 1;
			}
		}
	}
	public enum moduleType
	{
		Reactor,
	}
	private int scanMatrix(Array<Array<int>> blockMatrix, Array<int> blockPosition){
		// returns the occupation or power of the space of the blockmatrix
		Boolean occupied = false;
		Boolean powered = false;
		Boolean adjacent = false;
		int blockYOffset = blockPosition[1]+matrixSize/2;
		int blockXOffset = blockPosition[0]+matrixSize/2;

		for (int blockCount = 0; blockCount < blockMatrix.Count; blockCount++){
			Array<int> currentBlock = blockMatrix[blockCount];
			int matrixValue = shipMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset];
			int powerValue = powerMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset];
			int adjacentValue = adjacentMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset];
			if(powerValue > 0){
				powered = true;
			}
			if(matrixValue == 0){
				occupied = true;
			}
			if(adjacentValue > 0){
				adjacent = true;
			}
		}
		if (occupied == true){
			return 0;
		}
		if (occupied != true && powered != true && adjacent == true){
			return 1;
		}
		if (occupied != true && powered == true && adjacent == true){
			return 2;
		}
		return -1;
	}

	public Boolean placeModule(moduleType type, Array<Array<int>> blockMatrix, Array<int> blockPosition){

		int scan = scanMatrix(blockMatrix,blockPosition);
		if(scan == 0 || scan == -1){
			return false;
		}
		
		if(type == moduleType.Reactor){
			
		}
		return false;
	}
	public override void _Ready(){
		for (int y = 0; y < matrixSize; y++){
			Array<int> xPlane = new Array<int>();
			for (int x = 0; x < matrixSize; x++){
				xPlane.Add(0);
			}
			shipMatrix.Add(xPlane);
			powerMatrix.Add(xPlane);
			adjacentMatrix.Add(xPlane);
		}

		GD.Print(shipMatrix[5][2]);
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
			GD.Print(mouseGridX + " " + mouseGridY);
			module.Position = new Vector2(calculateModuleX,calculateModuleY);
		}
	}
}
