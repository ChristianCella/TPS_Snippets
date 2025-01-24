/*
This snippet allows to create a device operation. This was used to move the robot with the slider of the
camozzi Line.
*/

using System.IO;
using Tecnomatix.Engineering;
public class MainScript
{
    public static void MainWithOutput(ref StringWriter output)
    {
        // Get the line  	
        TxObjectList objects = TxApplication.ActiveDocument.GetObjectsByName("Line");
        var line = objects[0] as TxDevice;

        // Get the pose
        TxObjectList start_poses = TxApplication.ActiveDocument.GetObjectsByName("MIDDLE");
        var start_pose = start_poses[0] as TxPose;
        TxObjectList end_poses = TxApplication.ActiveDocument.GetObjectsByName("ATTEMPT");
        var end_pose = end_poses[0] as TxPose;

        // Get the device by name
        TxDeviceOperationCreationData data = new TxDeviceOperationCreationData();
        data.Duration = 0;
        data.Name = "MoveBase";
        TxApplication.ActiveDocument.OperationRoot.CreateDeviceOperation(data);

        // Get the created operation
        TxTypeFilter opFilter = new TxTypeFilter(typeof(TxContinuousRoboticOperation));
        TxOperationRoot opRoot = TxApplication.ActiveDocument.OperationRoot;

        TxObjectList operations = TxApplication.ActiveDocument.GetObjectsByName(data.Name);
        var MyOp = operations[0] as TxDeviceOperation;

        MyOp.Device = line;
        MyOp.SourcePose = start_pose;
        MyOp.TargetPose = end_pose;




    }
}