using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace IntroToRevitAPI20200109 {
    [Transaction(TransactionMode.Manual)]
    class ShowDialog : IExternalCommand {        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) {
			try {
                TaskDialog.Show("The Revit API sample succeeded!", "The application succeeded!");
                return Result.Succeeded;
			} catch (Exception ex) {
                TaskDialog.Show("The Revit API sample failed!", "Error message: " + ex.Message);
                return Result.Failed;
			}
        }
    }
}
