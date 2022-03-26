// Создайте приложение, которое выводит количество воздуховодов отдельно на 1 этаже и отдельно на 2 этаже.

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_API_2_4
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;           

            var ducts = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            int ducts1=0, ducts2=0;
            foreach (Duct duct in ducts)
            {
                if (duct.ReferenceLevel.Name == "Level 1")
                    ducts1 += 1;
                if (duct.ReferenceLevel.Name == "Level 2")
                    ducts2 += 1;
            }
            TaskDialog.Show("Результат", $"Количество воздуховодов: {Environment.NewLine}  Этаж №1 - {ducts1}{Environment.NewLine}  Этаж №2 - {ducts2}{Environment.NewLine}{Environment.NewLine}  Общее - {ducts.Count}");
            return Result.Succeeded;
        }
    }
}
