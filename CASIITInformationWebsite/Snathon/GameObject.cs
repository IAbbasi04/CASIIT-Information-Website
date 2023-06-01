using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CasiitCourses
{
    public class GameObject : Control
    {
        public Image ObjectImage { get; } = new Image();
        public System.Drawing.Point ContainingCellCoordinate { get; set; } // when webpage is first loaded the table's cells wont yet exist, making the cells always null, instead store cell coordinates

        public GameObject(Image objectImageFormat, System.Drawing.Point containingCellCoordinate)
        {
            // Interactable GameObject (Exists)
            // apply the image format on the object's image
            ObjectImage.ImageUrl = objectImageFormat.ImageUrl;
            ObjectImage.CssClass = objectImageFormat.CssClass;
            // assign this object's cell coordinate
            ContainingCellCoordinate = containingCellCoordinate;
        }
        public GameObject()
        {
            // Empty GameObject (Exists, but doesn't have a position; this type of GameObject is a container to group other GameObjects)
        }
    }
}