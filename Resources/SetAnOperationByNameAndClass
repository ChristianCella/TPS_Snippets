/*
This snippet allows to set an operation in the Sequence Editor by specifying its name and class.
Additionally, it allows to get the duration of the operation.
*/

using System;
using System.IO;
using System.Windows.Forms;
using Tecnomatix.Engineering;
using System.Linq;

public class MainScript
{
    
    public static void MainWithOutput(ref StringWriter output)
    {
    	/*
    	There are basically 2 types of operations we are really interested in:
    	1) Robot operations ==> TxContinuousRoboticOperation
    	2) Human operations ==> TxHumanTsbSimulationOperation
    	*/

        // Set (in the sequence editor) the desired operation by specifying its name   	
		var op = TxApplication.ActiveDocument.OperationRoot.GetAllDescendants(new 
		TxTypeFilter(typeof(TxHumanTsbSimulationOperation))).FirstOrDefault(x => x.Name.Equals("HumanPickAndPlace2")) as 
		TxHumanTsbSimulationOperation;     
		TxApplication.ActiveDocument.CurrentOperation = op;
		
		// Get the duration of the operation
		string Time = op.Duration.ToString();
		output.Write(Time);
    }
}
