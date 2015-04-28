package views;

import java.util.ArrayList;

import javafx.scene.Scene;
import javafx.scene.image.ImageView;

public class FloorButtonsView {

	
	private final String BUTTON_NAME = "button_floor_";
	
	ArrayList<ImageView> elevatorButton;
	
	
	public FloorButtonsView(int numOfButtons)
	{
		elevatorButton = new ArrayList<ImageView>();
		
		for (int i = 0; i < numOfButtons; i++) {
			
			ImageView iv = (ImageView) MainView.scene.lookup("#leftDoor");
			elevatorButton.add(iv);
		}
		
		
		
	}
	
	
	
	
	
	
	
	
	
	
}
