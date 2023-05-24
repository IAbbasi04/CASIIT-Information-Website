using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CasiitCourses
{
    public class Item : GameObject
    {
        public Item(Image objectImageFormat, System.Drawing.Point containingCellCoordinate) : base(objectImageFormat, containingCellCoordinate) // calls super constructor
        {
            
        }
    }
}