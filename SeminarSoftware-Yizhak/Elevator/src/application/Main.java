package application;
	
import java.io.File;
import java.io.FileInputStream;

import views.ElevatorView;
import views.MainView;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.stage.Stage;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.BorderPane;


public class Main extends Application {
	
	
	
	
	@Override
	public void start(Stage primaryStage) {
		
		MainView m = new MainView("MainScene.fxml", primaryStage, 800, 400);
		
		
	}

	public static void main(String[] args) {
		
		launch(args);
		
		
		
	}
}
