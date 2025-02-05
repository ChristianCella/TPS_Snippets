/*
This snippet allows to create a robot program from zero (and it's the equivalent of importing a program from the
simulator). 
It also allows to add waypoints in specific positions and set their parameters.
Finally, it allows to add OLP commands to attach an oabject to the robot in a specific waypoint.
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
		string operation_name = "Test_program";

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

		// Get the object to attach to the tool (and the tool)
		ITxObject item = TxApplication.ActiveDocument.
		GetObjectsByName("Middle_box_camozzi_1")[0];

		ITxObject tool = TxApplication.ActiveDocument.
		GetObjectsByName("Suction cup")[0];
    	   	
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

		TxRoboticViaLocationOperationCreationData Point4 = new TxRoboticViaLocationOperationCreationData();
        Point4.Name = "point4"; // Fourth point

		TxRoboticViaLocationOperationCreationData Point5 = new TxRoboticViaLocationOperationCreationData();
        Point5.Name = "point5"; // Fifth point
        
        TxRoboticViaLocationOperation FirstPoint = MyOp.CreateRoboticViaLocationOperation(Point1);
        TxRoboticViaLocationOperation SecondPoint = MyOp.CreateRoboticViaLocationOperationAfter(Point2, FirstPoint);
        TxRoboticViaLocationOperation ThirdPoint = MyOp.CreateRoboticViaLocationOperationAfter(Point3, SecondPoint);
		TxRoboticViaLocationOperation FourthPoint = MyOp.CreateRoboticViaLocationOperationAfter(Point4, ThirdPoint);
		TxRoboticViaLocationOperation FifthPoint = MyOp.CreateRoboticViaLocationOperationAfter(Point5, FourthPoint);
        
        // Impose a position to the new waypoint		
		double rotVal = Math.PI;
		TxTransformation rotX = new TxTransformation(new TxVector(rotVal, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		FirstPoint.AbsoluteLocation = rotX;
		
		var pointA = new TxTransformation(FirstPoint.AbsoluteLocation);
		pointA.Translation = new TxVector(650, -305, 100);
		FirstPoint.AbsoluteLocation = pointA;
		
		// Impose a position to the second waypoint		
		double rotVal2 = Math.PI;
		TxTransformation rotX2 = new TxTransformation(new TxVector(rotVal2, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		SecondPoint.AbsoluteLocation = rotX2;
		
		var pointB = new TxTransformation(SecondPoint.AbsoluteLocation);
		pointB.Translation = new TxVector(650, -305, -7.14);
		SecondPoint.AbsoluteLocation = pointB;
		
		// Impose a position to the third waypoint		
		double rotVal3 = Math.PI;
		TxTransformation rotX3 = new TxTransformation(new TxVector(rotVal3, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		ThirdPoint.AbsoluteLocation = rotX3;
		
		var pointC = new TxTransformation(ThirdPoint.AbsoluteLocation);
		pointC.Translation = new TxVector(650, -305, 300);
		ThirdPoint.AbsoluteLocation = pointC;

		// Impose a position to the fourth waypoint		
		double rotVal4 = Math.PI;
		TxTransformation rotX4 = new TxTransformation(new TxVector(rotVal4, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		FourthPoint.AbsoluteLocation = rotX4;
		
		var pointD = new TxTransformation(FourthPoint.AbsoluteLocation);
		pointD.Translation = new TxVector(650, -305, -7.14);
		FourthPoint.AbsoluteLocation = pointD;

		// Impose a position to the fifth waypoint		
		double rotVal5 = Math.PI;
		TxTransformation rotX5 = new TxTransformation(new TxVector(rotVal5, 0, 0), 
		TxTransformation.TxRotationType.RPY_XYZ);
		FifthPoint.AbsoluteLocation = rotX5;
		
		var pointE = new TxTransformation(FifthPoint.AbsoluteLocation);
		pointE.Translation = new TxVector(650, -305, 300);
		FifthPoint.AbsoluteLocation = pointE;

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

		paramHandler.OnComplexValueChanged("Tool Frame", new_tcp, FourthPoint);
		paramHandler.OnComplexValueChanged("Motion Type", new_motion_type, FourthPoint);
        paramHandler.OnComplexValueChanged("Speed", new_speed, FourthPoint);
        paramHandler.OnComplexValueChanged("Acc", new_accel, FourthPoint);
		paramHandler.OnComplexValueChanged("Zone", new_blend, FourthPoint);


		paramHandler.OnComplexValueChanged("Tool Frame", new_tcp, FifthPoint);
		paramHandler.OnComplexValueChanged("Motion Type", new_motion_type, FifthPoint);
        paramHandler.OnComplexValueChanged("Speed", new_speed, FifthPoint);
        paramHandler.OnComplexValueChanged("Acc", new_accel, FifthPoint);
		paramHandler.OnComplexValueChanged("Zone", new_blend, FifthPoint);

		// Choose the point for the 'attach'
		TxRoboticViaLocationOperation Waypoint1 =  TxApplication.ActiveDocument.
  		GetObjectsByName("point2")[0] as TxRoboticViaLocationOperation;

		// Choose the point for the 'detach'
		TxRoboticViaLocationOperation Waypoint2 =  TxApplication.ActiveDocument.
  		GetObjectsByName("point4")[0] as TxRoboticViaLocationOperation;

		// Create the OLP command for attachment
    	ArrayList elements1 = new ArrayList();
		ArrayList elements2 = new ArrayList();
	
    	var myCmd1 = new TxRoboticCompositeCommandStringElement("# Attach ");	
 		var myCmd11 = new TxRoboticCompositeCommandTxObjectElement(item);
		var myCmd111 = new TxRoboticCompositeCommandTxObjectElement(tool);

		// Append all the command	
    	elements1.Add(myCmd1);  
		elements1.Add(myCmd11);  
		elements1.Add(myCmd111); 

		// Write the command 	
    	TxRoboticCompositeCommandCreationData txRoboticCompositeCommandCreationData1 =
    	new TxRoboticCompositeCommandCreationData(elements1);	
    	Waypoint1.CreateCompositeCommand(txRoboticCompositeCommandCreationData1);	

		// Create the OLP command for detachment
    	var myCmd2 = new TxRoboticCompositeCommandStringElement("# Detach ");	
 		var myCmd21 = new TxRoboticCompositeCommandTxObjectElement(item);

		// Append all the command	
    	elements2.Add(myCmd2);  
		elements2.Add(myCmd21);  


		// Write the command 	
    	TxRoboticCompositeCommandCreationData txRoboticCompositeCommandCreationData2 =
    	new TxRoboticCompositeCommandCreationData(elements2);	
    	Waypoint2.CreateCompositeCommand(txRoboticCompositeCommandCreationData2);

		
               
    }
}















