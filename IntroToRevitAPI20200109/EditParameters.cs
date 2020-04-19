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
    class EditParameters : IExternalCommand {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) {
            try {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                Document doc = uidoc.Document;

                FilteredElementCollector collector = new FilteredElementCollector(doc);

                ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Windows);

                IList<Element> windows = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

                foreach (Element ele in windows) {
                    Parameter para = ele.LookupParameter("Head Height");
                    string storage = para.StorageType.ToString();
                    double value = para.AsDouble();
                    double newvalue = UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_MILLIMETERS);
                    // TaskDialog.Show("Parameters", "Parameter is a " + storage + " with value: " + newvalue.ToString());

                    double setvalue = UnitUtils.ConvertToInternalUnits(2100, DisplayUnitType.DUT_MILLIMETERS);
                    using (Transaction tran = new Transaction(doc, "Set Parameter")) {
                        tran.Start();
                        para.Set(setvalue);
                        tran.Commit();
                    }
                }

                TaskDialog.Show("Windows", windows.Count + " Windows Found!");

                return Result.Succeeded;

            } catch (Exception ex) {
                TaskDialog.Show("Revit API Sample", "App fail: " + ex.Message);
                return Result.Failed;
            }
        }
    }
}
