/*
This snippet allows to create a robot program from zero (and it's the equivalent of importing a program from the
simulator). 
It also allows to add waypoints in specific positions and set their parameters.
Finally, it allows to add OLP commands to close the gripper in a specific waypoint.
*/

using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tecnomatix.Engineering;
using Tecnomatix.Engineering.Olp;
using System.Windows.Forms;

public class MainScript
{
    public static void MainWithOutput(ref StringWriter output)
    {

		// Define some variables
		string operation_name = "RoboticProgram1";

		string new_tcp = "tgripper_tf";
    	string new_motion_type = "PTP";
		string new_speed = "100%";
		string new_accel = "100%";
		string new_blend = "fine";
		//string new_coord = "Cartesian";
		
		bool verbose = false; // Controls some display options
    
    	// Save the robot (the index may change)  	
    	TxObjectList objects = TxApplication.ActiveDocument.GetObjectsByName("GoFa12");
    	var robot = objects[0] as TxRobot;
    	   	
    	// Create the new operation    	
    	TxContinuousRoboticOperationCreationData data = new TxContinuousRoboticOperationCreationData(operation_name);
    	TxApplication.ActiveDocument.OperationRoot.CreateContinuousRoboticOperation(data);
    	
		// Get the created operation
    	TxObjectList allOps = TxApplication.ActiveDocument.GetObjectsByName(operation_name);
        TxContinuousRoboticOperation MyOp = allOps[0] as TxContinuousRoboticOperation;
		
		// Create all the necessary points       
        TxRoboticViaLocationOperationCreationData Point1 = new TxRoboticViaLocationOperationCreationData();
        Point1.Name = "point1"; // First point
        
        TxRoboticViaLocationOperationCreationData Point2 = new TxRoboticViaLocationOperationCreationData();
        Point2.Name = "point2"; // Second point
        
        TxRoboticViaLocationOperationCreationData Point3 = new TxRoboticViaLocationOperationCreationData();
        Point3.Name = "point3"; // Third point
        
        TxRoboticViaLocationOperation FirstPoint = MyOp.CreateRoboticViaLocationOperation(Point1);
        TxRoboticViaLocationOperation SecondPoint = MyOp.CreateRoboticViaLocationOperationAfter(Point2, FirstPoint);
        TxRoboticViaLocationOperation ThirdPoint = MyOp.CreateRoboticViaLocationOperationAfter(Point3, SecondPoint);
        
        // Impose a position to the new waypoint		
		double rotVal = Math.PI;
		TxTransformation rotX = new TxTransformation(new TxVector(rotVal, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		FirstPoint.AbsoluteLocation = rotX;
		
		var pointA = new TxTransformation(FirstPoint.AbsoluteLocation);
		pointA.Translation = new TxVector(500, 0, 300);
		FirstPoint.AbsoluteLocation = pointA;
		
		// Impose a position to the second waypoint		
		double rotVal2 = Math.PI;
		TxTransformation rotX2 = new TxTransformation(new TxVector(rotVal2, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		SecondPoint.AbsoluteLocation = rotX2;
		
		var pointB = new TxTransformation(SecondPoint.AbsoluteLocation);
		pointB.Translation = new TxVector(500, 0, 25);
		SecondPoint.AbsoluteLocation = pointB;
		
		// Impose a position to the third waypoint		
		double rotVal3 = Math.PI;
		TxTransformation rotX3 = new TxTransformation(new TxVector(rotVal3, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		ThirdPoint.AbsoluteLocation = rotX3;
		
		var pointC = new TxTransformation(ThirdPoint.AbsoluteLocation);
		pointC.Translation = new TxVector(500, 0, 300);
		ThirdPoint.AbsoluteLocation = pointC;

		// NOTE: you must associate the robot to the operation!
		MyOp.Robot = robot; 

		// Implement the logic to access the parameters of the controller		
		TxOlpControllerUtilities ControllerUtils = new TxOlpControllerUtilities();		
		TxRobot AssociatedRobot = ControllerUtils.GetRobot(MyOp); // Verify the correct robot is associated 
				
		ITxOlpRobotControllerParametersHandler paramHandler = (ITxOlpRobotControllerParametersHandler)
		ControllerUtils.GetInterfaceImplementationFromController(robot.Controller.Name,
		typeof(ITxOlpRobotControllerParametersHandler), typeof(TxRobotSimulationControllerAttribute),
		"ControllerName");
		
		// Set the new parameters for the waypoint					
		paramHandler.OnComplexValueChanged("Tool Frame", new_tcp, FirstPoint);
		paramHandler.OnComplexValueChanged("Motion Type", new_motion_type, FirstPoint);
        paramHandler.OnComplexValueChanged("Speed", new_speed, FirstPoint);
        paramHandler.OnComplexValueChanged("Acc", new_accel, FirstPoint);
		paramHandler.OnComplexValueChanged("Zone", new_blend, FirstPoint);
		
		paramHandler.OnComplexValueChanged("Tool Frame", new_tcp, SecondPoint);
		paramHandler.OnComplexValueChanged("Motion Type", new_motion_type, SecondPoint);
        paramHandler.OnComplexValueChanged("Speed", new_speed, SecondPoint);
        paramHandler.OnComplexValueChanged("Acc", new_accel, SecondPoint);
		paramHandler.OnComplexValueChanged("Zone", new_blend, SecondPoint);
		
		paramHandler.OnComplexValueChanged("Tool Frame", new_tcp, ThirdPoint);
		paramHandler.OnComplexValueChanged("Motion Type", new_motion_type, ThirdPoint);
        paramHandler.OnComplexValueChanged("Speed", new_speed, ThirdPoint);
        paramHandler.OnComplexValueChanged("Acc", new_accel, ThirdPoint);
		paramHandler.OnComplexValueChanged("Zone", new_blend, ThirdPoint);
		
               
    }
}















