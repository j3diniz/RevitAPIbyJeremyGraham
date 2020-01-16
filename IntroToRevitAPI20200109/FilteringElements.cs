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
    class FilteringElements : IExternalCommand {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) {
			try {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                
                Document doc = uidoc.Document;

                FilteredElementCollector collector = new FilteredElementCollector(doc);
                ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Windows);
                IList<Element> windows = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

                TaskDialog.Show("Windows", windows.Count + " Windows Found!");

                return Result.Succeeded;
			} catch (Exception ex) {
				TaskDialog.Show("Revit API Sample", "App fail: " + ex.Message);
                return Result.Failed;
			}
        }
    }
}
