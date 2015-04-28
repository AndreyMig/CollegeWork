package views;

import java.io.File;
import java.io.FileInputStream;
import java.util.ArrayList;

import controllers.ViewListener;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class MainView {

	
	
	public static Scene scene;
	private ArrayList<ViewListener> listeners;
	
	public MainView(String fxmlName, Stage primaryStage, int sceneHeight, int sceneWidth)
	{
		
		listeners = new ArrayList<ViewListener>();
		createScene(fxmlName, primaryStage, sceneHeight, sceneWidth);
		
		
		
		
		
	}
	
	
	private void createScene(String fxmlName, Stage primaryStage, int sceneHeight, int sceneWidth){
		try {
			// BorderPane root = new BorderPane();

			FXMLLoader fxmlRoot = new FXMLLoader();

			Parent root = (Parent) fxmlRoot.load(new FileInputStream(new File(
					fxmlName)));

			scene = new Scene(root, 500, 500);
			
			
//			ElevatorView e = new ElevatorView();
//
//			e.openDoors();
			
			
			primaryStage.setScene(scene);
			primaryStage.show();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	
	public void registerListener(ViewListener listener)
	{
		listeners.add(listener);
	}
	
	
	public void changeElevatorFloor(int floorNum)
	{
		
	}
	
	
	
	private void fireUpChangeFloorEvent(int floorNum)
	{
		
		for (ViewListener l : listeners) {
			l.changeFloor(floorNum);
		}
		
	}
	
	
	
	
	
}
