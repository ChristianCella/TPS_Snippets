/*
This snippet allows to cerate a snapshot of the scene: basically, it's very similar to what 'refresh display' does.
In this snippet you change the positions of the robot and the cube, display a message on the screen
(to verify that the resources have moved correctly), and then return everything to the original positions (in just one move).
*/

using System;
using System.IO;
using System.Windows.Forms;
using Tecnomatix.Engineering;

public class MainScript
{
    
    public static void MainWithOutput(ref StringWriter output)
    {
    
    	string snapName = "MySnap";
    
    	// Save the human   	
    	TxObjectList humans = TxApplication.ActiveSelection.GetItems();
		humans = TxApplication.ActiveDocument.GetObjectsByName("Jack");
		TxHuman human = humans[0] as TxHuman; 

		// Save the robot
		TxObjectList robots = TxApplication.ActiveSelection.GetItems();
		robots = TxApplication.ActiveDocument.GetObjectsByName("UR5e");
		var robot = robots[0] as ITxLocatableObject;

		// Save the cube
		TxObjectList cubes = TxApplication.ActiveSelection.GetItems();
		cubes = TxApplication.ActiveDocument.GetObjectsByName("YAOSC_cube1");
		var cube = cubes[0] as ITxLocatableObject;
		
		// Capture the current positions of the instances		
		TxSnapshotCreationData creationData = new TxSnapshotCreationData();
		creationData.Name = snapName;
		TxPhysicalRoot cell = TxApplication.ActiveDocument.PhysicalRoot;
		TxSnapshot txSnapshot = cell.CreateSnapshot(creationData);

		// Move the robot and a cube	
		var position_rob = new TxTransformation(robot.LocationRelativeToWorkingFrame);
		position_rob.Translation = new TxVector(0, 200, 0);
		robot.LocationRelativeToWorkingFrame = position_rob;

		var position_cube = new TxTransformation(cube.LocationRelativeToWorkingFrame);
		position_cube.Translation = new TxVector(300, 300, 25);
		cube.LocationRelativeToWorkingFrame = position_cube;
		
		TxApplication.RefreshDisplay();

		// Display a TxMessageBox on the screen tom see if resources moved correctly
		TxMessageBox.Show("Verify!", "Stop box", MessageBoxButtons.OK, MessageBoxIcon.Information);
		
		// Return everything to the original positions		
		TxApplySnapshotParams snapParam = new TxApplySnapshotParams();
        snapParam.ObjectsLocation = true;
		snapParam.ObjectsVisibility = true;
        snapParam.ObjectsAttachments = true;
        snapParam.DevicePoses = true; // Also applies to Human postures               
        txSnapshot.Apply(snapParam);

    }
}
