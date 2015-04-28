package views;

import java.util.ArrayList;

import application.Main;
import javafx.animation.Animation;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.PathTransition;
import javafx.animation.SequentialTransitionBuilder;
import javafx.animation.Timeline;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.scene.image.ImageView;
import javafx.scene.shape.CubicCurveTo;
import javafx.scene.shape.MoveTo;
import javafx.scene.shape.Path;
import javafx.util.Duration;

public class ElevatorView {

	ImageView leftDoor;
	ImageView rightDoor;
	ImageView eleavtorBg;

	
	
	// this holds the whole elevator
	ArrayList<ImageView> theWholeElevator;

	public ElevatorView() {

		leftDoor = (ImageView) MainView.scene.lookup("#leftDoor");
		rightDoor = (ImageView) MainView.scene.lookup("#rightDoor");

	}

	public void openDoors()
	{
		//left door opens animation
		final Timeline leftDoorTimeline = new Timeline();
		leftDoorTimeline.setCycleCount(1);
		leftDoorTimeline.setAutoReverse(true);
		final KeyValue leftDoorKv = new KeyValue(leftDoor.xProperty(), -10);
		final KeyFrame leftDoorKf = new KeyFrame(Duration.millis(2000), leftDoorKv);
		leftDoorTimeline.getKeyFrames().add(leftDoorKf);
		
		//right door opens animation
		final Timeline rightDoorTimeline = new Timeline();
		leftDoorTimeline.setCycleCount(1);
		leftDoorTimeline.setAutoReverse(true);
		final KeyValue rightDoorKv = new KeyValue(rightDoor.xProperty(), 10);
		
		
		
		
		  EventHandler<ActionEvent> onFinished = new EventHandler<ActionEvent>() {

	            public void handle(ActionEvent t) {

	            	closeDoors();
	            }

	        };
		
		
		
	        final KeyFrame rightDoorKf = new KeyFrame(Duration.millis(2000), onFinished, rightDoorKv);
			leftDoorTimeline.getKeyFrames().add(rightDoorKf);
		
		
		
		
		
		leftDoorTimeline.play();
		rightDoorTimeline.play();
		
		
		
	}
	
	
	public void closeDoors()
	{
		//left door opens animation
		final Timeline leftDoorTimeline = new Timeline();
		leftDoorTimeline.setCycleCount(1);
		leftDoorTimeline.setAutoReverse(true);
		final KeyValue leftDoorKv = new KeyValue(leftDoor.xProperty(), 0);
		final KeyFrame leftDoorKf = new KeyFrame(Duration.millis(1700), leftDoorKv);
		leftDoorTimeline.getKeyFrames().add(leftDoorKf);
		
		//right door opens animation
		final Timeline rightDoorTimeline = new Timeline();
		leftDoorTimeline.setCycleCount(1);
		leftDoorTimeline.setAutoReverse(true);
		final KeyValue rightDoorKv = new KeyValue(rightDoor.xProperty(), 0);
		final KeyFrame rightDoorKf = new KeyFrame(Duration.millis(1700), rightDoorKv);
		leftDoorTimeline.getKeyFrames().add(rightDoorKf);
		
		
		
		
		leftDoorTimeline.play();
		rightDoorTimeline.play();
		
		
		
	}
	
	
	
	public void anim() {
		
		
		

		
		
	}

}
