using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class gridScript : Node2D
{
	//index is [y][x] value is 0 = empty, 1 = power, 2 = full, 3 = power full
	public Array<Array<int>> shipMatrix = new Array<Array<int>>();
	public Array<int> mouseGridPosition = new Array<int>();
	private int matrixSize = 10;//must be divisible by 2
	private float gridScale = 64f;
	private int worldToGridOffset = 9;
	public Boolean isBuilding = true;
	public Array<int> currentGridPosition;
	[Export] public Sprite2D module;
	private void fillMatrix(Array<Array<int>> blockMatrix, Array<int> blockPosition, int fillType) {
		for (int blockCount = 0; blockCount < blockMatrix.Count; blockCount++){
			Array<int> currentBlock = blockMatrix[blockCount];
			int blockYOffset = blockPosition[1]+matrixSize/2;
			int blockXOffset = blockPosition[0]+matrixSize/2;
			if(fillType == 4){
				var blockValue = shipMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset];
				switch(blockValue){
					case 0:
					{
						shipMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset] = 1;
						break;
					}
					case 2:
					{
						shipMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset] = 3;
						break;
					}
				}
			}
			else{
				shipMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset] = fillType;
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

		int blockYOffset = blockPosition[1]+matrixSize/2;
		int blockXOffset = blockPosition[0]+matrixSize/2;

		for (int blockCount = 0; blockCount < blockMatrix.Count; blockCount++){
			Array<int> currentBlock = blockMatrix[blockCount];
			int matrixValue = shipMatrix[currentBlock[1]+blockYOffset][currentBlock[0]+blockXOffset];
			if(matrixValue == 1 || matrixValue == 3){
				powered = true;
			}
			if(matrixValue == 2 || matrixValue == 3){
				occupied = true;
			}
		}
		if (occupied != true && powered != true){
			return 0;
		}
		if (occupied != true && powered == true){
			return 1;
		}
		if (occupied == true && powered != true){
			return 2;
		}
		if (occupied == true && powered == true){
			return 3;
		}
		return -1;
	}

	public Boolean placeModule(moduleType type, Array<Array<int>> blockMatrix, Array<int> blockPosition){

		return false;
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
			GD.Print(mouseGridX + " " + mouseGridY);
			module.Position = new Vector2(calculateModuleX,calculateModuleY);
		}
	}
}
